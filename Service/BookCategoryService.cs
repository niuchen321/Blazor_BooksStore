using BooksStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace BooksStore.Service
{
    /// <summary>
    /// 图书分类
    /// </summary>
    public class BookCategoryService
    {
        private readonly Data.BooksContext _context;

        public BookCategoryService(Data.BooksContext context)
        {
            _context = context;
        }

        public IList<BookCategory> BookCategories { get; set; }
        /// <summary>
        /// 获取图书类型列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<BookCategory>> OnGetAsync()
        {
            return await _context.BookCategory.ToListAsync();
        }
        /// <summary>
        /// 添加图书类型
        /// </summary>
        /// <param name="bookCategory"></param>
        /// <returns></returns>
        public async Task<int> Insert(BookCategory bookCategory)
        {
            if (!string.IsNullOrEmpty(bookCategory.Img))
            {
                var filePath = "/UploadFile/";

                bookCategory.Img = Transport(bookCategory.Img, filePath, DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
            }

            bookCategory.CreateTime = DateTime.Now;
            bookCategory.EditTime = DateTime.Now;
            bookCategory.Id = Guid.NewGuid().ToString().Replace("-", "");
            _context.BookCategory.Add(bookCategory);
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// 获取图书类型
        /// </summary>
        /// <param name="id">图书类型id</param>
        /// <returns></returns>
        public async Task<BookCategory> OnGetModelAsync(string id)
        {
            return await _context.BookCategory.FirstOrDefaultAsync(b => b.Id == id);
        }
        /// <summary>
        /// 修改图书类型
        /// </summary>
        /// <param name="bookCategory">图书类型</param>
        /// <returns>修改成功条数</returns>
        public async Task<int> Edit(BookCategory bookCategory)
        {
            if (!BookCategoryExists(bookCategory.Id))
            {
                return 0;
            }
            if (!string.IsNullOrEmpty(bookCategory.Img) && !File.Exists(bookCategory.Img))
            {
                var filePath = "/UploadFile/";

                bookCategory.Img = Transport(bookCategory.Img, filePath, DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
            }
            bookCategory.EditTime = DateTime.Now;

            _context.Attach(bookCategory).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 图书类型是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool BookCategoryExists(string id)
        {
            return _context.BookCategory.Any(e => e.Id == id);
        }
        /// <summary>
        /// 删除图书类型
        /// </summary>
        /// <param name="id">图书类型id</param>
        /// <returns>删除成功条数</returns>
        public async Task<int> Delete(string id)
        {
            var bookCategory = await _context.BookCategory.FindAsync(id);

            if (bookCategory == null)
            {
                return 0;
            }
            //分类下存在图书，不能删除
            if (_context.Book.Any(e => e.CategoryId == bookCategory.Id))
            {
                return 0;
            }

            _context.Attach(bookCategory).State = EntityState.Deleted;

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改图书类型是否可用
        /// </summary>
        /// <param name="id">图书类型id</param>
        /// <returns>修改成功条数</returns>
        public async Task<int> EditEnabled(string id)
        {
            var bookCategory = await _context.BookCategory.FindAsync(id);

            if (bookCategory == null)
            {
                return 0;
            }

            bookCategory.EditTime = DateTime.Now;
            bookCategory.Enabled = !bookCategory.Enabled;

            _context.Attach(bookCategory).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改图书类型是否热门
        /// </summary>
        /// <param name="id">图书类型id</param>
        /// <returns>修改成功条数</returns>
        public async Task<int> EditIsHot(string id)
        {
            var bookCategory = await _context.BookCategory.FindAsync(id);

            if (bookCategory == null)
            {
                return 0;
            }

            bookCategory.EditTime = DateTime.Now;
            bookCategory.IsHot = !bookCategory.IsHot;

            _context.Attach(bookCategory).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据base64向远程文件夹保存图片
        /// </summary>
        /// <param name="src">要保存的文件的base64字符串</param>
        /// <param name="dst">保存文件的相对路径，不含名称及扩展名</param>
        /// <param name="fileName">保存文件的名称以及扩展名</param>
        public static string Transport(string src, string dst, string fileName)
        {
            try
            {
                //过滤特殊字符即可    
                var imgType = src.Substring(src.IndexOf("/"), src.IndexOf(";") - src.IndexOf("/"));

                src = src.Substring(src.IndexOf(",") + 1);//将‘，’以前的多余字符串删除

                //将纯净资源Base64转换成等效的8位无符号整形数组
                var imgByte = Convert.FromBase64String(src);
                //转换成无法调整大小的MemoryStream对象
                using (MemoryStream memory = new MemoryStream(imgByte))
                {
                    if (!Directory.Exists(dst))
                    {
                        Directory.CreateDirectory(dst);
                    }

                    Image image = Image.FromStream(memory);

                    dst = Directory.GetCurrentDirectory() + dst + fileName;

                    image.Save(dst, ImageFormat.Jpeg);
                    return dst;
                }
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        /// <summary>
        /// 根据图片地址获取图片流
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public  string GetImage(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    return "";
                }

                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

                int length = (int)fs.Length;

                byte[] image = new byte[length];

                fs.Read(image, 0, length);

                fs.Close();

                return Convert.ToBase64String(image);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

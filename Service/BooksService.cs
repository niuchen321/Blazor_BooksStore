using BooksStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Service
{
    /// <summary>
    /// 图书管理
    /// </summary>
    public class BooksService
    {
        private readonly Data.BooksContext _context;

        public BooksService(Data.BooksContext context)
        {
            _context = context;
        }

        public IList<Book> Books { get; set; }
        /// <summary>
        /// 获取图书列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Book>> OnGetAsync()
        {
            return await _context.Book.ToListAsync();
        }
        /// <summary>
        /// 添加图书
        /// </summary>
        /// <param name="book">图书信息</param>
        /// <returns>添加成功条数</returns>
        public async Task<int> Insert(Book book)
        {
            if (!string.IsNullOrEmpty(book.Img))
            {
                var filePath = "/UploadFile/";

                book.Img = Transport(book.Img, filePath, DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
            }

            book.CreateTime = DateTime.Now;
            book.EditTime = DateTime.Now;
            book.Id = Guid.NewGuid().ToString().Replace("-", "");
            _context.Book.Add(book);
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// 获取图书信息
        /// </summary>
        /// <param name="id">图书id</param>
        /// <returns></returns>
        public async Task<Book> OnGetModelAsync(string id)
        {
            return await _context.Book.FirstOrDefaultAsync(b => b.Id == id);
        }
        /// <summary>
        /// 修改图书
        /// </summary>
        /// <param name="book">图书信息</param>
        /// <returns>修改成功条数</returns>
        public async Task<int> Edit(Book book)
        {
            if (!BookExists(book.Id))
            {
                return 0;
            }
            if (!string.IsNullOrEmpty(book.Img) && !File.Exists(book.Img))
            {
                var filePath = "/UploadFile/";

                book.Img = Transport(book.Img, filePath, DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
            }
            book.EditTime = DateTime.Now;

            _context.Attach(book).State = EntityState.Modified;

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
        /// 图书是否存在
        /// </summary>
        /// <param name="id">图书id</param>
        /// <returns></returns>
        private bool BookExists(string id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
        /// <summary>
        /// 删除图书
        /// </summary>
        /// <param name="id">图书id</param>
        /// <returns>删除成功条数</returns>
        public async Task<int> Delete(string id)
        {           
           var book = await _context.Book.FindAsync(id);

            if (book==null)
            {
                return 0;
            }

            _context.Attach(book).State = EntityState.Deleted;

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
        /// 修改图书是否可用
        /// </summary>
        /// <param name="id">图书id</param>
        /// <returns>修改成功条数</returns>
        public async Task<int> EditEnabled(string id)
        {
            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                return 0;
            }

            book.EditTime = DateTime.Now;
            book.Enabled = !book.Enabled;

            _context.Attach(book).State = EntityState.Modified;

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
        /// 修改图书是否热门
        /// </summary>
        /// <param name="id">图书id</param>
        /// <returns>修改成功条数</returns>
        public async Task<int> EditIsHot(string id)
        {
            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                return 0;
            }

            book.EditTime = DateTime.Now;
            book.IsHot = !book.IsHot;

            _context.Attach(book).State = EntityState.Modified;

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
       /// 根据图书类型id判断是否存在图书
       /// </summary>
       /// <param name="categoryId">图书类型id</param>
       /// <returns></returns>
        public async Task<bool> BookExistsCategoryId(string categoryId)
        {
            return await _context.Book.AnyAsync(e => e.CategoryId == categoryId);
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
        public string GetImage(string path)
        {
            try
            {

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

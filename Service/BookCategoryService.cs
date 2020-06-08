using BooksStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                
                Transport(bookCategory.Img, filePath, Path.GetFileName(bookCategory.Img));
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
        /// 向远程文件夹保存本地内容，或者从远程文件夹下载文件到本地
        /// </summary>
        /// <param name="src">要保存的文件的路径，如果保存文件到共享文件夹，这个路径就是本地文件路径如：@"D:\1.avi"</param>
        /// <param name="dst">保存文件的路径，不含名称及扩展名</param>
        /// <param name="fileName">保存文件的名称以及扩展名</param>
        public static void Transport(string src, string dst, string fileName)
        {

            FileStream inFileStream = new FileStream(src, FileMode.Open);
            if (!Directory.Exists(dst))
            {
                Directory.CreateDirectory(dst);
            }
            dst = dst + fileName;
            FileStream outFileStream = new FileStream(dst, FileMode.OpenOrCreate);

            byte[] buf = new byte[inFileStream.Length];

            int byteCount;

            while ((byteCount = inFileStream.Read(buf, 0, buf.Length)) > 0)
            {

                outFileStream.Write(buf, 0, byteCount);

            }

            inFileStream.Flush();

            inFileStream.Close();

            outFileStream.Flush();

            outFileStream.Close();

        }

    }
}

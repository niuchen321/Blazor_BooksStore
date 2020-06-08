using BooksStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    }
}

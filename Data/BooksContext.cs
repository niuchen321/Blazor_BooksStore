using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksStore.Data
{
    /// <summary>
    /// 图书管理系统数据库请求上下文
    /// </summary>
    public class BooksContext:DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
        }
        //Add-Migration BooksDB  
        //Update-Database
        //Drop-Database
        public DbSet<BookCategory> BookCategory { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCategory>().ToTable("BookCategory");
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Customer>().ToTable("Customer");
        }
    }
}

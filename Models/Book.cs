using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Models
{
    /// <summary>
    /// 书籍
    /// </summary>
    public class Book
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [StringLength(50)]
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 分类id
        /// </summary>
        [Required(ErrorMessage ="分类名称不可为空")]
        [StringLength(50)]
        [Display(Name = "分类名称")]
        public string CategoryId { get; set; }
        /// <summary>
        /// 书名
        /// </summary>
        [Required(ErrorMessage = "书籍名称不可为空")]
        [StringLength(50)]
        [Display(Name = "书名")]
        public string Name { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [StringLength(500)]
        [Display(Name = "图片")]
        public string Img { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        [Required(ErrorMessage = "作者不可为空")]
        [StringLength(50)]
        [Display(Name = "作者")]
        public string Author { get; set; }
        /// <summary>
        /// 出版社
        /// </summary>
        [StringLength(50)]
        [Display(Name = "出版社")]
        public string Press { get; set; }
        /// <summary>
        /// 出版时间
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "出版时间")]
        public DateTime Pubtime { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [Display(Name = "价格")]
        public decimal Price { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [Required(ErrorMessage = "简介不可为空")]
        [StringLength(500)]
        [Display(Name = "简介")]
        public string Introduction { get; set; }
        /// <summary>
        /// 预览内容
        /// </summary>
        [Display(Name = "预览内容")]
        public string Content { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        [Required]
        [Display(Name = "是否可用")]
        public bool Enabled { get; set; }
        /// <summary>
        /// 是否热门
        /// </summary>
        [Required]
        [Display(Name = "是否热门")]
        public bool IsHot { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "修改时间")]
        public DateTime EditTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Models
{
    /// <summary>
    /// 图书类型
    /// </summary>
    public class BookCategory
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [StringLength(50)]
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        [Required(ErrorMessage = "类型名称不能为空")]
        [StringLength(50)]
        [Display(Name = "类型名称")]
        public string Name { get; set; }
        /// <summary>
        /// 类型图片
        /// </summary>
        [StringLength(500)]
        [Display(Name = "类型图片")]
        public string Img { get; set; }
        /// <summary>
        /// 类型说明
        /// </summary>
        [Required(ErrorMessage = "类型说明不能为空")]
        [StringLength(500)]
        [Display(Name = "类型说明")]
        public string Remark { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Required(ErrorMessage = "排序不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "排序必须大于0")]
        [Display(Name = "排序")]
        public int Sort { get; set; }
        /// <summary>
        /// 是否热门
        /// </summary>
        [Required]
        [Display(Name = "是否热门")]
        public bool IsHot { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        [Required]
        [Display(Name = "是否可用")]
        public bool Enabled { get; set; }
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

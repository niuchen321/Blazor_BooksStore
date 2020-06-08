using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [StringLength(50)]
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "用户密码")]
        public string Password { get; set; }
        /// <summary>
        /// 用户类型：0管理员，1普通用户
        /// </summary>
        [Required]
        [Range(0,1)]
        [Display(Name = "用户类型")]
        public int Category { get; set; }
        /// <summary>
        /// 资金
        /// </summary>
        [Required]
        [Display(Name = "资金")]
        public decimal Money { get; set; }
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

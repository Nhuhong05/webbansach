﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace websitesach.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            this.Carts = new HashSet<Cart>();
            this.DonHangs = new HashSet<DonHang>();
        }
       
        public int MaKH { get; set; }

        [Display(Name = "Tên Tài Khoản")]
        [Required(ErrorMessage = "Không được để trống!")]
        public string TenTaiKhoan { get; set; }
        [Display (Name ="Mật Khẩu")]
        [Required (ErrorMessage ="Không được để trống!")]
        public string MatKhau { get; set; }

        [Display(Name = "Email")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "{0} Không hợp lệ")]
        
        [Required(ErrorMessage = "Không được để trống!")]
        public string Email { get; set; }
        public Nullable<System.DateTime> Ngaysinh { get; set; }
        public string Gioitinh { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<int> SDT { get; set; }
        public string DiaChi { get; set; }
        public Nullable<int> MaDonHang { get; set; }
        public Nullable<int> MaGH { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual Cart Cart { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}

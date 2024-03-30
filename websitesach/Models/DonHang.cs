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

    public partial class DonHang
    {
        
        public int MaDonHang { get; set; }
        [Display (Name ="Phương Thức Thanh toán")]
        public string PhuongThucThanhToan { get; set; }
        [Display(Name = "Tình trạng giao hàng")]
        public string TinhTrangGiaoHang { get; set; }
        [Display(Name = "Phương Thức Thanh toán")]
        public Nullable<System.DateTime> NgayDat { get; set; }
        [Display(Name = "Ngày Đặt")]
        public Nullable<System.DateTime> NgayGiao { get; set; }
        [Display(Name = "ngày giao")]
        public Nullable<int> MaKH { get; set; }
        [Display(Name = "mã khách hàng")]
        public Nullable<int> MaSach { get; set; }
        [Display(Name = "mã sách")]
        public Nullable<int> SoLuong { get; set; }
        [Display(Name = "đơn giá")]
        public Nullable<decimal> DonGia { get; set; }
        

        public virtual KhachHang KhachHang { get; set; }
        public virtual Sach Sach { get; set; }
    }
}

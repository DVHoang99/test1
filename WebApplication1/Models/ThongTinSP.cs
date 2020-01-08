using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ThongTinSP
    {
        public List<infoProduct> infoProducts{ get; set; }
        public int ID { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public string NationalName { get; set; }
    }
}
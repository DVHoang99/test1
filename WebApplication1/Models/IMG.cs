//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class IMG
    {
        public int IDIMG { get; set; }
        public string title { get; set; }
        public string IMGPath { get; set; }
        public Nullable<int> IDProduct { get; set; }
    
        public virtual SanPham SanPham { get; set; }
    }
}

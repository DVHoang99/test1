﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class test2Entities : DbContext
    {
        public test2Entities()
            : base("name=test2Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<login> logins { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<infoProduct> infoProducts { get; set; }
        public virtual DbSet<NATIONAL> NATIONALs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<IMG> IMGs { get; set; }
    }
}

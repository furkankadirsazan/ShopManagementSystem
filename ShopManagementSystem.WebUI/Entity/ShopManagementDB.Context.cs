﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopManagementSystem.WebUI.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ShopManagementSystemEntities : DbContext
    {
        public ShopManagementSystemEntities()
            : base("name=ShopManagementSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Counties> Counties { get; set; }
        public virtual DbSet<ELMAH_Error> ELMAH_Error { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<OutOfStockStatuses> OutOfStockStatuses { get; set; }
        public virtual DbSet<ProductGallery> ProductGallery { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Provinces> Provinces { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Shops> Shops { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<SystemSettings> SystemSettings { get; set; }
        public virtual DbSet<TaxDescriptions> TaxDescriptions { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<WarrantyPeriods> WarrantyPeriods { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<ProductCategories> ProductCategories { get; set; }
    }
}

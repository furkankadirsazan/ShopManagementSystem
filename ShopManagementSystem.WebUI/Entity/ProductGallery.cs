//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class ProductGallery
    {
        public int ID { get; set; }
        public int ShopID { get; set; }
        public int ProductID { get; set; }
        public string ImagePath { get; set; }
        public string FileName { get; set; }
    
        public virtual Products Products { get; set; }
        public virtual Shops Shops { get; set; }
    }
}

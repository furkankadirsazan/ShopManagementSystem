using ShopManagementSystem.WebUI.Entity;
using System.Collections.Generic;

namespace ShopManagementSystem.WebUI.Areas.Admin.ViewModels
{
    public class ProductGalleryViewModel
    {
        public Shops Shop { get; set; }
        public Products Product { get; set; }
        public List<ProductGallery> ProductGallery { get; set; }
    }
}
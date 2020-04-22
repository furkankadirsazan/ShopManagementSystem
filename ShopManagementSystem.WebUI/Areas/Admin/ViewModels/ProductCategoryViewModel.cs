using ShopManagementSystem.WebUI.Entity;
using System.Collections.Generic;

namespace ShopManagementSystem.WebUI.Areas.Admin.ViewModels
{
    public class ProductCategoryViewModel
    {
        public Shops Shop { get; set; }
        public Products Product { get; set; }
        public List<ProductCategories> ProductCategories { get; set; }
    }
}
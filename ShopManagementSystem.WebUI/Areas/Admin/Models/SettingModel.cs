using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Areas.Admin.Models
{
    public class SettingModel
    {
        [AllowHtml]
        public Dictionary<string, string> Fields { get; set; }
        [AllowHtml]
        public Dictionary<string, bool> SystemSettings { get; set; }
    }
}
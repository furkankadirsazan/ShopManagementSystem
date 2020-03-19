﻿namespace ShopManagementSystem.WebUI.Areas.Admin.Models
{
    public class ShopAccountModel
    {
        public int ShopID { get; set; }
        public int ProvinceID { get; set; }
        public int CountyID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
        public string BannerImagePath { get; set; }
        public string LogoPath { get; set; }
        public int? Point { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string TaxNumber { get; set; }
        public string TaxAdministration { get; set; }
        public string ShopWebsite { get; set; }
        public string Address { get; set; }
    }
}
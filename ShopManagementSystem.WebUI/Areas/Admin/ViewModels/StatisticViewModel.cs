
namespace ShopManagementSystem.WebUI.Areas.Admin.ViewModels
{
    public class StatisticViewModel
    {
        public int OrdersTotal { get; set; } 
        public int OrdersInWait { get; set; } //status 1

        public int OrdersInProcess { get; set; } //status 2 + 3 + 7

        public int OrdersInCompleted { get; set; } //status 14

        public int OrdersInCancelled { get; set; } //status 10 

        public int OrdersInCargo { get; set; } //status 4

        public int OrdersInUnsuccesfull { get; set; } //status 5 + 6 + 8 + 12 +  13 + 15 
        public int OrdersInReturnBack { get; set; } //status 9 + 11

        public int TotalProducts { get; set; }






    }
}
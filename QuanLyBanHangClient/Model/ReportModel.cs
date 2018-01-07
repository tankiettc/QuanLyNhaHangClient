using QuanLyBanHangAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangClient.Model {
    public class ReportModel {
        public DateTime startDate;
        public DateTime endDate;
        public List<Order> payOrders;
        public int totalFood;
        public int totalFoodSold;
        public int totalProcessingFoodCancel;
        public int totalQueueFoodCancel;
        public int totalOrder;
        public int totalPayOrder;
        public List<Log> cancelFoodLogs;
        public decimal totalMoneyReceive;
        public decimal estimatedProfit;
        public decimal estimatedTotalFoodMoneyUse;
        public decimal totalProfitLossByCancelProcessingFood;

        public ReportModel() {
        }

    }
}

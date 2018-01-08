using Newtonsoft.Json;
using QuanLyBanHangClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangClient.Manager
{
    class ReportManager
    {
        static private ReportManager _instance = null;
        static public ReportManager getInstance() {
            if (_instance == null) {
                _instance = new ReportManager();
            }
            return _instance;
        }
        private string API_CONTROLLER = "/api/report";
        private ReportModel _report = null;
        public ReportModel ReportModel { get { return _report; } }
        private ReportManager() {
        }
        #region Network

        public async Task getAllReportModelFromServerAndUpdate(
                    string startDate, //format 15/12/2017
                    string endDate, //format 15/2/2018
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    _report = JsonConvert.DeserializeObject<ReportModel>(networkResponse.Data.ToString());
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            string uri = API_CONTROLLER;
            uri += (startDate == endDate ? "?reportDate=" + startDate : "?startDate=" + startDate + "&endDate=" + endDate);
            await RequestManager.getInstance().getAsync(
                uri,
                newCBSuccessSent,
                cbError
                );
        }
        #endregion
    }
}

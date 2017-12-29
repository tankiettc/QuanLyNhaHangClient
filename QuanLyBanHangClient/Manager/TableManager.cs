using Newtonsoft.Json;
using QuanLyBanHangAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangClient.Manager
{
    class TableManager
    {
        static private TableManager _instance = null;
        static public TableManager getInstance() {
            if(_instance == null) {
                _instance = new TableManager();
            }
            return _instance;
        }
        private string API_CONTROLLER = "/api/table";
        private Dictionary<int, Table> _tableList;
        public Dictionary<int, Table> TableList { get { return _tableList; } }
        private TableManager() {
            _tableList = new Dictionary<int, Table>();
        }
        #region Network

        public async Task getAllTableFromServerAndUpdate(
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if(networkResponse.Successful) {
                    _tableList.Clear();
                    List<Table> tableListFromSV = JsonConvert.DeserializeObject<List<Table>>(networkResponse.Data.ToString());
                    tableListFromSV.ForEach(delegate (Table table) {
                        _tableList.Add(table.TableId, table);
                    });
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            await RequestManager.getInstance().getAsync(
                API_CONTROLLER,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task createTableFromServerAndUpdate(
                    int tableId,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    Table tableCreated = JsonConvert.DeserializeObject<Table>(networkResponse.Data.ToString());
                    _tableList[tableCreated.TableId] = tableCreated;
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            KeyValuePair<string, string>[] keys = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("TableId", tableId.ToString())
            };
            await RequestManager.getInstance().postAsync(
                API_CONTROLLER,
                keys,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task updateTableFromServerAndUpdate(
                    int tableId,
                    int newTableId,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                if (networkResponse.Successful) {
                    Table tableCreated = JsonConvert.DeserializeObject<Table>(networkResponse.Data.ToString());
                    _tableList[tableCreated.TableId] = tableCreated;
                    _tableList.Remove(tableId);
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            KeyValuePair<string, string>[] keys = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("TableId", newTableId.ToString())
            };
            await RequestManager.getInstance().putAsync(
                API_CONTROLLER + "/" + tableId,
                keys,
                newCBSuccessSent,
                cbError
                );
        }
        public async Task deleteTableromServerAndUpdate(
                    int id,
                    Action<NetworkResponse> cbSuccessSent = null,
                    Action<string> cbError = null
            ) {
            Action<NetworkResponse> newCBSuccessSent = delegate (NetworkResponse networkResponse) {
                //debug
                //update tu sv
                if (networkResponse.Successful) {
                    _tableList.Remove(id);
                }
                cbSuccessSent?.Invoke(networkResponse);
            };
            await RequestManager.getInstance().deleteAsync(
                API_CONTROLLER + "/" + id,
                newCBSuccessSent,
                cbError
                );
        }
        #endregion
    }
}

using QuanLyBanHangClient.AppUserControl.FoodTab;
using QuanLyBanHangClient.AppUserControl.IngredientTab;
using QuanLyBanHangClient.WindowControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyBanHangClient.Manager {
    class WindownsManager {
        static private WindownsManager _instance = null;
        static public WindownsManager getInstance() {
            if (_instance == null) {
                _instance = new WindownsManager();
            }
            return _instance;
        }
        public void showMessageBoxSomeThingWrong() {
            MessageBox.Show(Constant.MS_ERROR_SOMETHING);
        }
        public void showMessageBoxCheckInfoAgain() {
            MessageBox.Show(Constant.MS_CHECK_INFO_AGAIN);
        }
        public MessageBoxResult showMessageBoxYesNoCustom(string info, string capture = "Xác nhận") {
            return MessageBox.Show(info, capture, MessageBoxButton.YesNo);
        }
        public MessageBoxResult showMessageBoxConfirm(string info, string capture = "Xác nhận") {
            return MessageBox.Show(info, capture);
        }
        public void showMessageBoxErrorNetwork() {
            MessageBox.Show(Constant.MS_ERROR_NETWORK);
        }
        public MessageBoxResult showMessageBoxConfirmDelete() {
            return MessageBox.Show("Bạn có thật sự muốn xóa? ", "Xác nhận xóa", MessageBoxButton.YesNo);
        }
        //ingredientId == -1: create new ingredient
        public void showDetailIngredientWindow(
            IngredientTab ingredientTab,
            int ingredientId = Constant.ID_CREATE_NEW) {
            IngredientDetail detail = new IngredientDetail(
                ingredientTab,
                ingredientId);
            detail.ShowDialog();
        }
        //unitId == -1: create new unit
        public void showDetailUnitWindow(
            IngredientTab ingredientTab,
            int unitId = Constant.ID_CREATE_NEW) {
            UnitDetail detail = new UnitDetail(
                ingredientTab,
                unitId);
            detail.ShowDialog();
        }
        public void showDetailFoodWithCategorizeWindow(
            FoodTab foodTab,
            int foodWithCategorizeId = Constant.ID_CREATE_NEW) {
            CategorizeDetail detail = new CategorizeDetail(
                foodTab,
                foodWithCategorizeId);
            detail.ShowDialog();
        }
        public void showDetailFoodWindow(
            FoodTab foodTab,
            int foodId = Constant.ID_CREATE_NEW) {
            FoodDetail detail = new FoodDetail(
                foodTab,
                foodId);
            detail.ShowDialog();
        }
        public void showChangePasswordWindow(
            string userName
            ) {
            var window = new ChangePassWindow(userName);
            window.ShowDialog();
        }
    }
}

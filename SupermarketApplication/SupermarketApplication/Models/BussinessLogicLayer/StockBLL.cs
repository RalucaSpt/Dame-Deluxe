using SupermarketApplication.Models.DataAccessLayer;
using SupermarketApplication.Models.EntityLayer;
using System.Collections.ObjectModel;

namespace SupermarketApplication.Models.BussinessLogicLayer
{
    public class StockBLL
    {
        public ObservableCollection<Stock> StockList { get; set; }
        public string ErrorMessage { get; set; }

        public StockBLL()
        {
            StockList = new ObservableCollection<Stock>(GetAllStocks());
        }

        public void AddMethod(object obj)
        {
            Stock stock = obj as Stock;
            if (stock != null)
            {
                StockDAL.AddStock(stock);
                stock.StockID = DBContext.context.Stocks.Max(item => item.StockID);
                StockList.Add(stock);
                ErrorMessage = "";
            }
        }

        public void UpdateMethod(object obj)
        {
            Stock stock = obj as Stock;
            if (stock == null)
            {
                ErrorMessage = "Select a stock item";
            }
            else if (stock.SellingPrice < stock.PurchasePrice)
            {
                ErrorMessage = "Selling price cannot be less than purchase price";
            }
            else
            {
                StockDAL.UpdateStock(stock);
                var itemIndex = StockList.IndexOf(stock);
                if (itemIndex >= 0)
                {
                    StockList[itemIndex] = stock;
                }
                ErrorMessage = "";
            }
        }

        public void DeleteMethod(object obj)
        {
            Stock stock = obj as Stock;
            if (stock == null)
            {
                ErrorMessage = "Select a stock item";
            }
            else
            {
                StockDAL.DeleteStock(stock);
                StockList.Remove(stock);
                ErrorMessage = "";
            }
        }

        public ObservableCollection<Stock> GetAllStocks()
        {
            List<Stock> stocks = StockDAL.GetAllStocks();
            ObservableCollection<Stock> result = new ObservableCollection<Stock>(stocks);
            return result;
        }
    }
}

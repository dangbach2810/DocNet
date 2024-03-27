using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BUS
{
    public class BUS_BillInfo
    {
        
        QLSanPhamDataContext db = new QLSanPhamDataContext();
      

        public List<tblBillInfo> ListBillInfo()
        {
            return null;// dalBillInfo.ListBillInfo();
        }

        public bool InsertBillInfo(tblBillInfo billInfo, int quantity)
        {
            return true;// dalBillInfo.InsertBillInfo(billInfo, quantity);
        }

        public double GetTotalPrice()
        {
            return 0;// dalBillInfo.GetTotalPrice();
        }

        public bool DeleteProductInBillInfo(int id)
        {
            return true;// dalBillInfo.DeleteProductInBillInfo(id);
        }

        public bool UpdateProductInBillInfo(int id, int quantity)
        {
            return true;// dalBillInfo.UpdateProductInBillInfo(id, quantity);
        }
        
    }
}

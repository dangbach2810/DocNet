using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Product
    {
        QLSanPhamDataContext db  = new QLSanPhamDataContext();

        public List<tblProduct> ListOfProducts()
        {
            var temp =  db.tblProducts.ToList();
            return temp;
        }

        public bool InsertProduct(tblProduct product)
        {
            try
            {
                db.tblProducts.InsertOnSubmit(product);
                db.SubmitChanges();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateProduct(tblProduct product)
        {
            try
            {
                tblProduct dbcontext = db.tblProducts.Where(pro => pro.Id == product.Id).FirstOrDefault();
                dbcontext.Name = product.Name;
                dbcontext.Note = product.Note;
                dbcontext.ImportUnitPrice = product.ImportUnitPrice;
                dbcontext.UnitPrice = product.UnitPrice;
                dbcontext.Quantity = product.Quantity;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var product = db.tblProducts.Where(pro => pro.Id == id).FirstOrDefault();
                db.tblProducts.DeleteOnSubmit(product);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<tblProduct> SearchProduct(string name)
        {
            return db.tblProducts.Where(pro => pro.Name.Contains(name)).ToList();
        }

       /* public string[] ListProductNameQuantity()
        {
            return dalProduct.ListProductNameQuantity();
        }

        public double GetUnitPrice(string name)
        {
            return dalProduct.GetUnitPrice(name);
        }

        public int GetProductId(string name)
        {
            return dalProduct.GetProductId(name);
        }*/
    }
}

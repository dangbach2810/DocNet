using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Customer
    {
        QLSanPhamDataContext db = new QLSanPhamDataContext();

        public List<tblCustomer> ListOfCustomers()
        {
            return db.tblCustomers.ToList();

        }
        public bool InsertCustomer(tblCustomer customer)
        {
            try
            {
                db.tblCustomers.InsertOnSubmit(customer);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool DeleteCustomer(int id)
        {
            try
            {
                tblCustomer tblCustomer = (tblCustomer)db.tblCustomers.FirstOrDefault(cus => cus.Id == id);
                db.tblCustomers.DeleteOnSubmit(tblCustomer);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /*public bool UpdateCustomer(tblCustomer customer)
        {
            try
            {
                tblCustomer tblCustomer = db.tblCustomers.Where(cus => cus.Id == customer.Id).FirstOrDefault();
             
                    tblCustomer.Name = customer.Name;
                    tblCustomer.Address = customer.Address;
                    tblCustomer.PhoneNumber = customer.PhoneNumber;
                    db.SubmitChanges();
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }*/

        public bool UpdateCustomer(tblCustomer customer)
        {
            try
            {
                tblCustomer tblCustomer = db.tblCustomers.FirstOrDefault(cus => cus.Name == customer.Name); ;
                if (tblCustomer != null)
                {
                    tblCustomer.Address = customer.Address;
                    tblCustomer.PhoneNumber = customer.PhoneNumber;
                    db.SubmitChanges();
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public List<tblCustomer> SearchCustomer(string name)
        {
            return db.tblCustomers.Where(cus => cus.Name.Contains(name)).ToList();
        }

    }
}

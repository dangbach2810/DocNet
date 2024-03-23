using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_Employee
    {
        private QLSanPhamDataContext db;

        public BUS_Employee()
        {
            db = new QLSanPhamDataContext();
        }
        public bool Login(string email, string password)
        {

            tblEmployee employee = db.tblEmployees.FirstOrDefault(emp => emp.Email == email);
            if (employee != null)
            {
                return true;
            }
            return false;

        }

        public tblEmployee TimNVByEmail(string email)
        {
            return db.tblEmployees.FirstOrDefault(nv => nv.Email == email);
        }



        /* private string Encrytion(string input)
         {
             using (MD5 md5 = MD5.Create())
             {
                 byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                 byte[] hashBytes = md5.ComputeHash(inputBytes);
                 StringBuilder sb = new StringBuilder();

                 for (int i = 0; i < hashBytes.Length; i++)
                 {
                     sb.Append(hashBytes[i].ToString("x2"));
                 }

                 return sb.ToString();
             }
         }*/

       

        /*public bool IsExistEmail(string email)
        {
            return dalEmployee.IsExistEmail(email);
        }

        public bool UpdatePassword(string email, string password)
        {
            password = Encrytion(password);
            return dalEmployee.UpdatePassword(email, password);
        }

        public bool GetEmployeeRole(string email)
        {
            return dalEmployee.GetEmployeeRole(email);
        }

        public bool ChangePassword(string email, string oldPassword, string newPassword)
        {
            oldPassword = Encrytion(oldPassword);
            newPassword = Encrytion(newPassword);
            return dalEmployee.ChangePassword(email, oldPassword, newPassword);
        }*/

        public List<tblEmployee> ListOfEmployees()
        {
            return db.tblEmployees.ToList();
        }

       public bool InsertEmployee(tblEmployee employee)
        {
            try
            {
                db.tblEmployees.InsertOnSubmit(employee);
                db.SubmitChanges();
                return true;
            }catch (Exception ex) 
            {
            
                return false;
            }
        }

        public bool UpdateEmployee(tblEmployee employee)
        {
            try
            {
                tblEmployee tblEmployee = db.tblEmployees.Where(emp => emp.Email == employee.Email).FirstOrDefault();
                tblEmployee.Name = employee.Name;
                tblEmployee.Address = employee.Address;
                tblEmployee.PhoneNumber = employee.PhoneNumber;
                tblEmployee.Email = employee.Email;
                tblEmployee.Role = employee.Role;
                tblEmployee.Status = employee.Status;

                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateEmployeeAddressPhoneNumber(tblEmployee employee)
        {
            try
            {
                tblEmployee tblEmployee = db.tblEmployees.FirstOrDefault(emp => emp.Email == employee.Email);

                if (tblEmployee != null)
                {
                    tblEmployee.Address = employee.Address;
                    tblEmployee.PhoneNumber = employee.PhoneNumber;

                    db.SubmitChanges();
                    return true;
                }
                else
                {
                    return false; // Trả về false nếu không tìm thấy nhân viên với email tương ứng
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            //try
            //    {
            //    tblEmployee tblEmployee = db.tblEmployees.Where(emp => emp.Email == employee.Email).FirstOrDefault();
            //    if (tblEmployee != null)
            //        {
            //            tblEmployee.Address = employee.Address;
            //            tblEmployee.PhoneNumber = employee.PhoneNumber;

            //            db.SubmitChanges();
            //            return true;
            //        }
            //        else
            //        {
            //            return false; // Trả về false nếu không tìm thấy nhân viên với email tương ứng
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        // Xử lý exception nếu có
            //        return false;
            //    }
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                tblEmployee tblEmployee = (tblEmployee)db.tblEmployees.FirstOrDefault(emp => emp.Id == id);
                db.tblEmployees.DeleteOnSubmit(tblEmployee);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<tblEmployee> SearchEmployee(string name)
        {
            return db.tblEmployees.Where(emp => emp.Name.Contains(name)).ToList();
        }

       /* public string GetEmployeeIdName(string email)
        {
            return dalEmployee.GetEmployeeIdName(email);
        }*/

       /* public string GetEmployeeAddressPhoneNumber(string email)
        {
            return dalEmployee.GetEmployeeAddressPhoneNumber(email);
        }*/

        public string GetRandomPassword()
        {
            Random r = new Random();
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(r.Next(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random r = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * r.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
            {
                return builder.ToString().ToUpper();
            }
            else return builder.ToString().ToLower();
        }

    }
}

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
        }

        //private string Encrytion(string input)
        //{
        //    using (MD5 md5 = MD5.Create())
        //    {
        //        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        //        byte[] hashBytes = md5.ComputeHash(inputBytes);

        //        // Chuyển mảng byte sang chuỗi hexa
        //        string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

        //        return hashString;
        //    }
        //}

        public bool ChangePassword(string email, string oldPassword, string newPassword)
        {
            try
            {
                // Lấy thông tin nhân viên từ cơ sở dữ liệu dựa trên email
                tblEmployee employee = db.tblEmployees.FirstOrDefault(emp => emp.Email == email);

                if (employee != null)
                {
                    // Kiểm tra mật khẩu cũ
                    if (employee.Password == oldPassword)
                    {
                        // Nếu mật khẩu cũ đúng, cập nhật mật khẩu mới
                        employee.Password = newPassword;
                        db.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        // Nếu mật khẩu cũ không đúng, trả về false
                        return false;
                    }
                }
                else
                {
                    // Nếu không tìm thấy nhân viên với email tương ứng, trả về false
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và trả về false nếu có lỗi xảy ra
                return false;
            }
        }

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

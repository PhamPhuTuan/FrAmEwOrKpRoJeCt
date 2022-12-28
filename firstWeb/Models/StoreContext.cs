using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FrameworkProject.Models;
using System.Globalization;

namespace firstWeb.Models
{
    public class StoreContext
    {
        public string ConnectionString { get; set; }

        public StoreContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection() 
        {
            return new MySqlConnection(ConnectionString);
        }
        // Liet ke tat ca product
        public List<Products> GetProducts()
        {
            List<Products> list = new List<Products>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from Products where IsDeleted = 0";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Products()
                        {
                            PROID = Int32.Parse(reader["ProID"].ToString()),
                            NAME = reader["Name"].ToString(),
                            DESCRIPTION = reader["Description"].ToString(),
                            STAR = Int32.Parse(reader["Star"].ToString()),
                            QUANTITY = Int32.Parse(reader["Quantity"].ToString()),
                            CATEID = Int32.Parse(reader["CateId"].ToString()),
                            CREATEDON = DateTime.Parse(reader["CreatedOn"].ToString()),
                            UPDATEDON = DateTime.Parse(reader["UpdatedOn"].ToString()),
                            STATUS = Int32.Parse(reader["Status"].ToString()),
                            ISDELETED = Int32.Parse(reader["IsDeleted"].ToString()),
                            PRICE = float.Parse(reader["Price"].ToString()),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        // Liet ke product yeu thich
        public List<Products> GetFavoriteProducts(int userID)
        {
            List<Products> list = new List<Products>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from favoriteproduct f, products p where p.ProductID =" +
                    " f.ProductID and f.UserID = @userid and p.IsDeleted = 0";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("userid", userID);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Products()
                        {
                            PROID = Int32.Parse(reader["ProID"].ToString()),
                            NAME = reader["Name"].ToString(),
                            DESCRIPTION = reader["Description"].ToString(),
                            STAR = Int32.Parse(reader["Star"].ToString()),
                            QUANTITY = Int32.Parse(reader["Quantity"].ToString()),
                            CATEID = Int32.Parse(reader["CateId"].ToString()),
                            CREATEDON = DateTime.Parse(reader["CreatedOn"].ToString()),
                            UPDATEDON = DateTime.Parse(reader["UpdatedOn"].ToString()),
                            STATUS = Int32.Parse(reader["Status"].ToString()),
                            ISDELETED = Int32.Parse(reader["IsDeleted"].ToString()),
                            PRICE = float.Parse(reader["Price"].ToString()),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        // Tim product 
        public List<Products> FindProducts(string name)
        {
            List<Products> list = new List<Products>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from Products where IsDeleted = 0 and Name Like %@name%";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("name", name);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Products()
                        {
                            PROID = Int32.Parse(reader["ProID"].ToString()),
                            NAME = reader["Name"].ToString(),
                            DESCRIPTION = reader["Description"].ToString(),
                            STAR = Int32.Parse(reader["Star"].ToString()),
                            QUANTITY = Int32.Parse(reader["Quantity"].ToString()),
                            CATEID = Int32.Parse(reader["CateId"].ToString()),
                            CREATEDON = DateTime.Parse(reader["CreatedOn"].ToString()),
                            UPDATEDON = DateTime.Parse(reader["UpdatedOn"].ToString()),
                            STATUS = Int32.Parse(reader["Status"].ToString()),
                            ISDELETED = Int32.Parse(reader["IsDeleted"].ToString()),
                            PRICE = float.Parse(reader["Price"].ToString()),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        // Product theo cate
        public List<Products> SelectProducts(int cate)
        {
            List<Products> list = new List<Products>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from Products where IsDeleted = 0 and CateId Like %@cate%";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("cate", cate);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Products()
                        {
                            PROID = Int32.Parse(reader["ProID"].ToString()),
                            NAME = reader["Name"].ToString(),
                            DESCRIPTION = reader["Description"].ToString(),
                            STAR = Int32.Parse(reader["Star"].ToString()),
                            QUANTITY = Int32.Parse(reader["Quantity"].ToString()),
                            CATEID = Int32.Parse(reader["CateId"].ToString()),
                            CREATEDON = DateTime.Parse(reader["CreatedOn"].ToString()),
                            UPDATEDON = DateTime.Parse(reader["UpdatedOn"].ToString()),
                            STATUS = Int32.Parse(reader["Status"].ToString()),
                            ISDELETED = Int32.Parse(reader["IsDeleted"].ToString()),
                            PRICE = float.Parse(reader["Price"].ToString()),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        // Invoice Detail
        public List<Products> SelectInvoiceDetail(Invoices i)
        {
            List<Products> list = new List<Products>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from products p, invoicedetail i WHERE p.ProductID = i.ProId and i.PackID = @packid";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("packid", i.PACKID);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Products()
                        {
                            PROID = Int32.Parse(reader["ProID"].ToString()),
                            NAME = reader["Name"].ToString(),
                            DESCRIPTION = reader["Description"].ToString(),
                            STAR = Int32.Parse(reader["Star"].ToString()),
                            QUANTITY = Int32.Parse(reader["Qty"].ToString()),
                            CATEID = Int32.Parse(reader["CateId"].ToString()),
                            CREATEDON = DateTime.Parse(reader["CreatedOn"].ToString()),
                            UPDATEDON = DateTime.Parse(reader["UpdatedOn"].ToString()),
                            STATUS = Int32.Parse(reader["Status"].ToString()),
                            ISDELETED = Int32.Parse(reader["IsDeleted"].ToString()),
                            PRICE = float.Parse(reader["Price"].ToString()),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        // All Review
        public List<Review> SelectReview()
        {
            List<Review> list = new List<Review>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from review";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Review()
                        {
                            REVIEWER = Int32.Parse(reader["Reviewer"].ToString()),
                            CONTENT = reader["Content"].ToString()
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        // ALL Product
        public List<Products> AllProducts()
        {
            List<Products> list = new List<Products>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from Products";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Products()
                        {
                            PROID = Int32.Parse(reader["ProID"].ToString()),
                            NAME = reader["Name"].ToString(),
                            DESCRIPTION = reader["Description"].ToString(),
                            STAR = Int32.Parse(reader["Star"].ToString()),
                            QUANTITY = Int32.Parse(reader["Quantity"].ToString()),
                            CATEID = Int32.Parse(reader["CateId"].ToString()),
                            CREATEDON = DateTime.Parse(reader["CreatedOn"].ToString()),
                            UPDATEDON = DateTime.Parse(reader["UpdatedOn"].ToString()),
                            STATUS = Int32.Parse(reader["Status"].ToString()),
                            ISDELETED = Int32.Parse(reader["IsDeleted"].ToString()),
                            PRICE = float.Parse(reader["Price"].ToString()),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        // ALL User
        public List<Users> AllUser()
        {
            List<Users> list = new List<Users>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from Users";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Users()
                        {
                            USERID = Int32.Parse(reader["UserID"].ToString()),
                            USERNAME = reader["Username"].ToString(),
                            PASSWORD = reader["Password"].ToString(),
                            ADDRESS = reader["Address"].ToString(),
                            EMAIL = reader["Email"].ToString(),
                            PHONE = reader["Phone"].ToString(),
                            FIRSTNAME = reader["Firstname"].ToString(),
                            LASTNAME = reader["Lastname"].ToString(),
                            DATEOFBIRTH = DateTime.Parse(reader["DateOfBirth"].ToString()),
                            REGISTRATIONDATE = DateTime.Parse(reader["Registrationdate"].ToString()),
                            UPDATEDON = DateTime.Parse(reader["UpdatedOn"].ToString()),
                            ISDELETED = Int32.Parse(reader["IsDeleted"].ToString()),
                            ROLEID = Int32.Parse(reader["IsAdmin"].ToString())
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        [HttpPost]
        /////// PRODUCT
        // Them product
        public int InsertProduct(Products p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into products ('Name', 'Description', 'price', 'quantity', " +
                    "'cateid') values(@productname, @description, @price, @quantity, @cateid)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("productname", p.NAME);
                cmd.Parameters.AddWithValue("description", p.DESCRIPTION);
                cmd.Parameters.AddWithValue("price", p.PRICE);
                cmd.Parameters.AddWithValue("quantity", p.QUANTITY);
                cmd.Parameters.AddWithValue("cateid", p.CATEID);

                return (cmd.ExecuteNonQuery());

            }
        }
        // Sua product
        public int UpdateProduct(Products p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE products SET Name = @name, Description = @description"
                    +", Price = @price, Quantity = @quantity, Cateid = @cateid, Status =" +
                    " @status, Isdeleted = @isdeleted WHERE ProductID = @productid";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("name", p.NAME);
                cmd.Parameters.AddWithValue("description", p.DESCRIPTION);
                cmd.Parameters.AddWithValue("price", p.PRICE);
                cmd.Parameters.AddWithValue("quantity", p.QUANTITY);
                cmd.Parameters.AddWithValue("cateid", p.CATEID);
                cmd.Parameters.AddWithValue("status", p.STATUS);
                cmd.Parameters.AddWithValue("isdeleted", p.ISDELETED);
                cmd.Parameters.AddWithValue("productid", p.PROID);
                return (cmd.ExecuteNonQuery());
            }
        }
        // Them product yeu thich
        public int InsertFavoriteProduct(Users u, Products p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "INSERT INTO 	favoriteproduct	(UserID, ProductID) values (@userid, @productid)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("userid", u.USERID);
                cmd.Parameters.AddWithValue("productid", p.PROID);

                return (cmd.ExecuteNonQuery());
            }
        }
        // Xoa product yeu thich
        public int DeleteFavoriteProduct(Users u, Products p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "Delete from favoriteproduct where UserID = @userid, ProductID = @productid";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("userid", u.USERID);
                cmd.Parameters.AddWithValue("productid", p.PROID);

                return (cmd.ExecuteNonQuery());
            }
        }

        /////// User
        // Dang ky
        public Users InsertUser(Users u)
        {
            Users u1 = new Users();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into products ('Username', 'Password', 'Address', 'Email', " +
                    "'Phone', 'Firstname', 'Lastname') values(@username, @password, @address," +
                    " @email, @phone, @first, @last)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("username", u.USERNAME);
                cmd.Parameters.AddWithValue("password", u.PASSWORD);
                cmd.Parameters.AddWithValue("address", u.ADDRESS);
                cmd.Parameters.AddWithValue("email", u.EMAIL);
                cmd.Parameters.AddWithValue("phone", u.PHONE);
                cmd.Parameters.AddWithValue("first", u.FIRSTNAME);
                cmd.Parameters.AddWithValue("last", u.LASTNAME);
                cmd.ExecuteNonQuery();

                string str1 = "select * from Users where Username = @username";
                MySqlCommand cmd1 = new MySqlCommand(str1, conn);
                cmd1.Parameters.AddWithValue("username", u.USERNAME);
                using (var reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        u1 = new Users(
                            Int32.Parse(reader["UserID"].ToString()),
                            reader["Username"].ToString(),
                            reader["Password"].ToString(),
                            reader["Address"].ToString(),
                            reader["Email"].ToString(),
                            reader["Phone"].ToString(),
                            reader["Firstname"].ToString(),
                            reader["Lastname"].ToString(),
                            DateTime.Parse(reader["DateOfBirth"].ToString()),
                            DateTime.Parse(reader["Registrationdate"].ToString()),
                            DateTime.Parse(reader["UpdatedOn"].ToString()),
                            Int32.Parse(reader["IsAdmin"].ToString()), 
                            Int32.Parse(reader["IsDeleted"].ToString()), 0
                        );
                    }
                    reader.Close();
                }
            }
            return u1;
        }
        // Dang nhap
        public Users LogIn(Users u)
        {
            Users u1 = new Users();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                string str = "select * from Users where Email = @username and Password = @pass";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("username", u.EMAIL);
                cmd.Parameters.AddWithValue("pass", u.PASSWORD);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CultureInfo culture = new CultureInfo("en-US");
                        DateTime day = Convert.ToDateTime("7/10/2002 12:10:15 PM", culture);

                        u1 = new Users();
                        u1.USERID = Int32.Parse(reader["UserID"].ToString());
                        u1.USERNAME = reader["Username"].ToString();
                        u1.PASSWORD = reader["Password"].ToString();
                        u1.EMAIL = reader["Email"].ToString();
                        
                            /*,
                            reader["Address"].ToString(),
                            ,
                            reader["Phone"].ToString(),
                            reader["Firstname"].ToString(),
                            reader["Lastname"].ToString(),
                            day,
                            day,
                            day,
                            Int32.Parse(reader["IsAdmin"].ToString()),
                            Int32.Parse(reader["IsDeleted"].ToString()), 0
                        );*/
                    }
                    reader.Close();
                }
            }
            return u1;
        }

        ////// CATEGORY
        // Them category
        public int InsertCategory(Category c)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into category ('CateName') values(@catename)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("catename", c.CATENAME);

                return (cmd.ExecuteNonQuery());

            }
        }
        // Sua category
        public int UpdateCategory(Category c)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE category SET CateName = @catename WHERE CateId = @cateid";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("catename", c.CATENAME);
                cmd.Parameters.AddWithValue("cateid", c.CATEID);
                return (cmd.ExecuteNonQuery());
            }
        }

        ///// INVOICE
        // Tao invoice
        public Invoices createinvoice(Users u)
        {
            Invoices i = new Invoices();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into invoice (UserID) values (@userid)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("userid", u.USERID);
                cmd.ExecuteNonQuery();

                var str1 = "SELECT * from invoice order by PackID DESC limit 1";
                MySqlCommand cmd1 = new MySqlCommand(str1, conn);
                using (var reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        i = new Invoices(
                            Int32.Parse(reader["PackID"].ToString()),
                            Int32.Parse(reader["IsPaid"].ToString()),
                            DateTime.Parse(reader["CreatedOn"].ToString()),
                            DateTime.Parse(reader["PaidOn"].ToString()),
                            float.Parse(reader["TotalPrice"].ToString()),
                            Int32.Parse(reader["UserID"].ToString())
                        );
                    }
                    reader.Close();
                }
            }
            return i;
        }
        // Them san pham vao invoice
        public int InsertProductintoInvoice(Products p, Invoices i, int quantity)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into invoicedetail (ProId, PackID, Qty) values (@proid, @packid, @quantity)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("proid", p.PROID);
                cmd.Parameters.AddWithValue("packid", i.PACKID);
                cmd.Parameters.AddWithValue("quantity", quantity);
                return cmd.ExecuteNonQuery();
            }
        }
        //Sua san pham trong invoice
        public int UpdateProductInInvoice (InvoiceDetail i)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "UPDATE invoicedetail SET Qty = @quantity WHERE ProId = @proid and PackID = @packid";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("proid", i.PROID);
                cmd.Parameters.AddWithValue("quantity", i.QUANTITY);
                cmd.Parameters.AddWithValue("packid", i.PACKID);
                return cmd.ExecuteNonQuery();
            }
        }
        // Xoa san pham trong invoice
        public int DeleteProductInInvoice (InvoiceDetail i)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "Delete from invoicedetail where ProId = @proid and PackID = @packid";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("proid", i.PROID);
                cmd.Parameters.AddWithValue("packid", i.PACKID);
                return cmd.ExecuteNonQuery();
            }
        }

        public int AddReview (Review r)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "Insert into review (	Reviewer , Content) values (@reviewer, @content)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("reviewer", r.REVIEWER);
                cmd.Parameters.AddWithValue("content", r.CONTENT);
                return cmd.ExecuteNonQuery();
            }
        }
        public StoreContext()
        {
        }
    }
}

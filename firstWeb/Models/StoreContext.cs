using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FrameworkProject.Models;

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

        




        public List<Khoa> GetKhoas()
        {
            List<Khoa> list = new List<Khoa>();

            //MySqlConnection conn = new MySqlConnection("server=127.0.0.1;user id=root;password=;port=3306;database=quanlycasi1;");
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from KHOA";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Khoa()
                        {
                            MaKhoa = reader["MaKhoa"].ToString(),
                            TenKhoa = reader["TenKhoa"].ToString(),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        public List<SinhVien> GetSinhViens()
        {
            List<SinhVien> list = new List<SinhVien>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from SINHVIEN";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SinhVien()
                        {
                            MaSinhVien = reader["MaSinhVien"].ToString(),
                            TenSinhVien = reader["TenSinhVien"].ToString(),
                            DiemTrungBinh = Convert.ToInt32(reader["DiemTrungBinh"]),
                            MaBoMon = reader["MaBoMon"].ToString(),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        public List<SinhVien> GetSinhViens(string mbm)
        {
            List<SinhVien> list = new List<SinhVien>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from SINHVIEN where MaBoMon=@mbm";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("mbm", mbm);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SinhVien()
                        {
                            MaSinhVien = reader["MaSinhVien"].ToString(),
                            TenSinhVien = reader["TenSinhVien"].ToString(),
                            DiemTrungBinh = Convert.ToDouble(reader["DiemTrungBinh"]),
                        }); 
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }

        [HttpPost]
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
        // Tao invoice
        public Invoices CreateInvoice()
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "Insert into invoice (TotalPrice) values ('0')";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.ExecuteNonQuery();


            }
        }

        public int InsertKhoa(Khoa kh)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into KHOA values(@makhoa, @tenkhoa)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("makhoa", kh.MaKhoa);
                cmd.Parameters.AddWithValue("tenkhoa", kh.TenKhoa);

                return (cmd.ExecuteNonQuery());

            }
        }
        public int UpdateKhoa(Khoa kh)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update KHOA set TenKhoa = @tenkhoa " +
                    "where MaKhoa=@makhoa";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("tenkhoa", kh.TenKhoa);
                cmd.Parameters.AddWithValue("makhoa", kh.MaKhoa);
                return (cmd.ExecuteNonQuery());
            }
        }
        public int XoaKhoa(string Id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from KHOA where MaKhoa=@makhoa";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("makhoa", Id);
                return (cmd.ExecuteNonQuery());
            }
        }
        public int XoaSinhVien(string Id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "delete from SINHVIEN where MaSinhVien=@maSinhVien";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("maSinhVien", Id);
                return (cmd.ExecuteNonQuery());
            }
        }
        public int InsertBoMon(BoMon bm)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into BOMON values(@mabomon, @tenbomon,@makhoa)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("mabomon", bm.MaBoMon);
                cmd.Parameters.AddWithValue("tenbomon", bm.TenBoMon);
                cmd.Parameters.AddWithValue("makhoa", bm.MaKhoa);
                return (cmd.ExecuteNonQuery());

            }
        }
        public Khoa ViewKhoa(string Id)
        {
            //Khoa kh = new Khoa("MK01","HTTT");
            Khoa kh = new Khoa();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from KHOA where MaKhoa=@makhoa";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("makhoa", Id);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    kh.MaKhoa=reader["MaKhoa"].ToString();
                    kh.TenKhoa = reader["TenKhoa"].ToString();
                }
            }
            return (kh);
        }
        public int TimSinhVienTheoTen(string ten)
        {
            int i = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "select * from SINHVIEN where TenSinhVien=@tensinhvien";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("tensinhvien", ten);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        i++;
                    }
                }
            }
            return i;
        }
        public List<SinhVien> LietKeNSinhVien(int n) {

            List<SinhVien> list = new List<SinhVien>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from SINHVIEN limit @sosinhvien";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                cmd.Parameters.AddWithValue("sosinhvien", n); 
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SinhVien()
                        {
                            MaSinhVien = reader["MaSinhVien"].ToString(),
                            TenSinhVien = reader["TenSinhVien"].ToString(),
                            DiemTrungBinh = Convert.ToDouble(reader["DiemTrungBinh"]),
                            MaBoMon = reader["MaBoMon"].ToString(),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        public List<SinhVien> SinhVienMax(){
            List<SinhVien> list = new List<SinhVien>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from SINHVIEN where DiemTrungBinh = (Select Max(DiemTrungBinh) from SINHVIEN)";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SinhVien()
                        {
                            MaSinhVien = reader["MaSinhVien"].ToString(),
                            TenSinhVien = reader["TenSinhVien"].ToString(),
                            DiemTrungBinh = Convert.ToDouble(reader["DiemTrungBinh"]),
                            //MaBoMon = reader["MaBoMon"].ToString(),
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        public List<BoMon> GetBoMons()
        {
            List<BoMon> list = new List<BoMon>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = "select * from BOMON";
                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new BoMon()
                        {
                            MaBoMon = reader["MaBoMon"].ToString(),
                            TenBoMon = reader["TenBoMon"].ToString(),
                            
                        });
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;
        }
        public List<object> SoSinhVienTrongBoMon()
        {
            List<object> list = new List<object>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                string str = "select b.TenBoMon, count(*) as SL from BOMON b,SINHVIEN s where b .MaBoMon = s .MaBoMon group by s.MaBoMon";

                MySqlCommand cmd = new MySqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ob = new {tenbomon= reader["TenBoMon"].ToString(), soluong= Convert.ToInt32(reader["SL"]) };
                        list.Add(ob); 
                   
                    }
                    reader.Close();
                }

                conn.Close();

            }
            return list;

        }
        public StoreContext()
        {
        }
    }
}

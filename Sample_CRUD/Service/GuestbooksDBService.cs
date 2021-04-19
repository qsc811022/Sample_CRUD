using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sample_CRUD.Models;
using Sample_CRUD.ViewModels;

namespace Sample_CRUD.Service
{
    public class GuestbooksDBService
    {
        //建立與資料庫的連線字串
        private readonly static string cnstr = ConfigurationManager.ConnectionStrings["ASP.NET.MVC"].ConnectionString;

        //建立與資料庫的連線
        private readonly SqlConnection conn = new SqlConnection(cnstr);
        
        //function 取得陣列資料方法

        public List<Guestbook> GetDataList(ForPaging Paging, string Search)
        {
            List<Guestbook> DataList = new List<Guestbook>();
            //sql語法
            //string sql = string.Empty;
            if (!string.IsNullOrWhiteSpace(Search))
            {

                SetMaxPaging(Paging,Search);

                DataList = GetAllDataList(Paging,Search);
                //
                //SetMaxPaging();

                //sql = $@"select * from Guestbooks where Name Like '%{Search}%' OR Content Like '%{Search}%' OR Reply Like '%{Search}%'";

            }
            else 
            {
              //sql = $@"Select * FROM Guestbooks;";
              SetMaxPaging(Paging);
              DataList = GetAllDataList(Paging);
            }

            //string sql = $@"Select * FROM Guestbooks;";

            //try
            //{
            //    //開啟資料庫連線
            //    conn.Open();
            //    //執行Sql指令
            //    SqlCommand cmd = new SqlCommand(sql,conn);
            //    //取得Sql資料
            //    SqlDataReader dr = cmd.ExecuteReader();
            //    while (dr.Read())
            //    {
            //        Guestbook Data = new Guestbook();
            //        Data.Id = Convert.ToInt32(dr["Id"]);
            //        Data.Name =  dr["Name"].ToString();
            //        Data.Content = dr["Content"].ToString();
            //        Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            //        if (!dr["ReplyTime"].Equals(DBNull.Value))
            //        {
            //            Data.Reply = dr["Reply"].ToString();
            //            Data.ReplyTime=Convert.ToDateTime(dr["ReplyTime"]);
            //        }
            //        DataList.Add(Data);

            //    }

            //}
            //catch(Exception e)
            //{
            //    //
            //    throw new Exception(e.Message.ToString());
            //}
            //finally
            //{
            //    conn.Close();
            //}
            return DataList;


        }

        public void SetMaxPaging(ForPaging Paging)
        {
            //計算列數
            int Row =0;
            //sql語法
            string sql = $@"Select * FROM Guestbooks;";

            try
            {
                //開啟資料庫連線
                conn.Open();
                //執行Sql指令
                SqlCommand cmd = new SqlCommand(sql, conn);
                //取得Sql資料
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Row++;
                }

            }
            catch (Exception e)
            {
                //
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }

            //計算所需的總頁數
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Row)/Paging.ItemNum));
            //重新設定正確的頁數
            Paging.SetRrightPage();


        }

        public void SetMaxPaging(ForPaging Paging,string Search)
        {
            //計算列數
            int Row = 0;
            //sql語法
            string sql = $@"select * from Guestbooks where Name Like '%{Search}%' OR Content Like '%{Search}%' OR Reply Like '%{Search}%'";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql,conn);
                
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Row++;
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Row)/Paging.ItemNum));

        }

        public List<Guestbook> GetAllDataList(ForPaging paging)
        {
            List<Guestbook> DataList = new List<Guestbook>();
            //sql 語法
            string sql = $@"select * From(select row_number() over(order by Id)As sort,* FROM Guestbooks) m Where m.sort BETWEEN {(paging.NowPage-1)* paging.ItemNum +1} AND {paging.NowPage * paging.ItemNum}";

            try
            {
                //開啟資料庫連線
                conn.Open();
                //執行Sql指令
                SqlCommand cmd = new SqlCommand(sql, conn);
                //取得Sql資料
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Guestbook Data = new Guestbook();
                    Data.Id = Convert.ToInt32(dr["Id"]);
                    Data.Name = dr["Name"].ToString();
                    Data.Content = dr["Content"].ToString();
                    Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    if (!dr["ReplyTime"].Equals(DBNull.Value))
                    {
                        Data.Reply = dr["Reply"].ToString();
                        Data.ReplyTime = Convert.ToDateTime(dr["ReplyTime"]);
                    }
                    DataList.Add(Data);

                }

            }
            catch (Exception e)
            {
                //
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }

            return DataList;

        }

        public List<Guestbook> GetAllDataList(ForPaging paging,string Search)
        {
            List<Guestbook> DataList = new List<Guestbook>();
            //sql語法
            string sql = $@"select * From(select row_number() over(order by Id)As sort,* FROM Guestbooks) m Where Name Like '%{Search}% OR Content Like '%{Search}% OR Reply LIKE '%{Search}%') m Where m.sort BETWEEN {(paging.NowPage - 1) * paging.ItemNum + 1} AND {paging.NowPage * paging.ItemNum}";

            try
            {
                //開啟資料庫連線
                conn.Open();
                //執行Sql指令
                SqlCommand cmd = new SqlCommand(sql, conn);
                //取得Sql資料
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Guestbook Data = new Guestbook();
                    Data.Id = Convert.ToInt32(dr["Id"]);
                    Data.Name = dr["Name"].ToString();
                    Data.Content = dr["Content"].ToString();
                    Data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    if (!dr["ReplyTime"].Equals(DBNull.Value))
                    {
                        Data.Reply = dr["Reply"].ToString();
                        Data.ReplyTime = Convert.ToDateTime(dr["ReplyTime"]);
                    }
                    DataList.Add(Data);

                }

            }
            catch (Exception e)
            {
                //
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
            return DataList;







        }





        //新增方法

        public void InsertGuestbooks(Guestbook newData)
        {
            //sql 新增語法
            //設定新增時間為現在

            string sql = $@"Insert into Guestbooks(Name,Content,CreateTime) VALUES('{newData.Name}','{newData.Content}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";

            try
            {
                conn.Open(); //開啟資料庫連線
                //執行Sql執行
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }



        }


        //藉由編號取得單筆資料的方法
        public Guestbook GetDataById(int Id)
        {
            Guestbook Data = new Guestbook();
            //sql語法
            string sql = $@"select * from Guestbooks Where Id = {Id}";
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                //取得sql 資料
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Data.Id = Convert.ToInt32(dr["Id"]);
                Data.Name = dr["Name"].ToString();
                Data.Content=dr["Content"].ToString();
                Data.CreateTime =  Convert.ToDateTime(dr["CreateTime"]);
                if (!string.IsNullOrEmpty(dr["Reply"].ToString()))
                {
                    Data.Reply = dr["Reply"].ToString();
                    Data.ReplyTime=Convert.ToDateTime(dr["ReplyTime"]);

                }
            }
            catch (Exception e)
            {
                //查無資料
                Data = null;
            }
            finally 
            {
                conn.Close();
            }


            return Data;


        }


        //修改留言的方法
        public void UpdateGuestbooks(Guestbook UpdateData)
        {
            //sql 修改語法
            string sql  = $@"Update Guestbooks SET Name = '{UpdateData.Name}', Content = '{UpdateData.Content}' where Id = {UpdateData.Id}";

            try
            {
                //開啟資料庫連線
                conn.Open();

                //執行sqlcommand
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            { 
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }


        }

        //回覆留言

        public void ReplyGuestbooks(Guestbook ReplyData)
        {
            //sql修改語法
            //設定回覆時間為現在
            string sql = $@"Update Guestbooks set Reply ='{ReplyData.Reply}',ReplyTime='{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}' Where Id
            ={ReplyData.Id}";

            try
            {
                conn.Open();
                //執行sql指令
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }

        }


        //修改資料判斷的方式

        public bool CheckUpdate(int Id)
        {
            //根據 Id 取得要修改的資料
            Guestbook Data = GetDataById(Id);
            //判斷並回傳
            return (Data !=null && Data.ReplyTime ==null);
        }


        //刪除留言
        public void DeleteGuestbooks(int Id)
        {
            //sql刪除語法
            string sql = $@"Delete from Guestbooks Where Id = {Id}";

            try
            {
                conn.Open();
                
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            { 
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
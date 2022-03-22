using Project.Helpers;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project.Managers
{
    public class BookContentManager
    {
        //public List<BookContentModel> GetMapList(string keyword, int pageSize, int pageIndex, out int totalRows)
        //{
        //    int skip = pageSize * (pageIndex - 1);  // 計算跳頁數
        //    if (skip < 0)
        //        skip = 0;


        //    string whereCondition = string.Empty;
        //    if (!string.IsNullOrWhiteSpace(keyword))
        //        whereCondition = " AND Title LIKE '%'+@keyword+'%' ";

        //    string connStr = ConfigHelper.GetConnectionString();
        //    string commandText =
        //        $@" SELECT TOP {pageSize} *
        //            FROM Books 
        //            WHERE 
        //                IsEnable = 'true' AND
        //                ID NOT IN 
        //                (
        //                    SELECT TOP {skip} ID
        //                    FROM Books
        //                    WHERE 
        //                        IsEnable = 'true' 
        //                        {whereCondition}
        //                    ORDER BY CreateDate DESC
        //                ) 
        //                {whereCondition}
        //            ORDER BY CreateDate DESC ";
        //    string commandCountText =
        //        $@" SELECT COUNT(ID) 
        //            FROM Books
        //            WHERE IsEnable = 'true' 
        //            {whereCondition}
        //            ";

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connStr))
        //        {
        //            using (SqlCommand command = new SqlCommand(commandText, conn))
        //            {
        //                if (!string.IsNullOrWhiteSpace(keyword))
        //                    command.Parameters.AddWithValue("@keyword", keyword);   // 參數化查詢

        //                conn.Open();
        //                SqlDataReader reader = command.ExecuteReader();
        //                List<BookContentModel> retList = new List<BookContentModel>();    // 將資料庫內容轉為自定義型別清單
        //                while (reader.Read())
        //                {
        //                    BookContentModel info = this.BuildBookContentModel(reader);
        //                    retList.Add(info);
        //                }
        //                reader.Close();

        //                // 取得總筆數
        //                command.CommandText = commandCountText;
        //                if (!string.IsNullOrWhiteSpace(keyword))
        //                {
        //                    command.Parameters.Clear();                             // 不同的查詢，必須使用不同的參數集合
        //                    command.Parameters.AddWithValue("@keyword", keyword);
        //                }
        //                totalRows = (int)command.ExecuteScalar();

        //                return retList;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog("MapContentManager.GetMapList", ex);
        //        throw;
        //    }
        //}

        // 從資料庫叫出全部的清單資料 OR 輸入書名搜尋關鍵字查詢
        public List<BookContentModel> GetAdminBookList(string keyword)
        {
            string whereCondition = string.Empty;       // 宣告一個變數 存放 要附加上去的SQL指令，指令先清空  
            if (!string.IsNullOrWhiteSpace(keyword))    // 有輸入搜尋關鍵字的話
                whereCondition = " WHERE BookName LIKE '%'+@keyword+'%' "; // 就組合好SQL指令預備著 
                                                                           // 之後再放入SQL中
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" SELECT *
                    FROM Books 
                    {whereCondition}  
                    ORDER BY Date DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                        {
                            command.Parameters.AddWithValue("@keyword", keyword);
                        }

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<BookContentModel> retList = new List<BookContentModel>();    // 將資料庫內容轉為自定義型別清單
                        while (reader.Read())
                        {
                            BookContentModel info = this.BuildBookContentModel(reader);
                            retList.Add(info);
                        }

                        return retList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookContentManager.GetAdminBookList", ex);
                throw;
            }
        }

        // 從資料庫叫出全部(IsEnableTrue)的清單資料 OR 輸入書名搜尋關鍵字查詢(IsEnableTrue)
        public List<BookContentModel> GetAdminBookListIsEnableTrue(string keyword)
        {
            string whereCondition = string.Empty;       // 宣告一個變數 存放 要附加上去的SQL指令，指令先清空  
            if (!string.IsNullOrWhiteSpace(keyword))    // 有輸入搜尋關鍵字的話
                whereCondition = " AND BookName LIKE '%'+@keyword+'%' "; // 就組合好SQL指令預備著 
                                                                         // 之後再放入SQL中
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" SELECT *
                    FROM Books
                    WHERE 
                        IsEnable = 'True' 
                    {whereCondition}
                    ORDER BY Date DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                        {
                            command.Parameters.AddWithValue("@keyword", keyword);
                        }

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<BookContentModel> retList = new List<BookContentModel>();    // 將資料庫內容轉為自定義型別清單
                        while (reader.Read())
                        {
                            BookContentModel info = this.BuildBookContentModel(reader);
                            retList.Add(info);
                        }

                        return retList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookContentManager.GetAdminBookListIsEnableTrue", ex);
                throw;
            }
        }

        // 從資料庫叫出全部(IsEnableFalse)的清單資料 OR 輸入書名搜尋關鍵字查詢(IsEnableFalse)
        public List<BookContentModel> GetAdminBookListIsEnableFalse(string keyword)
        {
            string whereCondition = string.Empty;       // 宣告一個變數 存放 要附加上去的SQL指令，指令先清空  
            if (!string.IsNullOrWhiteSpace(keyword))    // 有輸入搜尋關鍵字的話
                whereCondition = " AND BookName LIKE '%'+@keyword+'%' "; // 就組合好SQL指令預備著 
                                                                         // 之後再放入SQL中
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" SELECT *
                    FROM Books 
                    WHERE 
                        IsEnable = 'False' 
                    {whereCondition} 
                    ORDER BY Date DESC ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                        {
                            command.Parameters.AddWithValue("@keyword", keyword);
                        }

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<BookContentModel> retList = new List<BookContentModel>();    // 將資料庫內容轉為自定義型別清單
                        while (reader.Read())
                        {
                            BookContentModel info = this.BuildBookContentModel(reader);
                            retList.Add(info);
                        }

                        return retList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookContentManager.GetAdminBookListIsEnableFalse", ex);
                throw;
            }
        }

        // 去資料庫抓圖片的存檔路徑(圖片真正存放的地方)，再去圖片存檔的 資料夾 中，做 圖檔的刪除
        // 傳入 checkbox被選取 的 刪除id清單，跑for迴圈建立對應數量的 @參數 後，一一將ID放入SQL指令中查詢   
        public List<BookContentModel> GetBookList(List<Guid> ids)
        {
            if (ids == null || ids.Count == 0)
                return new List<BookContentModel>();

            List<string> idTextList = new List<string>();
            for (int i = 0; i < ids.Count; i++)         // 跑for迴圈建立對應數量的 @參數
            {
                idTextList.Add("@id" + i);
            }
            string whereCondition = string.Join(", ", idTextList);  // 組 要放入SQL指令 中的 字串 @id1, @id2, @id3, etc...


            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" SELECT *
                    FROM Books
                    WHERE BookID IN ({whereCondition}) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        for (int i = 0; i < ids.Count; i++)
                        {
                            command.Parameters.AddWithValue("@id" + i, ids[i]);
                        }


                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        List<BookContentModel> retList = new List<BookContentModel>();    // 將資料庫內容轉為自定義型別清單
                        while (reader.Read())
                        {
                            BookContentModel info = this.BuildBookContentModel(reader);
                            retList.Add(info);
                        }

                        return retList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookContentManager.GetBookList", ex);
                throw;
            }
        }

        private BookContentModel BuildBookContentModel(SqlDataReader reader)
        {
            var model = new BookContentModel()
            {
                BookID = (Guid)reader["BookID"],
                UserID = (Guid)reader["UserID"],
                //LabelID = (Guid)reader["LabelID"],

                CategoryName = reader["CategoryName"] as string,
                AuthorName = reader["AuthorName"] as string,
                BookName = reader["BookName"] as string,
                Description = reader["Description"] as string,
                Image = reader["Image"] as string,
                IsEnable = (bool)reader["IsEnable"],
                Date = (DateTime)reader["Date"],
                EndDate = /*(DateTime)*/reader["EndDate"] as DateTime?,

                //CreateDate = (DateTime)reader["CreateDate"],
                //CreateUser = (Guid)reader["CreateUser"],
                //UpdateDate = reader["UpdateDate"] as DateTime?,
                //UpdateUser = reader["UpdateUser"] as Guid?,
                //DeleteDate = reader["DeleteDate"] as DateTime?,
                //DeleteUser = reader["DeleteUser"] as Guid?
            };

            return model;
        }

        // 編輯書籍前 去資料庫找出 相對應BookID 的書籍資訊
        public BookContentModel GetBook(Guid id)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" SELECT *
                    FROM Books 
                    WHERE BookID = @BookID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@BookID", id);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        BookContentModel model = new BookContentModel();
                        if (reader.Read())
                        {
                            model = this.BuildBookContentModel(reader);
                        }

                        return model;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookContentManager.GetBook", ex);
                throw;
            }
        }

        // 新增 OR 編輯書籍前 去資料庫找出 相對應BookName 的書籍資訊
        public BookContentModel CompareBook(string bookname)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" SELECT *
                    FROM Books 
                    WHERE BookName = @BookName ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@BookName", bookname);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        BookContentModel model = new BookContentModel();
                        if (reader.Read())
                        {
                            model = this.BuildBookContentModel(reader);
                        }

                        return model;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookContentManager.CompareBook", ex);
                throw;
            }
        }

        // 新增書籍(書名不能重複? --> 好像可以)
        public void CreateBookContent(BookContentModel model)
        {
            // 1. 判斷資料庫是否有相同的 BookName
            //if (this.CompareBook(model.BookName) != null)
            //    throw new Exception("已存在相同的書名");   // throw: 不計一切代價，阻止接下來的事情發生

            // 2. 新增書籍
            string connStr = ConfigHelper.GetConnectionString();
            // LabelID, @LabelID,
            string commandText =
               @"   INSERT INTO Books
                        (BookID, UserID, CategoryName, AuthorName, BookName, Description, Date, EndDate, Image, IsEnable)
                    VALUES
                        (@BookID, @UserID, @CategoryName, @AuthorName, @BookName, @Description, @Date, @EndDate, @Image, @IsEnable) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        model.BookID = Guid.NewGuid(); // 傳址 所以外部呼叫的是同一個 並沒有問題

                        command.Parameters.AddWithValue("@BookID", model.BookID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        //command.Parameters.AddWithValue("@LabelID", model.LabelID);

                        command.Parameters.AddWithValue("@CategoryName", model.CategoryName);
                        command.Parameters.AddWithValue("@AuthorName", model.AuthorName);
                        command.Parameters.AddWithValue("@BookName", model.BookName);
                        command.Parameters.AddWithValue("@Description", model.Description);
                        command.Parameters.AddWithValue("@Date", model.Date);
                        command.Parameters.AddWithValue("@EndDate", model.EndDate);
                        command.Parameters.AddWithValue("@Image", model.Image);
                        command.Parameters.AddWithValue("@IsEnable", model.IsEnable);
                        //command.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                        //command.Parameters.AddWithValue("@CreateUser", cUserID);

                        conn.Open();
                        command.ExecuteNonQuery(); // 執行 並 回報 受影響的資料列數目
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookContentManager.CreateBookContent", ex);
                throw;
            }
        }

        // 編輯書籍(不允許更改書籍代碼)
        public void UpdateBookContent(BookContentModel model)
        {
            // 1. 判斷資料庫是否有相同的 BookID
            if (this.GetBook(model.BookID) == null)
                throw new Exception("書籍代碼不存在：" + model.BookID);   // throw: 不計一切代價，阻止接下來的事情發生

            // 2. 編輯書籍
            string connStr = ConfigHelper.GetConnectionString();
            // LabelID, @LabelID,
            string commandText =
               @"  UPDATE Books
                    SET 
                        UserID = @UserID,
                        CategoryName = @CategoryName,
                        AuthorName = @AuthorName,
                        BookName = @BookName,
                        Description = @Description,
                        Date = @Date,
                        EndDate = @EndDate,
                        Image = @Image,
                        IsEnable = @IsEnable
                    WHERE
                        BookID = @BookID ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@BookID", model.BookID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        //command.Parameters.AddWithValue("@LabelID", model.LabelID);

                        command.Parameters.AddWithValue("@CategoryName", model.CategoryName);
                        command.Parameters.AddWithValue("@AuthorName", model.AuthorName);
                        command.Parameters.AddWithValue("@BookName", model.BookName);
                        command.Parameters.AddWithValue("@Description", model.Description);
                        command.Parameters.AddWithValue("@Date", model.Date);
                        command.Parameters.AddWithValue("@EndDate", model.EndDate);
                        command.Parameters.AddWithValue("@Image", model.Image);
                        command.Parameters.AddWithValue("@IsEnable", model.IsEnable);
                        //command.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                        //command.Parameters.AddWithValue("@CreateUser", cUserID);

                        conn.Open();
                        command.ExecuteNonQuery(); // 執行 並 回報 受影響的資料列數目
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookContentManager.UpdateBookContent", ex);
                throw;
            }
        }

        // 做成軟性刪除 --> IsEnable值 改為 false
        // 傳入 checkbox被選取 的 刪除id清單，跑for迴圈建立對應數量的 @參數 後，一一將ID放入SQL指令中(修改)刪除
        public void DeleteBookContent(List<Guid> idList)
        {
            bool SoftDelete = false;  // 軟性刪除: 宣告一個變數 存放 IsEnable值(false)

            // 1. 判斷是否有傳入 id
            if (idList == null || idList.Count == 0)
                throw new Exception("需指定 id");

            List<string> idTextList = new List<string>();
            for (int i = 0; i < idList.Count; i++)         // 跑for迴圈建立對應數量的 @參數
            {
                idTextList.Add("@id" + i);
            }
            string whereCondition = string.Join(", ", idTextList);  // 組 要放入SQL指令 中的 字串 @id1, @id2, @id3, etc...

            // 2. 刪除資料
            string connStr = ConfigHelper.GetConnectionString();
            //string commandText =
            //   $@"  DELETE Books
            //        WHERE BookID IN ({whereCondition}); ";
            string commandText =
                $@"  UPDATE Books
                    SET 
                        IsEnable = @isEnable
                    WHERE
                        BookID = {whereCondition} ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@isEnable", SoftDelete);

                        for (int i = 0; i < idList.Count; i++)
                        {
                            command.Parameters.AddWithValue("@id" + i, idList[i]);
                        }

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookContentManager.DeleteBookContent", ex);
                throw;
            }
        }
    }
}
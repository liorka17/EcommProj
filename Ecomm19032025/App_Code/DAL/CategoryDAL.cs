using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using BLL;
using DATA;

namespace DAL
{
    public class CategoryDAL
    {

        public static Category GetById(int Cid)
        {
            DbContext Db = new DbContext(); // יצירת אובייקט מסוג דאטה בייס
            string sql = $"SELECT * FROM T_Category WHERE Cid = {Cid}";// שאילתא לשליפת קטגוריה לפי קוד
            DataTable Dt = Db.Execute(sql);// שליפת נתונים מהמסד
            Category Tmp = null;// יצירת אובייקט מסוג קטגוריה

            if (Dt.Rows.Count > 0)
            {
                Tmp = new Category()// יצירת אובייקט מסוג קטגוריה
                {
                    Cid = (int)Dt.Rows[0]["Cid"],// קוד קטגוריה
                    Cname = (string)Dt.Rows[0]["Cname"],// שם קטגוריה
                    ParentCid = (int)Dt.Rows[0]["ParentCid"],// קוד קטגוריה עליונה
                    Status = (int)Dt.Rows[0]["Status"]// סטטוס קטגוריה                   
                };
            }
            else
            {
                Tmp = new Category(); // מחזיר אובייקט ריק במקרה ולא נמצא
            }

            Db.Close(); // סגירת החיבור
            return Tmp;// מחזירה את האובייקט
        }

        public static List<Category> GetAll() // מחזירה את כל הקטגוריות
        {
            DbContext Db = new DbContext(); // יצירת אובייקט מסוג דאטה בייס
            string sql = $"SELECT * FROM T_Category";// שאילתא לשליפת כל הקטגוריות
            DataTable Dt = Db.Execute(sql); // שליפת נתונים מהמסד

            List<Category> lst = new List<Category>();

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Category Tmp = new Category();// יצירת אובייקט מסוג קטגוריה

                Tmp.Cid = (int)Dt.Rows[i]["Cid"];// קוד קטגוריה
                Tmp.Cname = (string)Dt.Rows[i]["Cname"];// שם קטגוריה
                Tmp.ParentCid = (int)Dt.Rows[i]["ParentCid"];// קוד קטגוריה עליונה
                Tmp.Status = (int)Dt.Rows[i]["Status"];// סטטוס קטגוריה


                lst.Add(Tmp);// הוספת האובייקט לרשימה
            }
            Db.Close(); // סגירת החיבור למסד
            return lst; // החזרת הרשימה
        }

        public static int Save(Category Tmp)
        {
            DbContext Db = new DbContext();// יצירת אובייקט מסוג דאטה בייס
            string sql;

            if (Tmp.Cid == -1)// אם קוד הקטגוריה שווה ל-1 אז מדובר בקטגוריה חדשה
            {
                sql = $"INSERT INTO T_Category (Cname, ParentCid, Status) VALUES (N'{Tmp.Cname}', {Tmp.ParentCid}, {Tmp.Status})";// שאילתא להוספת קטגוריה חדשה
            }
            else
            {
                sql = $"UPDATE T_Category SET ";// שאילתא לעדכון קטגוריה קיימת
                sql += $"Cname = N'{Tmp.Cname}', ";// שם קטגוריה
                sql += $"ParentCid = {Tmp.ParentCid}, ";// קוד קטגוריה עליונה
                sql += $"Status = {Tmp.Status} ";// סטטוס קטגוריה
                sql += $"WHERE Cid = {Tmp.Cid}";// קוד קטגוריה
            }

            int i = Db.ExecuteNonQuery(sql);// מחזירה מספר שורות שהוסרו מהמסד נתונים
            Db.Close();// סגירת החיבור למסד
            return i;// מחזירה את מספר השורות שהוסרו
        }


        public static int DeleteById(int Cid)//מוחקת את הקטגוריה לפי קוד
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג דאטה בייס
            string sql = $"DELETE FROM T_Category Where Cid={Cid}";//משפט השאילתא
            int i = Db.ExecuteNonQuery(sql);//מחזירה מספר שורות שהוסרו מהמסד נתונים
            Db.Close();//סגירת החיבור לבסיס הנתונים
            return i;//מחזירה את מספר השורות שהוסרו
        }
    }
}
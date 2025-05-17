using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BLL;
using DATA;

namespace DAL
{
    public class OrdersDAL
    {

        public static Orders GetById(int OrderID)
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג דאטה בייס
            string sql = $"SELECT * FROM T_Orders Where OrderID={OrderID}";//שאילתא לשליפת הזמנה לפי קוד
            DataTable Dt = Db.Execute(sql);//שליפת נתונים מהמסד
            Orders Tmp = null;//יצירת אובייקט מסוג הזמנה
            if (Dt.Rows.Count > 0)
            {
                Tmp = new Orders()//יצירת אובייקט מסוג הזמנה
                {
                    OrderId = int.Parse(Dt.Rows[0]["OrderId"] + " "),//קוד ההזמנה
                    Uid = int.Parse(Dt.Rows[0]["Uid"] + " "),//קוד משתמש
                    TotalPrice = Convert.ToSingle(Dt.Rows[0]["TotalPrice"]),//סכום כולל
                    TotalAmount = Convert.ToSingle(Dt.Rows[0]["TotalAmount"]),//כמות כוללת
                    Status = (string)Dt.Rows[0]["Status"],//סטטוס ההזמנה
                };
                Db.Close();//סגירת החיבור לבסיס הנתונים
                return Tmp;//מחזירה את האובייקט
            }
            return new Orders();//מחזירה אובייקט ריק במקרה ולא נמצא
        }

        public static List<Orders> GetAll() // מחזירה את כל ההזמנות
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג דאטה בייס
            string sql = "SELECT * FROM T_Orders";//שאילתא לשליפת כל ההזמנות
            DataTable Dt = Db.Execute(sql);//שליפת נתונים מהמסד
            List<Orders> lst = new List<Orders>();//יצירת רשימה חדשה של הזמנות

            //  בלולאה אנו מבצעים המרות כדי להתאים את סוגי הנתונים

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Orders Tmp = new Orders();//יצירת אובייקט מסוג הזמנה
                Tmp = new Orders()//יצירת אובייקט מסוג הזמנה
                {
                    // ההמרה למספר שלם נועדה לשדות כמו קוד ההזמנה ומספר המשתמש, כיוון שהנתונים שמתקבלים מהטבלה
                    // הם מסוג כללי ויש להמיר אותם למספרים שלמים לפני השמה למשתנה מתאים
                    // ההמרה למספר עשרוני נועדה לשדות כמו סכום כולל וכמות כוללת, כיוון שהם אמורים להכיל ערכים עם נקודה עשרונית
                    // ההמרה למחרוזת מבוצעת עבור שדות טקסטואליים כמו סטטוס, כדי לוודא שהמידע נשמר כמחרוזת תקינה
                    // המרות אלו הכרחיות על מנת שנוכל לעבוד עם הנתונים בצורה תקינה בתוך התוכנית

                    OrderId = Convert.ToInt32(Dt.Rows[i]["OrderId"]),//קוד ההזמנה
                    Uid = Convert.ToInt32(Dt.Rows[i]["Uid"]),////קוד משתמש
                    TotalPrice = Convert.ToSingle(Dt.Rows[i]["TotalPrice"]),//סכום כולל
                    TotalAmount = Convert.ToSingle(Dt.Rows[i]["TotalAmount"]),//כמות כוללת
                    Status = Dt.Rows[i]["Status"].ToString(),//סטטוס ההזמנה

                };
                lst.Add(Tmp);//הוספת האובייקט לרשימה
            }

            Db.Close(); // סגירת החיבור לבסיס הנתונים
            return lst;
        }


        public static int Save(Orders Tmp) 
        {
            DbContext Db = new DbContext(); //יצירת אובייקט מסוג דאטה בייס
            string sql;//מחרוזת שאילתא

            if (Tmp.OrderId == -1) //אם קוד ההזמנה שווה ל-1 אז מדובר בהזמנה חדשה
            {
                sql = $"INSERT INTO T_Orders(Uid, TotalPrice, TotalAmount, Status)";//שאילתא להוספת הזמנה חדשה
                sql += $" VALUES('{Tmp.Uid}', '{Tmp.TotalPrice}', '{Tmp.TotalAmount}', N'{Tmp.Status}')";//מוסיפה את הערכים לשאילתא
            }
            else
            {
                sql = $"UPDATE T_Orders SET ";//שאילתא לעדכון הזמנה קיימת
                sql += $"Uid = '{Tmp.Uid}', ";//מוסיפה את הערכים לשאילתא
                sql += $"TotalPrice = '{Tmp.TotalPrice}', ";//מוסיפה את הערכים לשאילתא
                sql += $"TotalAmount = '{Tmp.TotalAmount}', ";//מוסיפה את הערכים לשאילתא
                sql += $"Status = N'{Tmp.Status}' ";//מוסיפה את הערכים לשאילתא
                sql += $"WHERE OrderId = {Tmp.OrderId}";//מוסיפה את הערכים לשאילתא
            }

            int i = Db.ExecuteNonQuery(sql); //מחזירה מספר שורות שהוסרו מהמסד נתונים

            if (Tmp.OrderId == -1) //אם קוד ההזמנה שווה ל-1 אז מדובר בהזמנה חדשה
            {
                sql = $"SELECT MAX(OrderId) FROM T_Orders WHERE Uid = {Tmp.Uid}";//שאילתא לשליפת קוד ההזמנה
                Tmp.OrderId = (int)Db.ExecuteScalar(sql);//מחזירה את קוד ההזמנה
            }
            Db.Close(); //סגירת החיבור לבסיס הנתונים
            return i; //מחזירה את מספר השורות שהוסרו מהמסד נתונים
        }

        public static int DeleteById(int OrderId)//מוחקת את ההזמנה לפי קוד
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג דאטה בייס
            string sql = $"DELETE FROM T_Orders Where OrderId={OrderId}";//שאילתא למחיקת הזמנה לפי קוד
            int i = Db.ExecuteNonQuery(sql);//מחזירה מספר שורות שהוסרו מהמסד נתונים
            Db.Close();//סגירת החיבור לבסיס הנתונים
            return i;//מחזירה את מספר השורות שהוסרו מהמסד נתונים
        }
    }
}
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;//קישוור לספריית השאילתות והעבודה מול מסד נתונים
using System.Data;//קישור לספריית הנתונים לעבודה מול מבנה נתונים    
using DATA;

namespace DAL
{
    public class ProductDAL
    {

        public static List<Product> GetAll()
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג דאטה בייס
            string query = "SELECT * FROM T_Product";//שאילתא לשליפת כל המוצרים
            DataTable dt = Db.Execute(query);//שליפת נתונים מהמסד
            List<Product> lst = new List<Product>();//יצירת רשימה חדשה של מוצרים
            for (int i = 0; i < dt.Rows.Count; i++)//לולאת for כדי לעבור על כל השורות בטבלה
            {
                Product tmp = new Product()//יצירת אובייקט מסוג מוצר
                {
                    // - ההמרה למספר שלם (אינט32) מתבצעת עבור שדות כמו מזהה מוצר (פיאיידי), קוד קטגוריה (סיאיידי) וסטטוס,
                    //   כיוון שהם נשמרים כמספרים שלמים במסד ויש להתאים אותם למשתנים במסוג תואם
                    // - ההמרה למספר עשרוני (סינגל) מתבצעת עבור מחיר המוצר ),
                    //   כי ערך זה יכול להכיל נקודה עשרונית ויש להתאים אותו למשתנה מתאים לקלוט מחירים.
                    // - ההמרה למחרוזת (טו סטרינג) משמשת עבור שדות טקסטואליים כמו שם המוצר (פיניימ),
                    //   תיאור (פידסקריפשן) ושם הקובץ של התמונה (פיקניימ), כדי לוודא שהמידע נאסף כטקסט בצורה תקינה.
                    // כל ההמרות האלו נדרשות כיוון שהנתונים שנשלפים מהמסד הם מסוג כללי,
                    // ויש צורך להמיר אותם לסוגים המתאימים לצורך שימוש תקני באובייקט של מוצר.

                    Pid = Convert.ToInt32(dt.Rows[i]["Pid"]),//קוד מוצר
                    Pname = dt.Rows[i]["Pname"].ToString(),//שם המוצר
                    Pdesc = dt.Rows[i]["Pdesc"].ToString(),//תיאור המוצר
                    Price = Convert.ToSingle(dt.Rows[i]["Price"]),//מחיר המוצר
                    Picname = dt.Rows[i]["Picname"].ToString(),//שם התמונה
                    Cid = Convert.ToInt32(dt.Rows[i]["Cid"]),//קטגוריה
                    Status = Convert.ToInt32(dt.Rows[i]["Status"])//סטטוס המוצר

                };
                lst.Add(tmp);//הוספת האובייקט לרשימה
            }
            Db.Close();//סגירת החיבור לבסיס הנתונים
            return lst;//מחזירה את הרשימה של המוצרים
        }

        public static Product GetById(int Pid)
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג דאטה בייס
            string query = $"SELECT * FROM T_Product WHERE Pid = {Pid}";//שאילתא לשליפת מוצר לפי קוד
            DataTable dt = Db.Execute(query);//שליפת נתונים מהמסד
            Product tmp = new Product();//יצירת אובייקט מסוג מוצר

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tmp = new Product()//יצירת אובייקט מסוג מוצר
                {
                    Pid = Convert.ToInt32(dt.Rows[i]["Pid"]),//קוד מוצר
                    Pname = dt.Rows[i]["Pname"].ToString(),//שם המוצר
                    Pdesc = dt.Rows[i]["Pdesc"].ToString(),//תיאור המוצר
                    Price = Convert.ToSingle(dt.Rows[i]["Price"]),//מחיר המוצר
                    Picname = dt.Rows[i]["Picname"].ToString(),//שם התמונה
                    Cid = Convert.ToInt32(dt.Rows[i]["Cid"]),//קטגוריה
                    Status = Convert.ToInt32(dt.Rows[i]["Status"])//סטטוס המוצר
                };
            }
            Db.Close();//סגירת החיבור לבסיס הנתונים
            return tmp;//מחזירה את האובייקט
        }

        public static int Save(Product Tmp)
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג דאטה בייס
            string sql;//משפט השאילתא

            if (Tmp.Pid == -1)//אם קוד המוצר שווה ל-1 אז מדובר במוצר חדש
            {
                sql = $"INSERT INTO T_Product(Pname, Pdesc, Price, Cid, PicName, Status)";//שאילתא להוספת מוצר חדש
                sql += $" VALUES(N'{Tmp.Pname}', N'{Tmp.Pdesc}', {Tmp.Price}, {Tmp.Cid}, N'{Tmp.Picname}', {Tmp.Status})";//מוסיפה את הערכים לשאילתא
            }
            else
            {
                sql = $"UPDATE T_Product SET " +//שאילתא לעדכון מוצר קיים
                      $"Pname=N'{Tmp.Pname}', " +//שם המוצר
                      $"Pdesc=N'{Tmp.Pdesc}', " +//תיאור המוצר
                      $"Price={Tmp.Price}, " +//מחיר המוצר
                      $"Cid={Tmp.Cid}, " +//קטגוריה
                      $"PicName=N'{Tmp.Picname}', " +//שם התמונה
                      $"Status={Tmp.Status} " +///סטטוס המוצר
                      $"WHERE Pid={Tmp.Pid}";//מוסיפה את הערכים לשאילתא
            }

            int i = Db.ExecuteNonQuery(sql);//מחזירה מספר שורות שהוסרו מהמסד נתונים

            if (Tmp.Pid == -1)//אם קוד המוצר שווה ל-1 אז מדובר במוצר חדש
            {
                sql = $"SELECT MAX(Pid) FROM T_Product WHERE Pname = N'{Tmp.Pname}'";//שאילתא לשליפת קוד המוצר
                Tmp.Pid = (int)Db.ExecuteScalar(sql);//מחזירה את קוד המוצר
            }

            Db.Close();//סגירת החיבור לבסיס הנתונים
            return i;//מחזירה את מספר השורות שהוסרו מהמסד נתונים
        }


        public static int DeleteById(int Pid)//מוחקת את המוצר לפי קוד
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג דאטה בייס
            string sql = $"DELETE FROM T_Product Where Pid={Pid}";//משפט השאילתא
            int i = Db.ExecuteNonQuery(sql);//מחזירה מספר שורות שהוסרו מהמסד נתונים
            Db.Close();//סגירת החיבור לבסיס הנתונים
            return i;//מחזירה את המוצר שנמחק
        }
    }
}

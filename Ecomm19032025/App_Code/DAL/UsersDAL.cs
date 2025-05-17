using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    public class UsersDAL
    {

        public static Users GetById(int Uid)
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג דאטה בייס
            string sql = $"SELECT * FROM T_Users Where Uid={Uid}";//שאילתא שמחזירה את היוזר לפי קוד
            DataTable Dt = Db.Execute(sql);//מחזירה את היוזר לפי קוד
            Users Tmp = null;//יצירת אובייקט מסוג יוזר
            if (Dt.Rows.Count > 0)//אם יש שורות בטבלה
            {
                Tmp = new Users()//יצירת אובייקט מסוג יוזר ומילוי השדות שלו עם הערכים שנשלפו ממסד הנתונים
                {
                    // בקטע הזה אנו טוענים נתוני משתמש מטבלה במסד ומכניסים אותם לשדות מתאימים באובייקט:
                    // - המרה למספר מתבצעת על שדות כמו מזהה משתמש וסטטוס, כדי לאפשר עבודה עם מספרים בקוד (לדוגמה להשוואה או שמירה).
                    //   עושים זאת על ידי הפיכת הנתון למחרוזת ואז למספר – כדי למנוע שגיאות במקרה שהערך ריק או חסר.
                    // - שדות של טקסט כמו שם מלא, סיסמה, דוא"ל, טלפון וכתובת – מוכנסים כמו שהם, לאחר שווידאנו שהם מחרוזות.
                    //   פעולה זו מאפשרת לקלוט את הערכים בדיוק כפי שהם מופיעים במסד, ולהכניס אותם למשתנה מתאים בקוד.

                    Uid = int.Parse(Dt.Rows[0]["Uid"]+""),//קוד משתמש
                    FullName = (string)Dt.Rows[0]["FullName"],//שם מלא
                    Pass = (string)Dt.Rows[0]["Pass"],//סיסמא
                    Email = (string)Dt.Rows[0]["Email"],//אימייל
                    Phone = (string)Dt.Rows[0]["Phone"],//טלפון
                    Adress = (string)Dt.Rows[0]["Adress"],//כתובת
                    Status = int.Parse(Dt.Rows[0]["Status"] + "")//סטטוס 

                };
                Db.Close();//סגירת החיבור לבסיס הנתונים
                return Tmp;//מחזירה את היוזר שנמצא
            }
            return new Users();//מחזירה אובייקט יוזר ריק
        }

        public static List<Users> GetAll()//מחזירה את כל היוזרים
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג דאטה בייס
            string sql = $"SELECT * FROM T_Users";//שאילתא שמחזירה את כל היוזרים
            DataTable Dt = Db.Execute(sql);//מחזירה את כל היוזרים
            List<Users> lst = new List<Users>();//יצירת רשימה חדשה של יוזרים
            for (int i = 0; i < Dt.Rows.Count; i++)//עובר על כל השורות בטבלה
            {
                Users Tmp = new Users();//יצירת אובייקט מסוג יוזר
                Tmp = new Users()//יצירת אובייקט מסוג יוזר ומילוי השדות שלו עם הערכים שנשלפו ממסד הנתונים
                {
                    // בקטע הזה אנו קוראים נתונים ממסד הנתונים ומכניסים אותם לאובייקט משתמש
                    // - כל שדה מהשורה הנוכחית של הטבלה מועתק למשתנה תואם בתוך האובייקט
                    // - שדות מספריים כמו מזהה משתמש וסטטוס מומרצים ממחרוזת למספר שלם
                    //   עושים זאת על ידי הוספת רווח ריק ("" +) כדי למנוע שגיאה במקרה של ערך חסר
                    // - שדות כמו שם מלא, סיסמה, אימייל, טלפון וכתובת מומרים ישירות למחרוזת

                    Uid = int.Parse(Dt.Rows[i]["Uid"]+""),//קוד משתמש
                    FullName = (string)Dt.Rows[i]["FullName"],//שם מלא
                    Pass = (string)Dt.Rows[i]["Pass"],//סיסמא
                    Email = (string)Dt.Rows[i]["Email"],//אימייל
                    Phone = (string)Dt.Rows[i]["Phone"],//טלפון
                    Adress = (string)Dt.Rows[i]["Adress"],//כתובת
                    Status = int.Parse(Dt.Rows[i]["Status"] + "")//סטטוס

                };
                lst.Add(Tmp);//הוספת היוזר לרשימה
            }
            Db.Close();//סגירת החיבור לבסיס הנתונים
            return lst;//מחזירה את היוזרים
        }

        public static int Save(Users Tmp)
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג דאטה בייס
            string sql;//מחרוזת שאילתא

            if (Tmp.Uid == -1)//אם קוד המשתמש הוא -1 אז זה אומר שהמשתמש חדש
            {
                sql = $"INSERT INTO T_Users (FullName, Pass, Email, Phone, Adress, Status) " +
                      $"VALUES (N'{Tmp.FullName}', N'{Tmp.Pass}', N'{Tmp.Email}', N'{Tmp.Phone}', N'{Tmp.Adress}', {Tmp.Status})";//שאילתא להוספת משתמש חדש
            }
            else
            {
                sql = $"UPDATE T_Users SET " +//שאילתא לעדכון משתמש קיים
                      $"FullName=N'{Tmp.FullName}', " +//שם מלא
                      $"Pass=N'{Tmp.Pass}', " +//סיסמא
                      $"Email=N'{Tmp.Email}', " +//אימייל
                      $"Phone=N'{Tmp.Phone}', " +//טלפון
                      $"Adress=N'{Tmp.Adress}', " +//כתובת
                      $"Status={Tmp.Status} " +//סטטוס
                      $"WHERE Uid={Tmp.Uid}";//קוד משתמש
            }
            int i = Db.ExecuteNonQuery(sql);//מחזירה מספר שורות שהוסרו מהמסד נתונים

            if (Tmp.Uid == -1)//אם קוד המשתמש הוא -1 אז זה אומר שהמשתמש חדש
            {
                sql = $"SELECT MAX(Uid) FROM T_Users WHERE FullName = N'{Tmp.FullName}'";
                Tmp.Uid = (int)Db.ExecuteScalar(sql);
            }
            Db.Close();//סגירת החיבור לבסיס הנתונים
            return i;//מחזירה את מספר השורות שהוסרו מהמסד נתונים
        }

        public static int DeleteById(int Uid)//מוחקת את היוזר לפי קוד
        {
            DbContext Db = new DbContext();//יצירת אובייקט מסוג דאטה בייס
            string sql = $"DELETE FROM T_Users WHERE Uid={Uid}";
            int i = Db.ExecuteNonQuery(sql);//מחזירה מספר שורות שהוסרו מהמסד נתונים
            Db.Close();//סגירת החיבור לבסיס הנתונים
            return i;//מחזירה את היוזר שנמחק
        }
    }
}
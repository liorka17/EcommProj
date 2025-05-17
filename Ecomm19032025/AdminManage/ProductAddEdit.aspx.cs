﻿using BLL; // שימוש בשכבת הלוגיקה העסקית
using System; // שימוש בפונקציות כלליות של .NET
using System.Collections.Generic; // לעבודה עם רשימות
using System.Data.SqlClient;
using System.Linq; // לעבודה עם LINQ
using System.Web; // לעבודה עם Web Forms
using System.Web.UI; // לעבודה עם עמודי ASP.NET
using System.Web.UI.WebControls; // לעבודה עם פקדי Web (כמו DropDownList)

namespace Ecomm19032025.AdminManage // מרחב שמות של אזור הניהול באתר
{
    public partial class ProductAddEdit : System.Web.UI.Page // מחלקה שמייצגת את העמוד ProductAddEdit
    {
        protected void Page_Load(object sender, EventArgs e) // מתבצע בכל טעינת עמוד
        {
            // בעת טעינת העמוד של המוצר, קבל פרטים עם קוד המוצר
            // נבדוק אם קיים מוצר עם הפרמטר
            // במידה ולא נמצא המוצר או שנשלח פרמטר חריג - נטפל בהתאם
            // נרצה להציג את השדות עם ערכים קיימים, או שהמערכת תציג ערכים ריקים (למוצר חדש)
            // במידה ומדובר במוצר קיים, נשים בשדות את המידע הקיים
            // במידה ומדובר במוצר חדש, נוסיף שורה חדשה בטבלת המוצרים

            if (!IsPostBack) // התנאי מתבצע רק בטעינה הראשונה ולא אחרי לחיצה על כפתור
            {
                string Pid = Request["Pid"] + ""; // מקבל את מזהה המוצר מהשורת כתובת אם יש
                Product p = null; // מגדיר משתנה שיחזיק את המוצר

                if (!string.IsNullOrEmpty(Pid)) // אם יש מזהה מוצר בכתובת
                {
                    p = Product.GetById(int.Parse(Pid)); // מביא את המוצר לפי מזהה
                }

                // קודם כל ממלא את רשימת הקטגוריות
                List<Category> LstCat = Category.GetAll(); // מביא את כל הקטגוריות מהמסד
                DDLCategory.DataSource = LstCat; // קובע את המקור נתונים של ה-DropDown
                DDLCategory.DataValueField = "Cid"; // קובע את השדה שישמש כערך
                DDLCategory.DataTextField = "Cname"; // קובע את השדה שיוצג למשתמש
                DDLCategory.DataBind(); // טוען את הנתונים לרשימה

                if (p != null) // אם נמצא מוצר לא חדש
                {
                    TxtPname.Text = p.Pname; // ממלא את שם המוצר
                    TxtPdesc.Text = p.Pdesc; // ממלא את תיאור המוצר
                    TxtPrice.Text = p.Price + ""; // ממלא את המחיר
                    TxtPicname.Text = p.Picname; // ממלא את שם הקובץ של התמונה
                    DDLStatus.SelectedValue = p.Status + ""; // ממלא את הסטטוס של המוצר
                    HidPid.Value = p.Pid + ""; // שם את מזהה המוצר בשדה מוסתר

                    // אם הקטגוריה קיימת ברשימה, קובעת אותה כבחירה
                    if (DDLCategory.Items.FindByValue(p.Cid + "") != null)
                    {
                        DDLCategory.SelectedValue = p.Cid + ""; // בוחר את הקטגוריה של המוצר
                    }
                }
                else
                {
                    HidPid.Value = "-1";// אם המוצר לא קיים, שים ערך של -1 בשדה המוסתר
                    DDLStatus.SelectedValue = "1"; // ברירת מחדל לסטטוס פעיל

                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {   
            string x = GlobFunc.GetRndStr(8); // מייצרת מחרוזת אקראית באורך 8 תווים
            string Picname = " "; // משתנה שיכיל את שם התמונה

            if (UplPic.HasFile)
            {
                // יצירת שם קובץ רנדומלי עם סיומת מקורית (למשל .jpg)
                string FileName = GlobFunc.GetRndStr(6);// מייצרת שם קובץ אקראי באורך 8 תווים
                int ind = UplPic.FileName.LastIndexOf('.');// מחפש את האינדקס של הנקודה האחרונה בשם הקובץ
                string Ext = UplPic.FileName.Substring(ind); // לדוגמה: ".jpg"
                Picname = FileName + Ext;
                UplPic.SaveAs(Server.MapPath("/uploads/prods/img/") + Picname); // שמירת הקובץ לתיקייה בשרת
                TxtPicname.Text = Picname; // ממלא את שם הקובץ בתיבת הטקסט

            }
            Product p = new Product() // יצירת אובייקט חדש של מוצר
            {
                Pid = int.Parse(HidPid.Value), // מזהה המוצר
                Pname = TxtPname.Text, // שם המוצר
                Pdesc = TxtPdesc.Text, // תיאור המוצר
                Price = float.Parse(TxtPrice.Text), // מחיר המוצר
                Picname = TxtPicname.Text, // שם התמונה
                Cid = int.Parse(DDLCategory.SelectedValue), // מזהה הקטגוריה
                Status = int.Parse(DDLStatus.SelectedValue) // סטטוס המוצר

            };
            p.Save(); // שומר את המוצר במסד הנתונים
            Response.Redirect("ProductList.aspx"); // מפנה את המשתמש לרשימת המוצרים
        }       
    }
}

using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecomm19032025.AdminManage
{
    public partial class UsersAddEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) // מתבצע בכל טעינת עמוד
        {

            if (!IsPostBack) // התנאי מתבצע רק בטעינה הראשונה ולא אחרי לחיצה על כפתור
            {
                string Uid = Request["Uid"] + "";// מקבלת את הUid מהכתובת של העמוד
                Users u = null;// משתנה מסוג משתמשים

                if (!string.IsNullOrEmpty(Uid)) 
                {
                    u = Users.GetById(int.Parse(Uid));// מחזירה אובייקט משתמש לפי קוד
                }

                if (u != null) // אם האובייקט לא ריק
                {
                    TxtFullName.Text = u.FullName; // מחזירה את השם המלא
                    TxtEmail.Text = u.Email; // מחזירה את האימייל
                    TxtPhone.Text = u.Phone + "";// מחזירה את הטלפון 
                    TxtAdress.Text = u.Adress; // מחזירה את הכתובת
                    HidUid.Value = u.Uid + "";// מחזירה את קוד המשתמש
                    DDLStatus.SelectedValue = u.Status + "";// מחזירה את הסטטוס 

                }
                else
                {
                    HidUid.Value = "-1";// קוד משתמש חדש
                    DDLStatus.SelectedValue = "1";// סטטוס ברירת מחדל
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            Users u = new Users() // אובייקט משתמשים חדש
            {
                Uid = int.Parse(HidUid.Value), // קוד משתמש
                FullName = TxtFullName.Text, // שם מלא
                Email = TxtEmail.Text, // אימייל
                Phone = TxtPhone.Text, // טלפון
                Adress = TxtAdress.Text,// כתובת
                Status = int.Parse(DDLStatus.SelectedValue)// סטטוס
            };

            u.Save(); // שומר את המשתמש
            Response.Redirect("UsersList.aspx"); // הפניה לרשימת משתמשים
        }
    }
}
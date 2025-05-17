using BLL; 
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecomm19032025.AdminManage
{
    public partial class CategoryAddEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Cid = Request["Cid"] + ""; // מקבל את מזהה הקטגוריה מהשורת כתובת אם יש
                Category c = null; // מגדיר משתנה שיחזיק את הקטגוריה

                if (!string.IsNullOrEmpty(Cid)) // אם יש מזהה קטגוריה בכתובת
                {
                    c = Category.GetById(int.Parse(Cid)); // מביא את הקטגוריה לפי מזהה
                }

                if (c != null) // אם נמצאה קטגוריה לא חדשה
                {
                    TxtPname.Text = c.Cname; // שם הקטגוריה           
                    DDLStatus.SelectedValue = c.Status + ""; // סטטוס
                    HidCid.Value = c.Cid + ""; // מזהה מוסתר
                }
                else
                {
                    HidCid.Value = "-1"; // קטגוריה חדשה
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            Category c = new Category();// מגדיר אובייקט קטגוריה חדש

            c.Cid = int.Parse(HidCid.Value);// מזהה הקטגוריה
            c.Cname = TxtPname.Text;// שם הקטגוריה
            c.ParentCid = int.Parse(TxtParentCid.Text);// מזהה הקטגוריה ההורה
            c.Status = int.Parse(DDLStatus.SelectedValue);// סטטוס הקטגוריה

            c.Save();// שומר את הקטגוריה
            Response.Redirect("CategoryList.aspx");// מעביר לדף הקטגוריות
        }
    }
}

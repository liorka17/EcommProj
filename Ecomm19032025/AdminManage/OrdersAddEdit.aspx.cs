using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecomm19032025.AdminManage
{
    public partial class OrdersAddEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string OrderId = Request["OrderId"] + "";//מכניס את קוד ההזמנה למשתנה
                Orders o = null;//מכניס את ההזמנה למשתנה

                if (!string.IsNullOrEmpty(OrderId))
                {
                    o = Orders.GetById(int.Parse(OrderId));//מחזירה את ההזמנה לפי קוד
                }

                if (o != null && o.OrderId != -1)
                {
                    TxtUid.Text = o.Uid + "";//מכניס את קוד המשתמש למשתנה
                    TxtTotalPrice.Text = o.TotalPrice + "";//מכניס את הסכום הכולל למשתנה
                    TxtTotalAmount.Text = o.TotalAmount + "";//מכניס את הסכום הכולל למשתנה
                    DDLStatus.SelectedValue = o.Status;//מכניס את הסטטוס למשתנה
                    HidOrderId.Value = o.OrderId + "";//מכניס את קוד ההזמנה למשתנה
                }
                else
                {
                    HidOrderId.Value = "-1";//מכניס את קוד ההזמנה למשתנה
                    DDLStatus.SelectedValue = "בתהליך";//מכניס את הסטטוס למשתנה
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            Orders o = new Orders()//יוצרת אובייקט חדש של הזמנה
            {
                OrderId = int.Parse(HidOrderId.Value),//מכניס את קוד ההזמנה למשתנה
                Uid = int.Parse(TxtUid.Text),//מכניס את קוד המשתמש למשתנה
                TotalPrice = float.Parse(TxtTotalPrice.Text),//מכניס את הסכום הכולל למשתנה
                TotalAmount = int.Parse(TxtTotalAmount.Text),//מכניס את הכמות הכוללת למשתנה
                Status = DDLStatus.SelectedValue//מכניס את הסטטוס למשתנה
            };

            o.Save();//שומר את ההזמנה
            Response.Redirect("OrdersList.aspx");//מעביר לדף ההזמנות
        }
    }
}
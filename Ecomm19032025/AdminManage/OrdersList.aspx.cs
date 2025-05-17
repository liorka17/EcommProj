using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecomm19032025.AdminManage
{
    public partial class OrdersList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Orders> lst = Orders.GetAll();
                RptOrders.DataSource = lst; //מקשרת את רשימת ההזמנות לריפיטר
                RptOrders.DataBind(); //ממלאת את הריפיטר
            }
        }
    }
}
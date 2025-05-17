<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="OrdersList.aspx.cs" Inherits="Ecomm19032025.AdminManage.OrdersList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/dataTables.bootstrap4.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <h1 class="mb-4">טבלת ניהול הזמנות</h1>
    <a href="OrdersAddEdit.aspx" class="btn btn-info mb-3">הוספת הזמנה</a>

    <div class="card shadow">
        <div class="card-body">
            <table class="table table-bordered table-striped table-hover" id="MainTbl">
                <thead class="thead-light">
                    <tr>
                        <th>מזהה הזמנה</th>
                        <th>מזהה משתמש</th>
                        <th>מחיר כולל</th>
                        <th>כמות כוללת</th>
                        <th>סטטוס</th>
                        <th>ניהול</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RptOrders" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("OrderId") %></td>
                                <td><%# Eval("Uid") %></td>
                                <td><%# Eval("TotalPrice") %></td>
                                <td><%# Eval("TotalAmount") %></td>
                                <td><%# Eval("Status") %></td>
                                <td>
                                    <div class="dropdown">
                                        <button class="btn btn-sm btn-secondary dropdown-toggle" type="button" data-toggle="dropdown">
                                            פעולות
                                        </button>
                                        <div class="dropdown-menu">
                                            <a class="dropdown-item" href="OrdersAddEdit.aspx?OrderId=<%# Eval("OrderId") %>">עריכה</a>
                                            <a class="dropdown-item text-danger" href="#">הסרה</a>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CntFooter" runat="server">
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap4.min.js"></script>
    <script>
        $('#MainTbl').DataTable({
            autoWidth: true,
            "lengthMenu": [
                [16, 32, 64, -1],
                [16, 32, 64, "הכל"]
            ]
        });
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CntUnderFooter" runat="server">
</asp:Content>

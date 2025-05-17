<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="Ecomm19032025.AdminManage.ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/dataTables.bootstrap4.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <h1 class="mb-4">טבלת ניהול מוצרים</h1>
    <a href="ProductAddEdit.aspx" class="btn btn-info mb-3">הוספת מוצר</a>

    <div class="card shadow">
        <div class="card-body">
            <table class="table table-bordered table-striped table-hover" id="MainTbl">
                <thead class="thead-light">
                    <tr>
                        <th>קוד מוצר</th>
                        <th>שם מוצר</th>
                        <th>מחיר</th>
                        <th>תמונה</th>
                        <th>ניהול</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RptProds" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Pid") %></td>
                                <td><%# Eval("Pname") %></td>
                                <td><%# Eval("Price") %></td>
                                <td>
                                    <img src="/uploads/prods/img/<%# Eval("Picname") %>" class="avatar-img rounded-circle" width="40" />
                                </td>
                                <td>
                                    <div class="dropdown">
                                        <button class="btn btn-sm btn-secondary dropdown-toggle" type="button" data-toggle="dropdown">
                                            פעולות
                                        </button>
                                        <div class="dropdown-menu dropdown-menu-right">
                                            <a class="dropdown-item" href="ProductAddEdit.aspx?Pid=<%# Eval("Pid") %>">עריכה</a>
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

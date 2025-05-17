<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UsersList.aspx.cs" Inherits="Ecomm19032025.AdminManage.UsersList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/dataTables.bootstrap4.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <h1 class="mb-4">טבלת ניהול משתמשים</h1>
    <a href="UsersAddEdit.aspx" class="btn btn-info mb-3">הוספת משתמש</a>

    <div class="card shadow">
        <div class="card-body">
            <table class="table table-bordered table-striped table-hover" id="MainTbl">
                <thead class="thead-light">
                    <tr>
                        <th>קוד משתמש</th>
                        <th>שם משתמש</th>
                        <th>מייל</th>
                        <th>טלפון</th>
                        <th>כתובת</th>
                        <th>סטטוס</th>
                        <th>ניהול</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RptUsers" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Uid") %></td>
                                <td><%# Eval("FullName") %></td>
                                <td><%# Eval("Email") %></td>
                                <td><%# Eval("Phone") %></td>
                                <td><%# Eval("Adress") %></td>
                                <td><%# Eval("Status") %></td>
                                                                
                                <td>
                                    <div class="dropdown">
                                        <button class="btn btn-sm btn-secondary dropdown-toggle" type="button" data-toggle="dropdown">
                                            פעולות
                                        </button>
                                        <div class="dropdown-menu">
                                            <a class="dropdown-item" href="UsersAddEdit.aspx?Uid=<%# Eval("Uid") %>">עריכה</a>
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

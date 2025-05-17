<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UsersAddEdit.aspx.cs" Inherits="Ecomm19032025.AdminManage.UsersAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <h2 class="page-title">עריכת משתמשים</h2>
    <p class="text-muted">נא להזין את פרטי המשתמש</p>

    <div class="card shadow mb-4" style="max-width: 600px; margin: auto;">
        <div class="card-header">
            <strong class="card-title">פרטי המשתמש</strong>
        </div>
        <div class="card-body">
            <asp:HiddenField ID="HidUid" runat="server" Value="-1" />

            <div class="form-group">
                <label for="TxtFullName">שם מלא</label>
                <asp:TextBox ID="TxtFullName" runat="server" CssClass="form-control" placeholder="יש להזין שם פרטי ושם משפחה" />
            </div>

            <div class="form-group">
                <label for="TxtEmail">מייל</label>
                <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="יש להזין אימייל" />
            </div>

            <div class="form-group">
                <label for="TxtPhone">טלפון</label>
                <asp:TextBox ID="TxtPhone" runat="server" CssClass="form-control" TextMode="Phone" />
            </div>

            <div class="form-group">
                <label for="TxtAdress">כתובת</label>
                <asp:TextBox ID="TxtAdress" runat="server" CssClass="form-control" TextMode="SingleLine" />
            </div>

            <div class="form-group">
                <label for="DDLStatus">סטטוס</label>
                <asp:DropDownList ID="DDLStatus" runat="server" CssClass="form-control">
                    <asp:ListItem Text="פעיל" Value="1"></asp:ListItem>
                    <asp:ListItem Text="לא פעיל" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <asp:Button ID="BtnSave" Text="שמור" runat="server" CssClass="btn btn-primary btn-block" OnClick="BtnSave_Click" />
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CntFooter" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CntUnderFooter" runat="server">
</asp:Content>

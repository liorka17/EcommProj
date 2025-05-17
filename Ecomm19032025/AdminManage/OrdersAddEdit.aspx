<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="OrdersAddEdit.aspx.cs" Inherits="Ecomm19032025.AdminManage.OrdersAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <h2 class="page-title">עריכת הזמנה</h2>
    <p class="text-muted">נא להזין את פרטי ההזמנה</p>

    <div class="card-deck">
        <div class="card shadow mb-4">
            <div class="card-header">
                <strong class="card-title">פרטי הזמנה</strong>
            </div>
            <div class="card-body">
                <div class="form-row">
                    <div class="form-group col-md-6">
                        <label for="TxtUid">קוד משתמש</label>
                        <asp:TextBox ID="TxtUid" runat="server" class="form-control" placeholder="הכנס קוד משתמש" />
                    </div>
                    <div class="form-group col-md-6">
                        <label for="TxtTotalPrice">מחיר כולל</label>
                        <asp:TextBox ID="TxtTotalPrice" runat="server" class="form-control" placeholder="הכנס מחיר כולל" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group col-md-6">
                        <label for="TxtTotalAmount">כמות כוללת</label>
                        <asp:TextBox ID="TxtTotalAmount" runat="server" class="form-control" placeholder="הכנס כמות" />
                    </div>
                    <div class="form-group col-md-6">
                        <label for="DDLStatus">סטטוס</label>
                        <asp:DropDownList ID="DDLStatus" runat="server" class="form-control">
                            <asp:ListItem Text="בתהליך" Value="בתהליך"></asp:ListItem>
                            <asp:ListItem Text="בוצעה" Value="בוצעה"></asp:ListItem>
                            <asp:ListItem Text="בוטלה" Value="בוטלה"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <asp:HiddenField ID="HidOrderId" runat="server" Value="-1" />
                <asp:Button ID="BtnSave" runat="server" Text="שמור" CssClass="btn btn-primary" OnClick="BtnSave_Click" />
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CntFooter" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="CntUnderFooter" runat="server">
</asp:Content>

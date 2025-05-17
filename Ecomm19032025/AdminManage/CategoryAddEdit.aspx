<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CategoryAddEdit.aspx.cs" Inherits="Ecomm19032025.AdminManage.CategoryAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <h2 class="page-title">עריכת קטגוריות</h2>
    <p class="text-muted">נא להזין את פרטי הקטגוריה ולבצע שמירה</p>

    <div class="card-deck">
        <div class="card shadow mb-4">
            <div class="card-header">
                <strong class="card-title">פרטי הקטגוריה</strong>
            </div>
            <div class="card-body">
                <div class="form-row">
                    <div class="form-group col-md-6">
                        <label for="TxtPname">שם הקטגוריה</label>
                        <asp:TextBox ID="TxtPname" runat="server" class="form-control" placeholder="הזן שם קטגוריה" />
                    </div>
                    <div class="form-group col-md-6">
                        <label for="TxtParentCid">קוד אב</label>
                        <asp:TextBox ID="TxtParentCid" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group col-md-6">
                        <label for="DDLStatus">סטטוס</label>
                        <asp:DropDownList ID="DDLStatus" runat="server" class="form-control">
                            <asp:ListItem Text="פעיל" Value="1"></asp:ListItem>
                            <asp:ListItem Text="לא פעיל" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <asp:HiddenField ID="HidCid" runat="server" />

                <asp:Button ID="BtnSave" runat="server" Text="שמור" CssClass="btn btn-primary" OnClick="BtnSave_Click" />
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CntFooter" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CntUnderFooter" runat="server">
</asp:Content>

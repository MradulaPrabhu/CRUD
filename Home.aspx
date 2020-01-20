<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CRUD.Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
</head>
<body>
    <form id="form1" runat="server">
        <div>

        </div>
    <div>
        <asp:HiddenField ID="hfProductID" runat="server" />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="margin-left: 575px" Text="LOGOUT" Width="109px" />
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblProduct" runat="server" Text="Product"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtProduct" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                    
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
                <td>
                    <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor ="Green"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor ="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="PRODUCT_ID" HeaderText="Product_ID" />
                <asp:BoundField DataField="NAME" HeaderText="Product" />
                <asp:BoundField DataField="PRICE" HeaderText="Price" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%# Eval ("PRODUCT_ID") %>' OnClick="lnk_OnClick">View</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>


       
    </div>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
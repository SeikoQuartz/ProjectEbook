<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Project.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:PlaceHolder ID="plcLogin" runat="server">
        Account:<asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
        Password:<asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        <asp:Button ID="btnLogin" runat="server" Text="登入"  Onclick="btnLogin_Click"/>
               <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcUserInfo" runat="server">
        <asp:Literal ID="ltlAccount" runat="server"></asp:Literal><br/>
            請前往<a href="/BackAdmin/Index.aspx">後台</a>
        </asp:PlaceHolder>
        
        <asp:Button ID="btnRegister" runat="server" Text="註冊" OnClick="btnRegister_Click" />
     
     
    </form>
</body>
</html>

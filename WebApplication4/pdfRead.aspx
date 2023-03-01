<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pdfRead.aspx.vb" Inherits="WebApplication4.pdfRead" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        /*::selection {
            background: transparent;
            text-shadow: #000 0 0 2px;
        }*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="fuPdfUpload" runat="server" />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Height="43px" Text="Generate" Width="92px" />
            &nbsp;
            <asp:Button ID="Button2" runat="server" Height="43px" Text="Generate API" Width="113px" visible="false"/>
            <br />
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
&nbsp;&nbsp;
            </div>
    </form>
</body>
</html>

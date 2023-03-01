<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="AddStudent.aspx.vb" Inherits="WebApplication4.AddStudent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        ::selection {
            background: transparent;
            text-shadow: #000 0 0 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div> 
            <h1>Students</h1>
            <div>
                Student Name:
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtName" runat="server" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                &nbsp;<br />
                <br />
                Student Type :
                <asp:DropDownList ID="dpdStudentType" runat="server">
                    <asp:ListItem Selected="True" Value=""> Select... </asp:ListItem>
                    <asp:ListItem Value="1">Full Time</asp:ListItem>
                    <asp:ListItem Value="2">Part Time</asp:ListItem>
                    <asp:ListItem Value="3">Co-op</asp:ListItem>
                </asp:DropDownList>
                <%--<asp:RequiredFieldValidator ID="rfvStudentType" ControlToValidate="dpdStudentType" ForeColor="Red" runat="server" ErrorMessage="Must select one!" InitialValue="" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                <br />
                <br />
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <br />
                <br />
                <asp:Button Style="margin-left: 10px" ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" Height="28px" Width="81px" />
                <asp:Button Style="margin-left: 10px" ID="btnList" runat="server" Text="List" Height="28px" Width="81px" />
                <br />
                <br />
            </div>
            <div id="details" runat="server">
                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
            </div>
        </div>
        <br />
        <%--<a><asp:HyperLink ID="HyperLink1" runat="server" onClick="OpenPopup()" href="" >Register Courses</asp:HyperLink></a>--%>
        <asp:Button style="margin-left: 10px" ID="btnCourse" runat="server" Text="Register Courses" OnClientClick="return OpenPopup()" Height="28px" Width="157px" />
        <script type="text/javascript">
            function OpenPopup() {
                window.open('RegisterCourse.aspx', 'List', 'toolbar=no, location = no, status = yes, menubar = no, ScrollBars = yes, resizable = no, width = 1700, height = 900, Left() = 430, top = 100');
                return false;
            }
        </script>     
        <br />
         <h1>Remove Student</h1>
        <br />
        <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCode" runat="server" ErrorMessage="Required!" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                <br />
        <br />
                <asp:Button style="margin-left: 10px" ID="btnRemove" runat="server" Text="Delete" Height="28px" Width="81px" />
    </form>
</body>
</html>

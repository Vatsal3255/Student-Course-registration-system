<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RegisterCourse.aspx.vb" Inherits="WebApplication4.RegisterCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Registrations </h1>
            <p>
                Student : 
                <asp:DropDownList AutoPostBack="true" Style="margin-left: 40px" Height="25px" Width="300px" ID="drpStudent" runat="server" OnSelectedIndexChanged="drpStudent_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvStudent" ControlToValidate="drpStudent" ForeColor="Red" runat="server" ErrorMessage="Must select one!" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
            </p>

            <p>
                 <asp:Label ID="studentCoursesLabel" runat="server" Text="" ForeColor="Blue"></asp:Label>
            </p>
           
            <div id="pnlInfo" runat="server">
                <p>
                    <asp:Label ID="alert1" runat="server" Text="" ForeColor="Red"></asp:Label>
                </p>

                <h3 style="height: 19px">Following courses are currently available for registration</h3>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                </asp:CheckBoxList>
            </div>  

        <br />
        <br />
        <asp:Button style="margin-left: 10px" ID="btnSave" width="80px" height ="25px" runat="server" Text="Save" OnClick="Main" />
        <asp:Button style="margin-left: 10px" ID="btnFind" width="80px" height ="25px" runat="server" Text="Find"/>
        <asp:Button Style="margin-left: 10px" ID="btnEmail" Width="80px" Height="25px" runat="server" Text="Email"/>
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <br />
            &nbsp;<asp:Label ID="Label1" runat="server" Text="" ForeColor="Green"></asp:Label>
            <br /> <br />

            <asp:GridView ID="gv2" runat="server" Visible="false">
            </asp:GridView>
        </div>
        
    </form>
</body></html>

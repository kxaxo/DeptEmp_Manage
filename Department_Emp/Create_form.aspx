<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Create_form.aspx.vb" Inherits="Department_Emp.Create_form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="style_create.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@700&display=swap" rel="stylesheet">
    <title>Create Form</title>
</head>
<body>
    <header>
        <h1>Department Form</h1>
    </header>
    <a href="dept_form.aspx" class="back_homepage2">GO BACK</a>
    <h2>Create Department</h2>
    <hr />
    <form id="create_form" runat="server">
        <div class="create_department_form" align="center">
            <label class="lbl_deptcode_create">Department Code : </label>
            <asp:TextBox ID="txt_deptcode_create" runat="server" ReadOnly>:: Auto create ::</asp:TextBox>
            
            <label class="lbl_deptname_create">Department Name : </label>
            <asp:TextBox ID="txt_deptname_create" runat="server"></asp:TextBox><br /><br />
            
            <label class="lbl_remarks_create">Remarks : </label>
            <asp:TextBox ID="txt_remarks_create" runat="server"></asp:TextBox>
            <label class="lbl_status_create">Status : </label>
            <asp:DropDownList ID="ddl_status_create" runat="server">
                <asp:ListItem>Y</asp:ListItem>
                <asp:ListItem>N</asp:ListItem>
            </asp:DropDownList><br /><br />
            <asp:Button ID="button_create_department" runat="server" Text="CREATE" />
        </div>
    <h2>Create Employee</h2>
    <hr />
        <div class="create_employee_form" align="center">
            <label class="lbl_empcode">Employee Code : </label>
            <asp:TextBox ID="txt_empcode_create" runat="server" ReadOnly>:: Auto create ::</asp:TextBox>
            <label class="lbl_empdeptcode">Department Code : </label>
            <asp:TextBox ID="txt_empdeptcode_create" runat="server" ReadOnly>:: Select in the table ::</asp:TextBox><br /><br />
            <label class="lbl_firstname">Firstname : </label>
            <asp:TextBox ID="txt_firstname_create" runat="server"></asp:TextBox>
            <label class="lbl_lastname">Lastname : </label>
            <asp:TextBox ID="txt_lastname_create" runat="server"></asp:TextBox><br /><br />
            <asp:Button ID="button_create_employee" runat="server" Text="CREATE" />
            
            <asp:GridView ID="grid_deptcodename" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="70%">
                <Columns>
                    <asp:BoundField DataField="department_code" HeaderText="DeptCode" SortExpression="department_code" />
                    <asp:BoundField DataField="department_name" HeaderText="DeptName" SortExpression="department_name" />
                    <asp:ButtonField ButtonType="Button" CommandName="Select" Text="SELECT">
                    <ControlStyle BackColor="White" BorderStyle="None" Font-Bold="True" Font-Size="Medium" ForeColor="#00CC00" />
                    </asp:ButtonField>
                </Columns>
                <HeaderStyle BackColor="#0033CC" Font-Bold="True" Font-Size="Medium" ForeColor="White" />
            </asp:GridView>
        </div>
        <h2>List Data Created</h2>
        <hr />
        <div align="center">
            <asp:GridView ID="grid_all_deptemp" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="80%">
                <Columns>
                    <asp:BoundField DataField="department_code" HeaderText="DeptCode" SortExpression="department_code" />
                    <asp:BoundField DataField="department_name" HeaderText="DeptName" SortExpression="department_name" />
                    <asp:BoundField DataField="remarks" HeaderText="Remarks" SortExpression="remarks" />
                    <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                    <asp:BoundField DataField="employee_code" HeaderText="EmpCode" SortExpression="employee_code" />
                    <asp:BoundField DataField="firstname" HeaderText="Firstname" SortExpression="firstname" />
                    <asp:BoundField DataField="lastname" HeaderText="Lastname" SortExpression="lastname" />
                </Columns>
                <HeaderStyle BackColor="#0000CC" Font-Bold="True" Font-Size="Medium" ForeColor="White" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>

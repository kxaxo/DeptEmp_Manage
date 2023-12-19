<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Edit_DeptEmp.aspx.vb" Inherits="Department_Emp.Edit_DeptEmp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="style_edit.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@700&display=swap" rel="stylesheet">
    <title>Modify Form</title>
</head>
<body>
    <header>
        <h1>Department Form</h1>
    </header>
    <a href="dept_form.aspx" class="back_homepage">GO BACK</a>
    <h2>Update Form</h2>
    <hr />
    <form id="edit_form" runat="server">
        <div class="department_modify" align="center">

            <label class="lbl_deptcode_update">Department Code : </label>
            <asp:TextBox ID="txt_deptcode_update" runat="server" ReadOnly></asp:TextBox>
            <label class="lbl_deptname_update">Department Name : </label>
            <asp:TextBox ID="txt_deptname_update" runat="server"></asp:TextBox><br /><br />
            

            <label class="lbl_remarks_update">Remarks : </label>
            <asp:TextBox ID="txt_remarks_update" runat="server"></asp:TextBox>
            <label class="lbl_deptstatus_update">Status : </label>
            <asp:DropDownList ID="ddl_status_update" runat="server">
                <asp:ListItem>Y</asp:ListItem>
                <asp:ListItem>N</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Button ID="button_update_department" runat="server" Text="UPDATE DEPARTMENT" OnClientClick="return confirm(&quot;Are you sure to update?&quot;)" Width="223px" />
        </div>
    <h2>Update Employee</h2>
    <hr />
        <div class="employee_modify" align="center">
            <label>Employee Code : </label>
            <asp:TextBox ID="txt_empcode_update" runat="server" ReadOnly></asp:TextBox>
            <label>Firstname : </label>
            <asp:TextBox ID="txt_firstname_update" runat="server"></asp:TextBox>
            <label>Lastname : </label>
            <asp:TextBox ID="txt_lastname_update" runat="server"></asp:TextBox><br /><br />
            <label>Department Code : </label>
            <asp:TextBox ID="txt_deptcode_update2" runat="server" ReadOnly>Select DeptCode in the table</asp:TextBox>
            <br />
            <asp:Button ID="button_update_employee" runat="server" Text="UPDATE EMPLOYEE" OnClientClick="return comfirm(&quot;Are you sure to update?&quot;)" Width="207px" />
            <br />
            <br />
            <asp:GridView ID="grid_dept" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="70%">
                <Columns>
                    <asp:BoundField DataField="department_code" HeaderText="DeptCode" SortExpression="department_code" />
                    <asp:BoundField DataField="department_name" HeaderText="DeptName" SortExpression="department_name" />
                    <asp:BoundField DataField="employee_code" HeaderText="EmpCode" SortExpression="employee_code" />
                    <asp:BoundField DataField="firstname" HeaderText="Firstname" SortExpression="firstname" />
                    <asp:BoundField DataField="lastname" HeaderText="Lastname" SortExpression="lastname" />
                    <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                    <asp:ButtonField ButtonType="Button" CommandName="Select" Text="SELECT">
                    <ControlStyle BackColor="White" BorderStyle="None" Font-Bold="True" Font-Size="Medium" ForeColor="#00CC00" />
                    </asp:ButtonField>
                </Columns>
                <HeaderStyle BackColor="#0066FF" Font-Bold="True" Font-Size="Medium" ForeColor="White" />
            </asp:GridView>
        </div>
        
    </form>
</body>
</html>

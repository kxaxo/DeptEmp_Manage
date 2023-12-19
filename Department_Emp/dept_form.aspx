<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="dept_form.aspx.vb" Inherits="Department_Emp.dept_form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="style_dept_form.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@700&display=swap" rel="stylesheet">
    <title>DeptEmp Search</title>
</head>
<body>
    <header>
        <h1>Department Form</h1>
    </header>
    <h2>Search Department</h2>
    <hr />
    <form id="dept_emp_form" runat="server">
        <div class="search_form">
            <label class="lbl_deptcode">Department Code : </label>
            <asp:TextBox ID="txt_deptcode" runat="server"></asp:TextBox>
            <label class="lbl_deptname">Department Name : </label>
            <asp:TextBox ID="txt_deptname" runat="server"></asp:TextBox><br /><br />
            <label class="lbl_status">Status : </label>
            <asp:DropDownList ID="ddl_deptstatus" runat="server">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Y</asp:ListItem>
                <asp:ListItem>N</asp:ListItem>
            </asp:DropDownList>  
            <asp:Button ID="button_search" runat="server" Text="SEARCH" />
            &nbsp;&nbsp;
            <asp:Button ID="button_reset" runat="server" Text="RESET" BackColor="#66FF99" Font-Bold="True" Font-Size="Small" Height="35px" Width="98px" />
        </div>
    <h2>Department Detail</h2>
    <hr />
        <asp:TextBox ID="txt_selected_deptcode" runat="server" ReadOnly>Auto select</asp:TextBox>
        <asp:Button ID="button_delete" runat="server" Text="DELETE" OnClientClick="return confirm(&quot;Are you sure to delete?&quot;)" />
        <asp:Button ID="button_clear" runat="server" Text="CLEAR" />
        <a href="Create_form.aspx" class="create_link" id="button_create">CREATE</a>
        <div class="table_form">

            <asp:GridView ID="grid_department" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="90%">
                <Columns>
                    <asp:BoundField DataField="department_code" HeaderText="DeptCode" SortExpression="department_code" />
                    <asp:BoundField DataField="department_name" HeaderText="DeptName" SortExpression="department_name" />
                    <asp:BoundField DataField="employee_code" HeaderText="EmpCode" SortExpression="employee_code" />
                    <asp:BoundField DataField="remarks" HeaderText="Remarks" SortExpression="remarks" />
                    <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                    <asp:HyperLinkField DataNavigateUrlFields="department_code,employee_code" DataNavigateUrlFormatString="Edit_DeptEmp.aspx?department_code={0}&amp;employee_code={1}" Text="EDIT">
                    <ControlStyle Font-Bold="True" Font-Size="Medium" ForeColor="#FF6600" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:HyperLinkField>
                    <asp:ButtonField ButtonType="Button" CommandName="Select" Text="SELECT">
                    <ControlStyle BackColor="White" BorderStyle="None" Font-Bold="True" Font-Size="Medium" ForeColor="#33CC33" />
                    <HeaderStyle Width="30px" />
                    <ItemStyle Font-Bold="True" Width="35px" />
                    </asp:ButtonField>
                </Columns>
            </asp:GridView>

        </div>
    </form>
</body>
</html>

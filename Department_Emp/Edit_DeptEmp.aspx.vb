Imports System.Data.SqlClient
Public Class Edit_DeptEmp
    Inherits System.Web.UI.Page

    Dim connectionStringName As String = "DefaultDept"
    Dim connectionString As String = ConfigurationManager.ConnectionStrings(connectionStringName).ConnectionString
    Dim connect As New SqlConnection(connectionString)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txt_deptcode_update.Text = Request.QueryString("department_code")
        txt_empcode_update.Text = Request.QueryString("employee_code")
        Load_data()
    End Sub
    Private Sub Load_data()
        connect.Open()
        Dim command_query_all As New SqlCommand("SELECT dept.department_code, department_name, employee_code, firstname, lastname, status
                                                 FROM department_list AS dept LEFT JOIN employee_list AS emp
                                                 ON dept.department_code = emp.department_code
                                                 ORDER BY dept.department_code", connect)
        Dim Dataset As New SqlDataAdapter(command_query_all)
        FillInTable(Dataset)
    End Sub

    Private Sub FillInTable(ByVal data_set As SqlDataAdapter)

        Dim data_table As New DataTable
        Try
            data_set.Fill(data_table)
            If data_table.Rows.Count > 0 Then
                grid_dept.DataSource = data_table
                grid_dept.DataBind()
                connect.Close()
            Else
                MsgBox("Error: Not Found", MsgBoxStyle.Exclamation, "Error")
                Response.Redirect(Request.RawUrl)
            End If
        Catch ex As Exception
            MsgBox("Error: Not Found", MsgBoxStyle.Exclamation, "Error")
        End Try

    End Sub

    Protected Sub button_update_department_Click(sender As Object, e As EventArgs) Handles button_update_department.Click
        Dim DeptCode_Update As String = txt_deptcode_update.Text
        Dim DeptName_Update As String = txt_deptname_update.Text
        Dim Remarks_Update As String = txt_remarks_update.Text
        Dim Status_Update As String = ddl_status_update.SelectedValue

        If DeptName_Update = "" Then
            MsgBox("Error: Can't Update", MsgBoxStyle.Exclamation, "Error")
        Else
            connect.Open()
            If Remarks_Update = "" Then
                Dim command_update_dept1 As New SqlCommand($"UPDATE dbo.department_list 
                                                SET department_name = '{DeptName_Update}', remarks = NULL, status = '{Status_Update}' WHERE department_code = '{DeptCode_Update}';", connect)
                command_update_dept1.ExecuteNonQuery()
                MsgBox("Successfully Updated", MsgBoxStyle.Information, "Message")
            Else
                Dim command_update_dept2 As New SqlCommand($"UPDATE dbo.department_list 
                                                SET department_name = '{DeptName_Update}', remarks = '{Remarks_Update}', status = '{Status_Update}' WHERE department_code = '{DeptCode_Update}';", connect)
                command_update_dept2.ExecuteNonQuery()
                MsgBox("Successfully Updated", MsgBoxStyle.Information, "Message")
            End If
            connect.Close()
            Load_data()
        End If
    End Sub

    Protected Sub button_update_employee_Click(sender As Object, e As EventArgs) Handles button_update_employee.Click
        Dim EmpCode_Update As String = txt_empcode_update.Text
        Dim Firstname_Update As String = txt_firstname_update.Text
        Dim Lastname_Update As String = txt_lastname_update.Text
        Dim EmpDeptCode_Update As String = txt_deptcode_update2.Text

        If EmpDeptCode_Update <> "Select DeptCode in the table" Then
            If EmpCode_Update = "" Then
                MsgBox("Error: Can't Update Empty DeptCode.", MsgBoxStyle.Exclamation, "Error")
            Else
                If Firstname_Update <> "" And Lastname_Update <> "" Then
                    connect.Open()
                    Dim command_emp_update As New SqlCommand($"UPDATE employee_list SET firstname = '{Firstname_Update}', lastname = '{Lastname_Update}', 
                                                       department_code = '{EmpDeptCode_Update}' WHERE employee_code = '{EmpCode_Update}'", connect)
                    command_emp_update.ExecuteNonQuery()
                    MsgBox("Successfully Updated", MsgBoxStyle.Information, "Message")
                End If
            End If
        Else
            MsgBox("Error: Can't Update Empty DeptCode.", MsgBoxStyle.Exclamation, "Error")
        End If
        connect.Close()
        Load_data()
    End Sub

    Protected Sub grid_dept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grid_dept.SelectedIndexChanged
        txt_deptcode_update2.Text = HttpUtility.HtmlDecode(grid_dept.SelectedRow.Cells.Item(0).Text.ToString)
    End Sub
End Class
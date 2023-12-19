Imports System.Data.SqlClient
Imports System.Runtime.Remoting.Contexts
Public Class dept_form
    Inherits System.Web.UI.Page

    Dim connectionStringName As String = "DefaultDept"
    Dim connectionString As String = ConfigurationManager.ConnectionStrings(connectionStringName).ConnectionString
    Dim connect As New SqlConnection(connectionString)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Load_data()
    End Sub

    Private Sub Load_data()
        connect.Open()
        Dim command_query_all As New SqlCommand("SELECT dept.department_code, department_name, employee_code, remarks, status 
                                                    FROM dbo.department_list AS dept LEFT JOIN dbo.employee_list 
                                                    ON dept.department_code = employee_list.department_code ORDER BY dept.department_code;", connect)
        Dim Dataset As New SqlDataAdapter(command_query_all)
        FillInTable(Dataset)
    End Sub

    Private Sub FillInTable(ByVal data_set As SqlDataAdapter)

        Dim data_table As New DataTable
        data_set.Fill(data_table)
        If data_table.Rows.Count > 0 Then
            grid_department.DataSource = data_table
            grid_department.DataBind()
            connect.Close()
        Else
            MsgBox("Error: Not Found", MsgBoxStyle.Exclamation, "Error")
            Response.Redirect(Request.RawUrl)
        End If

    End Sub

    Private Sub Checkquery(ByVal command_check As SqlCommand)


        Try
            Dim read_check As SqlDataReader = command_check.ExecuteReader()
            Dim list_check As New List(Of String)()
            While read_check.Read()
                list_check.Add(read_check.GetString(0))
            End While
            If list_check.Count > 0 Then
                connect.Close()
                connect.Open()
                Dim data_set_check As New SqlDataAdapter(command_check)
                FillInTable(data_set_check)
            ElseIf list_check.Count = 0 Then
                MsgBox("Error: Not Found", MsgBoxStyle.Exclamation, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error: Not Found", MsgBoxStyle.Exclamation, "Error")
            Response.Redirect(Request.RawUrl)
        End Try
    End Sub


    Protected Sub button_search_Click(sender As Object, e As EventArgs) Handles button_search.Click

        Dim DeptCode_Search As String = txt_deptcode.Text
        Dim DeptName_Search As String = txt_deptname.Text
        Dim DeptStatus_Search As String = ddl_deptstatus.SelectedValue

        connect.Open()
        If DeptCode_Search = "" And DeptName_Search = "" Then
            If DeptStatus_Search = "All" Then
                connect.Close()
                Load_data()
            Else
                Dim command_S As New SqlCommand($"SELECT dept.department_code, department_name, employee_code, remarks, status 
                                                    FROM dbo.department_list AS dept LEFT JOIN dbo.employee_list 
                                                    ON dept.department_code = employee_list.department_code 
                                                    WHERE status = '{DeptStatus_Search}'
                                                    ORDER BY dept.department_code;", connect)
                Checkquery(command_S)
            End If
        ElseIf DeptCode_Search <> "" Or DeptName_Search <> "" Then
            If DeptStatus_Search = "All" Then
                If DeptCode_Search <> "" And DeptName_Search = "" Then
                    Dim command_C As New SqlCommand($"SELECT dept.department_code, department_name, employee_code, remarks, status 
                                                    FROM dbo.department_list AS dept LEFT JOIN dbo.employee_list 
                                                    ON dept.department_code = employee_list.department_code 
                                                    WHERE dept.department_code LIKE '%{DeptCode_Search}'
                                                    ORDER BY dept.department_code;", connect)
                    Checkquery(command_C)
                ElseIf DeptCode_Search = "" And DeptName_Search <> "" Then
                    Dim command_N As New SqlCommand($"SELECT dept.department_code, department_name, employee_code, remarks, status 
                                                    FROM dbo.department_list AS dept LEFT JOIN dbo.employee_list 
                                                    ON dept.department_code = employee_list.department_code 
                                                    WHERE department_name LIKE '%{DeptName_Search}%'
                                                    ORDER BY dept.department_code;", connect)
                    Checkquery(command_N)
                ElseIf DeptCode_Search <> "" And DeptName_Search <> "" Then
                    Dim command_CN As New SqlCommand($"SELECT dept.department_code, department_name, employee_code, remarks, status 
                                                    FROM dbo.department_list AS dept LEFT JOIN dbo.employee_list 
                                                    ON dept.department_code = employee_list.department_code 
                                                    WHERE dept.department_code LIKE '%{DeptCode_Search}' and department_name LIKE '%{DeptName_Search}%'
                                                    ORDER BY dept.department_code;", connect)
                    Checkquery(command_CN)
                End If
            ElseIf DeptStatus_Search <> "All" Then
                If DeptCode_Search <> "" And DeptName_Search = "" Then
                    Dim command_CS As New SqlCommand($"SELECT dept.department_code, department_name, employee_code, remarks, status 
                                                    FROM dbo.department_list AS dept LEFT JOIN dbo.employee_list 
                                                    ON dept.department_code = employee_list.department_code 
                                                    WHERE dept.department_code LIKE '%{DeptCode_Search}' and status = '{DeptStatus_Search}'
                                                    ORDER BY dept.department_code;", connect)
                    Checkquery(command_CS)
                ElseIf DeptCode_Search = "" And DeptName_Search <> "" Then
                    Dim command_NS As New SqlCommand($"SELECT dept.department_code, department_name, employee_code, remarks, status 
                                                    FROM dbo.department_list AS dept LEFT JOIN dbo.employee_list 
                                                    ON dept.department_code = employee_list.department_code 
                                                    WHERE department_name LIKE '%{DeptName_Search}%' and status = '{DeptStatus_Search}'
                                                    ORDER BY dept.department_code;", connect)
                    Checkquery(command_NS)
                ElseIf DeptCode_Search <> "" And DeptName_Search <> "" Then
                    Dim command_CNS As New SqlCommand($"SELECT dept.department_code, department_name, employee_code, remarks, status 
                                                    FROM dbo.department_list AS dept LEFT JOIN dbo.employee_list 
                                                    ON dept.department_code = employee_list.department_code 
                                                    WHERE dept.department_code LIKE '%{DeptCode_Search}' and department_name LIKE '%{DeptName_Search}%' and status = '{DeptStatus_Search}'
                                                    ORDER BY dept.department_code;", connect)
                    Checkquery(command_CNS)
                End If

            End If
        End If

    End Sub

    Protected Sub grid_department_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grid_department.SelectedIndexChanged
        txt_selected_deptcode.Text = HttpUtility.HtmlDecode(grid_department.SelectedRow.Cells.Item(0).Text.ToString)
    End Sub

    Protected Sub button_clear_Click(sender As Object, e As EventArgs) Handles button_clear.Click
        txt_selected_deptcode.Text = ""
    End Sub

    Protected Sub button_delete_Click(sender As Object, e As EventArgs) Handles button_delete.Click
        Dim DeptCode_Selected As String = txt_selected_deptcode.Text
        If DeptCode_Selected = "" Then
            MsgBox("Error: No have information to delete.", MsgBoxStyle.Exclamation, "Error")
        ElseIf DeptCode_Selected <> "" Then
            If Integer.Parse(CheckNumData()) = 1 Then
                MsgBox("Error: You must have 1 row in table.", MsgBoxStyle.Exclamation, "Error")
            Else
                connect.Close()
                connect.Open()
                Dim command_delete As New SqlCommand($"DELETE FROM dbo.department_list WHERE department_code = '{DeptCode_Selected}'", connect)
                command_delete.ExecuteNonQuery()
                MsgBox("Successfully Deleted", MsgBoxStyle.Information, "Message")
                connect.Close()
                Load_data()
            End If
        End If
    End Sub

    Private Function CheckNumData()

        connect.Open()
        Dim command_lasted_check As New SqlCommand("SELECT COUNT(department_code) AS Num_DeptCode FROM dbo.department_list", connect)
        Dim read_num_check As SqlDataReader = command_lasted_check.ExecuteReader()
        Dim list_num_check As New List(Of String)()
        While read_num_check.Read()
            list_num_check.Add(read_num_check.GetInt32(0))

        End While
        Return list_num_check(0)
        connect.Close()

    End Function

    Protected Sub button_reset_Click(sender As Object, e As EventArgs) Handles button_reset.Click
        txt_deptcode.Text = ""
        txt_deptname.Text = ""
        ddl_deptstatus.SelectedValue = "All"
        Load_data()
    End Sub
End Class
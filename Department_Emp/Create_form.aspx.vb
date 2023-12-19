Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Runtime.Remoting.Contexts

Public Class Create_form
    Inherits System.Web.UI.Page

    Dim connectionStringName As String = "DefaultDept"
    Dim connectionString As String = ConfigurationManager.ConnectionStrings(connectionStringName).ConnectionString
    Dim connect As New SqlConnection(connectionString)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Load_data()
        Load_data2()
    End Sub

    Private Sub Load_data()
        connect.Open()
        Dim command_query_all As New SqlCommand("SELECT department_code, department_name FROM department_list", connect)
        Dim Dataset As New SqlDataAdapter(command_query_all)
        FillInTable(Dataset)
    End Sub

    Private Sub Load_data2()
        connect.Open()
        Dim command_query_all2 As New SqlCommand("SELECT dept.department_code, department_name, remarks, status, employee_code, firstname, lastname 
                                                    FROM department_list AS dept LEFT JOIN employee_list AS emp
                                                    ON dept.department_code = emp.department_code", connect)
        Dim Dataset As New SqlDataAdapter(command_query_all2)
        FillInTable2(Dataset)
    End Sub

    Private Sub FillInTable(ByVal data_set As SqlDataAdapter)

        Dim data_table As New DataTable
        Try
            data_set.Fill(data_table)
            If data_table.Rows.Count > 0 Then
                grid_deptcodename.DataSource = data_table
                grid_deptcodename.DataBind()
                connect.Close()
            Else
                MsgBox("Error: Not Found", MsgBoxStyle.Exclamation, "Error")
                Response.Redirect(Request.RawUrl)
            End If
        Catch ex As Exception
            MsgBox("Error: Not Found", MsgBoxStyle.Exclamation, "Error")
        End Try

    End Sub
    Private Sub FillInTable2(ByVal data_set As SqlDataAdapter)

        Dim data_table As New DataTable
        Try
            data_set.Fill(data_table)
            If data_table.Rows.Count > 0 Then
                grid_all_deptemp.DataSource = data_table
                grid_all_deptemp.DataBind()
                connect.Close()
            Else
                MsgBox("Error: Not Found", MsgBoxStyle.Exclamation, "Error")
                Response.Redirect(Request.RawUrl)
            End If
        Catch ex As Exception
            MsgBox("Error: Not Found", MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub

    Protected Sub button_create_department_Click(sender As Object, e As EventArgs) Handles button_create_department.Click
        Dim New_DeptName As String = txt_deptname_create.Text
        Dim New_Remarks As String = txt_remarks_create.Text
        Dim New_Status As String = ddl_status_create.SelectedValue

        Dim enCulture As CultureInfo = CultureInfo.CreateSpecificCulture("en-US")
        Dim currentDate As DateTime = DateTime.Now
        Dim datetime_change_EN As String = currentDate.ToString("yy-MM", CultureInfo.InvariantCulture)
        Dim setdate2 As String = datetime_change_EN.Replace("-", "")
        Dim New_DeptCode As String = setdate2 & "-00" & String.Format("{0}", Integer.Parse(CheckLastedDeptData()) + 1)
        connect.Close()


        Try
            connect.Open()
            If New_DeptName = "" Then
                MsgBox("Error: Can't Not Create", MsgBoxStyle.Exclamation, "Error")
            Else
                If New_Remarks = "" Then
                    Dim command_insert As New SqlCommand($"INSERT INTO dbo.department_list VALUES ('{New_DeptCode}', '{New_DeptName}', NULL, '{New_Status}')", connect)
                    command_insert.ExecuteNonQuery()
                    MsgBox("Successfully Created", MsgBoxStyle.Information, "Message")
                Else
                    Dim command_insert_ As New SqlCommand($"INSERT INTO dbo.department_list VALUES ('{New_DeptCode}', '{New_DeptName}', '{New_Remarks}', '{New_Status}')", connect)
                    command_insert_.ExecuteNonQuery()
                    MsgBox("Successfully Created", MsgBoxStyle.Information, "Message")
                End If
            End If

            connect.Close()
            Load_data()
            Load_data2()
        Catch ex As Exception
            MsgBox("Error: Can't Not Create", MsgBoxStyle.Exclamation, "Error")
        End Try

    End Sub

    Private Function CheckLastedDeptData()

        connect.Open()
        'Dim command_lasted_check As New SqlCommand("SELECT MAX(department_code) AS Lasted_DeptCode FROM dbo.department_list", connect)

        Dim command_lasted_check As New SqlCommand("SELECT MAX(department_code) AS Lasted_DeptCode FROM dbo.department_list", connect)

        Dim read_lasted_check As SqlDataReader = command_lasted_check.ExecuteReader()
        Dim list_lated_check As New List(Of String)()
        While read_lasted_check.Read()
            list_lated_check.Add(read_lasted_check.GetString(0))

        End While
        Dim parts() As String = list_lated_check(0).Split("-"c)
        list_lated_check.Add(parts(1))
        Return list_lated_check(1)
        connect.Close()

    End Function

    Private Function CheckLastedEmpData()

        connect.Open()
        Dim command_lasted_check As New SqlCommand("SELECT 'EMP-00'+convert(nvarchar,max(convert(int,Right(employee_code,3)))+1) FROM employee_list", connect)
        Dim read_lasted_check As SqlDataReader = command_lasted_check.ExecuteReader()
        Dim list_lated_check As New List(Of String)()
        While read_lasted_check.Read()
            list_lated_check.Add(read_lasted_check.GetString(0))

        End While
        Return list_lated_check(0)
        connect.Close()

    End Function

    Protected Sub grid_deptcodename_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grid_deptcodename.SelectedIndexChanged
        txt_empdeptcode_create.Text = HttpUtility.HtmlDecode(grid_deptcodename.SelectedRow.Cells.Item(0).Text.ToString)
    End Sub

    Protected Sub button_create_employee_Click(sender As Object, e As EventArgs) Handles button_create_employee.Click

        Dim EmpDeptCode_Create As String = txt_empdeptcode_create.Text
        Dim Firstname_Create As String = txt_firstname_create.Text
        Dim Lastname_Create As String = txt_lastname_create.Text

        Dim EmpCode_Create As String = CheckLastedEmpData()
        connect.Close()


        Try
            connect.Open()
            If EmpDeptCode_Create <> ":: Select in the table ::" Then
                If Firstname_Create <> "" And Lastname_Create <> "" Then
                    Dim command_emp_create As New SqlCommand($"INSERT INTO employee_list VALUES ('{EmpCode_Create}', '{Firstname_Create}', '{Lastname_Create}', '{EmpDeptCode_Create}')", connect)
                    command_emp_create.ExecuteNonQuery()
                    MsgBox("Successfully Created", MsgBoxStyle.Information, "Message")
                    connect.Close()
                End If
            Else
                MsgBox("Error: Can't Not Create", MsgBoxStyle.Exclamation, "Error")
            End If
            Load_data()
            Load_data2()
        Catch ex As Exception
            MsgBox("Error: Can't Not Create", MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub
End Class
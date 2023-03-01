Imports WebApplication4.ServiceReference1

Partial Public Class AddStudent
    Inherits System.Web.UI.Page

    Public name As String
    Public registeredStudent As List(Of Student) = New List(Of Student)
    Private studentType As String
    Public studentTypeTxt As String
    Public sid As Integer = 0

    Dim ds, ds1, ds2 As New DataSet
    Dim client As New WebService1SoapClient()
    Dim client_web As New localhost.WebService1()

    Protected Sub btnList_Click(sender As Object, e As EventArgs) Handles btnList.Click
        Try
            'client.Endpoint.Binding.SendTimeout = New TimeSpan(0, 0, 0, 0, 10)
            'client.Endpoint.Binding.ReceiveTimeout = New TimeSpan(0, 0, 0, 0, 10)            'timeout in service reference--------
            ds = client.getStudentData()
            'client_web.Timeout = 1000                                                        'timeout in web reference------------
            'ds = client_web.getStudentData()

            GridView1.DataSource = ds
            GridView1.DataBind()
        Catch ex As Exception
            Response.Write("<script> alert('" + ex.Message + "')</script>")
        End Try


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Visible = False
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        name = txtName.Text
        studentType = dpdStudentType.SelectedValue
        studentTypeTxt = dpdStudentType.Text

        txtName.Text = ""
        dpdStudentType.SelectedIndex = -1

        If name IsNot Nothing And studentType <> "" Then
            Label1.Visible = False
            Dim addStudent As Student = Nothing
            Try
                ds1 = client.getStudentData()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            sid = ds1.Tables(0).Rows.Count + 1

            For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                If sid = ds1.Tables(0).Rows(i)("id") Then
                    sid = sid + 1
                End If
            Next

            Select Case studentType
                Case "1"
                    addStudent = New Student(sid, name, "Full Time Student")
                Case "2"
                    addStudent = New Student(sid, name, "Part Time Student")
                Case "3"
                    addStudent = New Student(sid, name, "Co-op Student")
            End Select

            Try
                ds2 = client.addStudent(sid, name, addStudent.Type.ToString)

                If ds2.Tables(0).Rows.Count > 0 Then
                    GridView1.DataSource = ds2
                    GridView1.DataBind()
                Else
                    GridView1.Visible = False
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        Else
            Label1.Visible = True
            Label1.Text = "All Fields are Required!!"
        End If
    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click

        client.deleteStudent(txtCode.Text)

        Try
            ds1 = client.getStudentData()

            If ds1.Tables(0).Rows.Count > 0 Then
                GridView1.DataSource = Nothing
                GridView1.DataSource = ds1
                GridView1.DataBind()
            Else
                GridView1.Visible = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class

Public Class Student
    Public Property ID As String
    Public Property Name As String
    Public Property Type As String
    Public Property RegisteredCourses As List(Of Course)

    Public Sub New(ByVal value As String, ByVal name As String, ByVal stype As String)
        ID = value
        Me.Name = name
        Me.Type = stype
        RegisteredCourses = New List(Of Course)
    End Sub
End Class

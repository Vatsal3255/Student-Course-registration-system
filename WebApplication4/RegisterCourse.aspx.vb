Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail

Partial Public Class RegisterCourse
    Inherits System.Web.UI.Page

    Dim ds, ds1, ds2, ds3 As New DataSet
    Dim helper As Helper = New Helper()
    Dim weeklyHours As Integer = 0
    Dim availableCourses As List(Of Course) = helper.GetAvailableCourses()
    Dim wh As Integer
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("SQL_DB").ConnectionString.ToString())
    Dim da As SqlDataAdapter
    Dim htmString As String

    Dim client As New ServiceReference1.WebService1SoapClient()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If con.State <> ConnectionState.Open Then con.Open()

        If IsPostBack = False Then
            GridView1.Visible = False
            Try
                ds = client.getStudentData()
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
            drpStudent.Items.Clear()
            drpStudent.Items.Add(New ListItem("Select...", "-1"))
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim id As String = ds.Tables(0).Rows(i)("Id")
                Dim name As String = ds.Tables(0).Rows(i)("Name")
                Dim type As String = ds.Tables(0).Rows(i)("Type")
                drpStudent.Items.Add(New ListItem(newListItem(id, name, type), ds.Tables(0).Rows(i)("ID")))
            Next
            For i As Integer = 0 To availableCourses.Count - 1
                Dim c As Course = availableCourses(i)
                CheckBoxList1.Items.Add(c.Title + " - " + c.WeeklyHours + " " + " hours/week")
            Next
        End If
    End Sub



    Public Function newListItem(id As String, name As String, type As String) As String
        Return id + " - " + name + " (" + type + ")"
    End Function

    Protected Sub btnEmail_Click(sender As Object, e As EventArgs) Handles btnEmail.Click

        da = New SqlDataAdapter("select * from COURSE_REGISTERED", con)
        ds3.Clear()
        da.Fill(ds3)

        htmString = getHtml(ds3)

        Email(htmString)
    End Sub

    Protected Sub drpStudent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpStudent.SelectedIndexChanged
        studentCoursesLabel.Text = ""
        alert1.Text = ""
    End Sub

    Public Sub Email(htmlString As String)
        Try
            Dim message As New MailMessage()
            Dim smtp As New SmtpClient()

            message.From = New MailAddress("vatsalpatel2035@gmail.com")
            message.To.Add(New MailAddress("vatsalpatel2035@gmail.com"))
            message.ReplyToList.Add("vatsalpatel2035@gmail.com")
            message.Priority = MailPriority.Low
            message.CC.Add(New MailAddress("pvatb007@gmail.com"))
            message.Bcc.Add(New MailAddress("patelvatsal3255@gmail.com"))
            message.Attachments.Add(New Attachment("D:\barcode.ico"))
            message.Subject = "COURSE REGISTERED STUDENT DATA"
            message.IsBodyHtml = True
            message.Body = htmlString
            smtp.Port = 587
            smtp.Host = "smtp.gmail.com"
            smtp.UseDefaultCredentials = False
            smtp.Credentials = New NetworkCredential("vatsalpatel2035@gmail.com", "ihnlhkzfnkhwiusl")
            smtp.EnableSsl = True
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network
            smtp.Send(message)
            ''Response.Write("<script >alert('Mail Sent Successfully')</script>")

        Catch ex As Exception

        End Try
    End Sub

    Public Function getHtml(ds5 As DataSet) As String
        Try
            Dim messageBody As String = "<font>The following are the Courses that students registered: </font><br><br>"
            If ds5.Tables(0).Rows.Count = 0 Then Return messageBody

            Dim htmlTableStart As String = "<table style=""border-collapse:collapse; text-align:center;"">"
            Dim htmlTableEnd As String = "</table>"
            Dim htmlHeaderRowStart As String = "<tr ""background-color:#6FA1D2; color:#ffffff;"">"

            Dim htmlHeaderRowEnd As String = "</tr>"
            Dim htmlTrStart As String = "<tr style=""color:#555555;"">"
            Dim htmlTrEnd As String = "</tr>"
            Dim htmlTdStart As String = "<td style="" border-color: #5c87b2; border-style:dotted; border-width:thin; padding: 5px;"">"
            Dim htmlTdEnd As String = "</td>"

            messageBody += htmlTableStart
            messageBody += htmlHeaderRowStart
            messageBody += htmlTdStart + "Student ID" + htmlTdEnd
            messageBody += htmlTdStart + "Student Name" + htmlTdEnd
            messageBody += htmlTdStart + "Course ID" + htmlTdEnd
            messageBody += htmlTdStart + "Course Title" + htmlTdEnd
            messageBody += htmlTdStart + "Course Weekly Hours" + htmlTdEnd
            messageBody += htmlHeaderRowEnd

            For i As Integer = 0 To ds5.Tables(0).Rows.Count - 1
                messageBody = messageBody + htmlTrStart
                messageBody = messageBody + htmlTdStart + ds5.Tables(0).Rows(i)("STUDENT_ID").ToString() + htmlTdEnd
                messageBody = messageBody + htmlTdStart + ds5.Tables(0).Rows(i)("STUDENT_NAME").ToString() + htmlTdEnd
                messageBody = messageBody + htmlTdStart + ds5.Tables(0).Rows(i)("COURSE_ID").ToString() + htmlTdEnd
                messageBody = messageBody + htmlTdStart + ds5.Tables(0).Rows(i)("COURSE_tITLE").ToString() + htmlTdEnd
                messageBody = messageBody + htmlTdStart + ds5.Tables(0).Rows(i)("COURSE_WHOURS").ToString() + htmlTdEnd
                messageBody = messageBody + htmlTrEnd
            Next

            messageBody = messageBody + htmlTableEnd
            Return messageBody
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Protected Sub Main(sender As Object, e As EventArgs)
        studentCoursesLabel.Text = ""
        alert1.Text = ""
        Dim selectedCourses As List(Of Course) = New List(Of Course)
        For i As Integer = 0 To availableCourses.Count - 1
            Dim c As Course = availableCourses(i)

            If CheckBoxList1.Items(i).Selected Then

                Dim selectedCourse As Course = availableCourses(i)
                weeklyHours += c.WeeklyHours
                selectedCourses.Add(selectedCourse)

                Try
                    ds2.Clear()
                    ds2 = client.getCourseRegisteredStudents(drpStudent.SelectedValue.Trim)
                    wh = 0
                    For k As Integer = 0 To ds2.Tables(0).Rows.Count - 1
                        wh = wh + ds2.Tables(0).Rows(k)("course_whours")
                    Next

                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End If
        Next

        If selectedCourses.Count > 0 Then
            If drpStudent.SelectedItem.Text.Contains("Part Time Student") Then
                If selectedCourses.Count > 3 Or ds2.Tables(0).Rows.Count > 3 Or selectedCourses.Count + ds2.Tables(0).Rows.Count > 3 Then
                    alert1.Text = "You can select only 3 courses."
                    GoTo MOVE_HERE2
                End If
            End If
            If drpStudent.SelectedItem.Text.Contains("Co-op Student") Then
                If weeklyHours > 6 Or weeklyHours + wh > 6 Or wh > 6 Or selectedCourses.Count > 3 Or ds2.Tables(0).Rows.Count > 3 Or selectedCourses.Count + ds2.Tables(0).Rows.Count > 3 Then
                    alert1.Text = "You can select only 3 courses as well as only 6 hours per week."
                    GoTo MOVE_HERE2
                End If
            End If
        End If

        If selectedCourses.Count = 0 Then
            alert1.Text = "Select at least 1 course"
            Return
        End If

        Try
            ds = client.getStudentData()

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If drpStudent.SelectedValue.Trim = ds.Tables(0).Rows(i)("id").ToString.Trim Then
                    For Each c As Course In selectedCourses
                        ds1.Clear()
                        ds1 = client.getCourseRegisteredStudents(ds.Tables(0).Rows(i)("id"))
                        For j As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                            If c.Code.Trim = ds1.Tables(0).Rows(j)("course_id").ToString.Trim Then
                                alert1.Text = "You have already registered the selected course(s)."
                                GoTo MOVE_HERE
                            End If
                        Next
                        client.registerNewCourse(ds.Tables(0).Rows(i)("id"), ds.Tables(0).Rows(i)("name"), c.Code, c.Title, c.WeeklyHours)
MOVE_HERE:
                    Next
                Else
                End If
            Next
            CheckBoxList1.ClearSelection()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        studentCoursesLabel.Text = String.Format("Selected student has registered {0} course(s), {1} hours weekly.", selectedCourses.Count, weeklyHours)
MOVE_HERE2:
    End Sub

    Protected Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        If drpStudent.SelectedValue Is Nothing Then alert1.Text = "Please select student."

        Try
            ds1.Clear()
            ds1 = client.getAllRegisteredCourses(drpStudent.SelectedValue)

            If ds1.Tables(0).Rows.Count > 0 Then
                GridView1.Visible = True
                GridView1.DataSource = ds1
                GridView1.DataBind()
            Else
                alert1.Text = "You have not registered any course(s) yet."
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
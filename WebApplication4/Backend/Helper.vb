Public Class Helper

    Dim ds As New DataSet
    Dim course As Course
    Dim courses As List(Of Course) = New List(Of Course)
    Dim client As New ServiceReference1.WebService1SoapClient()

    Public Function GetAvailableCourses() As List(Of Course)
        Try
            ds = client.listCourse()

            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    course = New Course(ds.Tables(0).Rows(i)("course_id").ToString(), ds.Tables(0).Rows(i)("course_name").ToString())
                    course.WeeklyHours = ds.Tables(0).Rows(i)("c_weekly_hours")
                    courses.Add(course)
                Next i
                Return courses
            Else
                Throw New Exception("Course data not found.")
            End If
            Return courses
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

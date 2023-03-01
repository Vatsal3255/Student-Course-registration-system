Public Class Course

    Private scode As String
    Private stitle As String
    Private sweeklyHours As Integer


    Public ReadOnly Property Code() As String
        Get
            Return scode
        End Get
    End Property

    Public ReadOnly Property Title() As String
        Get
            Return stitle
        End Get
    End Property

    Public Property WeeklyHours() As String
        Get
            Return sweeklyHours
        End Get
        Set(value As String)
            sweeklyHours = value
        End Set
    End Property

    Public Sub New(ByVal code As String, ByVal title As String)
        Me.scode = code
        Me.stitle = title
    End Sub
End Class

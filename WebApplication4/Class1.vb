Public Class Class1
    Public Function addition(num1 As Integer, num2 As Integer) As Integer
        Dim res = num1 + num2
        Return res
    End Function

    Public Function getTheGreetings(dt As DateTime) As String
        Return IIf(dt.ToString("dd-MM-yyyy hh:mm tt").ToUpper().Contains("AM"), "Good Morning!!", "Good Afternoon!!")
    End Function
End Class

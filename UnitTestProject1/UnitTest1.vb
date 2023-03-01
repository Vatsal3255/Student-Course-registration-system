Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports WebApplication4

<TestClass()> Public Class UnitTest1

    <TestMethod()> Public Sub AdditionTestingMethod1()
        Dim c1 As New Class1()
        Dim num1 = 12
        Dim num2 = 12

        Dim chk = c1.addition(num1, num2)
        Assert.AreEqual(24, chk)
    End Sub

    <TestMethod()> Public Sub AdditionTestingMethod2()
        Dim c1 As New Class1()
        Dim num1 = 23
        Dim num2 = 27

        Dim chk = c1.addition(num1, num2)
        Assert.AreEqual(50, chk)
    End Sub

    <TestMethod()> Public Sub GreetingsTestingMethod()
        Dim c1 As New Class1()

        Dim dt As String = DateTime.Now

        Dim chk = c1.getTheGreetings(dt)
        Assert.AreEqual("Good Afternoon!!", chk)
    End Sub

    <TestMethod()> Public Sub GreetingsTestingMethod1()
        Dim c1 As New Class1()

        Dim dt As String = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt")

        Dim chk = c1.getTheGreetings(Convert.ToDateTime(dt))
        Assert.AreEqual("Good Afternoon!!", chk)
    End Sub

    <TestMethod()> Public Sub GreetingsTestingMethod2()
        Dim c1 As New Class1()

        Dim dt As String = DateTime.Now.ToString("dd-MM-yyyy HH:mm")

        Dim chk = c1.getTheGreetings(Convert.ToDateTime(dt))
        Assert.AreEqual("Good Afternoon!!", chk)
    End Sub

    <TestMethod()> Public Sub GreetingsTestingMethod3()
        Dim c1 As New Class1()

        Dim dt As DateTime = DateTime.ParseExact("07-02-2023 09:27:34 AM", "dd-MM-yyyy hh:mm:ss tt", Nothing)

        Dim chk = c1.getTheGreetings(dt)
        Assert.AreEqual("Good Morning!!", chk)
    End Sub

End Class
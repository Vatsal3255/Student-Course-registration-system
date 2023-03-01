Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser

Public Class pdfRead
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If fuPdfUpload.HasFile Then
            Dim currentText As String
            Dim Text As StringBuilder = New StringBuilder()
            Dim pdfReader As PdfReader = New PdfReader(fuPdfUpload.FileBytes)
            Dim ds As New DataSet()

            For page As Integer = 1 To pdfReader.NumberOfPages
                Dim strategy As ITextExtractionStrategy = New LocationTextExtractionStrategy()
                currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy)
                currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.UTF8.GetBytes(currentText)))
                Text.Append(currentText)
                If Not page.Equals(pdfReader.NumberOfPages) Then
                    Text.Append(vbLf)
                End If
            Next
            pdfReader.Close()
            Dim data As String() = Text.ToString().Split(vbLf)
            Dim dt As DataTable = New DataTable("PdfTable")

            For i As Integer = 0 To data.Length - 1
                data(i) = data(i).Replace("  ", " ")
                data(i) = data(i).Replace("  ", " ")
                data(i) = data(i).Replace("  ", " ")
                data(i) = data(i).Replace("  ", " ")
                data(i) = data(i).Replace("  ", " ")
                data(i) = data(i).Replace("  ", " ")
            Next

            Dim headers As String() = data(0).Split(" "c)

            For j As Integer = 0 To headers.Length - 1
                If Not String.IsNullOrEmpty(headers(j)) Then
                    dt.Columns.Add(headers(j), GetType(String))
                End If
            Next
            Dim abc = dt.Columns.Count
            For i As Integer = 1 To data.Length - 1
                Dim content As String() = data(i).Split(" ")
                dt.Rows.Add()
                Dim head As Integer = -1
                For k As Integer = 0 To content.Length - 1
                    If Not content(k).Equals("") Then
                        head += 1
                    End If
                    If headers(head).Equals("SWIPEDATETIME") Or headers(head).Equals("PUNCH") Then
                        dt.Rows(dt.Rows.Count - 1)(head) = content(k) + " " + content(k + 1)
                        k = k + 1
                    ElseIf headers(head).Equals("Date") Then
                        dt.Rows(dt.Rows.Count - 1)(head) = content(k) + " " + content(k + 1) + " " + content(k + 2)
                        k = k + 2
                    ElseIf headers(head).Equals("INSERTED") And content(k).Length > 6 Then
                        dt.Rows(dt.Rows.Count - 1)(head) = content(k) + " " + content(k + 1)
                        k = k + 1
                    ElseIf headers(head).Equals("punchdate") And content(k).Length > 6 Then
                        dt.Rows(dt.Rows.Count - 1)(head) = content(k) + " " + content(k + 1)
                        k = k + 1
                    Else
                        If Not String.IsNullOrEmpty(content(k)) Then
                            dt.Rows(dt.Rows.Count - 1)(head) = content(k)
                        End If
                    End If

                Next
            Next
            ds.Clear()
            ds.Tables.Add(dt.Copy())
            GridView1.DataSource = ds.Tables(0).DefaultView
            GridView1.DataBind()
        End If
    End Sub
End Class
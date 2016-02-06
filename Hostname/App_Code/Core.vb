Imports Microsoft.VisualBasic

Public Module Core
    Public Sub WriteResponse(Request As HttpRequest, Response As HttpResponse)
        Const formatString As String = "{0,-11}"
        Response.ContentType = "text/plain"
        Response.Write(String.Format(formatString, "Hostname: "))
        Try
            Response.Write(Net.Dns.GetHostEntry(Request.UserHostAddress).HostName & vbCrLf)
        Catch ex As Exception
            Response.Write("N/A" & vbCrLf)
        End Try
        Response.Write(String.Format(formatString, "IP: ") & Request.UserHostAddress & vbCrLf)
        Response.Write(String.Format(formatString, "UserAgent: ") & Request.UserAgent & vbCrLf)
    End Sub
End Module

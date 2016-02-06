
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Request.UserAgent.StartsWith("curl") Then
            ' Users with curl may not want extra HTML and stuff
            Core.WriteResponse(Request, Response)
            Response.Write(vbCrLf & "Simplified output for curl. Change your UserAgent for HTML output." & vbCrLf)
            Response.End()
        End If

        Dim IPv6 As Boolean = Request.UserHostAddress.Contains(":")
        If Not IPv6 Then
            IPv4Javascript.Visible = False
        End If
        Try
            Hostname.Text = Net.Dns.GetHostEntry(Request.UserHostAddress).HostName
        Catch ex As Exception
            Hostname.Text = "N/A"
        End Try
        IPLabel.Text = Request.UserHostAddress
        UserAgent.Text = Request.UserAgent
    End Sub
End Class

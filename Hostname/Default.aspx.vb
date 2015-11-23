
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
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


Partial Class plain_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Core.WriteResponse(Request, Response)
    End Sub
End Class

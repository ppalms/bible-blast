
Partial Class AddUser
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not My.User.IsAuthenticated Then
            Response.Redirect("~/login.aspx", True)
        End If
    End Sub

End Class

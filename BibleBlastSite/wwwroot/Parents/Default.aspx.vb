
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Session("SearchName") = txtSearch.Text
        Dim path As String = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf("/"))
        Response.Redirect(path & "/search.aspx", True)
    End Sub

    Protected Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        btnSearch_Click(sender, e)
    End Sub

End Class

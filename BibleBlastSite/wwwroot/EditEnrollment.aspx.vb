
Partial Class EditEnrollment
    Inherits System.Web.UI.Page

    Protected Sub btnNewKid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewKid.Click
        DetailsView2.Visible = Not DetailsView2.Visible
        GridView1.Visible = Not GridView1.Visible
    End Sub

    Protected Sub DetailsView2_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewInsertedEventArgs) Handles DetailsView2.ItemInserted
        DetailsView2.Visible = False
        GridView1.Visible = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not My.User.IsAuthenticated Then
        'Response.Redirect("~/login.aspx", True)
        'End If
    End Sub

    Protected Sub btnFamily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFamily.Click
        DetailsView1.Visible = Not DetailsView1.Visible
        GridView2.Visible = Not GridView2.Visible
    End Sub

    Protected Sub DetailsView1_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewInsertedEventArgs) Handles DetailsView1.ItemInserted
        DetailsView1.Visible = False
        GridView2.Visible = True
    End Sub

End Class

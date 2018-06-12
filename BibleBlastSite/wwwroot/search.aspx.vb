
Partial Class search
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("SearchName") Is Nothing Then
            txtSearch.Text = Session("SearchName")
            Session.Remove("SearchName")
        End If
        Try
            ' Membership.CreateUser("rob102", "P@ssw0rd")
            'Membership.DeleteUser("mommy99")
            'ProfileManager.DeleteProfile("mommy99")
        Catch exc As Exception
        End Try
    End Sub

    Public Function SelectedID() As Int32
        Try
            Return GridView1.SelectedValue
        Catch ex As Exception
        End Try
        Return -1
    End Function

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Accordion1.DataBind()
        Accordion1.Visible = True
        txtSearch.Enabled = True
        btnSearch.Enabled = True
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Session.Add("SearchName", txtSearch.Text)
        Accordion1.Visible = False
        GridView1.DataBind()
        GridView1.SelectedIndex = -1
        txtSearch.Enabled = False
        btnSearch.Enabled = False
    End Sub

    Protected Sub UpdatePanel1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdatePanel1.Load
        Accordion1.DataBind()
    End Sub

    Protected Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        btnSearch_Click(sender, e)
    End Sub
End Class

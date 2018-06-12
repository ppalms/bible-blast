
Partial Class EditAwards
    Inherits System.Web.UI.Page

    Protected Sub btnAwards_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAwards.Click
        gvAwards.Visible = Not gvAwards.Visible
        dvAwards.Visible = Not dvAwards.Visible
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        'Response.Write(e.CommandSource.ToString & "<br/>" & e.CommandName & "<br/>" & e.CommandArgument & "<br/>")
        If e.CommandName = "AddQuestion" Then
            Dim conn As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("BBData").ConnectionString)
            Dim inscom As String = "INSERT INTO AwardQuestions(AwardID, QuestionID) VALUES (" & gvAwards.SelectedValue & "," & e.CommandArgument & ");"
            Dim comm As New Data.SqlClient.SqlCommand(inscom, conn)
            conn.Open()
            comm.ExecuteNonQuery()
            conn.Close()
        End If
        gvAwardQuestions.DataBind()
        GridView1.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not My.User.IsAuthenticated Then
        'Response.Redirect("~/login.aspx", True)
        'End If
    End Sub

End Class

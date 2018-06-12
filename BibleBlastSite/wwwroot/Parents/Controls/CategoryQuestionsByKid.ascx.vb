
Partial Class Controls_CategoryQuestionsByKid
    Inherits System.Web.UI.UserControl

    'Private _KidID As Integer
    Private _CategoryID As Integer
    Private _CategoryName As String

    Public Property KidID() As Integer
        Get
            Return Session("KidID")
        End Get
        Set(ByVal value As Integer)
            Session("KidID") = value
        End Set
    End Property
    Public Property CategoryID() As Integer
        Get
            Return _CategoryID
        End Get
        Set(ByVal value As Integer)
            _CategoryID = value
        End Set
    End Property

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        SqlDataSource1.SelectParameters.Item("CategoryID").DefaultValue = Me.CategoryID
    End Sub

    Private Function AnswerDate(ByVal KidID As Integer, ByVal QuestionID As Integer) As String
        Dim conn As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("BBData").ConnectionString)
        Dim selcom As String = "SELECT Date FROM QuestionAnswers WHERE KidID=" & KidID & " AND QuestionID=" & QuestionID & ";"
        Dim comm As New Data.SqlClient.SqlCommand(selcom, conn)
        conn.Open()
        Dim rdr As Data.SqlClient.SqlDataReader = comm.ExecuteReader
        If rdr.Read() Then
            Dim d As Date = rdr.Item("Date")
            AnswerDate = d.ToString("d")
        Else
            AnswerDate = "Today!"
        End If
        conn.Close()
    End Function

    Private Sub AnswerQuestion(ByVal KidID As Integer, ByVal QuestionID As String)
        Dim conn As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("BBData").ConnectionString)
        Dim inscom As String = "INSERT INTO QuestionAnswers(KidID,QuestionID,SubmittedBy) VALUES (" & KidID & "," & QuestionID & ",'" & My.User.Name.ToString & "');"
        Dim comm As New Data.SqlClient.SqlCommand(inscom, conn)
        conn.Open()
        comm.ExecuteNonQuery()
        conn.Close()
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "AnswerQuestion" Then
            Dim KID As Integer = Integer.Parse(e.CommandArgument.ToString.Split("~")(0))
            Dim QID As Integer = Integer.Parse(e.CommandArgument.ToString.Split("~")(1))
            AnswerQuestion(KID, QID)
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowIndex >= 0 Then
            Dim btn As Button = e.Row.Cells(2).Controls.Item(0)
            btn.CommandName = "AnswerQuestion"
            btn.CommandArgument = Me.KidID.ToString & "~" & e.Row.Cells(3).Text
            btn.Text = AnswerDate(Me.KidID, e.Row.Cells(3).Text)
            If Not My.User.IsAuthenticated And btn.Text = "Today!" Then
                btn.Text = "Not Completed"
                btn.Enabled = False
            End If
            If Not btn.Text = "Today!" Then
                btn.Enabled = False
            End If
        End If
        e.Row.Cells.RemoveAt(3)
        If e.Row.Cells(1).Text.Contains(":") Or e.Row.Cells(1).Text.Contains("-") Or e.Row.Cells(1).Text.Contains(",") Then
            Dim link As New HyperLink
            link.Text = e.Row.Cells(1).Text
            link.Target = "_blank"
            link.NavigateUrl = "http://www.biblegateway.com/passage/?version=31;&search=" & link.Text
            e.Row.Cells(1).Text = ""
            e.Row.Cells(1).Controls.Add(link)
        End If
    End Sub
End Class

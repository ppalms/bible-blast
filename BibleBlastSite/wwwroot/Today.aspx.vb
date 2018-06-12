
Partial Class Today
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
 
        If Not My.User.IsAuthenticated Then
            Response.Redirect("~/login.aspx", True)
        End If


        Dim conn As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("BBData").ConnectionString)
        conn.Open()
        Dim comm As Data.SqlClient.SqlCommand
        Dim selcom As String
        Dim rdr As Data.SqlClient.SqlDataReader

        selcom = "SELECT Points FROM vPointsToday WHERE IsMale=1;"
        comm = New Data.SqlClient.SqlCommand(selcom, conn)
        rdr = comm.ExecuteReader
        If rdr.Read Then
            lblTodayBoys.Text = rdr.Item("Points")
        End If
        rdr.Close()

        selcom = "SELECT Points FROM vPointsToday WHERE IsMale=0;"
        comm = New Data.SqlClient.SqlCommand(selcom, conn)
        rdr = comm.ExecuteReader
        If rdr.Read Then
            lblTodayGirls.Text = rdr.Item("Points")
        End If
        rdr.Close()

        Dim TodayTotal As Integer = Integer.Parse(lblTodayBoys.Text) + Integer.Parse(lblTodayGirls.Text)
        lblTodayTotal.Text = TodayTotal

        selcom = "SELECT Points FROM vPointsYear WHERE IsMale=1;"
        comm = New Data.SqlClient.SqlCommand(selcom, conn)
        rdr = comm.ExecuteReader
        If rdr.Read Then
            lblYearBoys.Text = rdr.Item("Points")
        End If
        rdr.Close()

        selcom = "SELECT Points FROM vPointsYear WHERE IsMale=0;"
        comm = New Data.SqlClient.SqlCommand(selcom, conn)
        rdr = comm.ExecuteReader
        If rdr.Read Then
            lblYearGirls.Text = rdr.Item("Points")
        End If
        rdr.Close()

        Dim YearTotal As Integer = Integer.Parse(lblYearBoys.Text) + Integer.Parse(lblYearGirls.Text)
        lblYearTotal.Text = YearTotal

        conn.Close()
    End Sub
End Class

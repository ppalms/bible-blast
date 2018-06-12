<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Today.aspx.vb" Inherits="Today" title="Untitled Page" %>

<%-- Add content controls here --%>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    What Happened in Memory Today&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;( <%=Now.ToShortDateString%> )                        
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder2">


    <div style="width: 20%; float: left; text-align: center;">
        <span style="text-size: xx-large; font-weight: bold;">Points Today:</span><br/>
        Boys: <asp:Label ID="lblTodayBoys" runat="server" Text="0"></asp:Label><br />
        Girls: <asp:Label ID="lblTodayGirls" runat="server" Text="0"></asp:Label><br />
        <strong>Total: <asp:Label ID="lblTodayTotal" runat="server" Text="0"></asp:Label></strong>
    </div>
    <div style="width: 20%; float: left; text-align: center;">
        <span style="text-size: xx-large; font-weight: bold;">Year's Points:</span><br/>
        Boys: <asp:Label ID="lblYearBoys" runat="server" Text="0"></asp:Label><br />
        Girls: <asp:Label ID="lblYearGirls" runat="server" Text="0"></asp:Label><br />
        <strong>Total: <asp:Label ID="lblYearTotal" runat="server" Text="0"></asp:Label></strong>
    </div>
    <div style="width: 20%; float: left; text-align: center;">
        <span style="text-size: xx-large; font-weight: bold;">Bonus Today:</span><br/>
        Boys: <br />
        Girls: <br />
        <strong>Total: </strong>
    </div>
    <div style="width: 20%; float: left; text-align: center;">
        <span style="text-size: xx-large; font-weight: bold;">Bonus Total:</span><br/>
        Boys: <br />
        Girls: <br />
        <strong>Total: </strong>
    </div>
    <div style="width: 20%; float: left; text-align: center;"
        <span style="text-size: xx-large; font-weight: bold;">Grand Total:</span><br/>
        Boys: <br />
        Girls: <br />
        <strong>Total: </strong>
    </div>




    <div style="clear: left; float: left; padding: 2%;">
        <span style="text-size: xx-large; font-weight: bold;">ABCs:</span><asp:SqlDataSource ID="dsABC" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BBData %>" 
            SelectCommand="SELECT Kids.FirstName, Kids.LastName, COUNT(QuestionAnswers.SubmittedBy) AS Number FROM QuestionAnswers INNER JOIN Kids ON QuestionAnswers.KidID = Kids.KidID INNER JOIN Questions ON QuestionAnswers.QuestionID = Questions.QuestionID WHERE (Convert(Date,QuestionAnswers.Date) = dbo.DateOnly(DATEADD (hh, -5, GETDATE()))) AND (Questions.CategoryID = 1) GROUP BY Kids.LastName, Kids.FirstName, Questions.CategoryID ORDER BY Kids.LastName"></asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataSourceID="dsABC" ForeColor="#333333" GridLines="None" 
            AllowSorting="True">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="First" 
                    SortExpression="FirstName">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="LastName" HeaderText="Last" 
                    SortExpression="LastName">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Number" HeaderText="Number" ReadOnly="True" 
                    SortExpression="Number" />
            </Columns>
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    </div>
    <div style="clear: right; float: right; padding: 2%;">
        <span style="text-size: xx-large; font-weight: bold;">XYZs:</span><asp:SqlDataSource ID="dsXYZ" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BBData %>" 
            SelectCommand="SELECT Kids.FirstName, Kids.LastName, COUNT(QuestionAnswers.SubmittedBy) AS Number FROM QuestionAnswers INNER JOIN Kids ON QuestionAnswers.KidID = Kids.KidID INNER JOIN Questions ON QuestionAnswers.QuestionID = Questions.QuestionID WHERE (Convert(Date,QuestionAnswers.Date) = dbo.DateOnly(DATEADD (hh, -5, GETDATE()))) AND (Questions.CategoryID = 2) GROUP BY Kids.LastName, Kids.FirstName, Questions.CategoryID ORDER BY Kids.LastName"></asp:SqlDataSource>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataSourceID="dsXYZ" ForeColor="#333333" GridLines="None" 
            AllowSorting="True">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="First" 
                    SortExpression="FirstName">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="LastName" HeaderText="Last" 
                    SortExpression="LastName">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Number" HeaderText="Number" ReadOnly="True" 
                    SortExpression="Number" />
            </Columns>
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    </div>
    <div style="clear: both; padding: 2%;">
        <span style="text-size: xx-large; font-weight: bold;">Awards:</span><asp:SqlDataSource ID="dsTodaysAwards" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BBData %>" 
            SelectCommand="SELECT * FROM [vAwardsCompletedDetailToday] ORDER BY [Title], [Description], [LastName], [FirstName]"></asp:SqlDataSource>                    
        <asp:GridView ID="gvAwards" runat="server" 
            AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
            DataSourceID="dsTodaysAwards" ForeColor="#333333" GridLines="None">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="First" 
                    SortExpression="FirstName">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="LastName" HeaderText="Last" 
                    SortExpression="LastName">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="Description" HeaderText="Description" 
                    SortExpression="Description" />
            </Columns>
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        </div>
    <div style="clear: left; float: left; padding: 2%;">
        <span style="text-size: xx-large; font-weight: bold;">Top Scoring Boys:</span><br />
       <asp:SqlDataSource ID="dsTopBoys" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BBData %>" 
            SelectCommand="SELECT Kids.FirstName, Kids.LastName, SUM(Questions.Points) AS Points FROM Questions INNER JOIN QuestionAnswers ON Questions.QuestionID = QuestionAnswers.QuestionID INNER JOIN Kids ON QuestionAnswers.KidID = Kids.KidID WHERE (Kids.IsMale = 1) AND (Convert(Date,QuestionAnswers.Date) = dbo.DateOnly(DATEADD (hh, -5, GETDATE()))) GROUP BY QuestionAnswers.KidID, Kids.LastName, Kids.FirstName ORDER BY Points DESC"></asp:SqlDataSource>
        <asp:GridView ID="gvTopBoys" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
            DataSourceID="dsTopBoys" ForeColor="#333333" GridLines="None" PageSize="3">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="First" 
                    SortExpression="FirstName" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="LastName" HeaderText="Last" 
                    SortExpression="LastName" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Points" HeaderText="Points" ReadOnly="True" 
                    SortExpression="Points" />
            </Columns>
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    </div>
    <div style="clear: right; float: right; padding: 2%;">
        <span style="text-size: xx-large; font-weight: bold;">Top Scoring Girls:</span><br />
        <asp:SqlDataSource ID="dsTopGirls" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BBData %>" 
            SelectCommand="SELECT Kids.FirstName, Kids.LastName, SUM(Questions.Points) AS Points FROM Questions INNER JOIN QuestionAnswers ON Questions.QuestionID = QuestionAnswers.QuestionID INNER JOIN Kids ON QuestionAnswers.KidID = Kids.KidID WHERE (Kids.IsMale = 0) AND (Convert(Date,QuestionAnswers.Date) = dbo.DateOnly(DATEADD (hh, -5, GETDATE()))) GROUP BY QuestionAnswers.KidID, Kids.LastName, Kids.FirstName ORDER BY Points DESC"></asp:SqlDataSource>
        <asp:GridView ID="gvTopGirls" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
            DataSourceID="dsTopGirls" ForeColor="#333333" GridLines="None" PageSize="3">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="FirstName" HeaderText="First" 
                    SortExpression="FirstName" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="LastName" HeaderText="Last" 
                    SortExpression="LastName" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Points" HeaderText="Points" ReadOnly="True" 
                    SortExpression="Points" />
            </Columns>
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
       </asp:GridView>
    </div>
</asp:Content>



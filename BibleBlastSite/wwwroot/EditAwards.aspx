<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="EditAwards.aspx.vb" Inherits="EditAwards" title="Untitled Page" %>

<%-- Add content controls here --%>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
                        Edit Awards 
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder2">
    <asp:SqlDataSource ID="dsAwards" runat="server" 
        ConnectionString="<%$ ConnectionStrings:BBData %>" 
        DeleteCommand="DELETE FROM [Awards] WHERE [AwardID] = @AwardID" 
        InsertCommand="INSERT INTO [Awards] ([Title], [Description], [Cost]) VALUES (@Title, @Description, @Cost)" 
        SelectCommand="SELECT * FROM [Awards]" 
        UpdateCommand="UPDATE [Awards] SET [Title] = @Title, [Description] = @Description WHERE [AwardID] = @AwardID">
        <DeleteParameters>
            <asp:Parameter Name="AwardID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="AwardID" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="Cost" Type="Decimal" />
        </InsertParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsAwardQuestions" runat="server" 
        ConnectionString="<%$ ConnectionStrings:BBData %>" 
        SelectCommand="SELECT * FROM [vAwardQuestions] WHERE ([AwardID] = @AwardID)" 
        DeleteCommand="DELETE FROM [AwardQuestions] WHERE [AwardID] = @AwardID AND [QuestionID] = @QuestionID">
        <SelectParameters>
            <asp:ControlParameter ControlID="gvAwards" Name="AwardID" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="AwardID" Type="Int32" />
            <asp:Parameter Name="QuestionID" Type="Int32" />
        </DeleteParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsQuestionView" runat="server" 
        ConnectionString="<%$ ConnectionStrings:BBData %>" 
        SelectCommand="SELECT * FROM vQuestions WHERE QuestionID NOT IN (SELECT QuestionID FROM AwardQuestions WHERE AwardID=@AwardID) ORDER BY Question">
        <SelectParameters>
            <asp:ControlParameter ControlID="gvAwards" Name="AwardID" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <div style="float: left;">
        <asp:Button ID="btnAwards" runat="server" Text="Add New Award" />
        <br /><br />
        <asp:DetailsView ID="dvAwards" runat="server" Height="50px" 
            CellPadding="4" ForeColor="#333333" GridLines="None" 
            AutoGenerateRows="False" DataKeyNames="AwardID" DataSourceID="dsAwards" 
            DefaultMode="Insert" Visible="False">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <Fields>
                <asp:BoundField DataField="AwardID" HeaderText="AwardID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="AwardID" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="Description" HeaderText="Description" 
                    SortExpression="Description" />
                <asp:BoundField DataField="Cost" DataFormatString="{0:c}" HeaderText="Cost" 
                    HtmlEncode="False" SortExpression="Cost" />
                <asp:CommandField ShowInsertButton="True" />
            </Fields>
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:DetailsView>
        <asp:GridView ID="gvAwards" runat="server" CellPadding="4" ForeColor="#333333" 
            GridLines="None" AllowPaging="True" AllowSorting="True" 
            AutoGenerateColumns="False" DataKeyNames="AwardID" DataSourceID="dsAwards">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                    ShowSelectButton="True" />
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
    <div style="float: right; margin: 20px;">
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" 
            DataSourceID="dsQuestionView" ForeColor="#333333" GridLines="None" 
            AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="QuestionID">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="Question" HeaderText="Available Questions" ReadOnly="True" 
                    SortExpression="Question" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:button ID="btnAdd" CommandName="AddQuestion" CommandArgument='<%# Eval("QuestionID") %>' 
                            runat="server" Text="Add" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    </div>
    <div style="float: right; margin: 20px;">
        <asp:GridView ID="gvAwardQuestions" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataSourceID="dsAwardQuestions" ForeColor="#333333" 
            GridLines="None" AllowPaging="True">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Requirements" SortExpression="Title" />
            </Columns>
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    </div>
</asp:Content>


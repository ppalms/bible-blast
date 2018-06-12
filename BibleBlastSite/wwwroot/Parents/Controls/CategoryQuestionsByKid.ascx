<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CategoryQuestionsByKid.ascx.vb" Inherits="Controls_CategoryQuestionsByKid" %>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:BBData %>" 
    SelectCommand="SELECT * FROM [Questions] WHERE ([CategoryID] = @CategoryID)">
    <SelectParameters>
        <asp:Parameter Name="CategoryID" Type="Int32" DefaultValue="0" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    CellPadding="3" DataKeyNames="QuestionID" DataSourceID="SqlDataSource1" 
    GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" 
    BorderWidth="1px">
    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
    <Columns>
        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
        <asp:BoundField DataField="Description" HeaderText="Description" 
            SortExpression="Description" />
        <asp:ButtonField ButtonType="Button" CommandName="KidAnsweredQuestion" 
            HeaderText="Completed On" InsertVisible="False" ShowHeader="True" Text="-error-">
            <ItemStyle HorizontalAlign="Right" />
        </asp:ButtonField>
        <asp:BoundField DataField="QuestionID" />
    </Columns>
    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#999999" Font-Bold="True" ForeColor="Maroon" />
    <AlternatingRowStyle BackColor="#DCDCDC" />
</asp:GridView>

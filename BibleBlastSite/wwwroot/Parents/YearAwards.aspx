<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="YearAwards.aspx.vb" Inherits="Awards" title="Untitled Page" %>

<%-- Add content controls here --%>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    This Year&#39;s Awards 
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder2">
    <center>
        <div style="width: 100%; text-align: center;">

            <asp:SqlDataSource ID="dsAwards" runat="server" 
                ConnectionString="<%$ ConnectionStrings:BBData %>" 
                SelectCommand="SELECT * FROM [vAwardsCompletedDetail] WHERE (Completed >= dbo.StartOfYear(GetDate())) ORDER BY [Completed] DESC, [LastName], [FirstName]">
            </asp:SqlDataSource>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" DataSourceID="dsAwards" ForeColor="#333333" 
                GridLines="None" AllowPaging="True" AllowSorting="True">
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:BoundField DataField="Title" HeaderText="Award" SortExpression="Title" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Description" SortExpression="Description" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FirstName" HeaderText="First" 
                        SortExpression="FirstName" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LastName" HeaderText="Last" 
                        SortExpression="LastName" >
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Completed" DataFormatString="{0:d}" 
                        HeaderText="Completed" HtmlEncode="False" SortExpression="Completed" >
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </div>
    </center>
</asp:Content>


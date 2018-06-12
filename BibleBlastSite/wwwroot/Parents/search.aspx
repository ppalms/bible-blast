<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="search.aspx.vb" Inherits="search" title="Untitled Page" %>

<%@ Register src="Controls/CategoryQuestionsByKid.ascx" tagname="CategoryQuestionsByKid" tagprefix="uc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%-- Add content controls here --%><asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    Child Lookup     
</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder2">
    <div style="text-align: center;">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        Name: 
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="Search" />
    </div>
    <div style="float: left; padding: 15px;">
        <asp:SqlDataSource ID="dsKids" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BBData %>" 
            SelectCommand="SELECT * FROM [vKids] WHERE (([FirstName] LIKE '%' + @FirstName + '%') OR ([LastName] LIKE '%' + @LastName + '%') OR ((FirstName + ' ' + LastName) LIKE @LastName)) ORDER BY LastName">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtSearch" Name="FirstName" 
                    PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtSearch" Name="LastName" PropertyName="Text" 
                    Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" DataSourceID="dsKids" 
            CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="KidID" 
            AllowPaging="True">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="FirstName" HeaderText="First" 
                    SortExpression="FirstName" />
                <asp:BoundField DataField="LastName" HeaderText="Last" 
                    SortExpression="LastName" />
                <asp:BoundField DataField="Grade" HeaderText="Grade" SortExpression="Grade" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    </div>
    <div style="float: right; padding: 15px;">
        <asp:SqlDataSource ID="dsCategories" runat="server" 
            ConnectionString="<%$ ConnectionStrings:BBData %>" 
            SelectCommand="SELECT * FROM [Categories]"></asp:SqlDataSource>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Block">
            <ContentTemplate>
                <cc1:Accordion ID="Accordion1" runat="server" FadeTransitions="true" TransitionDuration="250"
                 FramesPerSecond="40" DataSourceID="dsCategories" SelectedIndex="-1" ContentCssClass="accordionContent"
                 HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                 RequireOpenedPane="false" SuppressHeaderPostbacks="false" AutoSize="None" Visible="False">
                    <HeaderTemplate>
                        <%#Eval("Name")%>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <uc1:CategoryQuestionsByKid ID="CategoryQuestionsByKid2" runat="server" KidID="<%# SelectedID()%>" CategoryID='<%# Eval("CategoryID") %>' />
                    </ContentTemplate>
                </cc1:Accordion>
            </ContentTemplate>
        </asp:UpdatePanel>
                    
    </div>
    <div style="clear: both;"></div>
    
</asp:Content>


<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" title="Untitled Page" %>

<%-- Add content controls here --%><asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder2">
                    Welcome to the FUMC BibleBlast web site.&nbsp; As this site grows it will allow 
you to keep a closer view on your child&#39;s progress throughout the BibleBlast 
program, no matter where you are in the world.&nbsp; So from the comfort of your living room couch 
                    to Time's Square or downtown Beijing, you can always be able to see what your 
                    child has learned and accomplished here.<br />
                    <br />
                    As the site continues to expand, we hope to offer you a richer and fuller 
                    experience here, with more features always in the works.&nbsp; For now, you can 
                    search for your child and view their current progress in the BibleBlast program 
                    by searching for their name here.<br />
                    <br />
                    Name:&nbsp;<asp:TextBox ID="txtSearch" runat="server" Width="20%"></asp:TextBox>&nbsp;&nbsp;
                    <asp:Button
                        ID="btnSearch" runat="server" Text="Search" />
                    <br />
&nbsp;
</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
                        First United Methodist Church BibleBlast

</asp:Content>


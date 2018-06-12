<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" title="Untitled Page" %>

<%-- Add content controls here --%><asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

                        Login  
</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder2">

                    <asp:Login ID="Login1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" 
                        BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
                        Font-Size="0.8em" ForeColor="#333333">
                        <TextBoxStyle Font-Size="0.8em" />
                        <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
                        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" 
                            ForeColor="White" />
    </asp:Login>

</asp:Content>


<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="EditEnrollment.aspx.vb" Inherits="EditEnrollment" title="Untitled Page" %>

<%-- Add content controls here --%><asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

                        Edit Enrollment  
</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder2">
    <div>
    
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Family Lookup" />
        <br />
        <br />
                <asp:SqlDataSource ID="dsFamily" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:BBData %>" 
                    DeleteCommand="DELETE FROM [Families] WHERE [FamilyID] = @original_FamilyID" 
                    InsertCommand="INSERT INTO [Families] ([DadName], [MomName], [DadCell], [MomCell], [HomePhone], [Address1], [Address2], [City], [State], [Zip], [NonParentName], [EmergencyPhone], [Email], [IsActive]) VALUES (@DadName, @MomName, @DadCell, @MomCell, @HomePhone, @Address1, @Address2, @City, @State, @Zip, @NonParentName, @EmergencyPhone, @Email, @IsActive)" 
                    OldValuesParameterFormatString="original_{0}" 
                    SelectCommand="SELECT * FROM [Families] WHERE (([DadName] LIKE '%' + @DadName + '%') OR ([MomName] LIKE '%' + @MomName + '%') OR ([NonParentName] LIKE '%' + @NonParentName + '%'))" 
                    
                    
                    UpdateCommand="UPDATE [Families] SET [DadName] = @DadName, [MomName] = @MomName, [DadCell] = @DadCell, [MomCell] = @MomCell, [HomePhone] = @HomePhone, [Address1] = @Address1, [Address2] = @Address2, [City] = @City, [State] = @State, [Zip] = @Zip, [NonParentName] = @NonParentName, [EmergencyPhone] = @EmergencyPhone, [Email] = @Email, [IsActive] = @IsActive WHERE [FamilyID] = @original_FamilyID">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtSearch" Name="DadName" PropertyName="Text" 
                            Type="String" DefaultValue="%" />
                        <asp:ControlParameter ControlID="txtSearch" Name="MomName" PropertyName="Text" 
                            Type="String" DefaultValue="%" />
                        <asp:ControlParameter ControlID="txtSearch" Name="NonParentName" 
                            PropertyName="Text" Type="String" DefaultValue="%" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="original_FamilyID" Type="Int32" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="DadName" Type="String" />
                        <asp:Parameter Name="MomName" Type="String" />
                        <asp:Parameter Name="DadCell" Type="String" />
                        <asp:Parameter Name="MomCell" Type="String" />
                        <asp:Parameter Name="HomePhone" Type="String" />
                        <asp:Parameter Name="Address1" Type="String" />
                        <asp:Parameter Name="Address2" Type="String" />
                        <asp:Parameter Name="City" Type="String" />
                        <asp:Parameter Name="State" Type="String" />
                        <asp:Parameter Name="Zip" Type="String" />
                        <asp:Parameter Name="NonParentName" Type="String" />
                        <asp:Parameter Name="EmergencyPhone" Type="String" />
                        <asp:Parameter Name="Email" Type="String" />
                        <asp:Parameter Name="IsActive" Type="Boolean" />
                        <asp:Parameter Name="original_FamilyID" Type="Int32" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="DadName" Type="String" />
                        <asp:Parameter Name="MomName" Type="String" />
                        <asp:Parameter Name="DadCell" Type="String" />
                        <asp:Parameter Name="MomCell" Type="String" />
                        <asp:Parameter Name="HomePhone" Type="String" />
                        <asp:Parameter Name="Address1" Type="String" />
                        <asp:Parameter Name="Address2" Type="String" />
                        <asp:Parameter Name="City" Type="String" />
                        <asp:Parameter Name="State" Type="String" />
                        <asp:Parameter Name="Zip" Type="String" />
                        <asp:Parameter Name="NonParentName" Type="String" />
                        <asp:Parameter Name="EmergencyPhone" Type="String" />
                        <asp:Parameter Name="Email" Type="String" />
                        <asp:Parameter Name="IsActive" Type="Boolean" />
                    </InsertParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="dsKid" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:BBData %>" 
                    DeleteCommand="DELETE FROM [Kids] WHERE [KidID] = @KidID" 
                    InsertCommand="INSERT INTO [Kids] ([FirstName], [LastName], [Birthday], [FamilyID], [IsMale]) VALUES (@FirstName, @LastName, @Birthday, @FamilyID, @IsMale)" 
                    SelectCommand="SELECT * FROM [vKids] WHERE ([FamilyID] = @FamilyID)" 
                    UpdateCommand="UPDATE [Kids] SET [FirstName] = @FirstName, [LastName] = @LastName, [Birthday] = @Birthday, [IsActive] = @IsActive, [DateRegistered] = @DateRegistered, [IsMale] = @IsMale WHERE [KidID] = @KidID">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GridView2" Name="FamilyID" 
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="KidID" Type="Int32" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="FirstName" Type="String" />
                        <asp:Parameter Name="LastName" Type="String" />
                        <asp:Parameter Name="Birthday" Type="DateTime" />
                        <asp:Parameter Name="IsActive" Type="Boolean" />
                        <asp:Parameter Name="DateRegistered" Type="DateTime" />
                        <asp:Parameter Name="KidID" Type="Int32" />
                        <asp:Parameter Name="IsMale" Type="Boolean" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="FirstName" Type="String" />
                        <asp:Parameter Name="LastName" Type="String" />
                        <asp:Parameter Name="Birthday" Type="DateTime" />
                        <asp:ControlParameter ControlID="GridView2" Name="FamilyID" 
                            PropertyName="SelectedValue" Type="Int32" />
                        <asp:Parameter Name="IsMale" Type="Boolean" />
                    </InsertParameters>
                </asp:SqlDataSource>
                <div style="float: left;">
                    <asp:Button ID="btnFamily" runat="server" Text="Add Family" />
                <asp:DetailsView ID="DetailsView1" runat="server" DataSourceID="dsFamily" 
                    AllowPaging="True" AutoGenerateRows="False" CellPadding="4" 
                    ForeColor="#333333" GridLines="None" AutoGenerateDeleteButton="True" 
                    AutoGenerateEditButton="True" AutoGenerateInsertButton="True" 
                    DataKeyNames="FamilyID" EnablePagingCallbacks="True" DefaultMode="Insert" 
                        Visible="False">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <Fields>
                        <asp:BoundField DataField="FamilyID" HeaderText="FamilyID" 
                            SortExpression="FamilyID" InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="DadName" HeaderText="Dad" 
                            SortExpression="DadName" />
                        <asp:BoundField DataField="MomName" HeaderText="Mom" 
                            SortExpression="MomName" />
                        <asp:BoundField DataField="DadCell" HeaderText="Dad Cell" 
                            SortExpression="DadCell" />
                        <asp:BoundField DataField="MomCell" HeaderText="Mom Cell" 
                            SortExpression="MomCell" />
                        <asp:BoundField DataField="HomePhone" HeaderText="Home Phone" 
                            SortExpression="HomePhone" />
                        <asp:BoundField DataField="Address1" HeaderText="Address1" 
                            SortExpression="Address1" />
                        <asp:BoundField DataField="Address2" HeaderText="Address2" 
                            SortExpression="Address2" />
                        <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                        <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
                        <asp:BoundField DataField="Zip" HeaderText="Zip" 
                            SortExpression="Zip" />
                        <asp:BoundField DataField="NonParentName" HeaderText="NonParent" 
                            SortExpression="NonParentName" />
                        <asp:BoundField DataField="EmergencyPhone" HeaderText="Emergency Phone" 
                            SortExpression="EmergencyPhone" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:CheckBoxField DataField="IsActive" HeaderText="Active?" 
                            SortExpression="IsActive" />
                    </Fields>
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:DetailsView>
                    <asp:GridView ID="GridView2" runat="server" AllowSorting="True" 
                        AutoGenerateColumns="False" CellPadding="4" DataKeyNames="FamilyID" 
                        DataSourceID="dsFamily" ForeColor="#333333" GridLines="None" 
                        AllowPaging="True">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                                ShowSelectButton="True" />
                            <asp:BoundField DataField="DadName" HeaderText="DadName" 
                                SortExpression="DadName" />
                            <asp:BoundField DataField="MomName" HeaderText="MomName" 
                                SortExpression="MomName" />
                        </Columns>
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </div>
                <div style="float: right;">
                <asp:Button ID="btnNewKid" runat="server" Text="Open 'New Child' Form" />
                <asp:DetailsView ID="DetailsView2" runat="server" 
                    AutoGenerateRows="False" CellPadding="4" 
                    DataKeyNames="KidID" DataSourceID="dsKid" 
                    ForeColor="#333333" GridLines="None" EnablePagingCallbacks="True" 
                    AutoGenerateInsertButton="True" AllowPaging="True" DefaultMode="Insert" 
                    Visible="False">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <Fields>
                        <asp:BoundField DataField="KidID" HeaderText="KidID" 
                            SortExpression="KidID" InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="FirstName" HeaderText="First" 
                            SortExpression="FirstName" />
                        <asp:BoundField DataField="LastName" HeaderText="Last" 
                            SortExpression="LastName" />
                        <asp:BoundField DataField="Birthday" HeaderText="Birthday" 
                            SortExpression="Birthday" DataFormatString="{0:d}" HtmlEncode="False" />
                        <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" 
                            SortExpression="IsActive" InsertVisible="False" />
                        <asp:BoundField DataField="DateRegistered" HeaderText="DateRegistered" 
                            SortExpression="DateRegistered" InsertVisible="False" />
                        <asp:BoundField DataField="FamilyID" HeaderText="FamilyID" 
                            InsertVisible="False" SortExpression="FamilyID" />
                        <asp:CheckBoxField DataField="IsMale" HeaderText="Male?" 
                            SortExpression="IsMale" />
                    </Fields>
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:DetailsView>
                    <asp:GridView ID="GridView1" runat="server" 
                    AutoGenerateColumns="False" DataKeyNames="KidID" DataSourceID="dsKid" 
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                            <asp:BoundField DataField="FirstName" 
                                HeaderText="First" SortExpression="FirstName" />
                            <asp:BoundField DataField="LastName" HeaderText="Last" 
                                SortExpression="LastName" />
                            <asp:BoundField DataField="Birthday" HeaderText="Birthday" 
                                SortExpression="Birthday" DataFormatString="{0:d}" 
                                HtmlEncode="False" ApplyFormatInEditMode="True" />
                            <asp:BoundField DataField="Age" HeaderText="A" InsertVisible="False" 
                                ReadOnly="True" SortExpression="Age">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Grade" HeaderText="G" InsertVisible="False" 
                                ReadOnly="True" SortExpression="Grade">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="IsMale" HeaderText="M?" 
                                SortExpression="IsMale" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>
                            <asp:CheckBoxField DataField="IsActive" HeaderText="A?" 
                                SortExpression="IsActive" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>
                        </Columns>
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </div>
            
                
        <br />
    
    </div>    
</asp:Content>


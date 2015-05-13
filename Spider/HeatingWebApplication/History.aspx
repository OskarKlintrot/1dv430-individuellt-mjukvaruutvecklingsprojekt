<%@ Page Title="Historiska temperaturer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="HeatingWebApplication.History" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/Scripts/renderChart.js") %>
    </asp:PlaceHolder>

    <h2><%: Title %></h2>

    <asp:ListView ID="AvailableRoomsListView" runat="server"
        ItemType="Domain.Model.BLL.Room"
        SelectMethod="AvailableRoomsListView_GetData">
        <LayoutTemplate>
            <div id="ChartsToDisplay">
                <div class="col-md-8">
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                </div>
                <br />
                <div class="clearfix"></div>
                <div class="">
                    <label>
                        Startdatum: <br />
                    <asp:TextBox ID="StartDateTextBox" type="date" OnLoad="StartDateTextBox_Load" runat="server" />
                    </label>
                    <label>
                        Slutdatum: <br />
                    <asp:TextBox ID="EndDateTextBox" type="date" OnLoad="EndDateTextBox_Load" runat="server" />
                    </label>
                    <label>
                        Skala i minuter: <br />
                    <asp:TextBox ID="ScaleTextBox" runat="server" Text="10" />
                    </label>
                    <input id="ChartButton" type="button" value="Visa grafer"><span id="loadingAJAX" class="hide">  Laddar...</span></input>
                </div
            </div>
        </LayoutTemplate>
        <EmptyDataTemplate>
            <p>
                Det fanns ingen data att hämta från databasen.
            </p>
        </EmptyDataTemplate>
        <ItemTemplate>
            <label class="col-md-4">
                <input type="checkbox" runat="server" id="CheckBox" value='<%# Item.RoomID %>' />
                <%# Item.RoomDescription %>
            </label>
        </ItemTemplate>
    </asp:ListView>
    <%--<div>
        <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="bs-example alert-danger alert-error" HeaderText="Ett fel inträffade!"/>
    </div>--%>
<%--    <div id="ASPdotNETerrorDiv">
         Start date error 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="text-danger" 
            runat="server" ErrorMessage="Du måste fylla i ett startdatum!"
            Display="Dynamic" ControlToValidate="StartDateTextBox">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
            ErrorMessage="Startdatumet fylls i enligt åååå-MM-dd." CssClass="text-danger" 
            ControlToValidate="StartDateTextBox" Display="Dynamic" ValidationExpression="[1-2]\d{3}-[0-1][1-9]-[0-3][1-9]"
            >*</asp:RegularExpressionValidator>
         End date error 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="text-danger" 
            runat="server" ErrorMessage="Du måste fylla i ett slutdatum!"
            Display="Dynamic" ControlToValidate="EndDateTextBox">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ErrorMessage="Slutdatumet fylls i enligt åååå-MM-dd." CssClass="text-danger" 
            ControlToValidate="EndDateTextBox" Display="Dynamic" ValidationExpression="[1-2]\d{3}-[0-1][1-9]-[0-3][1-9]"
        >*</asp:RegularExpressionValidator>
         Range error 
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="text-danger" 
            runat="server" ErrorMessage="Du måste fylla i en skala!"
            Display="Dynamic" ControlToValidate="ScaleTextBox">*</asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" CssClass="text-danger" 
            runat="server" ErrorMessage="Skalan måste vara mellan 1-60 minuter."
            Display="Dynamic" MaximumValue="60" MinimumValue="1" ControlToValidate="ScaleTextBox">*</asp:RangeValidator>
    </div>--%>
    <div id="errorDiv"></div>
    <div id="chartDiv"></div>
    <div id="helpTextDiv"><p><em></em></p></div>
</asp:Content>

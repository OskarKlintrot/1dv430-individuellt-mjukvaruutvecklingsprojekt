<%@ Page Title="Historiska temperaturer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="HeatingWebApplication.History" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/chart") %>
        <%: Scripts.Render("~/Scripts/renderChart.js") %>
    </asp:PlaceHolder>

    <h2><%: Title %></h2>

    <asp:ListView ID="AvailableRoomsListView" runat="server"
        ItemType="Domain.Model.BLL.Room"
        SelectMethod="AvailableRoomsListView_GetData">
        <LayoutTemplate>
            <div id="ChartsToDisplay">
                <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                <label>
                    Startdatum:
                    <asp:TextBox ID="StartDateTextBox" type="date" OnLoad="StartDateTextBox_Load" runat="server" />
                </label>
                <label>
                    Slutdatum:
                    <asp:TextBox ID="EndDateTextBox" type="date" OnLoad="EndDateTextBox_Load" runat="server" />
                </label>
                <label>
                    Skala:
                    <asp:TextBox ID="ScaleTextBox" runat="server" Text="10" />
                </label>
                <input id="ChartButton" type="button" value="Visa grafer" />
            </div>
        </LayoutTemplate>
        <EmptyDataTemplate>
            <p>
                Det fanns ingen data att hämta från databasen.
            </p>
        </EmptyDataTemplate>
        <ItemTemplate>
            <label>
                <input type="checkbox" runat="server" id="CheckBox" value='<%# Item.RoomID %>' />
                <%# Item.RoomDescription %>
            </label>
            <br />
        </ItemTemplate>
    </asp:ListView>

    <%--<div>
        Your Name : 
    <asp:TextBox ID="UserNameTextBox" runat="server"></asp:TextBox>
        <input id="ChartButton" type="button" value="Test" />
    </div>--%>
</asp:Content>

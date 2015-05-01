<%@ Page Title="Historiska temperaturer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="HeatingWebApplication.History" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/chart") %>
        <%: Scripts.Render("~/Scripts/renderChart.js") %>
    </asp:PlaceHolder>

    <div>
        Your Name : 
    <asp:TextBox ID="UserNameTextBox" runat="server"></asp:TextBox>
        <input id="ChartButton" type="button" value="Test" />
    </div>
</asp:Content>

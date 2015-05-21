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
            <div id="ChartsToDisplay" class="well col-md-offset-1 col-md-10">
                <div >
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                </div>
                <div class="clearme">
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
                    <input id="ChartButton" type="button" value="Visa grafer" />
                    <span id="loadingAJAX" class="hide"><img src="Pics/ajax-loader.gif" /></span>
                </div>
            </div>
        </LayoutTemplate>
        <EmptyDataTemplate>
            <p>
                Tyvärr, databasen verkar ligga nere. Hör gärna av dig till oss och <a href="Contact">meddela felet</a>.
            </p>
        </EmptyDataTemplate>
        <ItemTemplate>
            <label class="col-md-4">
                <input type="checkbox" runat="server" id="CheckBox" value='<%# Item.RoomID %>' />
                <%# Item.RoomDescription %>
            </label>
        </ItemTemplate>
    </asp:ListView>
    <div id="errorDiv" class="clearme col-md-offset-1 col-md-10"></div>
    <div id="chartDiv" class="clearme"></div>
    <div id="helpTextDiv"><p><em></em></p></div>
</asp:Content>

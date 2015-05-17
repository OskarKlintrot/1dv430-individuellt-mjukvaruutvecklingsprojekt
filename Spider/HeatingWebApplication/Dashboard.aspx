<%@ Page Title="Reglering av värmen" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HeatingWebApplication.Dashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
            <h1><%: Title %></h1>
    </div>
    <asp:Panel runat="server" ID="SuccessMessagePanel" Visible="false">
            <div class="alert alert-success">
                <a href="#" class="close" data-dismiss="alert">&times;</a>
                <asp:Literal runat="server" ID="SuccessMessageLiteral" />
            </div>
    </asp:Panel>
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    </div>

    <div class="row">
        <asp:ListView ID="HeatingListView" runat="server"
            ItemType="Domain.Model.BLL.Room"
            SelectMethod="HeatingListView_GetData"
            UpdateMethod="HeatingListView_UpdateItem"
            DataKeyNames="RoomID">
            <LayoutTemplate>
                <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
            </LayoutTemplate>
            <EmptyDataTemplate>
                <p>
                    Det fanns ingen data att hämta från databasen.
                </p>
            </EmptyDataTemplate>
            <ItemTemplate>
                <div class="col-sm-6 col-md-4 text-center panel-default">
                    <div class="panel-heading">
                        <h2 class="panel-title">
                            <asp:Label ID="RoomLabel" runat="server" Text='<%# Item.RoomDescription %>'></asp:Label>
                        </h2>
                    </div>
                    <%--Heating on or off label--%>
                    <div>
                        <asp:PlaceHolder ID="HeatingOnPlaceHolder" Visible='<%# Item.Heating %>' runat="server">
                            <h3>
                                <asp:Label ID="HeatingOnLabel" CssClass="label label-success" runat="server" Text="Värme på"></asp:Label>
                            </h3>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="HeatingOffPlaceHolder" Visible='<%# !Item.Heating %>' runat="server">
                            <h3>
                                <asp:Label ID="HeatingOffLabel" CssClass="label label-danger" runat="server" Text="Värme av"></asp:Label>
                            </h3>
                        </asp:PlaceHolder>
                    </div>
                    <%--Temperature--%>
                    <div>
                        <asp:Label ID="TemperatureLabel" runat="server" ><%# Item.LastTemperature %>&deg;C</asp:Label>
                    </div>
                    <%--Turn heating on or off--%>
                    <div>
                        <asp:PlaceHolder ID="PlaceHolder1" Visible='<%# !Item.Heating %>' runat="server">
                            <p>
                                <asp:LinkButton ID="HeatOnLinkButton" CommandName="Update"
                                    CssClass="btn btn-success" Text="Sätt på värmen &raquo;" runat="server" />
                            </p>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="PlaceHolder2" Visible='<%# Item.Heating %>' runat="server">
                            <p>
                                <asp:LinkButton ID="HeatOffLinkButton" CommandName="Update"
                                    CssClass="btn btn-danger" Text="Stäng av värmen &raquo;" runat="server" />
                            </p>
                        </asp:PlaceHolder>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

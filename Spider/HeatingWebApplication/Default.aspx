<%@ Page Title="Värmereglering" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HeatingWebApplication._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Värmereglering av kyrkan</h1>
    </div>
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
            <ItemTemplate>
                <div class="col-xs-6 col-sm-4 col-md-3 text-center">
                    <div>
                        <h2>
                            <asp:Label ID="RoomLabel" runat="server" Text='<%# Item.RoomDescription %>'></asp:Label>
                        </h2>
                    </div>
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
                    <div>
                        <asp:PlaceHolder ID="PlaceHolder1" Visible='<%# !Item.Heating %>' runat="server">
                            <p>
                                <asp:LinkButton ID="HeatOnLinkButton" CommandName="Update"
                                    CssClass="btn btn-success" Text="Värme på &raquo;" runat="server" />
                            </p>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="PlaceHolder2" Visible='<%# Item.Heating %>' runat="server">
                            <p>
                                <asp:LinkButton ID="HeatOffLinkButton" CommandName="Update"
                                    CssClass="btn btn-danger" Text="Värme av &raquo;" runat="server" />
                            </p>
                        </asp:PlaceHolder>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

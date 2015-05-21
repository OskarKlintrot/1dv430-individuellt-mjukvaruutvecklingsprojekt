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
                    Tyvärr, databasen verkar ligga nere. Hör gärna av dig till oss och <a href="Contact">meddela felet</a>.
                </p>
            </EmptyDataTemplate>
            <ItemTemplate>
                <div class="col-sm-6 col-md-4 text-center">
                    <div >
                        <%-- Room Description --%>
                            <h3 class="well">
                                <asp:Label ID="RoomLabel" runat="server" Text='<%# Item.RoomDescription %>'></asp:Label>
                            </h3>
                        <%--Temperature--%>
                        <div>
                            <h3>
                                <asp:Label class="label label-info" ID="TemperatureLabel" runat="server">Aktuell temperatur: <%# Item.LastTemperature %>&deg;C</asp:Label>
                            </h3>
                        </div>
                        <%--Heating on or off label--%>
                        <asp:PlaceHolder ID="HeatingOnPlaceHolder" Visible='<%# Item.Heating %>' runat="server">
                            <h3>
                                <asp:LinkButton CommandName="Update" runat="server"><img title="Värmen är på" alt="Värmen är på" src="Pics/On-LED.png" /></asp:LinkButton>
                                <br />
                                <asp:Label ID="HeatingOnLabel" CssClass="label label-success" runat="server" Text="Värmen är på"></asp:Label>
                            </h3>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="HeatingOffPlaceHolder" Visible='<%# !Item.Heating %>' runat="server">
                            <h3>    
                                <asp:LinkButton CommandName="Update" runat="server"><img title="Värmen är av" alt="Värmen är av" src="Pics/Off-LED.png" /></asp:LinkButton>
                                <br />
                                <asp:Label ID="HeatingOffLabel" CssClass="label label-danger" runat="server" Text="Värmen är av"></asp:Label>
                            </h3>
                        </asp:PlaceHolder>
                    </div>
                    <%--Turn heating on or off--%>
                    <div>
                        <asp:PlaceHolder ID="PlaceHolder1" Visible='<%# !Item.Heating %>' runat="server">
                            <p>
                                <asp:LinkButton ID="HeatOnLinkButton" CommandName="Update"
                                    CssClass="heating btn btn-default" Text="Sätt på värmen &raquo;" runat="server" />
                            </p>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="PlaceHolder2" Visible='<%# Item.Heating %>' runat="server">
                            <p>
                                <asp:LinkButton ID="HeatOffLinkButton" CommandName="Update"
                                    CssClass="heating btn btn-default" Text="Stäng av värmen &raquo;" runat="server" />
                            </p>
                        </asp:PlaceHolder>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>

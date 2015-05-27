<%@ Page Title="Logga in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HeatingWebApplication.Login" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>

    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div id="logoutDiv" runat="server">
                        <div>
                            <p>
                                Du är redan inloggad, vill du logga ut?
                            </p>
                        </div>
                        <div class="col-md-offset-2 col-md-10">
                                <asp:Button runat="server" ID="LogOutButton" OnClick="LogOutButton_Click" Text="Logga ut" CssClass="btn btn-default" />
                        </div>
                    </div>

                <div id="loginDiv" runat="server" class="form-horizontal">
                    <h4>Skriv in användarnamn och lösenord.</h4>
                    <hr />
                      <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <div class="col-md-7">
                                <asp:Panel runat="server" ID="WarningMessagePanel" Visible="false" CssClass="icon-ok">
                                    <div class="alert alert-danger">
                                        <a href="#" class="close" data-dismiss="alert">&times;</a>
                                        <asp:Literal runat="server" ID="WarningMessageLiteral" />
                                    </div>
                                </asp:Panel>
                        </div>
                    </div>

                    <div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="UsernameTextBox" CssClass="col-md-2 control-label">Användarnamn</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="UsernameTextBox" CssClass="form-control" TextMode="SingleLine" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="UsernameTextBox"
                                    CssClass="text-danger" ErrorMessage="Du måste ange ett användarnamn." />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="PasswordTextBox" CssClass="col-md-2 control-label">Lösenord</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="PasswordTextBox" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="PasswordTextBox" CssClass="text-danger" ErrorMessage="Du måste ange ett lösenord." />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button runat="server" ID="LogInButton" OnClick="LogInButton_Click" Text="Logga in" CssClass="btn btn-default" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <p><em>Glömt lösenordet? <a href="/Contact">Kontakta oss</a> i så fall!</em></p>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>

        <div class="col-md-4">

        </div>
    </div>

</asp:Content>

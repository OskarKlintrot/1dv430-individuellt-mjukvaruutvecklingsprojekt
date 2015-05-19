﻿<%@ Page Title="Värmereglering" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HeatingWebApplication._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1><%: Title %></h1>
        <p class="lead">
            Välkommen till hemsidan som styr värmen i vår kyrklokal. Här kan du sätta på och av värmen i olika rum, kontrollera aktuella temperaturer och kolla tidigare temperaturer för de olika rummen.
        </p>
        <p>
            <asp:HyperLink ID="HyperLink1" class="btn btn-primary btn-lg" runat="server" NavigateUrl="Dashboard">Värmeregleringen &raquo;</asp:HyperLink>
        </p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Logga in</h2>
            <p>
                För att kunna ändra värmen i kyrkan behöver du logga in. Tänk på att logga ut om du sitter vid en allmän dator.
            </p>
            <p>
                <asp:HyperLink ID="HyperLink2" class="btn btn-default" runat="server" NavigateUrl="Login">Till inloggningen &raquo;</asp:HyperLink>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Historik</h2>
            <p>
                Här kan du hämta historiska temperaturer. Du väljer själv i vilket tidsspan, upplösning och vilka rum du vill se.
            </p>
            <p>
                <asp:HyperLink ID="HyperLink3" class="btn btn-default" runat="server" NavigateUrl="History">Historiska temperaturer &raquo;</asp:HyperLink>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Kontakt</h2>
            <p>
                Har du frågor eller funderingar kring den här webbapplikationen? Kontakta oss i så fall!
            </p>
            <p>
                <asp:HyperLink ID="HyperLink4" class="btn btn-default" runat="server" NavigateUrl="Contact">Kontakta oss &raquo;</asp:HyperLink>
            </p>
        </div>
    </div>

</asp:Content>
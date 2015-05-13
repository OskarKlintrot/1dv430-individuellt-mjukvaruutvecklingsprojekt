<%@ Page Title="Om projektet Värmereglering" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="HeatingWebApplication.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Ett projekt av Oskar Klintrot</h3>
    <p>Denna sidan är skapad av <a href="https://www.linkedin.com/profile/view?id=335055893">Oskar Klintrot</a> i kursen 
        <a href="https://coursepress.lnu.se/kurs/individuellt-mjukvaruutvecklingsprojekt/">Individuellt mjukvaruutvecklingsprojekt</a> 
        som ingår i utbildning till <a href="http://www.webbprogrammerare.se">webbprogrammerare</a>. Sidan är publicerad på Windows Azure 
        och kommunicerar med en Arduino ihopkopplad med en stationär PC hemma hos Oskar. Fungerar inte sidan är antagligen Oskars dator 
        avstängd. Tanken är att när Windows 10 släpps så ska en Raspberry PI 2 sköta kommunikationen med Arduino och om möjligt agera MSSQL Server.</p>
</asp:Content>

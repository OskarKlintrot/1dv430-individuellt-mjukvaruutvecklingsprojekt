<%@ Page Title="Kalender" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="HeatingWebApplication.Calendar" %>

<asp:content contentplaceholderid="ExtraStylesAndScripts" runat="server">    
    <link href="Components/fullcalendar-2.3.1/fullcalendar.css" rel="stylesheet" />
    <%--<script src="Components/fullcalendar-2.3.1/lib/jquery.min.js"></script>
    <script src="Components/fullcalendar-2.3.1/lib/moment.min.js"></script>
    <script src="Components/fullcalendar-2.3.1/fullcalendar.js"></script>
    <script src="Scripts/FullCalendarIO.js"></script>--%>
</asp:content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Start Scripts --%>
    <script src="Components/fullcalendar-2.3.1/lib/moment.min.js"></script>
    <script src="Components/fullcalendar-2.3.1/fullcalendar.js"></script>
    <script src="Components/fullcalendar-2.3.1/lang/sv.js"></script>
    <script src="Scripts/FullCalendarIO.js"></script>
    <%-- End Scripts --%>

    <div id="calendar"></div>
</asp:Content>
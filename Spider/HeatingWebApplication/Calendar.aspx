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
    <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
    <script type='text/javascript' src="Components/fullcalendar-2.3.1/lib/moment.min.js"></script>
    <script type='text/javascript' src="Components/fullcalendar-2.3.1/fullcalendar.js"></script>
    <script type='text/javascript' src="Components/fullcalendar-2.3.1/lang/sv.js"></script>
    <script type='text/javascript' src='Components/fullcalendar-2.3.1/gcal.js'></script>
    <script type='text/javascript' src="Scripts/FullCalendarIO.js"></script>
    <%-- End Scripts --%>

    <div id="calendar"></div>

    <div id="fullCalModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span> <span class="sr-only">Stäng</span></button>
                    <h4 id="modalTitle" class="modal-title"></h4>
                </div>
                <div id="modalBody" class="modal-body">

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Stäng</button>
                    <a class="btn btn-primary" id="eventUrl" href="#" target="_blank">Mer info</a>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
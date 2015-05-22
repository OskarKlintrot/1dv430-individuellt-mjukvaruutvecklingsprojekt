$(document).ready(function(){

    var defaultViewBasedOnWindowsWidth = function () {
        if ($(window).width() < 480) {
            return "agendaDay";
        }
        else if ($(window).width() < 768) {
            return "agendaWeek";
        }
        else {
            return "month";
        };
    };

    $("#calendar").fullCalendar({
        // View
        header: {
			    left: 'prev,next today',
			    center: 'title',
			    right: 'month,agendaWeek,agendaDay'
        },

        // Responsive
        defaultView: defaultViewBasedOnWindowsWidth(),
        windowResize: function (view) {
            $('#calendar').fullCalendar('changeView', defaultViewBasedOnWindowsWidth());
        },

        // Language
        lang: 'sv',

        // Date settings
        weekNumbers: true,
        weekNumberTitle: '',
        timeFormat: 'H:mm',

        // Google Calender
        googleCalendarApiKey: 'AIzaSyCLf3exIqBc5Z2m9S5U5vYIMYbooZP8HJE',
        eventSources: [
            {
                googleCalendarId: 'kalender@missionskyrkorna.se'
            }
        ],
    });

});
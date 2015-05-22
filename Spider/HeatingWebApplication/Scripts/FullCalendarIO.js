$(document).ready(function(){

    var defaultViewBasedOnWindowWidth = function () {
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

    // Get correct ordinal
    moment.locale('sv', {
        ordinal : function (number, token) {
            var b = number % 10;
            var output = (~~ (number % 100 / 10) === 1) ? ':e' :
                (b === 1) ? ':a' :
                (b === 2) ? ':a' : ':e';
            return number + output;
        }
    });

    $("#calendar").fullCalendar({
        // View
        header: {
			    left: 'prev,next today',
			    center: 'title',
			    right: 'month,agendaWeek,agendaDay'
        },

        // Responsive
        defaultView: defaultViewBasedOnWindowWidth(),
        windowResize: function (view) {
            $('#calendar').fullCalendar('changeView', defaultViewBasedOnWindowWidth());
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

        // Modal
        eventRender: function (event, element) {
            element.attr('href', 'javascript:void(0);');
        },
        eventClick: function (event, jsEvent, view) {
            // Remove old info
            $('#modalTitle').html('');
            $('#modalBody').html('');
            $('#eventUrl').attr('href','');
            $('#fullCalModal').modal();

            // Add new info
            $('#modalTitle').html(event.title);
            $('#modalBody').html("<strong>Starttid:</strong> " + moment(event.start).format('Do MMM HH:mm') +  "<br />");
            $('#modalBody').append("<strong>Sluttid:</strong> " + moment(event.end).format('Do MMM HH:mm') + "<br />");
            if (event.location) {
                $('#modalBody').append("<strong>Plats:</strong> " + event.location + "<br />");
            }
            if (event.description) {
                $('#modalBody').append("<br />" + event.description);
            }
            $('#eventUrl').attr('href',event.url);
            $('#fullCalModal').modal();
        }
    });

});
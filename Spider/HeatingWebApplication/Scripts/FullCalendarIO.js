$(document).ready(function(){

    $("#calendar").fullCalendar({
        lang: 'sv',
        weekNumbers: true,
        weekNumberTitle: '',
        timeFormat: 'H:mm',
        googleCalendarApiKey: 'AIzaSyCLf3exIqBc5Z2m9S5U5vYIMYbooZP8HJE',
        eventSources: [
            {
                googleCalendarId: 'kalender@missionskyrkorna.se'
            }
        ]
    });

});
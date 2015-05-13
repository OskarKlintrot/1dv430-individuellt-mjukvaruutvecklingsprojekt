"use strict";

$.getScript("https://www.google.com/jsapi", function () {
    google.load('visualization', '1', { 'callback': false, packages: ['corechart', 'line'] });
});

$(document).ready(function () {

    console.log("It's alive!");
    $("#ChartButton").click(function () {
        GetChartData();
    });

    function GetChartData() {
        var dataToSend = {
            roomID: CheckedBoxex(),
            startDate: $("input[id$='StartDateTextBox']").val(),
            endDate: $("input[id$='EndDateTextBox']").val(),
            scale: $("input[id$='ScaleTextBox']").val(),
        };

        $.ajax({
            type: "POST",
            url: "History.aspx/GetChartData",
            data: JSON.stringify(dataToSend),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            //failure: function(response) {
            //    alert("response.d");
            //}
        });
    };

    function OnSuccess(response) {
        var chartData = response.d;
        //$.each(chartData.Room, function (index) {
        //    console.log(chartData.Room[index].RoomDescription);
        //});
        drawBasic(chartData);
    };

    function CheckedBoxex() {
        var selected = [];
        $('#ChartsToDisplay input:checked').each(function () {
            selected.push(parseInt($(this).attr('value')));
        });
        return selected;
    };

    function drawBasic(chartData) {
        var data = new google.visualization.DataTable();

        data.addColumn('date', 'X');
        for (var i = 0; i < chartData.Room.length; i++) {
            data.addColumn('number', chartData.Room[i].RoomDescription);
        }

        var rows = [];
        for (var i = 0; i < chartData.Timestamp.length; i++) {
            rows[i] = [chartData.Room.length];
            // Add time stamp
            rows[i][0] = new Date(chartData.Timestamp[i]);
            for (var k = 0; k < chartData.Room.length; k++) {
                // Add temperature
                rows[i][k+1] = chartData.Room[k].Temperatures[i];
            };
        };

        data.addRows(rows);

        var options = {
            curveType: 'function', // Smoothes out the lines
            explorer: { 
                axis: 'horizontal',
                keepInBounds: true
            },
            legend: { position: 'top' },
            focusTarget: 'category',
            hAxis: {
                title: 'Datum och tid',
                gridlines: {
                    count: -1,
                    units: {
                        days: { format: ["yyyy-MM-dd"] },
                        hours: { format: ["HH:mm"] }
                    }
                },
                minorGridlines: {
                    units: {
                        days: { format: ["yyyy-MM-dd"] },
                        hours: { format: ["HH:mm"] },
                        minutes: { format: ["HH:mm", ":mm"] },
                    }
                },
            },
            vAxis: {
                title: 'Temperatur'
            },
            interpolateNulls: false
        };

        var chart = new google.visualization.LineChart(document.getElementById('chartDiv'));

        $("#helpTextDiv p em").empty();
        $("#helpTextDiv p em").append('Tips: Scrolla för att zooma in och ut, klicka och dra för att flytta skalan och högerklicka för att återställa.');

        chart.draw(data, options);
    }
});
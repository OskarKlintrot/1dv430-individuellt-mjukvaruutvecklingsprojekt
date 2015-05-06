﻿//$(document).ready(function (ChartLabels, ChartData1, ChartData2) {
//    console.log("It's alive!")
//    var randomScalingFactor = function () { return Math.round(Math.random() * 100) };
//    var lineChartData = {
//        labels: ["January", "February", "March", "April", "May", "June", "July"],
//        //labels: ChartLabels,
//        datasets: [
//            {
//                label: "Query Count",
//                fillColor: "rgba(220,220,220,0.2)",
//                strokeColor: "rgba(220,220,220,1)",
//                pointColor: "rgba(220,220,220,1)",
//                pointStrokeColor: "#fff",
//                pointHighlightFill: "#fff",
//                pointHighlightStroke: "rgba(220,220,220,1)",
//                data: [0, 1, 4, 6, 10, 8, 6]
//                //data: ChartData1
//                },
//            {
//                label: "My Second dataset",
//                fillColor: "rgba(151,187,205,0.2)",
//                strokeColor: "rgba(151,187,205,1)",
//                pointColor: "rgba(151,187,205,1)",
//                pointStrokeColor: "#fff",
//                pointHighlightFill: "#fff",
//                pointHighlightStroke: "rgba(151,187,205,1)",
//                data: [28, 48, 40, 19, 86, 27, 90]
//                //data: ChartData2
//                }
//        ]
//    }
//    function DrawChart() {
//        console.log("I want to draw something!")
//        var ctx = document.getElementById("canvas").getContext("2d");
//        window.myLine = new Chart(ctx).Line(lineChartData, {
//            responsive: true
//        });
//    }
//});

"use strict";

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

        console.log(dataToSend);

        $.ajax({
            type: "POST",
            url: "History.aspx/GetChartData",
            data: JSON.stringify(dataToSend),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function(response) {
                alert("response.d");
            }
        });
    };

    function OnSuccess(response) {
        var chartData = response.d;
        console.log(chartData);
        $.each(chartData, function (index) {
            console.log(chartData[index].RoomDescription);
        })
        //console.log(chartData[0].RoomDescription);
        //console.log(response.d);
    };

    function CheckedBoxex() {
        var selected = [];
        $('#ChartsToDisplay input:checked').each(function () {
            selected.push(parseInt($(this).attr('value')));
        });
        return selected;
    };
});
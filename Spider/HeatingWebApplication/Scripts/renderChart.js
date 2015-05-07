//$(document).ready(function (ChartLabels, ChartData1, ChartData2) {
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
        console.log(chartData);
        $.each(chartData, function (index) {
            console.log(chartData[index].RoomDescription);
        })
        //console.log(response.d);
        drawBasic();
    };

    function CheckedBoxex() {
        var selected = [];
        $('#ChartsToDisplay input:checked').each(function () {
            selected.push(parseInt($(this).attr('value')));
        });
        return selected;
    };

    //google.load('visualization', '1', { packages: ['corechart', 'line'] });
    //google.setOnLoadCallback(drawBasic);

    function drawBasic() {

        //google.load('visualization', '1', { packages: ['corechart', 'line'] });

        var data = new google.visualization.DataTable();
        data.addColumn('number', 'X');
        data.addColumn('number', 'Dogs');

        data.addRows([
          [0, 0], [1, 10], [2, 23], [3, 17], [4, 18], [5, 9],
          [6, 11], [7, 27], [8, 33], [9, 40], [10, 32], [11, 35],
          [12, 30], [13, 40], [14, 42], [15, 47], [16, 44], [17, 48],
          [18, 52], [19, 54], [20, null], [21, 55], [22, 56], [23, 57],
          [24, 60], [25, 50], [26, 52], [27, 51], [28, 49], [29, 53],
          [30, 55], [31, 60], [32, 61], [33, 59], [34, 62], [35, 65],
          [36, 62], [37, 58], [38, 55], [39, 61], [40, 64], [41, 65],
          [42, 63], [43, 66], [44, 67], [45, null], [46, null], [47, 70],
          [48, 72], [49, 68], [50, 66], [51, 65], [52, 67], [53, 70],
          [54, 71], [55, 72], [56, 73], [57, 75], [58, 70], [59, 68],
          [60, 64], [61, 60], [62, 65], [63, 67], [64, 68], [65, 69],
          [66, 70], [67, 72], [68, 75], [69, 80]
        ]);

        var options = {
            hAxis: {
                title: 'Datum och tid'
            },
            vAxis: {
                title: 'Temperatur'
            },
            interpolateNulls: false
        };

        var chart = new google.visualization.LineChart(document.getElementById('chart_div'));

        chart.draw(data, options);
    }
});
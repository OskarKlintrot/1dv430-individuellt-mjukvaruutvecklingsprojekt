"use strict";

$(document).ready(function () {
    console.log("It's alive!");

    var allManButtons = $("[id*='ManLinkButton']").parent().children("[id*='ControlLinkButton']");
    $(allManButtons).addClass('disabled');

    console.log(allManButtons);
});
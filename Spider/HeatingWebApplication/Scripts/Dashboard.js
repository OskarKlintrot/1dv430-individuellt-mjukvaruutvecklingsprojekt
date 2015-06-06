"use strict";

$(document).ready(function () {
    //console.log("It's alive!");

    var allManButtons = $("[id*='ManLinkButton']").parent().children("[id*='ControlLinkButton']");
    $(allManButtons).addClass('disabled');

    var allManLinkButtons = $("[id*='ManLinkButton']").parent().parent().parent().children("div").children("h3").children("[id*='HeatingLinkButton']");
    $(allManLinkButtons).addClass('not-active');

    //console.log(allManButtons);
    //console.log(allManLinkButtons);
});
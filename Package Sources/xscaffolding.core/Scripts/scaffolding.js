$(document).ready(function () {
    if (!Modernizr.inputtypes.date) {
        $('.date').datepicker();
    }

    $('select').chosen();
});
$(document).ready(function () {
    if (!Modernizr.inputtypes.date) {
        $(function () {
            $(".datecontrol").datepicker();
        });
    }

    jQuery('#DataInicial').datetimepicker({
        format: 'd.m.Y H:i',
        inline: true,
        lang: 'pt'
    });
    /*
    $("#DataInicial").datetimepicker(
                    {
                        defaultDate: new Date(),
                        format: 'DD/MM/YYYY HH:mm',
                        toolbarPlacement: 'bottom',
                        stepping: 1,
                        daysOfWeekDisabled: [0],
                        sideBySide: true,
                        collapse: false,
                        inline: true
                        , keepInvalid: true
                        , keepOpen: true
                         
                    });
   */

});



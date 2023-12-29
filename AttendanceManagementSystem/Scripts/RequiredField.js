$('input[type=text]').each(function () {
    var req = $(this).attr('data-val-required');
    if (undefined !== req) {
        var label = $('label[for="' + $(this).attr('id') + '"]');
        var text = label.text();
        if (text.length > 0) {
            label.append('<span style="color:#ff0000;"> *</span>');
        }
    }
});
$('select').each(function () {
    var req = $(this).attr('data-val-required');
    if (undefined !== req) {
        var label = $('label[for="' + $(this).attr('id') + '"]');
        var text = label.text();
        if (text.length > 0) {
            label.append('<span style="color:#ff0000;"> *</span>');
        }
    }
});
$('input[type=checkbox]').each(function () {
    var req = $(this).attr('data-val-required');
    if (undefined !== req) {
        var label = $('label[for="' + $(this).attr('id') + '"]');
        var text = label.text();
        if (text.length > 0) {
            label.append('<span style="color:#ff0000;"> *</span>');
        }
    }
});
$('textarea').each(function () {
    var req = $(this).attr('data-val-required');
    if (undefined !== req) {
        var label = $('label[for="' + $(this).attr('id') + '"]');
        var text = label.text();
        if (text.length > 0) {
            label.append('<span style="color:#ff0000;"> *</span>');
        }
    }
});

//$(".BSswitch").bootstrapSwitch({
//    onText: 'छ',
//    offText: 'छैन',
//    size: 'small',
//    onColor: 'success',
//    offColor:'danger'
//});

$(".BSswitch").bootstrapSwitch({
    onText: 'हो',
    offText: 'होइन',
    size: 'small',
    onColor: 'success',
    offColor: 'danger'
});

$(".switchYesNo").bootstrapSwitch({
    onText: 'हो',
    offText: 'होइन',
    size: 'small',
    onColor: 'success',
    offColor: 'danger'
});
$(".switchDo").bootstrapSwitch({
    onText: 'गर्ने',
    offText: 'नगर्ने',
    size: 'small',
    onColor: 'success',
    offColor: 'danger'
});
$(".datepicker").datepicker({
    format: 'yyyy-mm-dd',
    todayHighlight: true,
    autoclose: true,
    showOtherMonths: true,
    selectOtherMonths: false
});

$(".npdatepicker").nepaliDatePicker({
    npdMonth: true,
    npdYear: true,
    npdYearCount: 10
});





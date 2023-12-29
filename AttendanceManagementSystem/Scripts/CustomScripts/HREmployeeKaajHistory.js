
//-------------------------------------------
//Modify Prem Prakash Dhakal
$(document).ready(function () {
    $('#DataModel_IdKaajType').change(function () {

        $("#DataModel_KaajTitle").val($("#DataModel_IdKaajType option:selected").text());
        $("#DataModel_KaajShortName").val($("#DataModel_IdKaajType option:selected").text());
        $('.halfDay').hide();
        $('.recommend').hide();
        var date = $('#Appoinment').val();
        var dateNP = date.split('-');
        var Days = parseInt(dateNP[2]) + 2;
        if (Days.Length == 1)
            Days = '0' + Days;
        //alert(Days);
        var startDateNP = BsAddDays(dateNP[1] + '/' + Days + '/' + dateNP[0], 0);
        var d = startDateNP.split('-');
        var beforedate = d[1] + '/' + (d[2] - 1) + '/' + d[0];
        var dateNP = $('#Appoinment').val();
        $('#DateToNP').val(dateNP);
        PopulateEndDateNP($('#DateFromNP').val());
    });
});
$(document).ready(function () {
    $('#DataModel_KaajYearNP').change(function () {
        $('.halfDay').hide();
        $('.recommend').hide();
        var date = $('#Appoinment').val();
        var dateNP = date.split('-');
        var Days = parseInt(dateNP[2]) + 2;
        if (Days.Length == 1)
            Days = '0' + Days;
        var startDateNP = BsAddDays(dateNP[1] + '/' + Days + '/' + dateNP[0], 0);
        var d = startDateNP.split('-');
        var beforedate = d[1] + '/' + (d[2] - 1) + '/' + d[0];
        var dateNP = $('#Appoinment').val();
        $('#DateToNP').val(dateNP);
        PopulateEndDateNP($('#DateFromNP').val());
    });
});

$(document).ready(function () {
    $('.halfDay').hide();
    $('.recommend').hide();
    var date = $('#Appoinment').val();
    var dateNP = date.split('-');
    var Days = parseInt(dateNP[2]) + 2;
    if (Days.Length == 1)
        Days = '0' + Days;
    var startDateNP = BsAddDays(dateNP[1] + '/' + Days + '/' + dateNP[0], 0);
    var d = startDateNP.split('-');
    var beforedate = d[1] + '/' + (d[2] - 1) + '/' + d[0];
    $('#DateFromNP').nepaliDatePicker({
        
        npdMonth: true,
        npdYear: true,
        disableBefore: beforedate,
        onChange: function () {
            $('#DateToNP').val($('#DateFromNP').val());
            $('#DataModel_KaajFromDate').val(BS2AD($('#DateFromNP').val()));
            PopulateEndDateNP($('#DateFromNP').val());
        }
    });

    function PopulateEndDateNP(date) {
        PopulateKaajSummary($('#DataModel_IdHREmployee').val(), date);
        var dateNP = date.split('-');
        var startDateNP = BsAddDays(dateNP[1] + '/' + dateNP[2] + '/' + dateNP[0], 0);
        var d = startDateNP.split('-');
        var beforedate = d[1] + '/' + (d[2] - 1) + '/' + d[0];
        $('#DateToNP').nepaliDatePicker({
            npdMonth: true,
            npdYear: true,
            disableBefore: beforedate,
            //disableBefore: beforedate,
            onChange: function () {
                $('#DataModel_KaajToDate').val(BS2AD($('#DateToNP').val()));
            }
        });
    }

    function PopulateKaajSummary(idHREmployee, dateNP) {
        $.ajax({
            url: '/EmployeeManagement/HREmployeeKaajHistory/_GetHREmployeeKaajSummaryAsync',
            type: 'GET',
            dataType: "html",
            cache: false,
            async: false,
            data: { idHREmployee: idHREmployee, dateNP: dateNP },
            success: function (result) {
                $('#replacetarget').html(result);
            }
        });
    }

    $('#ddlKajVehicalSelected').multiselect({
        includeSelectAllOption: true,
        nSelectedText: 'चयन',
        nonSelectedText: 'चयन गर्नुहोस्',
        buttonWidth: 250,
        numberDisplayed: 5
    });
});






//-------------------------------------------
//Old Code 
//$(document).ready(function () {
//    $('#DateFromNP').nepaliDatePicker({
//        npdMonth: true,
//        npdYear: true,
//        onChange: function () {
//            $('#DateToNP').val($('#DateFromNP').val());
//            $('#DataModel_KaajFromDate').val(BS2AD($('#DateFromNP').val()));
//            PopulateEndDateNP($('#DateFromNP').val());

//        }
//    });

//    function PopulateEndDateNP(date) {
//        PopulateKaajSummary($('#DataModel_IdHREmployee').val(), date);
//        var dateNP = date.split('-');
//        var startDateNP = BsAddDays(dateNP[1] + '/' + dateNP[2] + '/' + dateNP[0], 0);
//        var d = startDateNP.split('-');
//        var beforedate = d[1] + '/' + (d[2] - 1) + '/' + d[0];
//        $('#DateToNP').nepaliDatePicker({
//            npdMonth: true,
//            npdYear: true,
//            disableBefore: beforedate,
//            //disableBefore: beforedate,
//            onChange: function () {
//                $('#DataModel_KaajToDate').val(BS2AD($('#DateToNP').val()));
//            }
//        });
//    }

//    function PopulateKaajSummary(idHREmployee, dateNP) {
//        $.ajax({
//            url: '/EmployeeManagement/HREmployeeKaajHistory/_GetHREmployeeKaajSummaryAsync',
//            type: 'GET',
//            dataType: "html",
//            cache: false,
//            async: false,
//            data: { idHREmployee: idHREmployee, dateNP: dateNP },
//            success: function (result) {
//                $('#replacetarget').html(result);
//            }
//        });
//    }

//    $('#ddlKajVehicalSelected').multiselect({
//        includeSelectAllOption: true,
//        nSelectedText: 'चयन',
//        nonSelectedText: 'चयन गर्नुहोस्',
//        buttonWidth: 250,
//        numberDisplayed: 5
//    });
//});

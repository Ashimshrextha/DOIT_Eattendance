$(document).ready(function () {

    $('.halfDay').hide();
    $('.recommend').hide();

    var date = $('#Appoinment').val();
    var dateNP = date.split('-');
    //alert(dateNP);
    var startDateNP = BsAddDays(dateNP[1] + '/' + dateNP[2] + '/' + dateNP[0], 0);
    var d = startDateNP.split('-');
    var beforedate = d[1] + '/' + (d[2] - 1) + '/' + d[0];

    $('#DataModel_IdLeaveType').change(function () {
        var id = $('#DataModel_IdHREmployee').val();
        if ($(this).val() >= 0) {
            CheckHalfDay($(this).val());
            IsRecommended($(this).val());
            PopulateLastTakenLeaveDate(id, $(this).val());
            PopulateBalanceLeave($(this).val(), $('#DateFromNP').val());
            PopulateAvailableDays(id, $(this).val(), $('#DateFromNP').val());
            PopulateLeaveSummary(id, $('#DateFromNP').val());
        }
        else {
            $('.halfDay').hide();
        }
    });

    $('#DateFromNP').nepaliDatePicker({
        npdMonth: true,
        npdYear: true,
        disableBefore: beforedate,
        onChange: function () {
            var dateNP = $('#DateFromNP').val();
            $('#DateToNP').val(dateNP);
            PopulateEndDateNP($('#DataModel_IdHREmployee').val(), $('#DataModel_IdLeaveType').val(), $('#DataModel_CurrentYearBalanceLeaveDay').val(), dateNP);
        }
    });

    function PopulateAvailableDays(idHREmployee, idLeaveType, dateNP) {
        $.ajax({
            url: '/EmployeeManagement/HREmployeeLeaveHistory/GetAvailableLeaveDays_Json',
            type: 'GET',
            dataType: 'JSON',
            cache: false,
            async: false,
            data: { idHREmployee: idHREmployee, idLeaveType: idLeaveType, dateNP: dateNP },
            success: function (result) {
                $('#DataModel_CurrentYearBalanceLeaveDay').val(result);
            }
        });
    }

    function PopulateBalanceLeave(idLeaveType, idHREmployee, dateNP) {
        $.ajax({
            url: '/EmployeeManagement/HREmployeeLeaveHistory/GetBalanceLeaveDays_Json',
            type: 'GET',
            dataType: 'JSON',
            cache: false,
            async: false,
            data: { idLeaveType: idLeaveType, idHREmployee: idHREmployee, dateNP: dateNP },
            success: function (result) {
                $('#DataModel_SavingLeave').val(result);
            }
        });
    }

    function PopulateLastTakenLeaveDate(idHREmployee, idLeaveType) {
        $.ajax({
            url: '/EmployeeManagement/HREmployeeLeaveHistory/GetLastDateLeave_Json',
            dataType: 'JSON',
            type: 'GET',
            data: { idHREmployee: idHREmployee },
            success: function (result) {
                $('#DateFromNP').val(result);
                PopulateFirstDateNP(result, idHREmployee, idLeaveType);
            }
        });
    }

    function PopulateFirstDateNP(dates, idHREmployee, idLeaveType) {
        var fdateNP = dates.split('-');
        var startfDateNP = fdateNP[1] + '/' + fdateNP[2] + '/' + fdateNP[0];
        $('#DateFromNP').nepaliDatePicker({
            npdMonth: true,
            npdYear: true,
            //  disableBefore: startfDateNP,
            onChange: function () {
                $('#DateToNP').val($('#DateFromNP').val());
                PopulateEndDateNP(idHREmployee, idLeaveType, $('#DataModel_CurrentYearBalanceLeaveDay').val(), $('#DateFromNP').val());
            }
        });
    }

    function PopulateEndDateNP(idhremployee, idLeaveType, day, startDateNP) {
        PopulateAvailableDays(idhremployee, idLeaveType, startDateNP);
        PopulateLeaveSummary(idhremployee, startDateNP);
        $.ajax({
            url: '/EmployeeManagement/HREmployeeLeaveHistory/GetEndDateOfLeave_Json',
            type: 'GET',
            dataType: 'JSON',
            cache: false,
            async: false,
            data: { idHREmployee: idhremployee, idLeaveType: idLeaveType, day: day, startdateNP: startDateNP },
            success: function (result) {
                $('#DateToNP').val(startDateNP);
                var firstDate = startDateNP.split('-');
                var disableBeforeDate = firstDate[1] + '/' + firstDate[2] + '/' + firstDate[0];
                var endDate = result.split('-');
                var disableAfterDate = endDate[1] + '/' + endDate[2] + '/' + endDate[0];
                $('#DateToNP').nepaliDatePicker({
                    npdMonth: true,
                    npdYear: true,
                    //disableBefore: disableBeforeDate,
                    // disableAfter: disableAfterDate
                });
            }
        });
    }

    function CheckHalfDay(idLeaveType) {
        $.ajax({
            url: '/EmployeeManagement/HREmployeeLeaveHistory/IsHalfDay_Json',
            type: 'GET',
            dataType: 'JSON',
            cache: false,
            async: false,
            data: { idLeaveType: idLeaveType },
            success: function (result) {
                if (result === "YES") {
                    $('.halfDay').show();
                }
                else {
                    $('.halfDay').hide();
                }
            }
        });
    }

    function PopulateLeaveSummary(idHREmployee, dateNP) {
        $.ajax({
            url: '/EmployeeManagement/HREmployeeLeaveHistory/_GetHREmployeeLeaveSummaryAsync',
            type: 'GET',
            dataType: "html",
            cache: false,
            async: false,
            data: { idHREmployee: idHREmployee, dateNP: dateNP },
            success: function (result) {
                $('#leaveSummary').html(result);
            }
        });
    }

    function IsRecommended(idLeaveType) {
        $.ajax({
            url: '/EmployeeManagement/HREmployeeLeaveHistory/IsRecommended_Json',
            type: 'GET',
            dataType: 'JSON',
            cache: false,
            async: false,
            data: { idLeaveType: idLeaveType },
            success: function (result) {
                if (result === "YES") {
                    $('.recommend').show();
                }
                else {
                    $('.recommend').hide();
                }
            }
        });
    }
});



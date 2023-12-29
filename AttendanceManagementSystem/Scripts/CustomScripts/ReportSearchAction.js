
$(document).ready(function () {

    $('#hiddenfile').hide();
    $('#JobStatusSection').hide();
    $('#EmployeeSection').hide();
    $('#DateSection').hide();
    $('#DivisionSection').hide();
    $('#YearSection').hide();
    $('#MonthSection').hide();
    $('#IdHRCompany').change(function () {
        if ($(this).val() != "") {
            PopulateParentCompany($(this).val());
            populateDivision($(this).val());
            populateHREmployeeJobStatus();
            populateShiftTitle($(this).val());
            populateEmployee($(this).val(), 0);
            $('#DateSection').fadeIn('slow');
            $('#YearSection').fadeIn('slow');
            $('#MonthSection').fadeIn('slow');
            $('#JobStatusSection').show('slow');
        }
        else {
            $('#hiddenfile').hide();
            $('#JobStatusSection').hide();
            $('#EmployeeSection').hide();
            $('#DateSection').hide();
            $('#DivisionSection').hide();
            $('#YearSection').hide();
            $('#MonthSection').hide();
            $('#JobStatusSection').show('slow');
        }
    });

    $('#IdDivision').change(function () {
        populateEmployee($('#IdHRCompany').val(), $(this).val());
        populateHREmployeeJobStatus($('#IdHRCompany').val(), $(this).val());
    });

    function PopulateParentCompany(idHRCompany) {
        $.ajax({
            url: '/AbsentReport/GetParentsCompany',
            type: 'GET',
            data: { idHRCompany: idHRCompany },
            dataType: 'json',
            success: function (result) {
                var data = result;
                var res = data.split('-');
                var name = res[0];
                var parentname = res[1];
                $('#ParentHRCompany').val(res);
                var option = $('#ParentName').val(name).css('color', 'green');
                $("label[for='ParentHRCompany']").text(parentname);
                $('#hiddenfile').fadeIn('slow');
            }
        });
    }

    function populateDivision(IdHRCompany) {
        $.ajax({
            url: '/AbsentReport/GetDivisionByCompany_Json',
            type: 'GET',
            dataType: 'JSON',
            data: { idHRCompany: IdHRCompany },
            success: function (result) {
                $('#DivisionSection').fadeIn('slow');
                var options = $('#IdDivision');
                $('option', options).remove();
                $.each(result, function () {
                    options.append($('<option/>').val(this.Key).text(this.Value));
                });
            }
        });
    }

    function populateEmployee(IdHRCompany, IdDivision) {
        $.ajax({
            url: '/AbsentReport/GetEmployeeByDivisionCompany_Json',
            type: 'GET',
            dataType: 'JSON',
            data: { idHRCompany: IdHRCompany, idDivision: IdDivision },
            success: function (result) {
                $('#EmployeeSection').fadeIn('slow');
                var options = $('#IdHREmployee');
                $('option', options).remove();
                $.each(result, function () {
                    options.append($('<option/>').val(this.Key).text(this.Value));
                });
            }
        });
    }

    function populateHREmployeeJobStatus() {
        $.ajax({
            url: '/AbsentReport/GetHREmployeeJobStatus_Json',
            type: 'GET',
            dataType: 'JSON',
            success: function (result) {
                $('#JobStatusSection').fadeIn('slow');
                var options = $('#IdJobStatus');
                $('option', options).remove();
                $.each(result, function () {
                    options.append($('<option/>').val(this.Key).text(this.Value));
                });
            }
        });
    }

    function populateShiftTitle(IdHRCompany) {
        $.ajax({
            url: '/AbsentReport/GetHRCompanyShiftTitle_Json',
            type: 'GET',
            dataType: 'JSON',
            data: { idHRCompany: IdHRCompany },
            success: function (result) {
                var options = $('#idShiftTitle');
                $('option', options).remove();
                $.each(result, function () {
                    options.append($('<option/>').val(this.Key).text(this.Value));
                });
            }
        });
    }

    $('#Date').datepicker({
        format: 'yyyy-mm-dd',
        autoclose: true,
        todayHighlight: true,
        changeMonth: true,
        changeYear: true
    });

});
$(document).ready(function () {

    $('#ConnectionModel_IdHRDevice').change(function () {
        $.ajax({
            url: '/DeviceManagement/HREmployeeBiometricTemplate/_GetDeviceConnectionComponentAsync',
            type: 'GET',
            dataType: 'html',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            cache: false,
            async: false,
            data: { idHRDevice: $(this).val() },
            success: function (data, status, xhr) {
                $('#deviceInfo').html(data);
            },
            error: function (jqXhr, textStatus, errorMessage) {
                alert(errorMessage);
            }
        });
    });

    $('#ConnectionModel_IdHRCompany').change(function () {
        $.ajax({
            url: '/DeviceManagement/HREmployeeBiometricTemplate/GetHRDeviceAsync_Json',
            type: 'GET',
            dataType: 'JSON',
            contentType: 'application/json',
            cache: false,
            async: false,
            data: { idHRCompany: $(this).val() },
            success: function (data, status, xhr) {
                var options = $('#ConnectionModel_IdHRDevice');
                $('option', options).remove();
                $.each(data, function () {
                    options.append($('<option/>').val(this.Key).text(this.Value));
                });
            },
            error: function (jqXhr, textStatus, errorMessage) {
                alert(errorMessage);
            }
        });
    });
});
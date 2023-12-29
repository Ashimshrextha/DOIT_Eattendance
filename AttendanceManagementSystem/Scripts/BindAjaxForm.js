function bindAjaxForm(dialog) {
    $('#ajaxForm').ajaxForm({
        success: function (data, textStatus, xhr) {
            if (data === '"_Login_"') {
                window.location = "/Account/Login";
                return;
            }
            $('#myModal').modal('hide');
            $('#replacetarget').html(data);
        },
        error: function (xhr, status) {
            $('#myModalContent').html(xhr.responseText, xhr.status);
            // enable reserve button again
            $('#btnSubmit').val($('#btnSubmit').val());
            $('#btnSubmit').prop('disabled', false);
            bindAjaxForm(dialog);
        }
    }).submit(function () {
        $('#btnSubmit').val($('#btnSubmit').val());
        $('#btnSubmit').prop('disabled', false);
    });
    return false;
}

$(document).on("click", "#example_paginate a", function () {
    if ($(this).attr("href") === null) return false;
    $('table,#mask').addClass('loading');
    $.ajax({
        url: $(this).attr("href"),
        type: 'GET',
        cache: false,
        success: function (result) {
            $('#content').html(result);
            $('table,#mask').removeClass('loading');
        }
    });
    return false;
});

$(function () {
    $.ajaxSetup({ cache: false });
    $(document).delegate("a[data-ajax-model]", "click", function (e) {
        $('table,#mask').addClass('loading');
        $('#myModalContent').load(this.href, function () {
            $('table,#mask').removeClass('loading');
            $('#myModal').modal({
                keyboard: true
            }, 'show');
            bindAjaxForm(this);
        });
        return false;
    });
});

$(document).on("click", "#example_paginate_popup a", function () {
    if ($(this).attr("href") === null) return false;
    $('table,#mask').addClass('loading');
    $.ajax({
        url: $(this).attr("href"),
        type: 'GET',
        cache: false,
        success: function (result) {
            $('#content_popup').html(result);
            $('table,#mask').removeClass('loading');
        }
    });
    return false;
});
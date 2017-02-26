$("#UseMyEmail").click(function (e) {
    if (e.toElement.checked) {
       
        $("#FromEmail").val(EmailUser);
        $("#FromEmail").prop('readonly', true);
    }
    else {
        $("#FromEmail").val("");
        $("#FromEmail").prop('readonly', false);
    }
});

$("#SendingNow").click(function (e) {
    if (e.toElement.checked) {
        $("#SendDate").val("");
        $("#SendDate").prop('disabled', true);
        $("#SendTime").val("");
        $("#SendTime").prop('disabled', true);
    }
    else {
        $("#SendDate").val("");
        $("#SendDate").prop('disabled', false);
        $("#SendTime").val("");
        $("#SendTime").prop('disabled',false);
    }
});

$('[data-confirm]').click(function (e) {
    if (!confirm($(this).attr("data-confirm"))) {
        e.preventDefault();
    }
});
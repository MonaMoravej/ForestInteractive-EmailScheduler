var checkUseMyMobile = function () {
    if ($("#UseMyEmail").prop('checked')) {
        $("#FromEmail").val(EmailUser);
        $("#FromEmail").prop('readonly', true);
    }
    else {
        $("#FromEmail").val("");
        $("#FromEmail").prop('readonly', false);
    }
};

var checkSendingNow = function () {
    if($("#SendingNow").prop('checked') ) {
        $("#SendDate").val("");
        $("#SendDate").prop('disabled', true);
        $("#SendTime").val("");
        $("#SendTime").prop('disabled', true);
    }
    else {
        $("#SendDate").val("");
        $("#SendDate").prop('disabled', false);
        $("#SendTime").val("");
        $("#SendTime").prop('disabled', false);
    }
}
checkUseMyMobile();
checkSendingNow();
$("#UseMyEmail").click(function (e) {
    checkUseMyMobile();
});

$("#SendingNow").click(function (e) {
    checkSendingNow();
});

$('[data-confirm]').click(function (e) {
    if (!confirm($(this).attr("data-confirm"))) {
        e.preventDefault();
    }
});



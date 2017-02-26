
$.validator.addMethod("comparemethod", function (value, element, params) {
    var sendtime = $(element).val();
    var senddate = $("#SendDate").val();
   
    if (senddate != "" && senddate != null && senddate != undefined && senddate != 'undefined'
        && sendtime != "" && sendtime != null && sendtime != undefined && sendtime != 'undefined')
    {
        var currentDate = new Date();
        var min = params['compareminutes'];
        var compare = params['comparestate'];
        var compareDateTime = DiffMinutes(compare, currentDate, min);
        if (compareDateTime != null) {
            var selectedDateTime = new Date(senddate+' '+sendtime);
            switch (compare) {
                case 'Before':
                    if (selectedDateTime > dateToCompare) return false;
                    break;
                case 'After':
                    if (selectedDateTime < dateToCompare) return false;
                    break;
                default:
                    return true;
                    break;
            }
        }
       
    }
    return true;

    function DiffMinutes(compare, date, minutes) {
        switch (compare) {
            case 'Before':
                return new Date(date.getTime() - minutes * 60000);
                break;
            case 'After':
                return new Date(date.getTime() + minutes * 60000);
                break;
            default:
                return null;
                break;
        }
       
    }

});

$.validator.unobtrusive.adapters.add("comparesenddatetimewithnow", ['compareminutes', 'comparestate'], function (options) {
    options.rules['comparemethod'] = options.params;
    options.messages['comparemethod'] = options.message;
});

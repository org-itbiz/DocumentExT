(function ($) {
    $.extend($, {
        procAjaxData: function (data, funcSuc, funcErr) {
            if (!data.Statu) {
                return;
            }

            switch (data.Statu) {
                case "OK":
                    alert("OK:" + data.Msg);
                    if (funcSuc) funcSuc(data);
                    break;
                case "ERROR":
                    alert("ERROR:" + data.Msg);
                    if (funcErr) funcErr(data);
                    break;
            }
        }
    });
}(jQuery));
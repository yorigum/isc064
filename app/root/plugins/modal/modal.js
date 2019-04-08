var loader = '<div id="loader"><br /><br /><br />'
                       + ' <div align="center" class="cssload-fond"> '
	                   + '     <div class="cssload-container-general">'
			           + '             <div class="cssload-internal"><div class="cssload-ballcolor cssload-ball_1"> </div></div>'
			           + '             <div class="cssload-internal"><div class="cssload-ballcolor cssload-ball_2"> </div></div>'
			           + '             <div class="cssload-internal"><div class="cssload-ballcolor cssload-ball_3"> </div></div>'
			           + '             <div class="cssload-internal"><div class="cssload-ballcolor cssload-ball_4"> </div></div>'
	                   + '     </div> '
                       + ' </div><br /><br /><br />'
                       + ' <p style="font-size:20px;text-align:center;">Please Wait......</p><br /><br /><br /></div>';
var error = '';
$(function () {
    //==================================================================================== OPEN MODAL 
    $('[show-modal]').on('click', function (e) {
        var target = jQuery(this).attr('show-modal');
        //var target = "#ModalPopUp";

        //=========================== adding Title
        var title = $(jQuery(this)).attr('modal-title');
        $(target).find('#modal-title').html(title);


        //=========================== Adding required url parameter
        var require_param = jQuery(this).attr('require-param');
        var url = jQuery(this).attr('modal-url');
        if (require_param != null) {
            var optionalparam = "&";
            var a = require_param.split(';');
            //alert(require_param);
            for (var i = 0; i < a.length; i++) {
                var b = a[i].split('=');
                if (b.length > 1) {
                    if (b[0] == 'param') {
                        optionalparam += b[1] + "=";
                    }
                    if (b[0] == 'ctrl') {
                        if ($(b[1]).val() != null) {
                            optionalparam += $(b[1]).val();
                        }
                    }
                }
            }
            url += optionalparam;

        }



        //=========================== adding Style
        var style = jQuery(this).attr('modal-content-style');
        $('.modal-content').attr('style', '');
        $('.modal-content').attr('style', style);

        //=========================== adding Style
        var style = jQuery(this).attr('modal-body-style');
        $('.modal-body').attr('style', '');
        $('.modal-body').attr('style', style);

        //onclose event
        var onclose_event = jQuery(this).attr('modal-onclose');
        $(target).find('.close').attr('modal-onclose', onclose_event);

        //=========================== Loading Page
        //=========================== Modal Type
        $(target).find('.modal-body').html(loader);
        $(target).find('.modal-body').html('<iframe id="modal-frame" style="width:99%;height:100%;" src="' + url + '"></iframe>');

        $(target).fadeIn(350);
        e.preventDefault();
    });

    //==================================================================================== CLOSE MODAL 
    $('[close-modal]').on('click', function (e) {
        var target = jQuery(this).attr('close-modal');
        $(target).find('.modal-body').html(loader);
        $(target).fadeOut(350);
        var close_event = jQuery(this).attr('modal-onclose');
        jQuery.globalEval(close_event);
        e.preventDefault();
    });
});
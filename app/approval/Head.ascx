<%@ Control Language="c#" Inherits="ISC064.APPROVAL.Head" CodeFile="Head.ascx.cs" %>
<script type="text/javascript" src="/Js/JQuery.min.js"></script>
<script type="text/javascript" src="/Js/Common.js"></script>
<script type="text/javascript" src="/Js/MD5.js"></script>
<script type="text/javascript" src="/Js/NumberFormat.js"></script>
<link href="/plugins/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
<link href="/plugins/bootstrap-datepicker/dist/css/datetime.css" rel="stylesheet" />
<link href="/plugins/modal/modal.css" rel="stylesheet" />
<link href="/plugins/modal/loader.css" rel="stylesheet" />
<script src="/plugins/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
<script src="/plugins/modal/modal.js"></script>
<div id="ModalPopUp" class="modal">
	<div class="overlay"></div>
	<div class="modal-content" style=";">
	    <div class="modal-header">
	      <span id="close"  class="close" close-modal="#ModalPopUp">&times;</span>
	      <b id="modal-title">Modal</b>
	    </div>
	    <div class="modal-body"></div>
	    <div class="modal-footer"></div>
	</div>
</div>
<script type="text/javascript">
    function popEditKontrak(nomor) {
        openPopUp('/approval/KontrakApproveDiskon2.aspx?nomor=' + nomor, '1050', '650')
    }
    function popLogDetil(logid, tb, sumber) {
        openPopUp('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber, '600', '550')
    }
    function popEditKontrak2(nomor) {
        openPopUp('/marketingjual/KontrakEdit.aspx?NoKontrak=' + nomor, '1050', '650')
    }
    function popEditCustomer(nomor) {
        openPopUp('/marketingjual/CustomerEdit.aspx?NoCustomer=' + nomor, '920', '650')
    }
    function popEditReservasi(nomor) {
        openPopUp('/marketingjual/ReservasiEdit.aspx?NoReservasi=' + nomor, '920', '650')
    }
    function popEditTTR(nomor) {
        openPopUp('/marketingjual/TTREdit.aspx?NoTTR=' + nomor, '920', '650')
    }
    function globalclose() {
        document.getElementById('close').click();
    }
    function init() {
        $(document).ready(function () {
            $('#dari,#sampai,#date,#tgllahir,#tglktp,#tglot').datepicker({
                autoclose: true,
                format: 'dd M yyyy',
                language: 'en'
            });
        })
    }
    init();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(init);
</script>

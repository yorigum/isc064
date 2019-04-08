<%@ Control Language="c#" Inherits="ISC064.NUP.Head" CodeFile="Head.ascx.cs" %>
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
<script type="text/javascript">
    function popDaftarCustomer2(status, project) {
        openModal('/nup/DaftarCustomer2.aspx?status=' + status + '&project=' + project, '770', '500')
    }
    function popNUP(nomor, Tipe, Project) {
        openPopUp('/nup/NUPEdit.aspx?NoNUP=' + nomor + '&Jenis=' + Tipe + '&Project=' + Project, '950', '650')
    }
    function popLog(logid, tb, sumber, pk, Jenis) {
        if (pk != null)
            openModal('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber + '&pk=' + pk + '&Jenis=' + Jenis, '600', '550');
        else
            openModal('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber, '600', '550');
    }
</script>
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
    function globalclose() {
        document.getElementById('close').click();
    }
    $(document).ready(function () {
        $('#dari,#sampai,#date,#tgllahir,#tglktp,#tglot,#tgl,#tbTglTerima,.tgl').datepicker({
            autoclose: true,
            format: 'dd M yyyy',
            language: 'en'
        });
    })
</script>

<%@ Control Language="c#" Inherits="ISC064.SETTINGS.Head" CodeFile="Head.ascx.cs" %>
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
    //add javascript function here
    function PopHtmlEditor(id, project) {
        openModal('/settings/HtmlEditorEdit.aspx?id=' + id + '&project=' + project, '1121', '650');
    }
    function PopNumerator(id) {
        openPopUp('/settings/NumeratorFile.aspx?id=' + id, '1121', '650');
    }
    function PopHelpHtmlEditor() {
        openModal('/settings/HtmlEditorHelp.aspx', '1121', '450');
    }
    function popEditJenis(nomor) {
        openPopUp('/settings/JenisEdit.aspx?NoJenis=' + nomor, '920', '650')
    }
    function popEditJenis(nomor) {
        openPopUp('/settings/JenisEdit.aspx?NoJenis=' + nomor, '920', '650')
    }
    function popEditTipeSales(nomor) {
        openPopUp('/settings/TipeSalesEdit.aspx?NoTipe=' + nomor, '920', '650')
    }
    function popEditLevelSales(nomor) {
        openPopUp('/settings/LevelSalesEdit.aspx?NoLevel=' + nomor, '920', '650')
    }
    function popEditLokasi(nomor) {
        openPopUp('/settings/LokasiEdit.aspx?NoLokasi=' + nomor, '920', '650')
    }
    function popEditBerkasPPJB(nomor) {
        openPopUp('/settings/BerkasPPJBEdit.aspx?NoBerkas=' + nomor, '920', '650')
    }
    function popEditFollowUp(nomor) {
        openPopUp('/settings/FollowUpEdit.aspx?NoJenis=' + nomor, '920', '650')
    }
    function popEditJenisProperti(nomor) {
        openPopUp('/settings/JenisPropertiEdit.aspx?NoJenis=' + nomor, '920', '650')
    }
    function popEditLokasiKontrak(nomor) {
        openPopUp('/settings/LokasiKontrakEdit.aspx?NoLokasi=' + nomor, '920', '650')
    }
    function popSkema(nomor) {
        openPopUp('/settings/SkemaEdit.aspx?Nomor=' + nomor, '1230', '650')
    }
    function popDaftarAktif(tb, tb2) {
        openPopUp('/settings/DaftarUserAktif2.aspx?ctrl=' + tb + '&ctrl2=' + tb2, '920', '650')
    }
    function popEditAcc(nomor) {
        openPopUp('/settings/AccEdit.aspx?Acc=' + nomor, '920', '650')
    }
    function popEditEmail(nomor) {
        openPopUp('/settings/AlamatEmailEdit.aspx?ID=' + nomor, '920', '650')
    }
    function popLogDetil(logid, tb, sumber) {
        openPopUp('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber, '600', '550')
    }
    function popLogDetil2(logid, tb, sumber, pk) {
        openPopUp('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber + pk + '&pk=' +pk, '600', '550')
    }
    function popLog(logid, tb, sumber, pk) {
        if (pk != null)
            openModal('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber + '&pk=' + pk, '600', '550');
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
        $('#dari,#sampai,#date,.tgl').datepicker({
            autoclose: true,
            format: 'dd M yyyy',
            language: 'en'
        });
    })
</script>

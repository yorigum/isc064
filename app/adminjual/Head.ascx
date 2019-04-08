<%@ Control Language="c#" Inherits="ISC064.ADMINJUAL.Head" CodeFile="Head.ascx.cs" %>
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
    function popDaftarAgent(status) {
        openModal('/adminjual/DaftarAgent.aspx?status=' + status, '550', '500')
    }
    function popDaftarUnit(status) {
        openModal('/adminjual/DaftarUnit.aspx?status=' + status, '770', '500')
    }
    function popSkema(nomor) {
        openPopUp('/adminjual/SkemaEdit.aspx?Nomor=' + nomor, '1230', '650')
    }
    function popSkom(nomor) {
        openPopUp('/adminjual/SkomEdit.aspx?Nomor=' + nomor, '980', '650')
    }
    function popEditAgent(nomor) {
        openPopUp('/adminjual/AgentEdit.aspx?NoAgent=' + nomor, '920', '650')
    }
    function popEditAgentLevel(nomor) {
        openPopUp('/adminjual/AgentLevelEdit.aspx?LevelID=' + nomor, '920', '650')
    }
    function popEditUnit(nomor) {
        openPopUp('/adminjual/UnitEdit.aspx?NoStock=' + nomor, '920', '650')
    }
    function popEditTipeSales(nomor) {
        openPopUp('/adminjual/TipeSalesEdit.aspx?NoTipe=' + nomor, '920', '650')
    }
    function popEditLevelSales(nomor) {
        openPopUp('/adminjual/LevelSalesEdit.aspx?NoLevel=' + nomor, '920', '650')
    }
    function popEditComplain(nomor) {
        openPopUp('/adminjual/ComplainEdit.aspx?NoComplain=' + nomor, '920', '650')
    }
    function popEditPemilik(nomor) {
        openPopUp('/adminjual/UnitEditPemilik.aspx?NoStock=' + nomor, '920', '650')
    }
    function popEditJenis(nomor) {
        openPopUp('/adminjual/JenisEdit.aspx?NoJenis=' + nomor, '920', '650')
    }
    function popEditLokasi(nomor) {
        openPopUp('/adminjual/LokasiEdit.aspx?NoLokasi=' + nomor, '920', '650')
    }
    function popEditKomisiOver(nomor) {
        openPopUp('/adminjual/KomisiOverEdit.aspx?SN=' + nomor, '920', '650')
    }
    function popEditKomisiCF(nomor) {
        openPopUp('/adminjual/KomisiCFEdit.aspx?Lvl=' + nomor, '920', '650')
    }
    function popEditFollowUp(nomor) {
        openPopUp('/adminjual/FollowUpEdit.aspx?No=' + nomor, '920', '650')
    }
    function popEditTipeGimmick(nomor) {
        openPopUp('/adminjual/TipeGimmickEdit.aspx?ID=' + nomor, '920', '650')
    }
    function popEditMGimmick(nomor) {
        openPopUp('/adminjual/GimmickEdit.aspx?ID=' + nomor, '920', '650')
    }
    function popPeta(f) {
        openPopUp('/adminjual/PetaDetil.aspx?f=' + f, '920', '650')
    }
    function popLogDetil(logid, tb, sumber) {
        openPopUp('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber, '600', '550')
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

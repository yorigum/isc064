<%@ Control Language="c#" Inherits="ISC064.COLLECTION.Head" CodeFile="Head.ascx.cs" %>
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
    function popDaftarPJT(status) {
        openModal('/collection/DaftarPJT.aspx?status=' + status, '770', '500')
    }
    function popDaftarTunggakan(status) {
        openModal('/collection/DaftarTunggakan.aspx?status=' + status, '770', '500')
    }
    function popDaftarSKL() {
        openModal('/collection/DaftarSKL.aspx?', '770', '500')
    }
    function popCIF(ref, tipe) {
        openPopUp('/collection/CustomerInfo.aspx?Ref=' + ref + '&Tipe=' + tipe, '920', '650')
    }
    function popEditPJT(nomor) {
        openPopUp('/collection/PJTEdit.aspx?NoPJT=' + nomor, '920', '650')
    }
    function popEditSKL(nomor) {
        openPopUp('/collection/SKLEdit.aspx?NoSKL=' + nomor, '920', '650')
    }
    function popEditTunggakan(nomor) {
        openPopUp('/collection/TunggakanEdit.aspx?NoTunggakan=' + nomor, '920', '650')
    }
    function popEditKontrak(nomor) {
        openPopUp('/marketingjual/KontrakEdit.aspx?NoKontrak=' + nomor, '1050', '650')
    }
    function popUnit(nomor) {
        openPopUp('/marketingjual/UnitInfo.aspx?NoStock=' + nomor, '920', '650')
    }
    function popEditCustomer(nomor) {
        openPopUp('/marketingjual/CustomerEdit.aspx?NoCustomer=' + nomor, '920', '650')
    }
    function popEditFU(nomor, tipe) {
        openPopUp('/collection/HistoryFolUp.aspx?Ref=' + nomor + '&Tipe=' + tipe, '920', '650')
    }
    function popEditFollowUp(nomor) {
        openPopUp('/collection/GroupingFollowUpEdit.aspx?NoJenis=' + nomor, '920', '650')
    }
    function popEditFolUp(nomor, tipe) {
        openPopUp('/collection/FollowUpEdit.aspx?NoFU=' + nomor, '920', '650')
    }
    function popEditRealisasi(nomor, tipe) {
        openPopUp('/collection/RealisasiDenda2.aspx?NoKontrak=' + nomor, '920', '650')
    }
    function popEditPutih(nomor, tipe) {
        openPopUp('/collection/PemutihanDenda2.aspx?NoKontrak=' + nomor, '920', '650')
    }
    function popLogDetil(logid, tb, sumber) {
        openPopUp('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber, '600', '550')
    }
    function popFU(nokontrak, nourut) {
        openPopUp('/collection/FollowUpDaftar.aspx?Ref=' + nokontrak + '&NoUrut=' + nourut, '920', '650')
    }
    function popHistoryFU(nokontrak, nourut) {
        openPopUp('/collection/HistoryFU.aspx?Ref=' + nokontrak + '&NoUrut=' + nourut, '920', '650')
    }
    function popLog(logid, tb, sumber, pk) {
        if (pk != null)
            openModal('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber + '&pk=' + pk, '600', '600');
        else
            openModal('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber, '600', '600');
    }
    function popDaftarKontrak(status) {
        openModal('DaftarKontrak.aspx?status=' + status, '770', '500')
    }
    function popDaftarKontrak2(status) {
        openModal('DaftarKontrak.aspx?status=' + status + '&dd=1', '770', '500')
    }
    function popJadwalTagihan(nomor) {
        openPopUp('/marketingjual/KontrakJadwalTagihan.aspx?NoKontrak=' + nomor, '920', '650')
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
        $('#dari,#sampai,#date,#tgllahir,#tglktp,#tglot,#tgl,.tgl,#tgljt').datepicker({
            autoclose: true,
            format: 'dd M yyyy',
            language: 'en'
        });
    })
</script>
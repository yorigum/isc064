<%@ Control Language="c#" Inherits="ISC064.KPA.Head" CodeFile="Head.ascx.cs" %>
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
    function popDaftarCustomer(status) {
        openModal('/marketingjual/DaftarCustomer.aspx?status=' + status, '770', '500')
    }
    function popDaftarReservasi(status) {
        openModal('/marketingjual/DaftarReservasi.aspx?status=' + status, '770', '500')
    }
    function popDaftarKontrak(status) {
        openModal('/kpa/DaftarKontrak.aspx?status=' + status, '770', '500')
    }
    function popDaftarKontrak(status, carabayar) {
        openModal('/kpa/DaftarKontrak.aspx?status=' + status, '770', '500')
    }
    function popDaftarUnit(status) {
        openModal('/marketingjual/DaftarUnit.aspx?l=1&status=' + status, '770', '500')
    }
    function popDaftarUnit2(status) {
        openModal('/marketingjual/DaftarUnit2.aspx?l=1&status=' + status, '770', '500')
    }
    function popDaftarUnit4(status) {
        openModal('/marketingjual/DaftarUnit3.aspx?l=1&status=' + status, '770', '500')
    }
    function popDaftarUnit3(status) {
        openModal('/marketingjual/DaftarUnit.aspx?gu=1&status=' + status, '770', '500')
    }
    function popUnit(nomor) {
        openPopUp('/marketingjual/UnitInfo.aspx?NoStock=' + nomor, '920', '650')
    }
    function popEditUnit(nomor) {
        openPopUp('/adminjual/UnitEdit.aspx?l=1&NoStock=' + nomor, '920', '650')
    }
    function popJadwalTagihan(nomor) {
        openPopUp('/marketingjual/KontrakJadwalTagihan.aspx?NoKontrak=' + nomor, '920', '650')
    }
    function popJadwalKomisi(nomor) {
        openPopUp('/adminjual/AgentJadwalKomisi.aspx?NoAgent=' + nomor, '920', '650')
    }
    function popSkema(nomor, pl, tgl) {
        openPopUp('/marketingjual/Skema.aspx?Nomor=' + nomor + '&pl=' + pl + '&tgl=' + tgl, '920', '650')
    }
    function popEditCustomer(nomor) {
        openPopUp('/marketingjual/CustomerEdit.aspx?NoCustomer=' + nomor, '920', '650')
    }
    function popEditReservasi(nomor) {
        openPopUp('/marketingjual/ReservasiEdit.aspx?NoReservasi=' + nomor, '920', '650')
    }
    function popEditKontrak(nomor) {
        openPopUp('/marketingjual/KontrakEdit.aspx?NoKontrak=' + nomor, '920', '650')
    }
    function popLog(logid, tb, sumber, pk) {
        if (pk != null)
            openModal('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber + '&pk=' + pk, '600', '550');
        else
            openModal('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber, '600', '550');
    }
    function popDaftarAgent(status) {
        openModal('/marketingjual/DaftarAgent.aspx?status=' + status, '770', '500');
    }
    function popDaftarTTR(status) {
        openModal('/marketingjual/DaftarTTR.aspx?status=' + status, '770', '500')
    }
    function popEditTTR(nomor) {
        openPopUp('/marketingjual/TTREdit.aspx?NoTTR=' + nomor, '920', '650')
    }
    function popDaftarVA() {
        openModal('/marketingjual/DaftarVA.aspx', '770', '500');
    }
    function popEditBankKPA(nomor, project) {
        openPopUp('/kpa/AccEdit.aspx?Kode=' + nomor + '&project=' + project, '920', '650')
    }
    function popKontrakKPA(nomor) {
        openPopUp('/kpa/KontrakProses.aspx?NoKontrak=' + nomor, '920', '650')
    }
    function popEditKontrakKPA(nomor) {
        openPopUp('/kpa/KontrakEdit.aspx?NoKontrak=' + nomor, '920', '650')
    }
    function popEditBerkas(nomor) {
        openPopUp('/kpa/KontrakBerkas.aspx?NoKontrak=' + nomor, '920', '650')
    }
    function popEditWawancara(nomor) {
        openPopUp('/kpa/KontrakWawancaraEdit.aspx?NoKontrak=' + nomor, '920', '650')
    }
    function popEditOTS(nomor) {
        openPopUp('/kpa/KontrakOTSEdit.aspx?NoKontrak=' + nomor, '920', '650')
    }
    function popEditLPA(nomor) {
        openPopUp('/kpa/KontrakLPAEdit.aspx?NoKontrak=' + nomor, '920', '650')
    }
    function popEditSP3K(nomor) {
        openPopUp('/kpa/KontrakSP3KEdit.aspx?NoKontrak=' + nomor, '920', '650')
    }
    function popEditAkad(nomor) {
        openPopUp('/kpa/KontrakAkadEdit.aspx?NoKontrak=' + nomor, '920', '650')
    }
    function popEditRetensi(nomor, project) {
        openPopUp('/kpa/RetensiEdit.aspx?kode=' + nomor + '&project=' + project, '920', '650')
    }
    function popRegisPengajuan(nomor) {
        openPopUp('/kpa/KontrakPengajuan.aspx?NoKontrak=' + nomor, '920', '650')
    }
    function popDaftarPengajuan(status) {
        openModal('/kpa/DaftarPengajuan.aspx?status=' + status, '770', '500')
    }
    function popEditPengajuan(nomor) {
        openPopUp('/kpa/PengajuanEdit.aspx?id=' + nomor, '800', '650')
    }
    function popDaftarRealisasi(status) {
        openModal('/kpa/DaftarRealisasi.aspx?status=' + status, '770', '500')
    }
    function popEditRealisasi(nomor) {
        openPopUp('/kpa/RealisasiEdit.aspx?id=' + nomor, '900', '650')
    }
    function popEditAcc(nomor, project) {
        openPopUp('/kpa/AccEdit.aspx?Kode=' + nomor + '&project=' + project, '920', '650')
    }
    function popLogDetil(logid, tb, sumber) {
        openPopUp('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber, '600', '550')
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
        $('#dari,#sampai,#date,#tgllahir,#tglktp,#tglot,#tgl,#tglmemo,.tgl').datepicker({
            autoclose: true,
            format: 'dd M yyyy',
            language: 'en'
        });
    })
</script>


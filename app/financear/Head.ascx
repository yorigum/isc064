<%@ Control Language="c#" Inherits="ISC064.FINANCEAR.Head" CodeFile="Head.ascx.cs" %>
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
    function popDaftarTTS(status) {
        openModal('/financear/DaftarTTS.aspx?status=' + status, '770', '500')
    }
    function popDaftarTA(status) {
        openModal('/financear/DaftarTA.aspx?status=' + status, '770', '500')
    }
    function popDaftarTB(status) {
        openModal('/financear/DaftarTB.aspx?status=' + status, '770', '500')
    }
    function popDaftarBG(status) {
        openModal('/financear/DaftarBG.aspx?status=' + status, '770', '500')
    }
    function popDaftarUnit(status) {
        openModal('/marketingjual/DaftarUnit.aspx?status=' + status, '770', '500')
    }
    function popDaftarUnit2(status) {
        openModal('/marketingjual/DaftarUnit.aspx?va=1', '770', '500')
    }
    function popDaftarVA() {
        openModal('/financear/DaftarVA.aspx', '770', '500')
    }
    function popCIF(ref, tipe) {
        openPopUp('/financear/CustomerInfo.aspx?Ref=' + ref + '&Tipe=' + tipe, '1120', '650')
    }
    function popEditPelunasan(tipe, nomor) {
        openPopUp('/financear/CustomerLunas.aspx?Tipe=' + tipe + '&Ref=' + nomor, '993', '650')
    }
    function popEditTTS(nomor) {
        openPopUp('/financear/TTSEdit.aspx?NoTTS=' + nomor, '931', '650')
    }
    function popEditTA(nomor) {
        openPopUp('/financear/TransferAnonimEdit.aspx?NoAnonim=' + nomor, '920', '650')
    }
    function popEditMEMO(nomor) {
        openPopUp('/financear/MEMOEdit.aspx?NoMemo=' + nomor, '920', '650')
    }
    function popEditCB(nomor) {
        openPopUp('/financear/CBEdit.aspx?NoCb=' + nomor, '920', '650')
    }
    function popEditVA(nomor) {
        openPopUp('/financear/VAEdit.aspx?NoVA=' + nomor, '920', '650')
    }
    function popEditAnonim(nomor) {
        openPopUp('/financear/TransferAnonimEdit.aspx?NoAnonim=' + nomor, '920', '650')
    }
    function popEditAcc(nomor) {
        openPopUp('/financear/AccEdit.aspx?Acc=' + nomor, '920', '650')
    }
    function popEditKasMasuk(nomor) {
        openPopUp('/financear/KasMasukEdit.aspx?NoVoucher=' + nomor, '920', '650')
    }
    function popEditKasKeluar(nomor) {
        openPopUp('/financear/KasKeluarEdit.aspx?NoVoucher=' + nomor, '920', '650')
    }
    function popLog(logid, tb, sumber, pk) {
        if (pk != null)
            openModal('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber + '&pk=' + pk, '600', '550');
        else
            openModal('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber, '600', '550');
    }
    function popPrintTTSMulti(from, to) {
        openPrint('PrintTTSMulti.aspx?from=' + from + '&to=' + to, '920', '650');
    }
    function popPrintBKMMulti(from, to) {
        openPrint('PrintBKMMulti.aspx?from=' + from + '&to=' + to, '920', '650');
    }
    function popPrintBKMBatch() {
        openPrint('PrintBKMBatch.aspx', '920', '650');
    }
    function popDaftarMEMO(status) {
        openModal('/financear/DaftarMEMO.aspx?status=' + status, '770', '500')
    }
    function popDaftarFP(ctrl1, ctrl2, project, tgl) {
        openModal('/financear/DaftarFP.aspx?ctrl1=' + ctrl1 + '&ctrl2=' + ctrl2 + '&project=' + project + '&tgl=' + tgl, '770', '500')
    }
    function popLogDetil(logid, tb, sumber) {
        openPopUp('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber, '600', '550')
    }
    function popLogDetil2(logid, tb, sumber,pk) {
        openPopUp('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber + '&pk=' + pk, '600', '550')
    }
    function popDaftarKontrak(status) {
        openModal('/marketingjual/DaftarKontrak.aspx?status=' + status, '770', '500')
    }
    function popEditCB(nomor) {
        openPopUp('/financear/CBEdit.aspx?Nocb=' + nomor, '920', '650')
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
	function globalclose(){
        document.getElementById('close').click();
    }
	$(document).ready(function(){
	    $('#tglbkm, #tgltts, #tglfp, #tglbg, #tgljtbg, #dari, #sampai, #date, #tgllahir, #tglktp, #tglot, #tgl, #tglmemo, .tgl').datepicker({
            autoclose: true,
            format: 'dd M yyyy',
            language: 'en'
        });
	})
</script>

<%@ Control Language="c#" Inherits="ISC064.LAUNCHING.Head" CodeFile="Head.ascx.cs" %>
<script type="text/javascript" src="/Js/JQuery.min.js"></script>
<script language="javascript" src="/Js/Pop.js"></script>
<script language="javascript" src="/Js/MD5.js"></script>
<script language="javascript" src="/Js/NumberFormat.js"></script>
<link href="/plugins/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
<link href="/plugins/bootstrap-datepicker/dist/css/datetime.css" rel="stylesheet" />
<link href="/plugins/modal/modal.css" rel="stylesheet" />
<link href="/plugins/modal/loader.css" rel="stylesheet" />
<script src="/plugins/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
<script src="/plugins/modal/modal.js"></script>
<script language="javascript">
function popEditTTS(nomor) {
	openPopUp('/launching/TTSEdit.aspx?NoTTS='+nomor,'920','650')
}
function popDaftarCustomer(status) {
	openModal('/marketingjual/DaftarCustomer.aspx?status=' + status,'770','500')
}
function popDaftarNUP(status) {
    openModal('/marketingjual/DaftarNUP1.aspx?status=' + status, '770', '500')
}
function popDaftarReservasi(status) {
	openModal('/marketingjual/DaftarReservasi.aspx?status='+status,'770','500')
}
function popDaftarKontrak(status) {
	openModal('/marketingjual/DaftarKontrak.aspx?status='+status,'770','500')
}
function popDaftarUnit(status) {
    openModal('/marketingjual/DaftarUnit.aspx?l=1&status=' + status, '770', '500')
}
function popDaftarUnit2(status) {
	openModal('/marketingjual/DaftarUnit2.aspx?l=1&status='+status,'770','500')
}
function popDaftarUnit4(status) {
    openModal('/marketingjual/DaftarUnit.aspx?nup=1&status=' + status, '770', '500')
}
function popDaftarUnit3(status) {
    openModal('/marketingjual/DaftarUnit.aspx?gu=1&status=' + status, '770', '500')
}
function popUnit(nomor) {
	openPopUp('/marketingjual/UnitInfo.aspx?NoStock='+nomor,'920','650')
}
function popJadwalTagihan(nomor) {
	openPopUp('/launching/KontrakJadwalTagihan.aspx?NoKontrak='+nomor,'920','650')
}
function popJadwalKomisi(nomor) {
    //	openPopUp('/marketingjual/KontrakJadwalKomisi.aspx?NoKontrak='+nomor,'920','650')
    openPopUp('/marketingjual/KontrakJadwalKomisi.aspx?NoKontrak=' + nomor, '920', '650')
}
function popSkema(nomor,pl,tgl) {
	openPopUp('/marketingjual/Skema.aspx?Nomor='+nomor+'&pl='+pl+'&tgl='+tgl,'920','650')
}
function popEditCustomer(nomor) {
	openPopUp('/marketingjual/CustomerEdit.aspx?NoCustomer='+nomor,'920','650')
}
function popEditReservasi(nomor) {
	openPopUp('/marketingjual/ReservasiEdit.aspx?NoReservasi='+nomor,'920','650')
}
function popEditKontrak(nomor) {
	openPopUp('/launching/KontrakEdit.aspx?NoKontrak='+nomor,'920','650')
}
function popKPREdit(nomor) {
	openPopUp('/launching/KontrakEdit.aspx?NoKontrak='+nomor,'920','650')
}
function popLog(logid,tb,sumber,pk) {
	if(pk!=null)
		openModal('LogDetil.aspx?LogID='+logid+'&tb='+tb+'&sumber='+sumber+'&pk='+pk,'600','550');
	else
		openModal('LogDetil.aspx?LogID='+logid+'&tb='+tb+'&sumber='+sumber,'600','550');
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
function popDaftarVA2(l,nomor) {
    openModal('/marketingjual/DaftarVA.aspx?l=1&stock=' + nomor, '770', '500');
}
function popDaftarPP(status) {
    openModal('/marketingjual/DaftarPP.aspx?status=' + status, '770', '500')
}
function popNUP(nomor,Tipe,Project) {
    openPopUp('/nup/NUPEdit.aspx?NoNUP=' + nomor + '&Jenis=' + Tipe + '&Project=' + Project, '950', '650')
}
function popKalku(nomor, stock, tipe, cb) {
    openPopUp('/launching/KalkulatorSkema2.aspx?NoNUP=' + nomor + '&NoStock=' + stock + '&Tipe=' + tipe + '&CB=' + cb, '950', '650')
}
function popKalku2(nomor, stock, tipe, cb, project) {
    openPopUp('/launching/KalkulatorSkema.aspx?NoNUP=' + nomor + '&NoStock=' + stock + '&Tipe=' + tipe + '&cby=' + cb + '&project=' + project, '950', '650')
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
	    $('#dari,#sampai,#date,#tgllahir,#tglktp,#tglot,#tglkontrak,#tgl,#tglkembali,#tglgu,#tglgn,#dptgl,#bftgl,#angtgl,#tglKontrak,#tglbg,#barutgl,#targetst,#tglditerima,.tgl').datepicker({
            autoclose: true,
            format: 'dd M yyyy',
            language: 'en'
        });
	})
</script>

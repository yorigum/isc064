<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.Head" CodeFile="Head.ascx.cs" %>
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
    openModal('/marketingjual/DaftarCustomer.aspx?status='+status,'770','500')
}
function popDaftarReservasi(status) {
    openModal('/marketingjual/DaftarReservasi.aspx?status='+status,'770','500')
}
function popDaftarKontrak(status) {
  	openModal('/marketingjual/DaftarKontrak.aspx?status='+status,'770','500')
}
function popDaftarUnit(status) {
    openModal('/marketingjual/DaftarUnit.aspx?status='+status,'770','500')
}
function popDaftarUnit2(status) {
	openModal('/marketingjual/DaftarUnit.aspx?l=1&status='+status,'770','500')
}
function popDaftarUnit3(status) {
	openModal('/marketingjual/DaftarUnit.aspx?gu=1&status='+status,'770','500')
}
function popDaftarUnit4(status) {
    openModal('/marketingjual/DaftarUnit.aspx?calc=1&status=' + status, '770', '500')
}
function popUnit(nomor) {
	openPopUp('/marketingjual/UnitInfo.aspx?NoStock='+nomor,'920','650')
}
function popJadwalTagihan(nomor) {
	openPopUp('/marketingjual/KontrakJadwalTagihan.aspx?NoKontrak='+nomor,'920','650')
}
function popJadwalKomisi(nomor) {
    //	openPopUp('/marketingjual/KontrakJadwalKomisi.aspx?NoKontrak='+nomor,'920','650')
    openPopUp('/adminjual/AgentJadwalKomisi.aspx?NoAgent=' + nomor, '920', '650')
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
	openPopUp('/marketingjual/KontrakEdit.aspx?NoKontrak='+nomor,'1050','650')
}
function popLog(logid,tb,sumber,pk) {
	if(pk!=null)
		openModal('LogDetil.aspx?LogID='+logid+'&tb='+tb+'&sumber='+sumber+'&pk='+pk,'600','550');
	else
		openModal('LogDetil.aspx?LogID='+logid+'&tb='+tb+'&sumber='+sumber,'600','550');
}
//function popDaftarAgent(status) {
//	openModal('/marketingjual/DaftarAgent.aspx?status=' + status, '770', '500');
    //}
function popDaftarAgent(status) {
    if (navigator.userAgent.indexOf("MSIE") != -1) openModal('/marketingjual/DaftarAgent.aspx?status=' + status, '770', '500');
    window.open('/marketingjual/DaftarAgent.aspx?status=' + status, "Daftar Agent", "scrollbars=1,resizable=1,Width=770,Height=500,Left=80");
}
function popDaftarTTR(status) {
	openModal('/marketingjual/DaftarTTR.aspx?status=' + status, '770', '500')
}
function popEditTTR(nomor) {
	openPopUp('/marketingjual/TTREdit.aspx?NoTTR=' + nomor, '920', '650')
}
function popDaftarVA(nomor) {
    openModal('/marketingjual/DaftarVA.aspx?NoStock=' + nomor, '770', '500');
}
function popDaftarReff() {
    openModal('/marketingjual/DaftarReff.aspx', '770', '500');
}
function popLogDetil(logid, tb, sumber) {
    openPopUp('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber, '600', '550')
}
function ClearGimmick(ctrl1, ctrl2, ctrl4, ctrl5, ctrl6, ctrl7, ctrl8) {
    foo1 = document.getElementById(ctrl1);
    foo2 = document.getElementById(ctrl2);
    foo4 = document.getElementById(ctrl4);
    foo5 = document.getElementById(ctrl5);
    foo6 = document.getElementById(ctrl6);
    foo7 = document.getElementById(ctrl7);
    foo8 = document.getElementById(ctrl8);

    foo1.value = '';
    foo2.value = '';
    foo4.value = '';
    foo5.value = '';
    foo6.value = '';
    foo7.value = '0';
    foo8.value = '';
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
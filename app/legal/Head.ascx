<%@ Control Language="c#" Inherits="ISC064.LEGAL.Head" CodeFile="Head.ascx.cs" %>
<script type="text/javascript" src="/Js/JQuery.min.js"></script>
<script type="text/javascript" src="/Js/Pop.js"></script>
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
  	openModal('/legal/DaftarKontrak.aspx?status='+status,'770','500')
}
function popDaftarPPJB(status) {
    openModal('/legal/DaftarPPJB.aspx?status=' + status, '770', '500')
}
function popDaftarAJB(status) {
    openModal('/legal/DaftarAJB.aspx?status=' + status, '770', '500')
}
function popDaftarST(status) {
    openModal('/legal/DaftarST.aspx?status=' + status, '770', '500')
}
function popDaftarIMB(status) {
    openModal('/legal/DaftarIMB.aspx?status=' + status, '770', '500')
}
function popDaftarSertifikat(status) {
    openModal('/legal/DaftarSertifikat.aspx?status=' + status, '770', '500')
}
function popLogDetil(logid, tb, sumber) {
    openPopUp('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber, '600', '550')
}
function popEditCustomer(nomor) {
    openPopUp('/marketingjual/CustomerEdit.aspx?NoCustomer='+nomor,'920','650')
}
function popEditReservasi(nomor) {
    openPopUp('/marketingjual/ReservasiEdit.aspx?NoReservasi='+nomor,'920','650')
}
function popEditKontrak(nomor) {
    openPopUp('/legal/KontrakEdit.aspx?NoKontrak='+nomor,'1050','650')
}
function popEditTTR(nomor) {
    openPopUp('/marketingjual/TTREdit.aspx?NoTTR=' + nomor, '920', '650')
}

//function popEditKontrak(nomor) {
//	openPopUp('/legal/KontrakEdit.aspx?NoKontrak='+nomor,'1050','650')
//}
function popKontrakPPJBEdit(nomor) {
    openPopUp('/legal/KontrakPPJBEdit.aspx?NoKontrak=' + nomor, '1050', '650')
}
function popKontrakAJBEdit(nomor) {
    openPopUp('/legal/KontrakAJBEdit.aspx?NoKontrak=' + nomor, '1050', '650')
}
function popKontrakIMBEdit(nomor) {
    openPopUp('/legal/KontrakIMBEdit.aspx?NoKontrak=' + nomor, '1050', '650')
}
function popKontrakSertifikatEdit(nomor) {
    openPopUp('/legal/KontrakSertifikatEdit.aspx?NoKontrak=' + nomor, '1050', '650')
}
function popKontrakSTEdit(nomor) {
    openPopUp('/legal/KontrakSTEdit.aspx?NoKontrak=' + nomor, '1050', '650')
}
function popLog(logid,tb,sumber,pk) {
	if(pk!=null)
		openModal('LogDetil.aspx?LogID='+logid+'&tb='+tb+'&sumber='+sumber+'&pk='+pk,'600','550');
	else
		openModal('LogDetil.aspx?LogID='+logid+'&tb='+tb+'&sumber='+sumber,'600','550');
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
		$('#dari,#sampai,#date,.tgl').datepicker({
            autoclose: true,
            format: 'dd M yyyy',
            language: 'en'
        });
	})
</script>
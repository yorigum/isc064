<%@ Control Language="c#" Inherits="ISC064.SECURITY.Head" CodeFile="Head.ascx.cs" %>
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
function popHalaman(hal) {
	openModal('MappingDetil.aspx?Halaman='+hal,'600','650')
}
function popPlugin(hal) {
    openModal('MappingPluginDetil.aspx?Halaman=' + hal, '600', '650')
}
function popDaftarAktif() {
    openModal('DaftarUserAktif.aspx','500','400')
}
function popDaftarBlokir() {
	openModal('DaftarUserBlokir.aspx','600','400')
}
function popEditUser(nomor) {
	openPopUp('EditUser.aspx?UserID='+nomor,'920','650')
}
function popProject(project) {
    openPopUp('Project2.aspx?Project=' + project, '920', '650')
}
function popHalaman2(hal) {
    openPopUp('MappingDetil.aspx?Halaman='+hal, '600', '650')
}
function popHalaman3(hal) {
    openPopUp('MappingPluginDetil.aspx?Halaman='+ hal, '600', '650')
}
function popSecLevel(nomor) {
	openPopUp('SecLevelEdit.aspx?Kode='+nomor,'920','650')
}
function popRefData(nomor) {
    openPopUp('MasterData.aspx?Kode=' + nomor, '920', '650')
}
function popTandaTangan(dok) {
    openPopUp('TandaTanganEdit.aspx?Dokumen=' + dok, '920', '650')
}
function popLogDetil(logid,tb,sumber) {
    openPopUp('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber, '600', '550')
    }
    function popEditCounterLaunching(nomor) {
	openPopUp('CounterLaunchingEdit.aspx?id='+nomor,'920','650')
}
function popLog(logid, tb, sumber, pk) {
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
		$('#dari,#sampai,#date').datepicker({
            autoclose: true,
            format: 'dd M yyyy',
            language: 'en'
        });
	})
</script>
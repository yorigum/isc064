<%@ Control Language="c#" Inherits="ISC064.KOMISI.Head" CodeFile="Head.ascx.cs" %>
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
    function popLog(logid, tb, sumber, pk) {
        if (pk != null)
            openModal('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber + '&pk=' + pk, '600', '550');
        else
            openModal('LogDetil.aspx?LogID=' + logid + '&tb=' + tb + '&sumber=' + sumber, '600', '550');
    }
    function popSkom(nomor) {
        openPopUp('SkemaKomisiEdit.aspx?Nomor=' + nomor, '980', '650')
    }
    function popSkomTerm(nomor) {
        openPopUp('TerminKomisiEdit.aspx?Nomor=' + nomor, '980', '650')
    }
    function popSkomCF(nomor) {
        openPopUp('SkemaCFEdit.aspx?Nomor=' + nomor, '980', '650')
    }
    function popSkomReward(nomor) {
        openPopUp('SkemaRewardEdit.aspx?Nomor=' + nomor, '980', '650')
    }
    function popEditCF(nomor, project) {
        openPopUp('CFEdit.aspx?Nomor=' + nomor + '&project=' + project, '920', '650')
    }
    function popEditCFP(nomor, project) {
        openPopUp('CFPEdit.aspx?Nomor=' + nomor + '&project=' + project, '920', '650')
    }
    function popEditCFR(nomor, project) {
        openPopUp('CFREdit.aspx?Nomor=' + nomor + '&project=' + project, '920', '650')
    }
    function popEditKomisi(nomor) {
        openPopUp('KomisiEdit.aspx?Nomor=' + nomor, '920', '650')
    }
    function popEditKomisiP(nomor) {
        openPopUp('KomisiPEdit.aspx?Nomor=' + nomor, '920', '650')
    }
    function popEditKomisiR(nomor) {
        openPopUp('KomisiREdit.aspx?Nomor=' + nomor, '920', '650')
    }
    function popEditReward(nomor) {
        openPopUp('RewardEdit.aspx?Nomor=' + nomor, '920', '650')
    }
    function popEditRewardP(nomor) {
        openPopUp('RewardPEdit.aspx?Nomor=' + nomor, '920', '650')
    }
    function popEditRewardR(nomor) {
        openPopUp('RewardREdit.aspx?Nomor=' + nomor, '920', '650')
    }
    //Clear
    function ClearSkema1(ctrl1) {
        foo1 = document.getElementById(ctrl1);

        foo1.value = '';
    }
    function ClearSkema2(ctrl1, ctrl2, ctrl3) {
        foo1 = document.getElementById(ctrl1);
        foo2 = document.getElementById(ctrl2);
        foo3 = document.getElementById(ctrl3);

        foo1.value = '';
        foo2.value = '';
        foo3.value = '';
    }
    function ClearSkema3(ctrl1, ctrl2) {
        foo1 = document.getElementById(ctrl1);
        foo2 = document.getElementById(ctrl2);

        foo1.value = '';
        foo2.value = '';
    }
    function ClearTermin(ctrl1, ctrl2, ctrl3, ctrl4, ctrl5, ctrl6, ctrl7, ctrl8, ctrl9, ctrl10, ctrl11, ctrl12, ctrl13) {
        foo1 = document.getElementById(ctrl1);
        foo2 = document.getElementById(ctrl2);
        foo3 = document.getElementById(ctrl3);
        foo4 = document.getElementById(ctrl4);
        foo5 = document.getElementById(ctrl5);
        foo6 = document.getElementById(ctrl6);
        foo7 = document.getElementById(ctrl7);
        foo8 = document.getElementById(ctrl8);
        foo9 = document.getElementById(ctrl9);
        foo10 = document.getElementById(ctrl10);
        foo11 = document.getElementById(ctrl11);
        foo12 = document.getElementById(ctrl12);
        foo13 = document.getElementById(ctrl13);

        foo1.value = '';
        foo2.value = '';
        foo3.checked = false;
        foo4.value = '';
        foo5.checked = false;
        foo6.value = '';
        foo7.checked = false;
        foo8.value = '';
        foo9.checked = false;
        foo10.value = '';
        foo11.checked = false;
        foo12.checked = false;
        foo13.checked = false;
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
        $('#dari,#sampai,#date,#tgl,.tgl').datepicker({
            autoclose: true,
            format: 'dd M yyyy',
            language: 'en'
        });
    })
</script>
<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.HeadCustomer" CodeFile="HeadCustomer.ascx.cs" %>
<script type="text/javascript" src="/Js/JQuery.min.js"></script>
<script type="text/javascript" src="/Js/Common.js"></script>
<script type="text/javascript" src="/Js/MD5.js"></script>
<script type="text/javascript" src="/Js/NumberFormat.js"></script>
<link href="/plugins/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
<link href="/plugins/bootstrap-datepicker/dist/css/datetime.css" rel="stylesheet" />
<script src="/plugins/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
<script type="text/javascript">
	preva = MM_preloadImages('/Media/icon_prev_a.gif');
	prevo = MM_preloadImages('/Media/icon_prev_o.gif');
	prevc = MM_preloadImages('/Media/icon_prev_c.gif');
	nexta = MM_preloadImages('/Media/icon_next_a.gif');
	nexto = MM_preloadImages('/Media/icon_next_o.gif');
	nextc = MM_preloadImages('/Media/icon_next_c.gif');
	function MM_preloadImages() {
		x = new Image;
		x.src = MM_preloadImages.arguments[0];
		return x
	}
	function sc(foo,imgnew) {
		if (document.images) {foo.src=eval(imgnew + ".src");}
	}
</script>
<script src="/plugins/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
<link href="/plugins/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
<link href="/plugins/bootstrap-datepicker/dist/css/datetime.css" rel="stylesheet" />
<table cellspacing="5">
	<tr>
		<td><a id="prev" runat="server" accesskey="," class="abtn"><i class="fa fa-long-arrow-left"></i> <b>Prev</b></a></td>
		<td><a id="next" runat="server" accesskey="." class="abtn"><b>Next</b> <i class="fa fa-long-arrow-right"></i></a></td>
		<td style="font-size:8pt;padding-left:10px">
			No. Customer :
			<asp:label id="nocustomer" runat="server" font-bold="True"></asp:label>
		</td>
		<td style="font-size:8pt;padding-left:10px">
			Nama :
			<asp:label id="nama" runat="server" font-bold="True"></asp:label>
		</td>
		<td style="font-size:8pt;padding-left:10px">
			Sales Account :
			<asp:label id="agent" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
</table>
<hr size="1" noshade style="color:silver;margin:0;border-bottom:1px solid silver">
<script type="text/javascript">
	function globalclose(){
        document.getElementById('close').click();
    }
	$(document).ready(function(){
	    $('#dari,#sampai,#date,#tgllahir,#tglktp,#tglot,#tglkontrak,#tgl,#tglkembali,#tglgu,#tglgn,#dptgl,#bftgl,#angtgl,.tgl').datepicker({
            autoclose: true,
            format: 'dd M yyyy',
            language: 'en'
        });
	})
</script>

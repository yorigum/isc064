<%@ Control Language="c#" Inherits="ISC064.FINANCEAR.HeadVA" CodeFile="HeadVA.ascx.cs" %>

<script type="text/javascript" src="/Js/Common.js"></script>
<script type="text/javascript" src="/Js/MD5.js"></script>
<script type="text/javascript" src="/Js/NumberFormat.js"></script>
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
	function sc(foo, imgnew) {
		if (document.images) { foo.src = eval(imgnew + ".src"); }
	}
</script>

<table cellspacing="5">
	<tr>
		<td><a id="prev" runat="server" accesskey="," class="abtn"><i class="fa fa-long-arrow-left"></i> <b>Prev</b> </a></td>
		<td><a id="next" runat="server" accesskey="." class="abtn"> <b>Next</b> <i class="fa fa-long-arrow-right"></i></a></td>
		<td style="font-size: 8pt; padding-left: 10px">
			No. VA :
			<asp:Label ID="nova" runat="server" Font-Bold="True"></asp:Label>
		</td>
		<td style="font-size: 8pt; padding-left: 10px">
			Ref. :
			<asp:Label ID="referensi" runat="server" Font-Bold="True"></asp:Label>
		</td>
		<td style="font-size: 8pt; padding-left: 10px">
			Unit :
			<asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
		</td>
		<td style="font-size: 8pt; padding-left: 10px">
			Customer :
			<asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
		</td>
	</tr>
</table>
<hr size="1" noshade style="color: SILVER; margin: 0; border-bottom: 1px solid silver">

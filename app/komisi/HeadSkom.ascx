<%@ Control Language="c#" Inherits="ISC064.KOMISI.HeadSkom" CodeFile="HeadSkom.ascx.cs" %>
<script type="text/javascript" src="/Js/Pop.js"></script>
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
	function sc(foo,imgnew) {
		if (document.images) {foo.src=eval(imgnew + ".src");}
	}
</script>
<table cellspacing="5">
	<tr>
		<td><a id="prev" runat="server" accesskey=","><img src="/Media/icon_prev_a.gif" onmouseover="sc(this,'prevo')" onmousedown="sc(this,'prevc')"
					onmouseout="sc(this,'preva')" border="0"></a></td>
		<td><a id="next" runat="server" accesskey="."><img src="/Media/icon_next_a.gif" onmouseover="sc(this,'nexto')" onmousedown="sc(this,'nextc')"
					onmouseout="sc(this,'nexta')" border="0"></a></td>
		<td style="font-size:8pt;padding-left:10">
			Nomor :
			<asp:label id="nomor" runat="server" font-bold="True"></asp:label>
		</td>
		<td style="font-size:8pt;padding-left:10">
			Nama :
			<asp:label id="nama" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
</table>
<hr size="1" noshade style="color:SILVER;margin:0;border-bottom:1px dashed silver">

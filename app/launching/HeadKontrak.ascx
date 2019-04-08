<%@ Control Language="c#" Inherits="ISC064.LAUNCHING.HeadKontrak" CodeFile="HeadKontrak.ascx.cs" %>
<script language="javascript" src="/Js/Pop.js"></script>
<script language="javascript" src="/Js/MD5.js"></script>
<script language="javascript" src="/Js/NumberFormat.js"></script>
<script language="javascript">
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
			No. Kontrak :
			<asp:label id="nokontrak" runat="server" font-bold="True"></asp:label>
		</td>
		<td style="font-size:8pt;padding-left:10">
			Unit :
			<asp:label id="unit" runat="server" font-bold="True"></asp:label>
		</td>
		<td style="font-size:8pt;padding-left:10">
			Customer :
			<asp:label id="customer" runat="server" font-bold="True"></asp:label>
		</td>
		<td style="font-size:8pt;padding-left:10">
			Sales :
			<asp:label id="agent" runat="server" font-bold="True"></asp:label>
		</td>
	</tr>
</table>
<hr size="1" noshade style="color:SILVER;margin:0;border-bottom:1px dashed silver">

<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.PrintKUPTemplate" CodeFile="PrintKUPTemplate.ascx.cs" %>
<div style="LEFT: 8.5cm; POSITION: absolute; TOP: 4.7cm">
	<asp:label id="namacust" runat="server"></asp:label>
</div>
<div style="LEFT: 8.5cm; POSITION: absolute; TOP: 5.4cm">
	<asp:label id="alamat" runat="server"></asp:label>
</div>
<div style="LEFT: 8.5cm; POSITION: absolute; TOP: 6.6cm">
	<asp:label id="notelp" runat="server"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;/&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	<asp:label id="nohp" runat="server"></asp:label>
</div>
<div style="LEFT: 8.5cm; POSITION: absolute; TOP: 7.2cm">
	<asp:label id="noktp" runat="server"></asp:label>
</div>
<div style="LEFT: 8.5cm; POSITION: absolute; TOP: 7.8cm">
	<asp:label id="jnsusaha" runat="server"></asp:label>
</div>
<div style="LEFT: 8.5cm; POSITION: absolute; TOP: 8.4cm">
	<asp:label id="sumber" runat="server"></asp:label>
</div>
<div style="LEFT: 6.8cm; POSITION: absolute; TOP: 11cm">
	<asp:label id="lokasi" runat="server"></asp:label>
</div>
<div style="LEFT: 6.8cm; POSITION: absolute; TOP: 11.8cm">
	<asp:label id="unit" runat="server"></asp:label>
</div>
<div style="LEFT: 6.8cm; POSITION: absolute; TOP: 12.5cm">
	<asp:label id="tipe" runat="server"></asp:label>
</div>
<div style="LEFT: 6.8cm; POSITION: absolute; TOP: 13.3cm">
	<asp:label id="luas" runat="server"></asp:label>
</div>
<div style="LEFT: 7.2cm; POSITION: absolute; TOP: 14cm">
	<asp:label id="harga" runat="server"></asp:label>
</div>
<div style="LEFT: 2.5cm; POSITION: absolute; TOP: 15.5cm">
	<asp:label id="chkcara1" runat="server"></asp:label>
</div>
<div style="LEFT: 7.7cm; POSITION: absolute; TOP: 15.5cm">
	<asp:label id="chkcara2" runat="server"></asp:label>
</div>
<div style="LEFT: 13.5cm; POSITION: absolute; TOP: 15.5cm">
	<asp:label id="chkcara3" runat="server"></asp:label>
</div>
<div style="LEFT: 8.4cm; POSITION: absolute; TOP: 16.6cm">
	<asp:label id="bf" runat="server"></asp:label></TD>
</div>
<div style="LEFT: 11.2cm; FONT: 8pt Arial; POSITION: absolute; TOP: 16.6cm">
	<asp:label id="bf2" runat="server"></asp:label>&nbsp;&nbsp;RUPIAH</TD>
</div>
<div style="LEFT: 15.5cm; POSITION: absolute; TOP: 25.9cm">
	<asp:label id="agent" runat="server"></asp:label></TD>
</div>
<div style="LEFT: 3.8cm; POSITION: absolute; TOP: 21.4cm">
	<% Response.Write(ISC064.Cf.Day(DateTime.Today)); %>
</div>

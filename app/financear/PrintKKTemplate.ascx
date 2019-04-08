<%@ Control Language="c#" Inherits="ISC064.FINANCEAR.PrintKKTemplate" CodeFile="PrintKKTemplate.ascx.cs" %>
<font style="font-size: 14pt;"><strong>BUKTI PENGELUARAN KAS BANK</strong></font>
<br>
No. :
<asp:label id="nomorl" runat="server"></asp:label>
<br>
Tgl :
<asp:label id="tgl" runat="server"></asp:label>
<br>
Rekening :
<asp:label id="acc" runat="server"></asp:label>
<br>
Keterangan :
<asp:label id="keterangan" runat="server"></asp:label>
<br>
Dibayar Kepada :
<asp:label id="dibayarkepada" runat="server"></asp:label>
<br>
Nilai :
<asp:label id="nilai" runat="server"></asp:label>
<br>
Cara Bayar :
<asp:label id="carabayar" runat="server"></asp:label>
<br>
Alat Bayar :
<asp:label id="alatbayar" runat="server"></asp:label>
<br>
Terbilang :
<asp:label id="terbilang" runat="server"></asp:label>
<div style="position: absolute; left: 15.7cm; top: 8.5cm;">
	<asp:label id="lblTgl" runat="server"></asp:label>
</div>
<div style="position: absolute; left: 19.9cm; top: 8.5cm;">
	<asp:label id="lblThn" runat="server"></asp:label>
</div>
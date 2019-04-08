<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.PrintJadwalTagihanReservasiTemplate" CodeFile="PrintJadwalTagihanReservasiTemplate.ascx.cs" %>
<div style="TEXT-ALIGN: center; WIDTH: 100%">
	<h4>SCHEDULE PEMBAYARAN <br />
	    MARGONDA RESIDENCE VI</h4>
</div>
<div style="padding-left:10px;">
	<table width="100%">
	    <tr>
	        <td colspan="7"><asp:Label ID="skema" runat="server"></asp:Label></td>
	    </tr>
	    <tr>
	        <td width="10%">Nama</td>
	        <td width="1%">:</td>
	        <td colspan="5"><asp:Label ID="customer" runat="server"></asp:Label></td>
	    </tr>
	    <%--<tr>
	        <td width="10%">Unit</td>
	        <td width="1%">:</td>
	        <td colspan="5"><asp:Label ID="unit" runat="server"></asp:Label></td>
	    </tr>--%>
	    <tr>
	        <td width="10%">No. Unit</td>
	        <td width="1%">:</td>
	        <td colspan="5"><asp:Label ID="nounit" runat="server"></asp:Label></td>
	    </tr>
	    <tr>
	        <td width="10%">Size</td>
	        <td width="1%">:</td>
	        <td width="55%"><asp:Label ID="size" runat="server"></asp:Label>M2(Semi Gross)</td>
	        <td width="15%">Harga</td>
	        <td width="1%">:</td>
	        <td width="3%">Rp.</td>
	        <td width="15%" align="right"><asp:Label ID="harga" runat="server"></asp:Label></td>
	    </tr>
	    <tr>
	        <td>Type</td>
	        <td>:</td>
	        <td><asp:Label ID="tipe" runat="server"></asp:Label></td>
	        <td>PPN 10%</td>
	        <td>:</td>
	        <td width="3%">Rp.</td>
	        <td align="right" style="border-bottom: solid 1px black;"><asp:Label ID="ppn" runat="server"></asp:Label></td>
	    </tr>
	    <tr>
	        <td>Menghadap</td>
	        <td>:</td>
	        <td><asp:Label ID="view" runat="server"></asp:Label></td>
	        <td>Total Harga Jual</td>
	        <td>:</td>
	        <td width="3%">Rp.</td>
	        <td align="right"><asp:Label ID="hargajual" runat="server"></asp:Label></td>
	    </tr>
	    <tr>
	        <td colspan="7">&nbsp;<%--<asp:Label ID="skema2" runat="server"></asp:Label>--%></td>
	    </tr>
	    <tr>
	        <td valign="top" style="width:100px;"><b><asp:Label ID="skema2" runat="server"></asp:Label></b></td>
	        <td colspan="6" align="left">
	            <asp:Table ID="tb" runat="server" CellSpacing="0" Width="50%">
                    <asp:TableHeaderRow BorderWidth="1" BorderStyle="Solid">
                        <asp:TableHeaderCell HorizontalAlign="Center">Pembayaran</asp:TableHeaderCell>
                        <asp:TableHeaderCell HorizontalAlign="Center">Jumlah</asp:TableHeaderCell>
                        <asp:TableHeaderCell HorizontalAlign="Center">Jatuh Tempo</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
	        </td>
	    </tr>
	</table>
	<br />
	<table width="100%">
	    <tr>
	        <td width="33%" align="center">Mengetahui</td>
	        <td width="34%" align="center">Dibuat oleh,</td>
	        <td width="33%" align="center">Menyetujui,</td>
	    </tr>
	    <tr>
	        <td>
	            <br />
	            <br />
	            <br />
	            <br />
	        </td>
	        <td>&nbsp;</td>
	        <td>&nbsp;</td>
	    </tr>
	    <tr>
	        <td align="center">Gustav</td>
	        <td align="center">Yudi Kurniawan</td>
	        <td align="center"><asp:Label ID="customer2" runat="server"></asp:Label></td>
	    </tr>
	</table>
	<br />
	<table width="100%">
	    <tr>
	        <td width="10%" valign="top">Note :</td>
	        <td>
	            <ol>
	                <li>
	                    Pembayaran dapat ditransfer melalui Bank BCA Cabang Depok sesuai dengan Virtual Account Pemesan unit
	                </li>
	                <li>
	                    Bukti transfer, foto copy Kartu Keluarga dan KTP mohon di fax ke no : 7761054 u.p Administrasi / Keuangan
	                    Margonda Residence VI
	                </li>
	                <li>
	                    Apabila ada perubahan nama dalam surat pesanan, wajib melaporkan dan mendapat Persetujuan dari pihak Apertemen
	                    Margonda Residence VI
	                </li>
	                <li>
	                    Apabila setelah serah terima unit pembayaran belum lunas, maka status unit hanya hak pinjam pakai (tercantum
	                    dalam PPJB )
	                </li>
	                <li>
	                    Apabila terjadi keterlambatan pembayaran akan dikenakan sanksi sesuai dengan ketentuan yang berlaku ( tercantum
	                    dalam surat pesanan PPJB)
	                </li>
	                <li>
	                    Dalam waktu 14 (empat belas) hari sejak pembayaran Booking Fee tidak melakukan pembayaran DP atau Angsuran dan
	                    seterusnya maka secara otomatis menjadi batal atau dianggap mengundurkan diri dan uang Booking Fee tidak dapat
	                    dikembalikan (Hangus)
	                </li>
	            </ol>
	        </td>
	    </tr>
	</table>
</div>

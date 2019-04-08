<%@ Control Language="c#" Inherits="ISC064.FINANCEAR.PrintFPSTemplate" CodeFile="PrintFPSTemplate.ascx.cs" %>

<div align="center">
    <span style="font-size: 14pt;"><b>FAKTUR PAJAK</b></span>

    <table style="border: 1px solid black; border-collapse: collapse; width: 100%" cellpadding="5px;">
        <tr>
            <td style="border: 1px solid black;" colspan="3">Kode dan Nomor Seri Faktur Pajak :
                <asp:Label ID="nopajak" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td style="border: 1px solid black;" colspan="3">
                <b>PENGUSAHA KENA PAJAK</b>
            </td>
        </tr>

        <tr>
            <td colspan="3">
                <table style="border: 0px;">
                    <tr>
                        <td width="20%">Nama</td>
                        <td width="1%">:</td>
                        <td>
                            <asp:Label ID="npwpnama" runat="server"></asp:Label></td>
                    </tr>

                    <tr>
                        <td width="20%" valign="top">Alamat</td>
                        <td width="1%" valign="top">:</td>
                        <td>
                            <asp:Label ID="npwpalamat" runat="server"></asp:Label></td>
                    </tr>

                    <tr>
                        <td width="20%">NPWP</td>
                        <td width="1%">:</td>
                        <td>
                            <asp:Label ID="npwpno" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="border: 1px solid black;" colspan="3">
                <b>PEMBELI BARANG KENA PAJAK / PENERIMA JASA KENA PAJAK</b>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table>
                    <tr>
                        <td width="20%">Nama</td>
                        <td width="1%">:</td>
                        <td>
                            <asp:Label ID="nama" runat="server"></asp:Label></td>
                    </tr>

                    <tr>
                        <td width="20%" valign="top">Alamat</td>
                        <td width="1%" valign="top">:</td>
                        <td>
                            <asp:Label ID="alamat" runat="server"></asp:Label></td>
                    </tr>

                    <tr>
                        <td width="20%">NPWP</td>
                        <td width="1%">:</td>
                        <td>
                            <asp:Label ID="npwp" runat="server"></asp:Label></td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td style="border: 1px solid black;" align="center" width="10%">No Urut</td>
            <td style="border: 1px solid black;" align="center">Nama Barang Kena Pajak / Jasa Kena Pajak</td>
            <td style="border: 1px solid black;" align="center" width="25%">Harga Jual / Penggantian / Uang Muka / Termin<br />
                (Rp)</td>
        </tr>
        <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
    </table>
    <br />
    <br />
    <table width="100%">
        <tr>
            <td colspan="2">Pajak Penjualan Atas Barang Mewah</td>
        </tr>

        <tr>
            <td width="50%">
                <table style="border: 1px solid black; border-collapse: collapse;" cellpadding="4">
                    <tr>
                        <td align="center" style="border: 1px solid black; width: 100%">Tarif</td>
                        <td align="center" style="border: 1px solid black; width: 100%">DPP</td>
                        <td align="center" style="border: 1px solid black; width: 100%">PPn BM</td>
                    </tr>

                    <tr>
                        <td align="center" style="border-right: 1px solid black;">........... %</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                    </tr>
                    <tr>
                        <td align="center" style="border-right: 1px solid black;">........... %</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                    </tr>
                    <tr>
                        <td align="center" style="border-right: 1px solid black;">........... %</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                    </tr>
                    <tr>
                        <td align="center" style="border-right: 1px solid black;">........... %</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                    </tr>
                    <tr>
                        <td style="border: 1px solid black;" colspan="2">Jumlah</td>
                        <td align="center" style="border: 1px solid black;">Rp. ...........</td>
                    </tr>

                </table>
            </td>

            <td width="20%">&nbsp;</td>

            <td width="50%">
                <table>
                    <tr>
                        <td align="center">Jakarta,
                            <asp:Label ID="tgl" runat="server"></asp:Label>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            (<asp:Label ID="ttd" runat="server"></asp:Label>)
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td colspan="2">*) Coret yang tidak perlu</td>
        </tr>
    </table>
</div>

<div align="center" style="page-break-before: always;">
    <span style="font-size: 14pt;"><b>FAKTUR PAJAK</b></span>

    <table style="border: 1px solid black; border-collapse: collapse; width: 100%" cellpadding="5px;">
        <tr>
            <td style="border: 1px solid black;" colspan="3">Kode dan Nomor Seri Faktur Pajak :
                <asp:Label ID="nopajak2" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td style="border: 1px solid black;" colspan="3">
                <b>PENGUSAHA KENA PAJAK</b>
            </td>
        </tr>

        <tr>
            <td colspan="3">
                <table style="border: 0px;">
                    <tr>
                        <td width="20%">Nama</td>
                        <td width="1%">:</td>
                        <td>
                            <asp:Label ID="npwpnama2" runat="server"></asp:Label></td>
                    </tr>

                    <tr>
                        <td width="20%" valign="top">Alamat</td>
                        <td width="1%" valign="top">:</td>
                        <td>
                            <asp:Label ID="npwpalamat2" runat="server"></asp:Label></td>
                    </tr>

                    <tr>
                        <td width="20%">NPWP</td>
                        <td width="1%">:</td>
                        <td>
                            <asp:Label ID="npwpno2" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="border: 1px solid black;" colspan="3">
                <b>PEMBELI BARANG KENA PAJAK / PENERIMA JASA KENA PAJAK</b>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table>
                    <tr>
                        <td width="20%">Nama</td>
                        <td width="1%">:</td>
                        <td>
                            <asp:Label ID="nama2" runat="server"></asp:Label></td>
                    </tr>

                    <tr>
                        <td width="20%" valign="top">Alamat</td>
                        <td width="1%" valign="top">:</td>
                        <td>
                            <asp:Label ID="alamat2" runat="server"></asp:Label></td>
                    </tr>

                    <tr>
                        <td width="20%">NPWP</td>
                        <td width="1%">:</td>
                        <td>
                            <asp:Label ID="npwp2" runat="server"></asp:Label></td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td style="border: 1px solid black;" align="center" width="10%">No Urut</td>
            <td style="border: 1px solid black;" align="center">Nama Barang Kena Pajak / Jasa Kena Pajak</td>
            <td style="border: 1px solid black;" align="center" width="25%">Harga Jual / Penggantian / Uang Muka / Termin<br />
                (Rp)</td>
        </tr>
        <asp:PlaceHolder ID="list2" runat="server"></asp:PlaceHolder>
    </table>
    <br />
    <br />
    <table width="100%">
        <tr>
            <td colspan="2">Pajak Penjualan Atas Barang Mewah</td>
        </tr>

        <tr>
            <td width="50%">
                <table style="border: 1px solid black; border-collapse: collapse;" cellpadding="4">
                    <tr>
                        <td align="center" style="border: 1px solid black;" width="10%">Tarif</td>
                        <td align="center" style="border: 1px solid black;" width="10%">DPP</td>
                        <td align="center" style="border: 1px solid black;" width="10%">PPn BM</td>
                    </tr>

                    <tr>
                        <td align="center" style="border-right: 1px solid black;">........... %</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                    </tr>
                    <tr>
                        <td align="center" style="border-right: 1px solid black;">........... %</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                    </tr>
                    <tr>
                        <td align="center" style="border-right: 1px solid black;">........... %</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                    </tr>
                    <tr>
                        <td align="center" style="border-right: 1px solid black;">........... %</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                    </tr>
                    <tr>
                        <td style="border: 1px solid black;" colspan="2">Jumlah</td>
                        <td align="center" style="border: 1px solid black;">Rp. ...........</td>
                    </tr>

                </table>
            </td>

            <td width="20%">&nbsp;</td>

            <td width="50%">
                <table>
                    <tr>
                        <td align="center">Jakarta,
                            <asp:Label ID="tgl2" runat="server"></asp:Label>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            (<asp:Label ID="ttd2" runat="server"></asp:Label>)
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td colspan="2">*) Coret yang tidak perlu</td>
        </tr>
    </table>
</div>

<div align="center" style="page-break-before: always;">
    <span style="font-size: 14pt;"><b>FAKTUR PAJAK</b></span>

    <table style="border: 1px solid black; border-collapse: collapse;" width="100%" cellpadding="5px;">
        <tr>
            <td style="border: 1px solid black;" colspan="3">Kode dan Nomor Seri Faktur Pajak :
                <asp:Label ID="nopajak3" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td style="border: 1px solid black;" colspan="3">
                <b>PENGUSAHA KENA PAJAK</b>
            </td>
        </tr>

        <tr>
            <td colspan="3">
                <table style="border: 0px;">
                    <tr>
                        <td width="20%">Nama</td>
                        <td width="1%">:</td>
                        <td>
                            <asp:Label ID="npwpnama3" runat="server"></asp:Label></td>
                    </tr>

                    <tr>
                        <td width="20%" valign="top">Alamat</td>
                        <td width="1%" valign="top">:</td>
                        <td>
                            <asp:Label ID="npwpalamat3" runat="server"></asp:Label></td>
                    </tr>

                    <tr>
                        <td width="20%">NPWP</td>
                        <td width="1%">:</td>
                        <td>
                            <asp:Label ID="npwpno3" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="border: 1px solid black;" colspan="3">
                <b>PEMBELI BARANG KENA PAJAK / PENERIMA JASA KENA PAJAK</b>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table>
                    <tr>
                        <td width="20%">Nama</td>
                        <td width="1%">:</td>
                        <td>
                            <asp:Label ID="nama3" runat="server"></asp:Label></td>
                    </tr>

                    <tr>
                        <td width="20%" valign="top">Alamat</td>
                        <td width="1%" valign="top">:</td>
                        <td>
                            <asp:Label ID="alamat3" runat="server"></asp:Label></td>
                    </tr>

                    <tr>
                        <td width="20%">NPWP</td>
                        <td width="1%">:</td>
                        <td>
                            <asp:Label ID="npwp3" runat="server"></asp:Label></td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td style="border: 1px solid black;" align="center" width="10%">No Urut</td>
            <td style="border: 1px solid black;" align="center">Nama Barang Kena Pajak / Jasa Kena Pajak</td>
            <td style="border: 1px solid black;" align="center" width="25%">Harga Jual / Penggantian / Uang Muka / Termin<br />
                (Rp)</td>
        </tr>
        <asp:PlaceHolder ID="list3" runat="server"></asp:PlaceHolder>
    </table>
    <br />
    <br />
    <table width="100%">
        <tr>
            <td colspan="2">Pajak Penjualan Atas Barang Mewah</td>
        </tr>

        <tr>
            <td width="50%">
                <table style="border: 1px solid black; border-collapse: collapse;" cellpadding="4">
                    <tr>
                        <td align="center" style="border: 1px solid black;" width="10%">Tarif</td>
                        <td align="center" style="border: 1px solid black;" width="10%">DPP</td>
                        <td align="center" style="border: 1px solid black;" width="10%">PPn BM</td>
                    </tr>

                    <tr>
                        <td align="center" style="border-right: 1px solid black;">........... %</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                    </tr>
                    <tr>
                        <td align="center" style="border-right: 1px solid black;">........... %</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                    </tr>
                    <tr>
                        <td align="center" style="border-right: 1px solid black;">........... %</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                    </tr>
                    <tr>
                        <td align="center" style="border-right: 1px solid black;">........... %</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                        <td align="center" style="border-right: 1px solid black;">Rp. ...........</td>
                    </tr>
                    <tr>
                        <td style="border: 1px solid black;" colspan="2">Jumlah</td>
                        <td align="center" style="border: 1px solid black;">Rp. ...........</td>
                    </tr>

                </table>
            </td>

            <td width="20%">&nbsp;</td>

            <td width="50%">
                <table>
                    <tr>
                        <td align="center">Jakarta,
                            <asp:Label ID="tgl3" runat="server"></asp:Label>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            (<asp:Label ID="ttd3" runat="server"></asp:Label>)
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td colspan="2">*) Coret yang tidak perlu</td>
        </tr>
    </table>
</div>













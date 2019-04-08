<%@ Control Language="c#" Inherits="ISC064.MARKETINGJUAL.PrintFBatalTemplate" CodeFile="PrintFBatalTemplate.ascx.cs" %>

<table width="100%" align="center">
    <tr>
        <td align="right">
             <img src="/Media/west-point.png" width="120px" height="100px" />
        </td>
    </tr>
</table>
<table width="100%" align="center">
    <tr>
        <td style="font-size:18;text-align:left;" colspan="3"><b>FORM PEMBATALAN UNIT</b></td>
    </tr>
    <tr>
        <td style="font-size:14;" colspan="3"><b>PT. Andaland Property Development</b></td>
    </tr>
    <tr>
        <td style="height:20px">&nbsp;</td>
    </tr>
    <tr>
        <td style="width:20%">Tanggal</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">
            <asp:label id="tglbatal" runat="server" font-bold="True"></asp:label>
        </td>
    </tr>
    <tr>
        <td style="width:12%">Nomor</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">
            <asp:Label ID="nobatal1" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="height:20px">&nbsp;</td>
    </tr>
</table>
<table width="100%" align="center">
    <tr>
        <td style="text-align:left;" colspan="3">Dengan hormat,<br />Saya sebagai Pemesan Unit Apartemen sebagai berikut:</td>
    </tr>
    <tr>
        <td style="height:20px">&nbsp;</td>
    </tr>
    <tr>
        <td style="width:20%">Nama Customer</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">
            <asp:label id="cs" runat="server" font-bold="True"></asp:label>
        </td>
    </tr>
    <tr>
        <td style="width:12%">No. KTP</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">
            <asp:Label runat="server" ID="noktp1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width:12%">Alamat Rumah / Kantor</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">
            <asp:Label runat="server" ID="alamatktp"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width:12%"></td>
        <td style="width:1%"></td>
        <td style="border-bottom:1px dotted black;width:auto;">&nbsp;</td>
    </tr>
    <tr>
        <td style="width:12%">No.Telp. Rumah / Kantor</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">
            <asp:Label runat="server" ID="telphpfax"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width:12%">Unit</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">
            <asp:Label ID="unit" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="width:12%">Tipe</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">
            <asp:Label ID="tipe1" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="width:12%">No. SPA</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">
            <asp:Label ID="nomor2" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="text-align:left;" colspan="3"><br />
            Dengan ini saya mengajukan  PEMBATALAN  unit .
        </td>
    </tr>
    <tr>
        <td style="width:12%">Alasan Pembatalan</td>
        <td style="width:1%">:</td>
        <td style="border-bottom:1px dotted black;width:auto;">
            <asp:Label ID="alasan1" runat="server" />
        </td>
    </tr>
</table>
<br />
<table width="100%" cellpadding="3" cellspacing="0" align="center">
    <tr>
        <td style="text-align:center;border:1px solid black;width:25%">Pemohon,</td>
        <td style="text-align:center;border:1px solid black;width:25%">Mengetahui,</td>
        <td style="text-align:center;border:1px solid black;width:25%">&nbsp;</td>
        <td style="text-align:center;border:1px solid black;width:25%">&nbsp;</td>
    </tr>
    <tr>
        <td style="text-align:center;border:1px solid black;height:120px;vertical-align:bottom">
            (&nbsp;......................&nbsp;)<br />
            &nbsp;
        </td>
        <td style="text-align:center;border:1px solid black;height:120px;vertical-align:bottom">
            (&nbsp;......................&nbsp;)<br />
            SAD
        </td>
        <td style="text-align:center;border:1px solid black;height:120px;vertical-align:bottom">
            (&nbsp;<asp:label id="ag" runat="server" font-bold="True"></asp:label>&nbsp;)<br />
            SALES MANAGER
        </td>
        <td style="text-align:center;border:1px solid black;height:120px;vertical-align:bottom">
            (&nbsp;......................&nbsp;)<br />
            GM Sales
        </td>
    </tr>
</table>
<br />
<table width="100%" align="center">
    <tr>
        <td><div style="font-size:12px">
            <i>&nbsp; &nbsp; 
                *Lampiran : Copy SPA.
            </i>
            </div>
        </td>
    </tr>
</table>
<%--END--%>
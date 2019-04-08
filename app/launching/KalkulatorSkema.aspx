<%@ page language="c#" inherits="ISC064.LAUNCHING.KalkulatorSkema" CodeFile="KalkulatorSkema.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Kalkulator Skema</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Kalkulator Skema">
</head>
<body>
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <h1>
        Cara Pembayaran</h1>
    <br>
    <table cellspacing="0" cellpadding="0" id="pilih" runat="server">
        <tr valign="top">
            <td>
                <table cellspacing="5">
                    <tr valign="top">
                        <td>
                            Cara Bayar :
                            <br>
                            <asp:ListBox ID="carabayar" runat="server" CssClass="ddl" Rows="12" Width="300" OnSelectedIndexChanged="carabayar_SelectedIndexChanged"
                                AutoPostBack="true"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Tanggal Kontrak:
                            <asp:TextBox ID="tglkontrak" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                            <input type="button" value="..." class="btn" onclick="openCalendar('tglkontrak')">
                            <asp:Label ID="tglkontrakc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">
            </td>
            <td>
                <table cellspacing="5">
                    <tr>
                        <td>
                            No. Unit
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="nounit" ReadOnly Font-Bold></asp:TextBox>
                            <asp:TextBox runat="server" Style="display: none;" ID="nostock" ReadOnly Font-Bold></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            Price List
                        </td>
                        <td>
                            :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="gross" runat="server" CssClass="txt_num" Width="125" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="font: 8pt; padding-left: 10">
                            rupiah
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            Discount
                        </td>
                        <td>
                            :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="disc" runat="server" CssClass="txt_num" Width="30px" Enabled="false"></asp:TextBox>
                            &nbsp;
                            <asp:TextBox ID="nilaiDiskon" runat="server" CssClass="txt_num" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="font: 8pt; padding-left: 10">
                            % Graded
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            Present Value
                        </td>
                        <td>
                            :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="persenbunga" runat="server" CssClass="txt_num" Width="30px" Enabled="false"></asp:TextBox>
                            &nbsp;
                            <asp:TextBox ID="nilaiBunga" runat="server" CssClass="txt_num" Width="125px" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="font: 8pt; padding-left: 10">
                            % Graded
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            Additional Discount
                        </td>
                        <td>
                            :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="diskontambahan" runat="server" CssClass="txt_num" Width="125px"
                                Text="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td colspan="3" align="right">
                            <hr size="1" noshade>
                        </td>
                        <td style="font: bold">
                            -
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            DPP
                        </td>
                        <td>
                            :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="dpp" runat="server" CssClass="txt_num" Width="125" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="font: 8pt; padding-left: 10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            VAT
                        </td>
                        <td>
                            :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="ppn" runat="server" CssClass="txt_num" Width="125" Enabled="false"></asp:TextBox>
                        </td>
                        <td style="font: 8pt; padding-left: 10">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            PriceList
                        </td>
                        <td>
                            :
                        </td>
                        <td align="right">
                            <asp:TextBox ID="nilai" runat="server" TabIndex="99" CssClass="txt_num" Width="125"></asp:TextBox>
                        </td>
                        <td style="font: 8pt; padding-left: 10">
                            rupiah
                            <asp:Label ID="nilaic" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="hitung" runat="server" CssClass="btn" Text="Hitung" OnClick="hitung_Click">
                            </asp:Button>
                            <asp:Button ID="back" runat="server" CssClass="btn" Text="Cancel" OnClick="back_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="hasil" runat="server">
        <p style="padding: 5">
            <a href="javascript:history.back(-1)">Back...</a>
        </p>
        <asp:Table ID="rpt" runat="server" CssClass="tb" CellSpacing="5">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell>No.</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="300">Keterangan</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="100">Jatuh Tempo</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="120">Jumlah</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    <div id="noskema" runat="server" style="padding: 5px; margin: 40px">
        <h2>
            Skema Belum Tersedia</h2>
        <p style="font: 8pt">
            Silakan menghubungi administrasi untuk melakukan setup skema cara bayar.</p>
    </div>

    <script language="javascript">
			    function load() {
			    
		        Tgl = document.getElementById('tglkontrak').value;
			        PriceList = cvtnum(document.getElementById('gross').value);
			        Tambahan = cvtnum(document.getElementById('diskontambahan').value);

			        foo = document.getElementById('carabayar');
			        nounitk = document.getElementById('nostock');
			        Nomor = foo.options[foo.selectedIndex].value;
			        
			        location.href = '?NoStock=' + nounitk ;
			    }
			    function recaldisc(baseTxt, discTxt, nettTxt) {
			        base = cvtnum(baseTxt.value);
			        nett = base;
			        disc = discTxt.value.split("+");

			        discTxt.value = "";

			        for (i = 0; i < disc.length; i++) {
			            if (!isNaN(disc[i]) && disc[i] != "") {
			                nett = nett - (nett * (disc[i] / 100) * -1);
			                if (discTxt.value != "") discTxt.value = discTxt.value + "+";
			                discTxt.value = discTxt.value + disc[i];
			            }
			        }

			        n = Math.round(100 * nett) / 100;
			        eval("nettTxt.value = FinalFormat('" + n + "')");
			    }
			    function nohitung() {
			        document.getElementById('hitung').disabled = true;
			    }
			    function okhitung() {
			        document.getElementById('hitung').disabled = false;
			    }
			    function cvtnum(foo) {
			        return foo.replace(/,/gi, "");
			    }
    </script>

    </form>
</body>
</html>

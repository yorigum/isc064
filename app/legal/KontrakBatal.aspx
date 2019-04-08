<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.LEGAL.KontrakBatal" CodeFile="KontrakBatal.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Pembatalan Kontrak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Pembatalan Kontrak">
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <input style="display: none">
    <div id="pilih" runat="server">
        <h1 class="title title-line">Pembatalan Kontrak</h1>
        <p><b><i>Halaman 1 dari 2</i></b></p>
        <br>
        <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid;
            border-bottom: #dcdcdc 1px solid" cellspacing="5">
            <tr>
                <td>
                    <b>No. Kontrak : </b>
                </td>
                <td>
                    <asp:TextBox ID="nokontrak" runat="server" Width="100" CssClass="txt"></asp:TextBox>
                    <input type="button" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" onclick="popDaftarKontrak('a')" id="btnpop"
                        runat="server" name="btnpop">
                </td>
                <td>
                    <asp:LinkButton id="next" runat="server" cssclass="btn btn-blue" onclick="next_Click">
                        Next <i class="fa fa-arrow-right"></i>
					</asp:LinkButton>
                </td>
            </tr>
        </table>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
    </div>
    <div id="frm" runat="server">
        <h1 class="title title-line">Pembatalan Kontrak</h1>
        <p><b><i>Halaman 2 dari 2</i></b></p>
        <br>
        <table cellspacing="5">
            <tr>
                <td>
                    No. Kontrak
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="nokontrakl" runat="server" Font-Bold="True"></asp:Label>
                    &nbsp;&nbsp;&nbsp; <font style="font-weight: normal; font-size: 8pt; line-height: normal;
                        font-style: normal; font-variant: normal">Status kontrak tersebut akan menjadi :
                        <u>BATAL</u></font>
                </td>
            </tr>
            <tr>
                <td>
                    Unit
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Customer
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Sales
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <br>
        <table cellspacing="5">
            <tr>
                <td colspan="3">
                    Alasan Pembatalan :
                    <asp:RadioButtonList ID="alasan" runat="server" AutoPostBack="true" OnSelectedIndexChanged="alasan_SelectedIndexChanged">
                        <asp:ListItem Selected="True">PEMBAYARAN TIDAK DITERUSKAN</asp:ListItem>
                        <asp:ListItem>KREDIT TIDAK DISETUJUI</asp:ListItem>
                        <asp:ListItem>NEGOSIASI ULANG</asp:ListItem>
                        <asp:ListItem>UNIT HILANG</asp:ListItem>
                        <asp:ListItem>PERIZINAN BERUBAH</asp:ListItem>
                        <asp:ListItem>LAINNYA</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr id="lain" runat="server" visible="false">
                <td>
                    Keterangan :
                    <br />
                    <asp:TextBox ID="ketalasan" runat="server" TextMode="MultiLine" Width="200" Height="100" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <br />
                </td>
            </tr>
            <tr style="display: none;">
                <td>
                    Tgl Kuasa
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="tglKuasa" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                    <input class="btn" onclick="openCalendar('tglKuasa')" type="button" value="&#xf073;" style="font-family: 'fontawesome'" />
                    <asp:Label ID="tglKuasac" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Tgl Pembatalan
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="tgl" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                    <input class="btn" onclick="openCalendar('tgl')" type="button" value="&#xf073;" style="font-family: 'fontawesome'" />
                    <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr style="display: none;">
                <td>
                    Biaya Administrasi
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="nilaibiaya" runat="server" CssClass="txt_num" Width="100">0</asp:TextBox>
                    <asp:Label ID="nilaibiayac" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Total Pelunasan
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="totalPelunasan" runat="server" CssClass="txt_num" Width="100">0</asp:TextBox>
                    <asp:Label ID="totalpelunasanc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Tgl Pengembalian
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="tglkembali" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                    <input class="btn" onclick="openCalendar('tglkembali')" type="button" value="&#xf073;" style="font-family: 'fontawesome'" />
                    <asp:Label ID="tglkembalic" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Total Pengembalian
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="totalPengembalian" runat="server" CssClass="txt_num" Width="100"
                        AutoPostBack="true" OnTextChanged="totalklaim">0</asp:TextBox>
                    <asp:Label ID="totalpengembalianc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Nilai Klaim
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="nilaiklaim" runat="server" CssClass="txt_num" Width="100">0</asp:TextBox>
                    <asp:Label ID="nilaiklaimc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Rekening Pembatalan
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="acc" runat="server" Width="300">
                        <asp:ListItem Selected="True">- Pilih Rekening Bank -</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="accerr" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
        </table>
        <table height="50">
            <tr>
                <td>
                    <asp:Button ID="save" runat="server" CssClass="btn btn-blue" Text="OK" Width="75" OnClick="save_Click">
                    </asp:Button>
                </td>
                <td>
                    <input type="button" onclick="location.href='?'" class="btn" value="Cancel" style="width: 75px"
                        id="cancel" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="warning" runat="server" CssClass="err" Font-Bold="True" Font-Size="12pt"></asp:Label>
    <div id="nodel" runat="server">
        <h1>
            Kontrak Tidak Dapat Dibatalkan
        </h1>
        <br />
        <div class="plike">
            <h2>
                Kemungkinan Terjadi Karena:</h2>
            <ul>
            </ul>
        </div>
    </div>

    <script type="text/javascript">
			function call(nokontrak)
			{
				document.getElementById('nokontrak').value = nokontrak;
				document.getElementById('next').click();
			}
    </script>

    </form>
</body>
</html>

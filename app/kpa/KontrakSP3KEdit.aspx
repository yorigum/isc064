<%@ Reference Page="~/Log.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.KPA.KontrakSP3KEdit" CodeFile="KontrakSP3KEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Input SP3K</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Input SP3K">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Kembali ke halaman proses KPR?')) document.getElementById('cancel').click()">

    <script type="text/javascript" src="/Js/Common.js"></script>

    <script type="text/javascript" src="/Js/NumberFormat.js"></script>

    <form class="cnt" id="Form1" method="post" runat="server">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <div id="pilih" runat="server">
        <h1 class="title title-line">
            Input SP3K</h1>
        <p>
            Halaman 1 dari 2</p>
        <br />
        <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid;
            border-bottom: #dcdcdc 1px solid" cellspacing="5">
            <tr>
                <td>
                    No. Kontrak :
                </td>
                <td>
                    <asp:TextBox ID="nokontrak" runat="server" Width="100" CssClass="txt"></asp:TextBox>
                    <input class="btn btn-orange" id="btnpop" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a&amp;kpr=1' type="button" value="&#xf002;" style="font-family: 'fontawesome'"
								name="btnpop" runat="server" />
                </td>
                <td>
                    <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" OnClick="next_Click"> Next
                        <i class="fa fa-arrow-right"></i>
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
    </div>
    <div id="frm" runat="server">
        <h1 class="title title-line">
            Input SP3K</h1>
        <div id="pageof" runat="server">
            <p>
                Halaman 2 dari 2</p>
            <br />
        </div>
        <table cellspacing="5">
            <tr>
                <td>
                    No. Kontrak
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="kontrakno" runat="server" Font-Bold="True"></asp:Label>
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
        </table>
        <br>
        <table cellspacing="5">
            <tr>
                <td>
                    Status
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:RadioButtonList ID="rblStatus" runat="server" CssClass="radio" AutoPostBack="True" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="rblStatus_SelectedIndexChanged">
                        <asp:ListItem style="padding-right: 30px">BELUM DITENTUKAN</asp:ListItem>
                        <asp:ListItem style="padding-right: 30px">DIJADWALKAN</asp:ListItem>
                        <asp:ListItem style="padding-right: 30px">DIAJUKAN</asp:ListItem>
                        <asp:ListItem style="padding-right: 30px">SELESAI</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <table id="dijadwalkan" cellspacing="5" runat="server" style="width: 550px">
            <tr>
                <td style="width: 175px">
                    Target SP3K
                </td>
                <td style="width: 5px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="tbTarget" runat="server" CssClass="txt_center tgl" Width="75" Font-Size="8"></asp:TextBox>
                    <label for="tbTarget" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="lblTarget" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="diajukan" cellspacing="5" runat="server" style="width: 550px">
            <tr>
                <td style="width:175px">
                    Tgl. Pengajuan SP3K
                </td>
                <td style="width: 5px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="tbPengajuan" runat="server" CssClass="txt_center tgl" Width="75" Font-Size="8"></asp:TextBox>
                    <label for="tbPengajuan" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="lblPengajuan" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="selesai" cellspacing="5" runat="server" style="width: 550px">
            <tr>
                <td style="width: 175px">
                    Tgl. Hasil SP3K
                </td>
                <td style="width: 5px">
                    :
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbTgl" runat="server" CssClass="txt_center tgl" Width="75" Font-Size="8"></asp:TextBox>
                    <label for="tbTgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="lblTgl" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    No. SP3K
                </td>
                <td>
                    :
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbNoSP3K" runat="server" Width="100" CssClass="input-text" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Hasil SP3K
                </td>
                <td>
                    :
                </td>
                <td colspan="2">
                    <asp:RadioButtonList ID="rblHasil" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">TOLAK</asp:ListItem>
                        <asp:ListItem>SETUJU</asp:ListItem>
                        <asp:ListItem>SETUJU SEBAGIAN</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    Keterangan
                </td>
                <td>
                    :
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbKet" runat="server" Width="250px" CssClass="input-text" Columns="200" Rows="5" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Nilai Pengajuan KPR
                </td>
                <td>
                    :
                </td>
                <td style="text-align:right; width:125px">
                    <asp:Label ID="nilaipengajuan" runat="server" CssClass="txt_num right" Width="150"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    Nilai KPR Disetujui
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="nilai" runat="server" CssClass="txt_num nilai" Width="150" Font-Size="10"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="nilaic" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Tambahan Uang Muka
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="tambahum" runat="server" CssClass="txt_num tambahum" Width="150" Font-Size="10"></asp:TextBox>
                    <asp:Label ID="tambahumc" runat="server" CssClass="err"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tgljtum" runat="server" CssClass="txt_center tgl" Width="75" Font-Size="8" Height="34"></asp:TextBox>
                        <label for="tgljtum" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="tgljtumc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
        </table>
        <table height="50">
            <tr>
                <td>
                    <asp:LinkButton ID="ok" runat="server" Width="75" CssClass="btn btn-blue" OnClick="ok_Click">
                    <i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
                <td>
                    <input class="btn btn-red" id="cancel" style="width: 100px" type="button" value="Cancel" name="cancel"
                        runat="server">
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        function call(nokontrak) {
            document.getElementById('nokontrak').value = nokontrak;
            document.getElementById('next').click();
        }

        //untuk di kolom nilai pengajuan
        $('.nilai').on('change', function () {
            var nilaikpr = $('#nilaipengajuan').text().replace(/,/g, "") * 1;
            var nilaidiajukan = $('#nilai').val().replace(/,/g, "") * 1;

            Hasil = Math.round(nilaikpr - nilaidiajukan);

            tambahum = document.getElementById('tambahum');
            eval("tambahum.value = FinalFormat('" + Hasil + "')");
        });

        //untuk di kolom tambah um
        $('.tambahum').on('change', function () {
            var nilaikpr = $('#nilaipengajuan').text().replace(/,/g, "") * 1;
            var tambahum = $('#tambahum').val().replace(/,/g, "") * 1;

            Hasil = Math.round(nilaikpr - tambahum);

            nilaidiajukan = document.getElementById('nilai');
            eval("nilaidiajukan.value = FinalFormat('" + Hasil + "')");
        });

    </script>

    </form>
</body>
</html>

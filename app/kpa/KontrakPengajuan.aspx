<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KontrakPengajuan.aspx.cs"
    Inherits="ISC064.KPA.KontrakPengajuan" %>

<!DOCTYPE html>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Registrasi Pengajuan Tagihan KPR</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="KPA - Registrasi Pengajuan Tagihan">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Registrasi Pengajuan Tagihan KPR</h1>
        <br />
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <div style="margin-left: 10px;" id="transaksi" runat="server">
            <table>
                <tr>
                    <td>No. Surat
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="nosurat" Width="200" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Tgl Formulir
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="tglform" runat="server" CssClass="txt_center tgl" Width="75" Font-Size="8"></asp:TextBox>
                        <label for="tglform" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="tglformc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Tgl Rencana Cair
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="tglcair" runat="server" CssClass="txt_center tgl" Width="75" Font-Size="8"></asp:TextBox>
                        <label for="tglcair" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="tglcairc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Keterangan
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="keterangan" runat="server" TextMode="MultiLine" Width="200" Height="75"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellspacing="5">
                <tr>
                    <td style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">
                        <input class="btn btn-orange" id="btnpop" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a&amp;pengajuan=1' type="button" value="&#xf002;" style="font-family: 'fontawesome'"
								name="btnpop" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="keyword" placeholder=" Cari No. Kontrak / Customer/ Unit" runat="server" CssClass="input-text" Width="350"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0">Project :</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="tipe" runat="server">
                            <asp:ListItem Selected="True" Value="0">Kategori Retensi :</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="search" runat="server" CssClass="btn btn-blue" Text="Search" AccessKey="s"
                            OnClick="search_Click"></asp:Button>
                    </td>
                </tr>
            </table>
            <br />
            <div id="tb_fill" runat="server" visible="false">
            <table class="tb blue-skin" cellspacing="1">
                <tr>
                    <th>No. Kontrak
                    </th>
                    <th>No. Tagihan
                    </th>
                    <th>Customer
                    </th>
                    <th>Unit
                    </th>
                    <th>Nama Tagihan
                    </th>
                    <th>Tipe
                    </th>
                    <th>Tgl. Jatuh Tempo
                    </th>
                    <th>Sisa Tagihan
                    </th>
                    <th>Nilai Pengajuan
                    </th>
                    <th></th>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                <tr>
                    <td colspan="8" align="right">Total
                    </td>
                    <td>
                        <asp:TextBox ID="total" runat="server" Width="150" CssClass="input-text"></asp:TextBox>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td colspan="9" align="right">
                        <asp:Label ID="err" CssClass="err" runat="server"></asp:Label>
                        <asp:LinkButton ID="save" runat="server" Width="75" CssClass="btn btn-blue" OnClick="save_Click"><i class="fa fa-share"></i> OK
                        </asp:LinkButton>
                        <%--<a id="save" onclick="save_Click" style="width:75px" class="btn btn-blue"><i class="fa fa-share"></i> OK</a>--%>
                    </td>
                    <td />
                </tr>
            </table>
            </div>
        </div>

        <script type="text/javascript">
            function callkontrakproject(nokontrak, project) {
                document.getElementById('keyword').value = nokontrak;
                document.getElementById('project').value = project;
                //document.getElementById('search').click();
            }

            function keyup() {
                if (event.keyCode == 27 && confirm('Apakah anda ingin membatalkan proses registrasi?')) {
                    if (document.getElementById('cancel'))
                        document.getElementById('cancel').click();
                    else if (document.getElementById('cancel2'))
                        document.getElementById('cancel2').click();
                }
            }
            function tagihan(no, nilai, foo) {
                if (foo.checked)
                    document.getElementById('lunas_' + no).value = nilai;
                else
                    document.getElementById('lunas_' + no).value = "";

                hitunggt();
            }
            function hitunggt() {
                foogt = document.getElementById('total');
                grandtotal = 0 * 1;

                eof = false;
                i = 0 * 1;
                while (!eof) {
                    foo = document.getElementById('lunas_' + i);
                    if (!foo) {
                        eof = true;
                        break;
                    }
                    else {
                        total = cvtnum(foo.value);
                        if (!isNaN(total))
                            grandtotal = grandtotal + (total * 1);
                        i++;
                    }
                }

                finalnet = Math.round(100 * grandtotal) / 100;
                eval("foogt.value = FinalFormat('" + finalnet + "')");
            }
            function cvtnum(foo) {
                return foo.replace(/,/gi, "");
            }
            
            function hitungtotal() {
                var gt = parseFloat(document.getElementById('total').value.replace(/,/g, ""));
                var adm = parseFloat(document.getElementById('admBank').value.replace(/,/g, ""));
                var bulat = parseFloat(document.getElementById('lebihbayar').value.replace(/,/g, ""));
                var lebih = parseFloat(document.getElementById('lb').value.replace(/,/g, ""));

                document.getElementById('total').value = gt + bulat + lebih;
                CalcBlur(document.getElementById('grandtotal'));
            }
        </script>

    </form>
</body>
</html>

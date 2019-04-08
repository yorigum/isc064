<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegistrasiFollowUp.aspx.cs" Inherits="ISC064.COLLECTION.RegistrasiFollowUp" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<!DOCTYPE html>

<html>
<head>
    <title>Registrasi Pemberitahuan Follow Up</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="P. Jatuh Tempo - Registrasi Pemberitahuan Follow Up(Marketing)">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin membatalkan proses registrasi?')) document.getElementById('cancel').click();">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Registrasi Follow Up </h1>
        <p>Halaman 2 dari 2</p>
        <br>
        <table cellspacing="5">
            <tr>
                <td><b>No Kontrak</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="nokontrak" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Nilai Kontrak</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="nilaikontrak" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Customer</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Tagihan</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="tagihan" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Unit</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Adm</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="adm" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top"><b>Marketing</b></td>
                <td style="vertical-align:top"><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="marketing" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Tagihan + Adm</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="tagadm" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>No HP</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="hp1" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Pembayaran</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="pembayaran" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>No HP 2</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="hp2" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Pelunasan (10 %)</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="lunas" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr style="display:none">
                <td><b>Tipe</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="tipe" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top"><b>Alamat</b></td>
                <td style="vertical-align:top"><b>:</b></td>
                <td>
                    <asp:Label ID="alamat" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <hr size="1" noshade color="silver">
        <br>
        <table class="tb blue-skin" cellspacing="1">
				<tr align="left" valign="bottom">
					<th>
						No.</th>
					<th>
						Tagihan</th>
					<th>
						Tipe</th>
					<th>
						Jatuh Tempo</th>
                    <th align="right">
						Tagihan
					</th>
                    <th align="right">
						Pelunasan
					</th>
					<th align="right">
						Sisa Tagihan
					</th>
                    <th>Action</th>
                    <th>Catatan</th>
				</tr>
                <asp:placeholder id="list" runat="server"></asp:placeholder>
		</table>
<%--        <table height="50">
            <tr>
                <td>
                    <asp:LinkButton ID="save" runat="server" Width="75" CssClass="btn btn-blue" OnClick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton></td>
                <td>
                    <br />
                </td>
                <td>
                    <input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href = 'FollowUp.aspx'"
                        type="button" value="Cancel" name="cancel" runat="server">
                </td>
            </tr>
        </table>--%>
        <script type="text/javascript">
            function tagihan(no, nilai, foo) {
                if (foo.checked)
                    document.getElementById('lunas_' + no).value = nilai;
                else
                    document.getElementById('lunas_' + no).value = "";

                hitunggt();
            }
            function hitunggt() {
                foogt = document.getElementById('gt');
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
            function folup(nokontrak, nourut) {
                popFU(nokontrak, nourut);
            }
            function history(nokontrak, nourut) {
                popHistoryFU(nokontrak, nourut);
            }

        </script>
    </form>
</body>
</html>

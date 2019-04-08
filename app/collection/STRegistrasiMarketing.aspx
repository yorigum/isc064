<%@ Reference Page="~/Customer.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.COLLECTION.STRegistrasiMarketing" CodeFile="STRegistrasiMarketing.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Registrasi Surat Peringatan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Surat Peringatan - Registrasi Surat Peringatan (Marketing)">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin membatalkan proses registrasi?')) document.getElementById('cancel').click();">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Registrasi Surat Peringatan</h1>
        <p>Halaman 2 dari 2</p>
        <br />
        <table cellspacing="5">
            <tr>
                <td><b>Tipe</b></td>
                <td><b>:</b></td>
                <td width="100">
                    <asp:Label ID="tipe" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Unit</b></td>
                <td><b>:</b></td>
                <td>
                    <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Ref.</b></td>
                <td><b>:</b></td>
                <td>
                    <asp:Label ID="referensi" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Customer</b></td>
                <td><b>:</b></td>
                <td>
                    <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <hr size="1" noshade color="silver">
        <table cellspacing="5">
            <tr>
                <td colspan="3">
                    <asp:Label ID="lvlc" runat="server" CssClass="err" /></td>
            </tr>
            <tr>
                <td style="vertical-align: top;"><b>Level</b></td>
                <td style="vertical-align: top;"><b>:</b></td>
                <td style="vertical-align: top;">
                    <asp:RadioButton GroupName="level" runat="server" ID="level_1" Text="SP 1" />&nbsp;<asp:Literal runat="server" ID="stsLevel_1" /><br />
                    <asp:RadioButton GroupName="level" runat="server" ID="level_2" Text="SP 2" />&nbsp;<asp:Literal runat="server" ID="stsLevel_2" /><br />
                    <asp:RadioButton GroupName="level" runat="server" ID="level_3" Text="SP 3" />&nbsp;<asp:Literal runat="server" ID="stsLevel_3" /><br />
                    <asp:RadioButton GroupName="level" runat="server" ID="level_4" Text="Pembatalan" />&nbsp;<asp:Literal runat="server" ID="stsLevel_4" /><br />
                </td>
            </tr>
        </table>
        <div runat="server" id="divInsert">
            <table cellspacing="5">
                <tr runat="server" id="trTglSuratKuasa">
                    <td><b>Tgl Kuasa</b></td>
                    <td><b>:</b></td>
                    <td>
                        <div class="input-group input-medium">
                            <asp:TextBox ID="tglKuasa" runat="server" type="text" CssClass="form-control" Style="width: 50%" Height="20"></asp:TextBox>
                            <span class="input-group-btn" style="height: 34px; display: block">
                                <button class="btn-a default" runat="server"  type="button" style="height: 100%">
                                    <i class="fa fa-calendar"></i>
                                </button>
                            </span>
                        </div>
                        <asp:Label ID="tglKuasac" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style=""><b>Tgl. Cetak</b></td>
                    <td style=""><b>:</b></td>
                    <td>
                        <asp:TextBox ID="tgl" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                        <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>No. Telp:</b></td>
                    <td><b>:</b></td>
                    <td>
                        <asp:TextBox ID="notelp" runat="server" CssClass="input-text" Width="300" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr valign="top">
                    <td rowspan="3"><b>Alamat:</b></td>
                    <td rowspan="3"><b>:</b></td>
                    <td>
                        <p>
                            <asp:TextBox ID="alamat1" runat="server" CssClass="input-text" Width="300" MaxLength="50"></asp:TextBox>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p>
                            <asp:TextBox ID="alamat2" runat="server" CssClass="input-text" Width="300" MaxLength="50"></asp:TextBox>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p>
                            <asp:TextBox ID="alamat3" runat="server" CssClass="input-text" Width="150" MaxLength="50"></asp:TextBox>
                        </p>
                    </td>
                </tr>
            </table>
            <br>
            <table class="tb blue-skin" cellspacing="1">
                <tr align="left" valign="bottom">
                    <th>No.</th>
                    <th>Tagihan</th>
                    <th>Tipe</th>
                    <th>Jatuh Tempo</th>
                    <th align="right">Sisa Tagihan
                    </th>
                    <th></th>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                <tr>
                    <td colspan="4">
                        <b id="gtc" runat="server">Grand Total</b>
                    </td>
                    <td>
                        <asp:TextBox ID="gt" runat="server" CssClass="txt_num" ReadOnly="True" Width="120"></asp:TextBox>
                    </td>
                    <td></td>           
                </tr>
            </table>
        </div>
        <table height="50">
            <tr>
                <td>
                    <asp:LinkButton ID="save" runat="server" Width="75" CssClass="btn btn-blue" OnClick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton></td>
                <td>
                    <br />
                </td>
                <td>
                    <input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href = 'STRegistrasi.aspx'"
                        type="button" value="Cancel" name="cancel" runat="server">
                </td>
            </tr>
        </table>
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
            function call(nomor) {
                popEditTunggakan(nomor);
            }
        </script>
    </form>
</body>
</html>

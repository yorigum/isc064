<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.TagihanEdit" CodeFile="TagihanEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Edit Jadwal Tagihan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tagihan - Edit Jadwal Tagihan">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Kembali ke halaman jadwal tagihan?')) document.getElementById('cancel').click()">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <script type="text/javascript" src="/Js/Common.js"></script>
    <script type="text/javascript" src="/Js/NumberFormat.js"></script>
    <script type="text/javascript" src="/Js/moment.min.js"></script>
    <form id="Form1" method="post" runat="server" class="cnt">
        <h1 class="title title-line">Edit Jadwal Tagihan
        </h1>
        <table cellpadding="0" cellspacing="0">
            <tr valign="top">
                <td width="400">
                    <table cellspacing="5">
                        <tr>
                            <td>No. Kontrak</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="nokontrak" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Unit</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Customer</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="nama" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Sales</td>
                            <td>:</td>
                            <td>
                                <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                    </table>
                    <p class="feed" style="padding-left: 5px">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
                <td>
                    <table cellspacing="5" style="border-right: 1px inset; border-top: 1px inset; border-left: 1px inset; border-bottom: 1px inset">
                        <tr>
                            <td colspan="3">Skema :
									<asp:Label ID="skema" runat="server" Font-Underline="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="150">Nilai Kontrak</td>
                            <td>:</td>
                            <td width="150" align="right">
                                <asp:Label ID="nilai" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Tagihan</td>
                            <td>:</td>
                            <td align="right">
                                <asp:Label ID="totaltagihan" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Biaya</td>
                            <td>:</td>
                            <td align="right">
                                <asp:Label ID="totalbiaya" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Tagihan + Biaya</td>
                            <td>:</td>
                            <td align="right">
                                <asp:Label ID="tagihanbiaya" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr id="outtr" runat="server">
                            <td align="right" colspan="3">
                                <asp:Label ID="outofbalance" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br>
        <table cellspacing="0" class="blue-skin tb" cellspacing="1" cellpadding="1">
            <tr align="left">
                <th style="border-right: solid white 1px; max-width:20px;">No.</th>
                <th style="border-right: solid white 1px;">Tipe</th>
                <th style="border-right: solid white 1px;">Jenis</th>
                <th style="border-right: solid white 1px;">Nama</th>
                <th style="border-right: solid white 1px;">Tgl</th>
                <th style="border-right: solid white 1px;">Nilai</th>
                <th style="border-right: solid white 1px;">KPR</th>
                <th style="border-right: solid white 1px;" colspan="2" ></th>
                <%--<th style="border-right: solid white 1px;"></th>
                <th style="border-right: solid white 1px;"></th>--%>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
            <tr>
                <td>Baru:</td>
                <td>
                    <asp:RadioButton ID="barubf" runat="server" CssClass="radio" GroupName="barutipe" Text="BF" Checked="True" Font-Size="8"></asp:RadioButton>&nbsp;
						<asp:RadioButton ID="barudp" runat="server" CssClass="radio" GroupName="barutipe" Text="DP" Font-Size="8"></asp:RadioButton>&nbsp;
						<asp:RadioButton ID="baruang" runat="server" CssClass="radio" GroupName="barutipe" Text="ANG" Font-Size="8"></asp:RadioButton>&nbsp;
						<asp:RadioButton ID="baruadm" runat="server" CssClass="radio" GroupName="barutipe" Text="ADM" Font-Size="8"></asp:RadioButton>&nbsp;
						<asp:RadioButton ID="barudll" runat="server" CssClass="radio" GroupName="barutipe" Text="DLL" Font-Size="8"></asp:RadioButton>
                </td>
                <td>
                    <asp:DropDownList ID="jenis2" runat="server">
                        <asp:ListItem Value="">Jenis :</asp:ListItem>
                        <asp:ListItem Value="DM">Denda Manual</asp:ListItem>
                        <asp:ListItem Value="PPJB">PPJB</asp:ListItem>
                        <asp:ListItem Value="AJB">AJB</asp:ListItem>
                        <asp:ListItem Value="BAST">BAST</asp:ListItem>
                        <asp:ListItem Value="GN">Pengalihan Hak</asp:ListItem>
                        <asp:ListItem Value="GU">Pindah Unit</asp:ListItem>
                        <asp:ListItem Value="LAIN">Lain-Lain</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="barunama" runat="server" CssClass="txt" Width="140" MaxLength="50" Font-Size="8"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="barutgl" runat="server" CssClass="txt_center" Width="75" Font-Size="8"></asp:TextBox>
                    <label for="barutgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                </td>
                <td>
                    <asp:TextBox ID="barunilai" runat="server" CssClass="txt_num" Width="90" Font-Size="8"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="barukpr" runat="server"></asp:CheckBox>
                </td>
                <td></td>
                <td>
                    <asp:Label ID="baruc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
        </table>
        <table height="50">
            <tr>
                <td>
                    <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a"
                        OnClick="save_Click"><i class="fa fa-check"></i>Apply</asp:LinkButton>
                </td>
                <td>
                    <input class="btn btn-red" id="cancel" style="width: 75px" type="button" value="Cancel" name="cancel"
                        runat="server">
                </td>
                <td style="padding-left: 10px; font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">
                    <asp:Label ID="noedit" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function hapus(nokontrak, nourut) {
                if (confirm('Apakah anda ingin menghapus tagihan : ' + nokontrak + '.' + nourut + '?\nPerhatian bahwa data akan dihapus secara PERMANEN.')) {
                    location.href = 'TagihanDel.aspx?NoKontrak=' + nokontrak + '&NoUrut=' + nourut;
                }
            }
            function nonaktif(bf, dp, ang, adm, jenis) {
                if (adm.checked) {
                    jenis.disabled = false;
                }
                else {
                    jenis.disabled = true;
                }
            }
            function updateSetelah(asal) {
                i = 1;
                target = asal + 1;
                while (true) {
                    var tanggal = moment($("#tgl_" + asal).val(), "D MMM YYYY");

                    if (!$("#tgl_" + target).length)
                        break;

                    var tglbaru = tanggal.add(i, 'month');

                    $("#tgl_" + target).val(tglbaru.format('D MMM YYYY'));

                    i++;
                    target++;
                }
            }
        </script>
    </form>
</body>
</html>

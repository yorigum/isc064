<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.KonfirmasiNUP2" CodeFile="KonfirmasiNUP2.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NUP - Konfirmasi NUP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="NUP - Konfirmasi NUP (Hal. 2 dari 3)">
    <style type="text/css">
        .sm TD
        {
            font-weight: normal;
            font-size: 8pt;
            line-height: normal;
            font-style: normal;
            font-variant: normal;
        }
        .nav, .navsub
        {
            border: 0px;
            background-color: #EEEEEE;
            font: 8pt Trebuchet MS;
            padding-left: 7;
            text-align: left;
            width: 190;
            height: 18px;
        }
        .nav2
        {
            border: 0px;
            background-color: #EEEEEE;
            font: 14pt Trebuchet MS;
            padding-left: 7;
            text-align: left;
            width: 200;
            height: 30px;
        }
    </style>
</head>
<body style="height: 700px;">
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <div style="float: left; width: 40%;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 20px">
                    <img src="/Media/icon_prev_c.gif" style="width: 30px; height: 30px;">
                </td>
                <td>
                    <input type="button" value="Back" class="nav2" style="width: 80px;" onmouseover="over(this)"
                        onclick="window.location='Nup.aspx';" onmouseout="out(this)">
                </td>
            </tr>
        </table>
    </div>
    <div style="float: right; width: 10%;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 20px">
                    <img src="/Media/icon_gateway.gif" style="width: 30px; height: 30px;">
                </td>
                <td>
                    <input type="button" value="Gateway" class="nav2" onclick="top.location.href='/Gateway.aspx'"
                        style="width: 100px" onmouseover="over(this)" onmouseout="out(this)">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20px">
                    <img src="/Media/icon_out.gif" style="width: 30px; height: 30px;">
                </td>
                <td>
                    <input type="button" value="Sign-Out" class="nav2" onclick="if(confirm('Apakah anda ingin melakukan sign-out?\nProgram dan absensi aktif anda akan ditutup.')){top.location.href='SignOut.aspx'}"
                        style="width: 100px" onmouseover="over(this)" onmouseout="out(this)">
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <br />
    <h1>
        Konfirmasi NUP</h1>
    <p style="font-size: 8pt; color: #666;">
        Halaman 2 dari 3</p>
    <br>
    <table cellspacing="0" cellpadding="20">
        <tr valign="top">
            <td style="width: 50%;">
                <table>
                    <tr>
                        <td colspan="3">
                            <p style="font-size: 12pt">
                                <b>Identitas Customer</b></p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. NUP
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="nomorNUP" runat="server" CssClass="txt" Width="200" ReadOnly="True"
                                Style="border: none; font-weight: bold;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            No NUP Lain
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td>
                            <asp:Label ID="nuplain" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Tanggal NUP
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="tglNUP" runat="server" Width="100px" CssClass="txt_center" ReadOnly="True"
                                Style="border: none; text-align: left;" Height="20px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sumber Data
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="sumberdata" runat="server" CssClass="txt" Width="250" MaxLength="100"
                                ReadOnly="True" Style="border: none; text-align: left;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Nama
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="nama" runat="server" CssClass="txt" Width="250" MaxLength="100"
                                ReadOnly="True" Style="border: none; text-align: left;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. Telp / HP
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="ctelp" runat="server" CssClass="txt" Width="70" ReadOnly="True"
                                Style="border: none; text-align: left;"></asp:TextBox>
                            &nbsp;/&nbsp;
                            <asp:TextBox ID="chp" runat="server" CssClass="txt" Width="90" ReadOnly="True" Style="border: none;
                                text-align: left;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. KTP
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="noktp" runat="server" CssClass="txt" Width="140" MaxLength="50"
                                ReadOnly="True" Style="border: none; text-align: left;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="Save" runat="server" CssClass="btn" Text="OK" Width="75" OnClick="save_Click">
                            </asp:Button>
                        </td>
                        <td colspan="2">
                            <input id="Cancel" runat="server" class="btn" onclick="location.href='Nup.aspx'; return false;"
                                style="width: 75px" type="button" value="Cancel">
                        </td>
                        <p class="feed">
                            <asp:Label ID="feed" runat="server"></asp:Label>
                        </p>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; display: none;">
            </td>
        </tr>
    </table>

    <script language="javascript">
        function call(no, nm, hp, telp, noktp, ktp1, ktp2, ktp3, ktp4, kor1, kor2, kor3, kor4, rekb, rekc, rekno, reknam) {
            //document.getElementById('nocustomer').value = no;
            document.getElementById('nama').value = nm;
            document.getElementById('ctelp').value = telp;
            document.getElementById('chp').value = hp;
            document.getElementById('noktp').value = noktp;
            document.getElementById('ktp1').value = ktp1;
            document.getElementById('ktp2').value = ktp2;
            document.getElementById('ktp3').value = ktp3;
            document.getElementById('ktp4').value = ktp4;
            document.getElementById('Korespon1').value = kor1;
            document.getElementById('Korespon2').value = kor2;
            document.getElementById('Korespon3').value = kor3;
            document.getElementById('Korespon4').value = ktp4;
            document.getElementById('bank').value = rekb;
            document.getElementById('cabang').value = rekc;
            document.getElementById('rek').value = rekno;
            document.getElementById('reknama').value = reknam;
        }

        function samaisi() {
            document.getElementById('Korespon1').value = document.getElementById('ktp1').value;
            document.getElementById('Korespon2').value = document.getElementById('ktp2').value;
            document.getElementById('Korespon3').value = document.getElementById('ktp3').value;
            document.getElementById('Korespon4').value = document.getElementById('ktp4').value;
        }

        function bedaisi() {
            document.getElementById('Korespon1').value = '';
            document.getElementById('Korespon2').value = '';
            document.getElementById('Korespon3').value = '';
            document.getElementById('Korespon4').value = '';
        }
    </script>

    </form>
</body>
</html>

<%@ Page Language="c#" Inherits="ISC064.NUP.NUPEdit" CodeFile="NUPEdit.aspx.cs" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Edit NUP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="NUP - Edit NUP">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <%--<uc1:navunit id="NavUnit1" runat="server" aktif="1"></uc1:navunit>--%>
        <div class="tabdata">
            <div class="pad">
                <%--<uc1:headunit id="HeadUnit1" runat="server"></uc1:headunit>--%>
                <table cellspacing="5">
                    <tr>
                        <td>Print</td>
                        <td>:</td>
                        <td class="printhref"><a id="printNUP" runat="server"><b>Form NUP</b></a></td>
                        <td class="printhref">
                            <a id="printTTS" runat="server"><b>TTS</b></a>
                        </td>
                        <td class="printhref"><a id="printTTS2" runat="server"><b>TTS 2</b></a></td>
                        <td class="printhref" style="display:none;"><a id="printRefund" runat="server"><b>Print Refund</b></a></td>
                        <td width="100%"></td>
                        <%--<td>
                        <input type="button" class="btn" value="  Log  " id="btnlog" runat="server" name="btnlog"
                            accesskey="l" style="display: none">
                    </td>--%>
                        <%--<td>
                        <input type="button" class="btn" value="Delete" id="btndel" runat="server" name="btndel"
                            accesskey="d">
                    </td>--%>
                    </tr>
                </table>
                <table cellspacing="5">
                    <tr>
                        <td width="100%"></td>
                        <td>
                            <input type="button" class="btn btn-blue" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                accesskey="l">
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="stamp">Revisi :
                        <asp:Label ID="revisi" runat="server"></asp:Label>
                        </td>
                        <td class="stamp">Sudah dibayar :
                        <asp:Label ID="lunasrupiah" runat="server"></asp:Label>&nbsp;<asp:Label ID="lunaspersen" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr valign="top">
                        <td>
                            <table cellspacing="5" style="width: 400px; float: left;">
                                <tr>
                                    <td colspan="3">
                                        <p style="font-size: 12pt">
                                            <b>Identitas Customer</b>
                                        </p>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>No. Customer
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nocustomer" runat="server" CssClass="txt" Width="75" Text="#AUTO#"
                                            ReadOnly="true" Font-Bold="True"></asp:TextBox>
                                        &nbsp;
                                    <input class="btn" id="btnpop" onclick="popDaftarCustomer2('a')" type="button" value="..." />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nama
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nama" runat="server" CssClass="txt" Width="250"></asp:TextBox>
                                        <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>No. Telp / Hp
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ctelp" runat="server" CssClass="txt" Width="90" MaxLength="100"></asp:TextBox>
                                        &nbsp;/&nbsp;
                                    <asp:TextBox ID="chp" runat="server" CssClass="txt" Width="90" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="ctelpc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Email
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="email" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>
                                        <asp:Label ID="emailc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td>Tanggal Lahir</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="tgllahir" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                        <input type="button" value="..." class="btn" onclick="openCalendar('tgllahir')">
                                        <asp:Label ID="tgllahirc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>No. KTP
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="noktp" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>
                                        <asp:Label ID="noktpc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>NPWP
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="npwp" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>
                                        <asp:Label ID="npwpc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Alamat KTP
                                    </td>
                                    <td style="vertical-align: top;">:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ktp1" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>&nbsp;<font
                                            style="font-size: 7pt">(alamat)</font>
                                        <br />
                                        <asp:TextBox ID="ktp2" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                            style="font-size: 7pt">(RT/RW)</font>
                                        <br />
                                        <asp:TextBox ID="ktp3" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                            style="font-size: 7pt">(Kecamatan)</font>
                                        <br />
                                        <asp:TextBox ID="ktp4" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                            style="font-size: 7pt">(Kotamadya)</font>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Alamat Korespondensi
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="beda" runat="server" Text="BEDA dengan KTP" GroupName="tipe2" OnCheckedChanged="beda_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                                        <asp:RadioButton ID="sama" runat="server" Text="SAMA dengan KTP" GroupName="tipe2"
                                            OnCheckedChanged="sama_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                                    </td>
                                </tr>
                                <tr id="trKoresponden" runat="server">
                                    <td colspan="2"></td>
                                    <td>
                                        <asp:TextBox ID="Korespon1" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>&nbsp;<font
                                            style="font-size: 7pt">(alamat)</font>
                                        <br />
                                        <asp:TextBox ID="Korespon2" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                            style="font-size: 7pt">(RT/RW)</font>
                                        <br />
                                        <asp:TextBox ID="Korespon3" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                            style="font-size: 7pt">(Kecamatan)</font>
                                        <br />
                                        <asp:TextBox ID="Korespon4" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>&nbsp;<font
                                            style="font-size: 7pt">(Kotamadya)</font>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top">Rek. untuk Refund
                                    </td>
                                    <td style="vertical-align: top">:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="bank" runat="server" CssClass="txt" Width="140" MaxLength="100"></asp:TextBox>&nbsp;<font
                                            style="font-size: 7pt">(Bank)</font>&nbsp;<asp:Label ID="bankc" runat="server" CssClass="err"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="cabang" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>&nbsp;<font
                                            style="font-size: 7pt">(Cabang)</font>&nbsp;<asp:Label ID="cabangc" runat="server" CssClass="err"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="rek" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>&nbsp;<font
                                            style="font-size: 7pt">(No. Rekening)</font>&nbsp;<asp:Label ID="rekc" runat="server" CssClass="err"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="reknama" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>&nbsp;<font
                                            style="font-size: 7pt">(Atas Nama)</font>&nbsp;<asp:Label ID="reknamac" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table cellspacing="5" style="width: 430px; float: right;">
                                <tr>
                                    <td colspan="3">
                                        <p style="font-size: 12pt">
                                            <b>Data NUP</b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>No. NUP
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="nomorNUP" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Sales
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="agent" runat="server" Width="280" CssClass="ddl">
                                            <asp:ListItem Value="0">Pilih Agent :</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="agentc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tipe
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="tipe" runat="server" />
                                    </td>
                                </tr>
                                <tr id="trsave" runat="server">
                                    <td>
                                        <asp:Button ID="save" runat="server" CssClass="btn btn-blue" Text="OK" Width="75" OnClick="save_Click"></asp:Button>
                                    </td>
                                    <td colspan="2">
                                        <input id="cancel" runat="server" class="btn btn-red" onclick="javascript: window.close(); return false;"
                                            style="width: 75px" type="button" value="Cancel">
                                        &nbsp;
                                    <asp:Label ID="feed" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trotorisasi" runat="server" visible="false">
                                    <td colspan="3">
                                        <table>
                                            <tr>
                                                <td>Username
                                                </td>
                                                <td>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="username" runat="server" CssClass="txt" Width="120"></asp:TextBox>
                                                    &nbsp;
                                                <asp:Label ID="usernamec" runat="server" CssClass="err"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Password
                                                </td>
                                                <td>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="password" TextMode="Password" runat="server" CssClass="txt" Width="120"></asp:TextBox>
                                                    &nbsp;
                                                <asp:Label ID="passc" runat="server" CssClass="err"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="otorisasi" runat="server" CssClass="btn" Text="Otorisasi" Width="75"
                                                        OnClick="otorisasi_Click"></asp:Button>
                                                </td>
                                                <td>
                                                    <input id="cancel2" runat="server" class="btn" onclick="javascript: window.close(); return false;"
                                                        style="width: 75px" type="button" value="Cancel">
                                                </td>
                                            </tr>
                                            <td colspan="3">
                                                <asp:Label ID="lblotorisasi" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>

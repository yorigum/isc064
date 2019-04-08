<%@ Reference Page="~/Log.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.LEGAL.KontrakIMBEdit" CodeFile="KontrakIMBEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrak" Src="NavKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Edit IMB</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Kontrak - Edit IMB">
</head>
<body onkeyup="if(event.keyCode==27) document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div class="content-header">
            <uc1:NavKontrak ID="NavKontrak1" runat="server" Aktif="5"></uc1:NavKontrak>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadKontrak ID="HeadIMB1" runat="server"></uc1:HeadKontrak>
                <table cellspacing="5">
                    <tr valign="middle">
                        <td style="width: 8%"></td>
                        <td></td>
                        <td width="80%"></td>
                        <td>
                            <input type="button" class="btn btn-blue" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                accesskey="l">
                        </td>
                        <td>
                            <%--<input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel"
                                    accesskey="d">--%>
                            <a id="aStatus" class="btn btn-red" runat="server">Reset</a>
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td>
                            <b>Status :</b>
                        </td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="stat" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="stat_SelectedIndexChanged">
                                <asp:ListItem Value="B" Selected="True">Belum</asp:ListItem>
                                <asp:ListItem Value="S">Target IMB</asp:ListItem>
                                <asp:ListItem Value="D">Proses IMB</asp:ListItem>
                                <asp:ListItem Value="T">Registrasi IMB</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0">
                    <tbody>
                        <tr valign="top">
                            <td>
                                <table id="belum" runat="server" cellspacing="5">
                                    <tr>
                                        <td>Tgl Target</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="tbTglTarget2" runat="server" CssClass="txt_center" Width="75" Font-Size="8" ReadOnly="true"></asp:TextBox>
                                            <label for="tbTglTarget2" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="lblTglTarget2" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tgl Proses</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="tbTglProses2" runat="server" CssClass="txt_center" Width="75" Font-Size="8" ReadOnly="true"></asp:TextBox>
                                            <label for="tbTglProses2" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="lblTglProses2" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>No. IMB</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="tbNoIMB2" runat="server" CssClass="txt" MaxLength="20" Width="100" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tgl. IMB</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="tbTgl2" runat="server" CssClass="txt_center" Width="75" Font-Size="8" ReadOnly="true"></asp:TextBox>
                                            <label for="tbTgl2" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="lblTgl2" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Keterangan IMB</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="tbKeteranganIMB2" runat="server" CssClass="txt" MaxLength="1000" Width="300" Height="80" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table id="selesai" runat="server" cellspacing="5">
                                    <tr>
                                        <td>No. IMB</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="tbNoIMB" runat="server" CssClass="txt" MaxLength="20" Width="100" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tgl. IMB</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="tbTgl" runat="server" CssClass="txt_center" Width="75" Font-Size="8" ReadOnly="true"></asp:TextBox>
                                            <label for="tbTgl" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="lblTgl" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Keterangan IMB</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="tbKeteranganIMB" runat="server" CssClass="txt" MaxLength="1000" Width="300" Height="80" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table id="proses" runat="server" cellspacing="5">
                                    <tr>
                                        <td>Tgl Proses</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="tbTglProses" runat="server" CssClass="txt_center" Width="75" Font-Size="8" ReadOnly="true"></asp:TextBox>
                                            <label for="tbTglProses" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="lblTglProses" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Keterangan</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="tbKeterangan" runat="server" CssClass="txt" MaxLength="1000" Width="300" Height="80" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table id="target" runat="server" cellspacing="5">
                                    <tr>
                                        <td>Tgl Target</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="tbTglTarget" runat="server" CssClass="txt_center" Width="75" Font-Size="8" ReadOnly="true"></asp:TextBox>
                                            <label for="tbTglTarget" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="lblTglTarget" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                                <table height="50" style="display: none">
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="ok" runat="server" Enabled="false" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK
                                            </asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="save" runat="server" Enabled="false" CssClass="btn btn-orange" Width="75" AccessKey="a"
                                                OnClick="save_Click"><i class="fa fa-check"></i>Apply</asp:LinkButton>
                                        </td>
                                        <td>
                                            <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel"
                                                style="width: 100px">
                                        </td>
                                        <td style="padding-left: 10px">
                                            <p class="feed">
                                                <asp:Label ID="feed" runat="server"></asp:Label>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                                <input class="btn" id="refresh" type="button" value="Refresh Unit" name="refresh"
                                    runat="server" style="display: none;">
                                <div style="padding-right: 5px; padding-left: 5px; font-weight: bold; font-size: 15pt; padding-bottom: 5px; line-height: normal; padding-top: 5px; font-style: normal; font-variant: normal; display: none;">
                                    <asp:Label ID="refreshinfo" runat="server" ForeColor="red"></asp:Label>&nbsp;
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        function call(nova) {
            document.getElementById('nova').value = nova;
        }
    </script>

</body>
</html>

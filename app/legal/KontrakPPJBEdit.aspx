<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KontrakPPJBEdit.aspx.cs" Inherits="KontrakPPJBEdit" %>--%>

<%@ Page Language="c#" Inherits="ISC064.LEGAL.KontrakPPJBEdit" CodeFile="KontrakPPJBEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrak" Src="NavKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Edit PPJB</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Kontrak - Edit PPJB">
</head>
<body onkeyup="if(event.keyCode==27) document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div class="content-header">
            <uc1:NavKontrak ID="NavKontrak1" runat="server" Aktif="2"></uc1:NavKontrak>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadKontrak ID="HeadPPJB1" runat="server"></uc1:HeadKontrak>
                <table cellspacing="5">
                    <tr valign="middle">
                        <td style="width: 8%"><b>Print :</b>
                        </td>
                        <td class="printhref">
                            <a id="printPPJB" runat="server"><b>PPJB</b></a>
                        </td>
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
                                <asp:ListItem Value="S">Target PPJB</asp:ListItem>
                                <asp:ListItem Value="D">PPJB</asp:ListItem>
                                <asp:ListItem Value="T">Tanda Tangan</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <%--                <table cellspacing="5">
                    <tr valign="middle">
                        <td style="width: 8%"><b>Print :</b>
                        </td>
                        <td>
                            
                        </td>
                        <td width="80%"></td>
                    </tr>
                </table>--%>
                <table cellspacing="0" cellpadding="0">
                    <tbody>
                        <tr valign="top">
                            <td>
                                <table id="belum" runat="server" visible="false" cellspacing="5">
                                    <tr>
                                        <td colspan="3">
                                            <table width="100%">
                                                <tr>
                                                    <td valign="top">
                                                        <h3>Kelengkapan Berkas :</h3>
                                                        </td>
                                                </tr>
                                                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                                                <tr runat="server" id="lkp">
                                                    <td valign="top">Tanggal Lengkap</td>
                                                    <td valign="top">:</td>
                                                    <td>
                                                        <asp:TextBox ID="tglkp" runat="server" Width="85" CssClass="txt_center" ReadOnly="true"></asp:TextBox>
                                                        <label for="tglkp" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label>
                                                        <asp:Label ID="tglkpc" runat="server" CssClass="err"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table id="target" runat="server" cellspacing="5">
                                    <tr>
                                        <td>Tgl Target</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="tbTglTarget" runat="server" CssClass="tgl" Width="75" Font-Size="8" ReadOnly="true"></asp:TextBox>
                                            <label for="tbTglTarget" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="lblTglTarget" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table id="selesai" runat="server" cellspacing="5">
                                    <tr>
                                        <td style="width: 110px">No. PPJB</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="noppjb1" runat="server" CssClass="txt" Width="100" MaxLength="20" ReadOnly="True" Style="text-align: center">#AUTO#</asp:TextBox>
                                            <asp:Label ID="noppjbc1" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                        <td rowspan="2">
                                            <asp:RadioButtonList ID="ppjbused1" runat="server" RepeatDirection="Vertical" CellPadding="10">
                                                <asp:ListItem Value="0" Selected="True" Enabled="false">Auto</asp:ListItem>
                                                <asp:ListItem Value="1" Enabled="false">Manual</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>No. PPJB Manual</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="noppjbm1" runat="server" CssClass="txt" Width="200" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                            <asp:Label ID="noppjbmc1" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tanggal PPJB</td>
                                        <td>:</td>
                                        <td>
                                            <nobr>
								<asp:textbox id="tglppjb1" runat="server" cssclass="txt_center" width="85" ReadOnly="true"></asp:textbox>
								<label for="tglppjb1" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label>
							</nobr>
                                            <asp:Label ID="tglppjbc1" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Biaya PPJB</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="nilaibiaya1" runat="server" CssClass="txt_num" Width="100" ReadOnly="true">0</asp:TextBox>
                                            <asp:Label ID="nilaibiayac1" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Keterangan</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="keterangan1" runat="server" TextMode="MultiLine" Width="200" Height="75" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table id="ttd" runat="server" cellspacing="5">
                                    <tr>
                                        <td style="width: 110px">No. PPJB</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="noppjb2" runat="server" CssClass="txt" Width="100" MaxLength="20" ReadOnly="True" Style="text-align: center">#AUTO#</asp:TextBox>
                                            <asp:Label ID="noppjbc2" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                        <td rowspan="2">
                                            <asp:RadioButtonList ID="ppjbused2" runat="server" RepeatDirection="Vertical" CellPadding="10">
                                                <asp:ListItem Value="0" Selected="True" Enabled="false">Auto</asp:ListItem>
                                                <asp:ListItem Value="1" Enabled="false">Manual</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>No. PPJB Manual</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="noppjbm2" runat="server" CssClass="txt" Width="200" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                            <asp:Label ID="noppjbmc2" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tanggal PPJB</td>
                                        <td>:</td>
                                        <td>
                                            <nobr>
								                <asp:textbox id="tglppjb2" runat="server" cssclass="tgl" width="85" ReadOnly="true"></asp:textbox>
								                <label for="tglppjb2" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label>
							                </nobr>
                                            <asp:Label ID="tglppjc2" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Biaya PPJB</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="nilaibiaya2" runat="server" CssClass="txt_num" Width="100" ReadOnly="true">0</asp:TextBox>
                                            <asp:Label ID="nilaibiayac2" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Keterangan</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="keterangan2" runat="server" TextMode="MultiLine" Width="200" Height="75" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tgl. Tanda Tangan</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="tglttd2" runat="server" Width="85" CssClass="txt_center" ReadOnly="true"></asp:TextBox>
                                            <label for="tglttd2" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="tglttdc2" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table height="50" style="display:none">
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

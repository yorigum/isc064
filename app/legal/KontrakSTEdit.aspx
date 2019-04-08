<%@ Reference Page="~/Log.aspx" %>
<%@ Page Language="c#" Inherits="ISC064.LEGAL.KontrakSTEdit" CodeFile="KontrakSTEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrak" Src="NavKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Edit Serah Terima</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Kontrak - Edit Serah Terima">
</head>
<body onkeyup="if(event.keyCode==27) document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div class="content-header">
            <uc1:NavKontrak ID="NavKontrak1" runat="server" Aktif="4"></uc1:NavKontrak>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadKontrak ID="HeadST1" runat="server"></uc1:HeadKontrak>
                <table cellspacing="5">
                    <tr valign="middle">
                        <td style="width: 8%"><b>Print :</b>
                        </td>
                        <td class="printhref">
                            <a id="printBAST" runat="server"><b>BAST</b></a>
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
                            <asp:RadioButtonList ID="stat" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="stat_SelectedIndexChanged">
                            <asp:ListItem Value="B" Selected="True"> Belum</asp:ListItem>
                            <asp:ListItem Value="S"> Target BAST</asp:ListItem>
                            <asp:ListItem Value="D"> BAST</asp:ListItem>
                            <asp:ListItem Value="T"> Tanda Tangan</asp:ListItem>
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
						<td>No. BAST</td>
						<td>:</td>
						<td>
							<asp:textbox id="nost" runat="server" cssclass="txt" width="120" maxlength="20" readonly="True">#AUTO#</asp:textbox>
							<asp:label id="nostc" runat="server" cssclass="err"></asp:label>
						</td>
                        <td rowspan="2">
                            <asp:RadioButtonList ID="stused" runat="server" RepeatDirection="Vertical" CellPadding="10" >
                                <asp:ListItem Value="0" Enabled="false">Auto</asp:ListItem>
                                <asp:ListItem Value="1" Selected="True" Enabled="false">Manual</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
					</tr>
                    <tr>
						<td>No. BAST Manual</td>
						<td>:</td>
						<td>
							<asp:textbox id="nostm" runat="server" cssclass="txt" width="200" maxlength="50" ReadOnly="true"></asp:textbox>
							<asp:label id="nostmc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Tanggal BAST</td>
						<td>:</td>
						<td>
							<nobr>
								<asp:textbox id="tglst" runat="server" cssclass="txt_center" width="85" ReadOnly="true"></asp:textbox>
								<label for="tglst" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label> </nobr>
							<asp:label id="tglstc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Luas Semi Gross</td>
						<td>:</td>
						<td>
							<asp:textbox id="luas" runat="server" cssclass="txt_num" width="75" ReadOnly="true"></asp:textbox>
							m2
							<asp:label id="luasc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
                    <tr>
						<td>Luas Nett</td>
						<td>:</td>
						<td>
							<asp:textbox id="luasnett" runat="server" cssclass="txt_num" width="75" ReadOnly="true">0</asp:textbox>
							m2
							<asp:label id="luasnettc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Biaya Tambahan</td>
						<td>:</td>
						<td>
							<asp:textbox id="nilaibiaya" runat="server" cssclass="txt_num" width="100" ReadOnly="true">0</asp:textbox>
							<asp:label id="nilaibiayac" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
                    <tr>
						<td>Lebih Bayar</td>
						<td>:</td>
						<td>
							<asp:textbox id="lebihbayar" runat="server" cssclass="txt_num" width="100" ReadOnly="true">0</asp:textbox>
							<asp:label id="lebihbayarc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
                    <tr>
                        <td>Keterangan</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="keterangan" runat="server" TextMode="MultiLine" Width="200" Height="75" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>    
                        <td>Tgl. Tanda Tangan</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="tglttd" runat="server" Width="85" CssClass="txt_center" ReadOnly="true"></asp:TextBox>
                            <label for="tglttd" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label>
                            <asp:label id="tglttdc" runat="server" cssclass="err"></asp:label>
                        </td>
                    </tr>
				</table>
                <table id="target" runat="server" cellspacing="5">
                <tr>
                    <td>Tgl Target</td>
                    <td>:</td>
                    <td>
                    <asp:TextBox ID="tgltarget" runat="server" CssClass="txt_center" Width="75" Font-Size="8" ReadOnly="true"></asp:TextBox>
                    <label for="tgltarget" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="lblTglTarget" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                </table>
                <table id="bast" runat="server" cellspacing="5">
					<tr>
						<td>No. BAST</td>
						<td>:</td>
						<td>
							<asp:textbox id="nost1" runat="server" cssclass="txt" width="120" maxlength="20" readonly="True">#AUTO#</asp:textbox>
							<asp:label id="nostc1" runat="server" cssclass="err"></asp:label>
						</td>
                        <td rowspan="2">
                            <asp:RadioButtonList ID="stused1" runat="server" RepeatDirection="Vertical" CellPadding="10" >
                                <asp:ListItem Value="0"  Selected="True" Enabled="false">Auto</asp:ListItem>
                                <asp:ListItem Value="1" Enabled="false">Manual</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
					</tr>
                    <tr>
						<td>No. BAST Manual</td>
						<td>:</td>
						<td>
							<asp:textbox id="nostm1" runat="server" cssclass="txt" width="200" maxlength="50" ReadOnly="true"></asp:textbox>
							<asp:label id="nostmc1" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Tanggal BAST</td>
						<td>:</td>
						<td>
							<nobr>
								<asp:textbox id="tglst1" runat="server" cssclass="txt_center" width="85" ReadOnly="true"></asp:textbox>
								<label for="tglst1" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label> </nobr>
							<asp:label id="tglstc1" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Luas Semi Gross</td>
						<td>:</td>
						<td>
							<asp:textbox id="luas1" runat="server" cssclass="txt_num" width="75" ReadOnly="true"></asp:textbox>
							m2
							<asp:label id="luasc1" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
                    <tr>
						<td>Luas Nett</td>
						<td>:</td>
						<td>
							<asp:textbox id="luasnett1" runat="server" cssclass="txt_num" width="75" ReadOnly="true">0</asp:textbox>
							m2
							<asp:label id="luasnettc1" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Biaya Tambahan</td>
						<td>:</td>
						<td>
							<asp:textbox id="nilaibiaya1" runat="server" cssclass="txt_num" width="100" ReadOnly="true">0</asp:textbox>
							<asp:label id="nilaibiayac1" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
                    <tr>
						<td>Lebih Bayar</td>
						<td>:</td>
						<td>
							<asp:textbox id="lebihbayar1" runat="server" cssclass="txt_num" width="100" ReadOnly="true">0</asp:textbox>
							<asp:label id="lebihbayarc1" runat="server" cssclass="err"></asp:label>
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
						<td>No. BAST</td>
						<td>:</td>
						<td>
							<asp:textbox id="nost2" runat="server" cssclass="txt" width="120" maxlength="20" readonly="True">#AUTO#</asp:textbox>
							<asp:label id="nostc2" runat="server" cssclass="err"></asp:label>
						</td>
                        <td rowspan="2">
                            <asp:RadioButtonList ID="stused2" runat="server" RepeatDirection="Vertical" CellPadding="10" >
                                <asp:ListItem Value="0" Enabled="false">Auto</asp:ListItem>
                                <asp:ListItem Value="1" Enabled="false" Selected="True">Manual</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
					</tr>
                    <tr>
						<td>No. BAST Manual</td>
						<td>:</td>
						<td>
							<asp:textbox id="nostm2" runat="server" cssclass="txt" width="200" maxlength="50" ReadOnly="true"></asp:textbox>
							<asp:label id="nostmc2" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Tanggal BAST</td>
						<td>:</td>
						<td>
							<nobr>
								<asp:textbox id="tglst2" runat="server" cssclass="txt_center" width="85" ReadOnly="true"></asp:textbox>
								<label for="tglst2" class="btn btn-cal" aria-disabled="true"><i class="fa fa-calendar"></i></label> </nobr>
							<asp:label id="tglstc2" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Luas Semi Gross</td>
						<td>:</td>
						<td>
							<asp:textbox id="luas2" runat="server" cssclass="txt_num" width="75" ReadOnly="true"></asp:textbox>
							m2
							<asp:label id="luasc2" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
                    <tr>
						<td>Luas Nett</td>
						<td>:</td>
						<td>
							<asp:textbox id="luasnett2" runat="server" cssclass="txt_num" width="75" ReadOnly="true">0</asp:textbox>
							m2
							<asp:label id="luasnettc2" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Biaya Tambahan</td>
						<td>:</td>
						<td>
							<asp:textbox id="nilaibiaya2" runat="server" cssclass="txt_num" width="100" ReadOnly="true">0</asp:textbox>
							<asp:label id="nilaibiayac2" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
                    <tr>
						<td>Lebih Bayar</td>
						<td>:</td>
						<td>
							<asp:textbox id="lebihbayar2" runat="server" cssclass="txt_num" width="100" ReadOnly="true">0</asp:textbox>
							<asp:label id="lebihbayarc2" runat="server" cssclass="err"></asp:label>
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
                            <asp:label id="tglttdc2" runat="server" cssclass="err"></asp:label>
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


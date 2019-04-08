<%@ Reference Page="~/Customer.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.TTSEdit" CodeFile="TTSEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadTTS" Src="HeadTTS.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavTTS" Src="NavTTS.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Edit Tanda Terima Sementara</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Tanda Terima Sementara - Edit TTS">
</head>
<body onkeyup="if(event.keyCode==27)window.close()">
    <form id="Form1" method="post" runat="server">
    <uc1:NavTTS ID="NavTTS1" runat="server" Aktif="1"></uc1:NavTTS>
    <div class="tabdata">
        <div class="pad">
            <uc1:HeadTTS ID="HeadTTS1" runat="server"></uc1:HeadTTS>
            <table cellspacing="5">
                <tr valign="top">
                    <td>
                        Print :
                    </td>
                    <td class="printhref">
                        <p>
                            <a id="printTTS" runat="server"><b>Tanda Terima Sementara</b></a></p>
                        <p>
                            <a id="printBKM" runat="server"><b>Kwitansi</b></a></p>
                        <p><a id="printFPS" runat="server"><b>Faktur Pajak</b></a></p>
                    </td>
                    <td width="100%">
                    </td>
                    <td>
                        <input type="button" class="btn" value="  Log  " id="btnlog" runat="server" name="btnlog"
                            accesskey="l">
                    </td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td style="display: none;">
                        <input type="button" class="btn" value="Slip Setoran" id="btnslip" runat="server"
                            name="btnslip" style="width: 100px">
                    </td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <p>
                            <input type="button" class="btn" value="Batal Kwitansi" id="btnbatalkw" runat="server"
                                name="btnbatalkw" style="width: 100px"></p>
                    </td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <p>
                            <input type="button" class="btn" value="Void" id="btnvoid" runat="server" name="btnvoid"
                                style="width: 100px"></p>
                        <p>
                            <input type="button" class="btn" value="Void Batal FP" id="btnvoidfp" runat="server" name="btnvoidfp"
                                style="width: 100px"></p>
                        <p style="display:none;">
                            <input type="button" class="btn" value="Void Reimburse" id="btnvoid2" runat="server"
                                name="btnvoid2" style="width: 100px"></p>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="stamp">
                        Kasir :
                        <asp:Label ID="kasir" runat="server"></asp:Label>,
                        <asp:Label ID="ip" runat="server"></asp:Label>
                        &nbsp;<asp:Label ID="tglInput" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr valign="top">
                    <td>
                        <div style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <p>
                                            <b>Status</b></p>
                                    </td>
                                    <td>
                                        <p>
                                            <b>Cara Bayar</b></p>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td width="300">
                                        <p>
                                            <asp:Label ID="status" runat="server" Font-Bold="True" Font-Size="20"></asp:Label>&nbsp;</p>
                                        <table>
                                            <tr>
                                                <td>
                                                    No. Kwitansi
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="bkminfo" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Sumber Bayar
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="sumberbayar" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <p>
                                            <asp:Label ID="carabayar" runat="server" Font-Bold="True" Font-Size="20"></asp:Label></p>
                                        <p>
                                            <asp:Label ID="tolak" runat="server" Font-Bold="True" ForeColor="Red" Font-Size="10"></asp:Label></p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table cellspacing="5">
                            <tr>
                                <td colspan="3">
                                    <p>
                                        <b>Data Tanda Terima Sementara</b></p>
                                </td>
                            </tr>
                            <tr id="bkmtr" runat="server">
                                <td>
                                    Tgl. Kwitansi
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="tglbkm" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                    <input type="button" value="..." class="btn" onclick="openCalendar('tglbkm')" />
                                    <%--<input type="button" class="btn" value="Buka Kwitansi" id="btnkw" runat="server"
                                        name="btnkw" style="width: 100px" />--%>
                                    <asp:Button ID="btnkw" runat="server" CssClass="btn" Text="Buka Kwitansi" 
                                        Width="100" onclick="btnkw_Click" />
                                    <asp:Label ID="tglbkmc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tgl. TTS
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="tgltts" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                    <input type="button" value="..." class="btn" onclick="openCalendar('tgltts')" />
                                    <asp:Label ID="tglttsc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    No. Manual
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    TTS
                                    <asp:TextBox ID="manualtts" runat="server" Width="60" CssClass="txt_center"></asp:TextBox>
                                    <asp:Label ID="manualttsc" runat="server" CssClass="err"></asp:Label>
                                    &nbsp;&nbsp; &nbsp;&nbsp; Kwitansi
                                    <asp:TextBox ID="manualbkm" runat="server" Width="60" CssClass="txt_center" MaxLength="6"></asp:TextBox>
                                    <asp:Label ID="manualbkmc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                        	<tr id="trAdm" runat="server">
								<td>Administrasi Bank</td>
								<td>:</td>
								<td>
									<asp:textbox id="admBank" runat="server" width="100" cssclass="txt_num" maxlength="200"></asp:textbox>
				                    <asp:label id="nilaic" runat="server" cssclass="err"></asp:label>
								</td>
							</tr>
							<tr id="trLb" runat="server">
								<td>Pembulatan</td>
								<td>:</td>
								<td>
									<asp:textbox id="lebihBayar" runat="server" width="100" cssclass="txt_num" maxlength="200" Enabled="false"></asp:textbox>
				                    <asp:label id="lebihBayarc" runat="server" cssclass="err"></asp:label>
								</td>
							</tr>
							<tr id="trL" runat="server">
								<td>Lebih Bayar</td>
								<td>:</td>
								<td>
									<asp:textbox id="lb" runat="server" width="100" cssclass="txt_num" maxlength="200" Enabled="false"></asp:textbox>
				                    <asp:label id="lbc" runat="server" cssclass="err"></asp:label>
								</td>
							</tr>
                            <tr>
                                <td>
                                    Keterangan
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ket" runat="server" Width="425" CssClass="txt" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Rekening Bank
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAcc" runat="server" CssClass="ddl" Width="350">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            									<tr>
										<td>No. Faktur Pajak</td>
										<td>:</td>
										<td>
											<asp:label id="lblNoFaktur" runat="server"></asp:label>
											<asp:textbox id="tbNoFaktur" runat="server" width="150" cssclass="txt" maxlength="50" Enabled="false"></asp:textbox>
											<asp:CheckBox ID="delfp" runat="server" Text="Delete No. FP" />
										</td>
									</tr>
                            <tr>
                                <td colspan="3">
                                    <br />
                                    <p>
                                        <b>Informasi Cek Giro</b></p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    No. BG
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="nobg" runat="server" Width="150" CssClass="txt" MaxLength="20"></asp:TextBox>
                                    <asp:Label ID="nobgc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tgl. BG
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="tglbg" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                    <input type="button" value="..." class="btn" onclick="openCalendar('tglbg')" />
                                    <asp:Label ID="tglbgc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pengelola
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="titip" runat="server" Width="250" CssClass="txt" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <br />
                                    <p>
                                        <b>Informasi Kartu Kredit</b></p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    No. Kartu
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:textbox id="nokk" runat="server" width="125" cssclass="txt" maxlength="50"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
					            <td>Bank</td>
					            <td>:</td>
					            <td>
						            <asp:textbox id="bankkk" runat="server" width="125" cssclass="txt" maxlength="50"></asp:textbox>
					            </td>
				            </tr>
                            <tr>
                                <td colspan="3">
                                    <br>
                                    <p>
                                        <b>Identitas Customer</b></p>
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
                                    <asp:TextBox ID="unit" runat="server" Width="200" CssClass="txt" MaxLength="100"></asp:TextBox>
                                    <asp:Label ID="unitc" runat="server" CssClass="err"></asp:Label>
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
                                    <asp:TextBox ID="customer" runat="server" Width="300" CssClass="txt" MaxLength="100"></asp:TextBox>
                                    <asp:Label ID="customerc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table id="tblOK" style="height: 50px">
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="alasan" runat="server" Style="font-size: 18px; color: red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="ok" runat="server" CssClass="btn" Text="OK" Width="75" OnClick="ok_Click">
                                    </asp:Button>
                                </td>
                                <td>
                                    <input id="cancel" type="button" onclick="window.close()" class="btn" value="Cancel"
                                        style="width: 75px" />
                                </td>
                                <td>
                                    <asp:Button ID="save" runat="server" CssClass="btn" Text="Apply" Width="75" AccessKey="a"
                                        OnClick="save_Click"></asp:Button>
                                </td>
                                <td style="padding-left: 10px">
                                    <p class="feed">
                                        <asp:Label ID="feed" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="padding-left: 10px">
                                    <p class="fobo">
                                        <asp:Label ID="statusFOBO" runat="server"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                        Nilai :
                        <p>
                            <asp:Label ID="nilai" runat="server" Font-Bold="True" Font-Size="18"></asp:Label></p>
                        <p>
                            <asp:CheckBox ID="pph" runat="server" Text="PPH"></asp:CheckBox></p>
                        <br />
                        <p id="alokasi" runat="server">
                            <b>Alokasi Pembayaran</b></p>
                        <ul id="detil" runat="server">
                        </ul>
                        <br />
                        <asp:Label ID="lblAkunting" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>

<%@ Page Language="c#" Inherits="ISC064.LEGAL.KontrakBerkasPPJB" CodeFile="KontrakBerkasPPJB.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrak" Src="NavKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>Proses PPJB</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Kontrak - Proses PPJB">
</head>
<body onkeyup="if(event.keyCode==27) document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div id="pilih" runat="server">
				<h1 class="title title-line">Kelengkapan Berkas</h1>
				<p><b><i>Halaman 1 dari 2</i></b></p>
				<br>
				<table style="BORDER-RIGHT:#dcdcdc 1px solid; BORDER-TOP:#dcdcdc 1px solid; BORDER-LEFT:#dcdcdc 1px solid; BORDER-BOTTOM:#dcdcdc 1px solid"
					cellspacing="5" cellpadding="1">
					<tr>
						<td><b>No. Kontrak :</b></td>
						<td>
							<asp:textbox id="nokontrak" runat="server" width="100" cssclass="txt"></asp:textbox>
                        <input class="btn btn-orange" id="Button1" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a&ppjb=B' type="button" value="&#xf002;" style="font-family: 'fontawesome'"
								name="btnpop" runat="server" />
						</td>
						<td>
							<asp:LinkButton id="next" runat="server" cssclass="btn btn-blue" onclick="next_Click">
                                Next <i class="fa fa-arrow-right"></i>
							</asp:LinkButton>
						</td>
					</tr>
				</table>
				<p class="feed">
					<asp:label id="feed" runat="server"></asp:label>
				</p>
				<input type="button" id="backbtn" runat="server" onclick="history.back(-1)" value="Cancel"
					class="btn" style="MARGIN:5px" name="backbtn">
			</div>
        <div id="frm" runat="server" class="tabdata">
            <h1 class="title title-line">Kelengkapan Berkas</h1>
            <p><b><i>Halaman 2 dari 2</i></b></p>
            <br>
            <h2>Kelengkapan Berkas</h2>
            <br />
            <table cellspacing="5" cellpadding="1">
					<tr>
						<td style="width:100px">No. Kontrak</td>
						<td>:</td>
						<td>
							<asp:label id="nokontrakl" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Unit</td>
						<td>:</td>
						<td>
							<asp:label id="unit" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Customer</td>
						<td>:</td>
						<td>
							<asp:label id="customer" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Sales</td>
						<td>:</td>
						<td>
							<asp:label id="agent" runat="server" font-bold="True"></asp:label>
						</td>
					</tr>
					<tr>
						<td>Pelunasan</td>
						<td>:</td>
						<td>
							<asp:label id="persenlunas" runat="server" font-bold="True"></asp:label>% 
							&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:label id="lunasinfo" runat="server" cssclass="err" font-bold="True"></asp:label>
                            <asp:Label ID="noberkas" runat="server" Visible="false"></asp:Label>
						</td>
					</tr>
				</table>
				<br>
            <table width="100%">
                <tr>
                    <td valign="top">
                        <table>
<%--                            <tr>
                                <td valign="middle">KTP Suami
                                </td>
                                <td valign="middle">:
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="statktp" runat="server" RepeatDirection="Horizontal" CellPadding="10">
                                        <asp:ListItem Value="0" Selected="True"> Tidak Ada</asp:ListItem>
                                        <asp:ListItem Value="1"> Ada</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="ktp" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle">KTP Istri</td>
                                <td valign="middle">:</td>
                                <td>
                                    <asp:RadioButtonList ID="statktp2" runat="server" RepeatDirection="Horizontal" CellPadding="10">
                                        <asp:ListItem Value="0" Selected="True"> Tidak Ada</asp:ListItem>
                                        <asp:ListItem Value="1"> Ada</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle">Kartu Keluarga</td>
                                <td valign="middle">:</td>
                                <td>
                                    <asp:RadioButtonList ID="kk" runat="server" RepeatDirection="Horizontal" CellPadding="10">
                                        <asp:ListItem Value="0" Selected="True"> Tidak Ada</asp:ListItem>
                                        <asp:ListItem Value="1"> Ada</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle">Surat Nikah</td>
                                <td valign="middle">:</td>
                                <td>
                                    <asp:RadioButtonList ID="nikah" runat="server" RepeatDirection="Horizontal" CellPadding="10">
                                        <asp:ListItem Value="0" Selected="True"> Tidak Ada</asp:ListItem>
                                        <asp:ListItem Value="1"> Ada</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle">SKK</td>
                                <td valign="middle">:</td>
                                <td>
                                    <asp:RadioButtonList ID="skk" runat="server" RepeatDirection="Horizontal" CellPadding="10">
                                        <asp:ListItem Value="0" Selected="True"> Tidak Ada</asp:ListItem>
                                        <asp:ListItem Value="1"> Ada</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle">RK</td>
                                <td valign="middle">:</td>
                                <td>
                                    <asp:RadioButtonList ID="rk" runat="server" RepeatDirection="Horizontal" CellPadding="10">
                                        <asp:ListItem Value="0" Selected="True"> Tidak Ada</asp:ListItem>
                                        <asp:ListItem Value="1"> Ada</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle">BT</td>
                                <td valign="middle">:</td>
                                <td>
                                    <asp:RadioButtonList ID="bt" runat="server" RepeatDirection="Horizontal" CellPadding="10">
                                        <asp:ListItem Value="0" Selected="True"> Tidak Ada</asp:ListItem>
                                        <asp:ListItem Value="1"> Ada</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle">Kwitansi</td>
                                <td valign="middle">:</td>
                                <td>
                                    <asp:RadioButtonList ID="kw" runat="server" RepeatDirection="Horizontal" CellPadding="10">
                                        <asp:ListItem Value="0" Selected="True"> Tidak Ada</asp:ListItem>
                                        <asp:ListItem Value="1"> Ada</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle">Denah Unit</td>
                                <td valign="middle">:</td>
                                <td>
                                    <asp:RadioButtonList ID="du" runat="server" RepeatDirection="Horizontal" CellPadding="10">
                                        <asp:ListItem Value="0" Selected="True"> Tidak Ada</asp:ListItem>
                                        <asp:ListItem Value="1"> Ada</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle">Denah Lantai</td>
                                <td valign="middle">:</td>
                                <td>
                                    <asp:RadioButtonList ID="dl" runat="server" RepeatDirection="Horizontal" CellPadding="10">
                                        <asp:ListItem Value="0" Selected="True"> Tidak Ada</asp:ListItem>
                                        <asp:ListItem Value="1"> Ada</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle">Spesifikasi Material</td>
                                <td valign="middle">:</td>
                                <td>
                                    <asp:RadioButtonList ID="sm" runat="server" RepeatDirection="Horizontal" CellPadding="10">
                                        <asp:ListItem Value="0" Selected="True"> Tidak Ada</asp:ListItem>
                                        <asp:ListItem Value="1"> Ada</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>--%>
                            <asp:PlaceHolder ID="col1" runat="server" />
                            <asp:PlaceHolder ID="col2" runat="server" />
                            <asp:PlaceHolder ID="col3" runat="server" />
                            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                            <tr runat="server" id="lkp">
                                <td valign="top">Tanggal Lengkap</td>
                                <td valign="top">:</td>
                                <td>
                                    <asp:TextBox ID="tglkp" runat="server" Width="85" CssClass="txt_center tgl"></asp:TextBox>
                                    <label for="tglkp" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                    <asp:label id="tglkpc" runat="server" cssclass="err"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table height="50">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK
                                                </asp:LinkButton>
                                            </td>
                                            <%--<td>
                                                <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a"
                                                    OnClick="save_Click"><i class="fa fa-check"></i>Apply</asp:LinkButton>
                                            </td>--%>
                                            <td>
                                                <input type="button" onclick="location.href='?'" class="btn btn-red" value="Cancel" style="WIDTH:100px"
								                    id="cancel" runat="server">
                                            </td>
                                            <td style="padding-left: 10px">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <script language="javascript" type="text/javascript">
			function call(nokontrak)
			{
				document.getElementById('nokontrak').value = nokontrak;
				document.getElementById('next').click();
			}
			</script>
    </form>

<%--    <script language="javascript" type="text/javascript">
        function call(nova) {
            document.getElementById('nova').value = nova;
        }
    </script>--%>

</body>
</html>


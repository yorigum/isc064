<%@ Page Language="c#" Inherits="ISC064.SETTINGS.Skema" CodeFile="Skema.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Setup Skema Cara Bayar</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Setup Skema Cara Bayar">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Setup Skema Cara Bayar</h1>
        <br>
        <table cellpadding="0" cellspacing="0">
            <tr valign="top">
                <td width="250">
                    <p style="font: bold 10pt">Skema Aktif</p>
                    <ul id="aktif" runat="server" class="plike">
                    </ul>
                    <br>
                    <p style="font: bold 10pt">Skema Inaktif</p>
                    <ul id="inaktif" runat="server" class="plike">
                    </ul>
                </td>
                <td style="padding: 5px 10px 0px 15px">
                    <img src="/Media/line_vert.gif"></td>
                <td>
                    <h2 style="padding-left: 5px; padding-bottom: 5px">Pendaftaran Skema Baru</h2>
                    <table cellspacing="5">
                        <tr>
                            <td colspan="3">
                                <p><b>Rumus Global</b></p>
                            </td>
                        </tr>
                        <tr>
                            <td>Nama Skema</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="nama" runat="server" Width="250" MaxLength="100" CssClass="txt"></asp:TextBox>
                                <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Project</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="project" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
						<tr>
			                <td valign="top">Present Value</td>
			                <td valign="top">:</td>
			                <td>
			                <div id="persentingakat" runat="server">
				                <input class="btn" onclick="popbunga('bunga2','bungaket')" type="button" value="..." id="btn2" runat="server">
				                <asp:textbox id="bunga2" runat="server" cssclass="txt" width="50px" Text='0'>0</asp:textbox>&nbsp;
                                <div style="DISPLAY: none"><asp:textbox id="bungaket" runat="server"></asp:textbox></div>
				                    <asp:label id="bunga2c" runat="server" cssclass="err"></asp:label>
				                </div>
			                </td>
		                </tr>
                        <tr>
                            <td>Diskon</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="diskon" runat="server" Width="150" CssClass="txt" MaxLength="100"></asp:TextBox>
                                <input type="button" class="btn" value="..." onclick="popdiskon('diskon', 'diskonket')">
                                <div style="display: none">
                                    <asp:TextBox ID="diskonket" runat="server"></asp:TextBox>
                                </div>
                                % bertingkat &nbsp;&nbsp;
									<asp:CheckBox ID="round" runat="server" Text="Pembulatan Nilai" Checked="True"></asp:CheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Tipe</td>
                            <td>:</td>
                            <td>
                                <asp:RadioButtonList ID="jenis" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem class="igroup-radio">KPR</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">CASH BERTAHAP</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">CASH KERAS</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:Label ID="carabayarc" runat="server" CssClass="err"></asp:Label></td>
                            <td></td>
                        </tr>
                    </table>
                    <br>
                    <table cellspacing="5">
                        <tr>
                            <td colspan="8">
                                <p><b>Rumus Nilai</b></p>
                            </td>
                        </tr>
                        <tr>
                            <td>Booking Fee</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="bfkali" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox>&nbsp;x&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:RadioButton ID="bfrupiah" runat="server" Text="Rp" GroupName="bftipe" Checked="True"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="bfpersen" runat="server" Text="%" GroupName="bftipe"></asp:RadioButton></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="bfjumlah" runat="server" CssClass="txt_num">5,000,000</asp:TextBox>
                                <asp:Label ID="bfc" runat="server" CssClass="err"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>DP</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="dpkali" runat="server" Width="40" CssClass="txt_center">5</asp:TextBox>&nbsp;x&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:RadioButton ID="dprupiah" runat="server" Text="Rp" GroupName="dptipe"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="dppersen" runat="server" Text="%" GroupName="dptipe" Checked="True"></asp:RadioButton></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="dpjumlah" runat="server" CssClass="txt_num">30</asp:TextBox>
                                <asp:Label ID="dpc" runat="server" CssClass="err"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Angsuran</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="angkali" runat="server" Width="40" CssClass="txt_center">18</asp:TextBox>&nbsp;x&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:RadioButton ID="angrupiah" runat="server" Text="Rp" GroupName="angtipe"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="angpersen" runat="server" Text="%" GroupName="angtipe" Checked="True"></asp:RadioButton></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="angjumlah" runat="server" CssClass="txt_num">70</asp:TextBox>
                                <asp:Label ID="angc" runat="server" CssClass="err"></asp:Label></td>
                        </tr>
                    </table>
                    <br>
                    <table cellspacing="5">
                        <tr>
                            <td colspan="9">
                                <p><b>Rumus Jadwal</b></p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;</td>
                            <td colspan="3"><u>Interval</u></td>
                            <td>&nbsp;</td>
                            <td colspan="3"><u>Pertama</u></td>
                        </tr>
                        <tr>
                            <td>Booking Fee</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="bflama1" runat="server" Width="40" CssClass="txt_center">7</asp:TextBox></td>
                            <td>
                                <asp:RadioButton ID="bfbln1" runat="server" Text="Bln" GroupName="bf1"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="bfhari1" runat="server" Text="Hari" GroupName="bf1" Checked="True"></asp:RadioButton></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="bflama2" runat="server" Width="40" CssClass="txt_center">0</asp:TextBox></td>
                            <td>
                                <asp:RadioButton ID="bfbln2" runat="server" Text="Bln" GroupName="bf2"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="bfhari2" runat="server" Text="Hari" GroupName="bf2" Checked="True"></asp:RadioButton>
                                <asp:Label ID="bf2c" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>DP</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="dplama1" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox></td>
                            <td>
                                <asp:RadioButton ID="dpbln1" runat="server" Text="Bln" GroupName="dp1" Checked="True"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="dphari1" runat="server" Text="Hari" GroupName="dp1"></asp:RadioButton></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="dplama2" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox></td>
                            <td>
                                <asp:RadioButton ID="dpbln2" runat="server" Text="Bln" GroupName="dp2" Checked="True"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="dphari2" runat="server" Text="Hari" GroupName="dp2"></asp:RadioButton>
                                <asp:Label ID="dp2c" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Angsuran</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="anglama1" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox></td>
                            <td>
                                <asp:RadioButton ID="angbln1" runat="server" Text="Bln" GroupName="ang1" Checked="True"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="anghari1" runat="server" Text="Hari" GroupName="ang1"></asp:RadioButton></td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:TextBox ID="anglama2" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox></td>
                            <td>
                                <asp:RadioButton ID="angbln2" runat="server" Text="Bln" GroupName="ang2" Checked="True"></asp:RadioButton></td>
                            <td>
                                <asp:RadioButton ID="anghari2" runat="server" Text="Hari" GroupName="ang2"></asp:RadioButton>
                                <asp:Label ID="ang2c" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br>
                    <table cellspacing="5">
                        <tr>
                            <td valign="top">Booking Fee dipotong di :
									<br>
                                <asp:RadioButton ID="dp1potong" runat="server" GroupName="bfpotong" Text="DP 1" Checked="True"></asp:RadioButton>
                                &nbsp;&nbsp;
									<asp:RadioButton ID="ang1potong" runat="server" GroupName="bfpotong" Text="Angsuran 1"></asp:RadioButton>
                                &nbsp;&nbsp;
									<asp:RadioButton ID="dpspotong" runat="server" GroupName="bfpotong" Text="DP disebar"></asp:RadioButton>
                                &nbsp;&nbsp;
									<asp:RadioButton ID="angspotong" runat="server" GroupName="bfpotong" Text="Angsuran disebar"></asp:RadioButton>
                                <asp:Label ID="cc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table height="50">
                        <tr>
                            <td>
                                <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> 
                                    OK</asp:LinkButton>
                            </td>
                            <td style="padding-left: 10px">
                                <p class="feed">
                                    <asp:Label ID="feed" runat="server"></asp:Label>
                                </p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function popdiskon(d1, d2) {
                foo1 = document.getElementById(d1);
                foo2 = document.getElementById(d2);
                openModal('SkemaDiskon.aspx?t1=' + foo1.value + '&t2=' + foo2.value + '&d1=' + d1 + '&d2=' + d2, '450', '360');
            }
            function popbunga(d1, d2) {
                foo1 = document.getElementById(d1);
                foo2 = document.getElementById(d2);
                openModal('SkemaBunga.aspx?t1=' + foo1.value + '&t2=' + foo2.value + '&d1=' + d1 + '&d2=' + d2, '450', '360');
            }
            function recaldisc(discTxt) {
                disc = discTxt.value.split("+");

                discTxt.value = "";

                for (i = 0; i < disc.length; i++) {
                    if (!isNaN(disc[i]) && disc[i] != "") {
                        if (discTxt.value != "") discTxt.value = discTxt.value + "+";
                        discTxt.value = discTxt.value + disc[i];
                    }
                }
            }
        </script>
    </form>
</body>
</html>

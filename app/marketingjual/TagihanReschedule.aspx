<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.TagihanReschedule" CodeFile="TagihanReschedule.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <meta name="vs_snapToGrid" content="False">
    <meta name="vs_showGrid" content="False">
    <title>Reschedule Tagihan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tagihan - Reschedule Tagihan">
</head>
<body class="body-padding" onkeyup="cancelclick();">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input style="display: none">
        <div style="display: none">
            <asp:CheckBox ID="dariDaftar" runat="server"></asp:CheckBox>
        </div>
        <div id="pilih" runat="server">
            <h1 class="title title-line">Reschedule Tagihan</h1>
            <p><b><i>Halaman 1 dari 3</i></b></p>
            <br>
            <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid; border-bottom: #dcdcdc 1px solid"
                cellspacing="5">
                <tr>
                    <td><b>No. Kontrak :</b></td>
                    <td>
                        <asp:TextBox ID="nokontrak1" runat="server" CssClass="txt" Width="100"></asp:TextBox>
                        <input type="button" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a' id="btnpop"
                            runat="server" name="btnpop">
                    </td>
                    <td>
                        <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" OnClick="next_Click">
                                Next <i class="fa fa-arrow-right"></i>
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
            <p class="feed">
                <asp:Label ID="feed" runat="server"></asp:Label>
            </p>
            <p class="resc">
                <asp:Label ID="resc" runat="server"></asp:Label>
            </p>
            <input type="button" id="backbtn" runat="server" onclick="history.back(-1)" value="Cancel"
                class="btn" style="margin: 5px" name="backbtn">
        </div>
        <div id="frm" runat="server">
            <h1 class="title title-line">Reschedule Tagihan</h1>
            <p><b><i>Halaman 2 dari 3</i></b></p>
            <br>
            <asp:ScriptManager runat="server" ID="script" EnablePartialRendering="true"></asp:ScriptManager>
            <asp:UpdatePanel runat="server" ID="update" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellpadding="0" cellspacing="0">
                        <tr valign="top">
                            <td width="600px">
                                <table cellspacing="5">
                                    <tr>
                                        <td colspan="8">
                                            <p><b>Rumus Nilai</b></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tagihan belum dibayar</td>
                                        <td>:</td>
                                        <td colspan="5">
                                            <asp:TextBox ID="netto" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="rounding" runat="server" Text="Pembulatan nilai" Checked="True"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Cara Bayar</td>
                                        <td valign="top">:</td>
                                        <td colspan="8">
                                            <asp:RadioButtonList ID="carabayar2" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem>KPR</asp:ListItem>
                                                <asp:ListItem>CASH BERTAHAP</asp:ListItem>
                                                <asp:ListItem>CASH KERAS</asp:ListItem>
                                            </asp:RadioButtonList></td>
                                    </tr>
                                    <tr>
                                        <td>Booking Fee</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="bfkali" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox>
                                            x</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:RadioButton ID="bfrupiah" runat="server" GroupName="bftipe" Text="Rp" Checked="True"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="bfpersen" runat="server" GroupName="bftipe" Text="%"></asp:RadioButton></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="bfjumlah" runat="server" CssClass="txt_num">10,000,000</asp:TextBox>
                                            <asp:Label ID="bfc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DP</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="dpkali" runat="server" Width="40" CssClass="txt_center">3</asp:TextBox>
                                            x</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:RadioButton ID="dprupiah" runat="server" GroupName="dptipe" Text="Rp"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="dppersen" runat="server" GroupName="dptipe" Text="%" Checked="True"></asp:RadioButton></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="dpjumlah" runat="server" CssClass="txt_num">30</asp:TextBox>
                                            <asp:Label ID="dpc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Angsuran</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="angkali" runat="server" Width="40" CssClass="txt_center">20</asp:TextBox>
                                            x</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:RadioButton ID="angrupiah" runat="server" GroupName="angtipe" Text="Rp"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="angpersen" runat="server" GroupName="angtipe" Text="%" Checked="True"></asp:RadioButton></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="angjumlah" runat="server" CssClass="txt_num">70</asp:TextBox>
                                            <asp:Label ID="angc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br>
                                <table cellspacing="5">
                                    <tr>
                                        <td colspan="8">
                                            <p><b>Skema Cara Bayar</b></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Skema</td>
                                        <td>:</td>
                                        <td colspan="5">
                                            <asp:DropDownList ID="skema" runat="server" Width="350px" AutoPostBack="True" OnSelectedIndexChanged="skema_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                                <table cellspacing="5">
                                    <tr>
                                        <td colspan="9">
                                            <p><b>Rumus Jadwal</b></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tgl. Kontrak</td>
                                        <td>:</td>
                                        <td colspan="7">
                                            <asp:TextBox ID="tgl" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                            <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"></td>
                                        <td colspan="3">
                                            <u>Interval</u>
                                        </td>
                                        <td></td>
                                        <td colspan="3">
                                            <u>Pertama</u>
                                        </td>
                                        <td>
                                            <u>Tgl. Mulai</u>
                                        </td>
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
                                            <asp:TextBox ID="bflama2" runat="server" Width="40" CssClass="txt_center">0</asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="bfbln2" runat="server" Text="Bln" GroupName="bf2"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="bfhari2" runat="server" Text="Hari" GroupName="bf2" Checked="True"></asp:RadioButton>
                                            <asp:Label ID="bf2c" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="bftgl" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                            <label for="bftgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="bftglc" runat="server" CssClass="err"></asp:Label>
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
                                            <asp:TextBox ID="dplama2" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="dpbln2" runat="server" Text="Bln" GroupName="dp2" Checked="True"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="dphari2" runat="server" Text="Hari" GroupName="dp2"></asp:RadioButton>
                                            <asp:Label ID="dp2c" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="dptgl" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                            <label for="dptgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="dptglc" runat="server" CssClass="err"></asp:Label>
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
                                            <asp:TextBox ID="anglama2" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="angbln2" runat="server" Text="Bln" GroupName="ang2" Checked="True"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="anghari2" runat="server" Text="Hari" GroupName="ang2"></asp:RadioButton>
                                            <asp:Label ID="ang2c" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="angtgl" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                            <label for="angtgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="angtglc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br>
                                <table cellspacing="5">
                                    <tr>
                                        <td>Booking Fee dipotong di :
										<br>
                                            <asp:RadioButton ID="dp1potong" runat="server" GroupName="bfpotong" Text="DP 1" Checked="True"></asp:RadioButton>
                                            &nbsp;&nbsp;
										<asp:RadioButton ID="ang1potong" runat="server" GroupName="bfpotong" Text="Angsuran 1"></asp:RadioButton>
                                            &nbsp;&nbsp;
										<asp:RadioButton ID="dpspotong" runat="server" GroupName="bfpotong" Text="DP disebar"></asp:RadioButton>
                                            &nbsp;&nbsp;
										<asp:RadioButton ID="angspotong" runat="server" GroupName="bfpotong" Text="Angsuran disebar"></asp:RadioButton>
                                            &nbsp;&nbsp;
										<asp:RadioButton ID="tidakpotong" runat="server" GroupName="bfpotong" Text="BF tidak disebar"></asp:RadioButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="cc" runat="server" CssClass="err"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px">
                                <img src="/Media/line_vert.gif"></td>
                            <td>
                                <table cellspacing="5">
                                    <tr>
                                        <td>No. Kontrak :<br>
                                            <asp:Label ID="nokontrak2" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Unit :
										<br>
                                            <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Customer :
										<br>
                                            <asp:Label ID="nama" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Sales :
										<br>
                                            <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table height="50">
                        <tr>
                            <td>
                                <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                            </td>
                            <td>
                                <input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href = '?'" type="button"
                                    value="Cancel" name="cancel" runat="server"></td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ok" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div id="hasil" runat="server">
            <h1 class="title title-line">Reschedule Tagihan</h1>
            <p><b><i>Halaman 3 dari 3</i></b></p>
            <br>
            <table cellpadding="0" cellspacing="0">
                <tr valign="top">
                    <td width="640">
                        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="0" EnableViewState="True">
                            <asp:TableRow HorizontalAlign="Left">
                                <asp:TableHeaderCell>No.</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Tipe</asp:TableHeaderCell>
                                <asp:TableHeaderCell Width="150">Keterangan</asp:TableHeaderCell>
                                <asp:TableHeaderCell Width="60">Tanggal</asp:TableHeaderCell>
                                <asp:TableHeaderCell Width="120" HorizontalAlign="Right">Jumlah</asp:TableHeaderCell>
                                <asp:TableHeaderCell>BF</asp:TableHeaderCell>
                            </asp:TableRow>
                        </asp:Table>
                    </td>
                    <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px; padding-top: 10px">
                        <img src="/Media/line_vert.gif"></td>
                    <td valign="top">
                        <table cellspacing="5">
                            <tr>
                                <td>No. Kontrak :<br>
                                    <asp:Label ID="nokontrakdetail" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Unit :
										<br>
                                    <asp:Label ID="unitdetail" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Customer :
										<br>
                                    <asp:Label ID="namadetail" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:CheckBox ID="alokasi" runat="server" Text="Auto Alokasi TTS" Checked="True"></asp:CheckBox>
            <table height="50">
                <tr>
                    <td>
                        <asp:LinkButton ID="insert" runat="server" CssClass="btn btn-blue" Width="75" OnClick="insert_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                    </td>
                    <td>
                        <input class="btn" id="cancel2" style="width: 75px" onclick="javascript: history.back();"
                            type="button" value="Cancel" name="cancel2" runat="server">
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript">
            function cancelclick() {
                if (event.keyCode == 27) {
                    if (document.getElementById('cancel'))
                        document.getElementById('cancel').click();
                    else if (document.getElementById('cancel2'))
                        document.getElementById('cancel2').click();
                    else if (document.getElementById('backbtn'))
                        document.getElementById('backbtn').click();
                }
            }
            function call(nokontrak) {
                document.getElementById('nokontrak1').value = nokontrak;
                document.getElementById('next').click();
            }
        </script>
    </form>
</body>
</html>

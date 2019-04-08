<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.UnitEditSpek" CodeFile="UnitEditSpek.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadUnit" Src="HeadUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavUnit" Src="NavUnit.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Edit Unit (Spesifikasi)</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Edit Unit (Spesifikasi)">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavUnit ID="NavUnit1" runat="server" Aktif="2"></uc1:NavUnit>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadUnit ID="HeadUnit1" runat="server"></uc1:HeadUnit>
                <table cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td width="270">
                            <table cellspacing="5" style="">
                                <tr style="display: none;">
                                    <td colspan="3">
                                        <b>Dimensi</b>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td colspan="3">
                                        <table cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td>Panjang</td>
                                                <td rowspan="3">&nbsp;&nbsp;x&nbsp;&nbsp;</td>
                                                <td>Lebar</td>
                                                <td rowspan="3">&nbsp;&nbsp;x&nbsp;&nbsp;</td>
                                                <td>Tinggi</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="panjang" runat="server" CssClass="txt" Width="60"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="lebar" runat="server" CssClass="txt" Width="60"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tinggi" runat="server" CssClass="txt" Width="60"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="panjangc" runat="server" CssClass="err"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lebarc" runat="server" CssClass="err"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="tinggic" runat="server" CssClass="err"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tanah</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="luassg" runat="server" CssClass="txt" Width="70"></asp:TextBox>
                                        m2
											<asp:Label ID="luassgc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Luas Bangunan.</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="luasnett" runat="server" CssClass="txt" Width="70"></asp:TextBox>
                                        m2
											<asp:Label ID="luasnettc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>Lebar Jalan</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="lebarjalan" runat="server" CssClass="txt_center" Width="50"></asp:TextBox>
                                        m
											<asp:Label ID="lebarjalanc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td>
                            <table cellspacing="5">
                                <tr style="display: none;">
                                    <td colspan="3">
                                        <b>Klasifikasi</b> &nbsp;&nbsp;&nbsp;
											<asp:RadioButton ID="outdoor" runat="server" Text="Outdoor" GroupName="extipe"></asp:RadioButton>
                                        <asp:RadioButton ID="indoor" runat="server" Text="Indoor" GroupName="extipe"></asp:RadioButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Zoning</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="zoning" runat="server" CssClass="txt" Width="320" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Arah Hadap</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="arahhadap" runat="server" CssClass="txt" Width="100" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Panorama</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="panorama" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nama Jalan</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="NJ" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td colspan="3">
                                        <br>
                                        <p><b>Nilai Strategis :</b></p>
                                        <asp:CheckBox ID="HadapAtrium" runat="server" Text="Hadap Atrium/Void"></asp:CheckBox>
                                        <br>
                                        <asp:CheckBox ID="HadapEntrance" runat="server" Text="Hadap Entrance"></asp:CheckBox>
                                        <br>
                                        <asp:CheckBox ID="HadapEskalator" runat="server" Text="Hadap Eskalator"></asp:CheckBox>
                                        <br>
                                        <asp:CheckBox ID="HadapLift" runat="server" Text="Hadap Lift"></asp:CheckBox>
                                        <br>
                                        <asp:CheckBox ID="HadapParkir" runat="server" Text="Hadap Parkir"></asp:CheckBox>
                                        <br>
                                        <asp:CheckBox ID="HadapAxis" runat="server" Text="Hadap Axis"></asp:CheckBox>
                                        <br>
                                        <asp:CheckBox ID="Hook" runat="server" Text="Hook"></asp:CheckBox>
                                    </td>
                                </tr>
                                <tr style="display:none">
                                    <td>Tambahan Harga Gimmick</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="hargagimmick" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="hargagimmickc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display:none">
                                    <td>Tambahan Harga Lain - Lain</td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="hargalainlain" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="hargalainlainc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table style="height: 50px;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                        </td>
                        <td>
                            <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel" style="width: 75px">
                        </td>
                        <td>
                            <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-check"></i> Apply </asp:LinkButton>
                        </td>
                        <td style="padding-left: 10px">
                            <p class="feed">
                                <asp:Label ID="feed" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>

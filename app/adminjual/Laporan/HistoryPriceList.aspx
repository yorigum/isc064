<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.Laporan.HistoryPriceList" CodeFile="HistoryPriceList.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Price List</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Price List">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" cellspacing="3" width="100%" runat="server">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">History Price List
                    </h1>
                    <table cellspacing="0" cellpadding="0">
                        <tr>
                            <td>&nbsp</td>
                        </tr>
                        <tr>
                            <td style="width: 100px"><p class="pparam"><b>Project :</b>
                                <asp:DropDownList ID="project" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:DropDownList></p>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <p class="pparam">
                                    <b>Periode :</b>
                                    <br>
                                    <asp:ListBox ID="periode" runat="server" Rows="10" CssClass="ddl" Width="200">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:ListBox>
                                </p>
                                <p class="pparam" style="display: none">
                                    <b>Lokasi :</b>
                                    <br>
                                    <asp:ListBox ID="lokasi" runat="server" Rows="10" CssClass="ddl" Width="200" Visible="false">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:ListBox>
                                </p>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <table>
                                    <tr>
                                        <td colspan="5">
                                            <p class="pparam">
                                                <b>Status</b><b style="padding-left: 15px">:</b>
                                                <asp:RadioButton ID="statusS" runat="server" CssClass="igroup-radio" Text="SEMUA" GroupName="status"></asp:RadioButton>
                                                <asp:RadioButton ID="statusA" runat="server" CssClass="igroup-radio" Text="AKTIF" GroupName="status" Checked="True"></asp:RadioButton>
                                                <asp:RadioButton ID="statusB" runat="server" CssClass="igroup-radio" Text="SOLD" GroupName="status"></asp:RadioButton>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <p class="pparam">
                                                <asp:CheckBox ID="jenisCheck" runat="server" Text="<b>Jenis :</b>" Checked="True" AutoPostBack="True" OnCheckedChanged="jenisCheck_CheckedChanged"></asp:CheckBox>
                                                <asp:Label ID="jenisc" runat="server" CssClass="err"></asp:Label>
                                                <asp:CheckBoxList ID="jenis" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" Style="margin-top: 19px;"></asp:CheckBoxList>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td style="width: 100px"><b>Perusahaan</b></td>
                                        <td><b>:</b></td>
                                        <td>
                                            <asp:DropDownList ID="pers" runat="server" Width="150px">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <br>
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" runat="server" CssClass="btn btn-blue" AccessKey="s" OnClick="scr_Click">
											<i class="fa fa-search"></i> Screen Preview
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="xls" runat="server" CssClass="btn btn-green" AccessKey="e" OnClick="xls_Click">
											Download Excel
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="pdf" runat="server" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click">
											Download PDF
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. Stock</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. Unit</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>


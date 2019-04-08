<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Laporan.AgingPiutang" CodeFile="AgingPiutang.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Aging Tagihan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Aging Piutang">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Aging Piutang
                    </h1>
                    <div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <asp:Label ID="Label2" runat="server"><b>Perusahaan</b><b style="padding-left:14px;font-size:12px">:</b></asp:Label>
                            </div>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:DropDownList ID="pers" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="pers_SelectedIndexChanged">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <asp:Label ID="Label1" runat="server"><b>Project</b><b style="padding-left:51px;font-size:12px">:</b></asp:Label>
                            </div>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Lokasi</p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <b style="padding-right:10px;vertical-align:top">:</b>
                                                <asp:ListBox ID="lokasi" runat="server" Width="200" CssClass="ddl" Rows="10">
                                                    <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">As Of</p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <b style="padding-right:10px;vertical-align:top">:</b>
                                                <asp:TextBox ID="tgl" runat="server" type="text" CssClass="txt_center"></asp:TextBox>
                                                <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                                <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Sales</p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <b style="padding-right:10px;vertical-align:top">:</b>
                                                <asp:DropDownList ID="ddlAgent" runat="server" CssClass="input-dropdown" Width="200">
                                                    <asp:ListItem Selected="True" Value="SEMUA"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
<%--                        <div class="form-inline col">
                            <div class="pparam">
                                <asp:CheckBox ID="cbPrincipal" runat="server" Text="<strong>Principal:</strong>" Checked="True"
                                    AutoPostBack="True" OnCheckedChanged="cbPrincipal_CheckedChanged"></asp:CheckBox>
                                <asp:Label ID="lblPrincipal" runat="server" CssClass="err"></asp:Label>
                                <asp:CheckBoxList ID="cblPrincipal" runat="server"></asp:CheckBoxList>
                            </div>
                        </div>--%>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Status KPR</p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <b style="padding-right:10px;vertical-align:top">:</b>
                                                <asp:RadioButton ID="kpa1" runat="server" Text="INCLUDE TAGIHAN KPR" GroupName="kpa" style="padding-right:30px;"></asp:RadioButton>
                                                <asp:RadioButton ID="kpa2" runat="server" Text="EXCLUDE TAGIHAN KPR" GroupName="kpa" Checked="True"></asp:RadioButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col pparam sub">
                            <div class="ins">
                            <table>
                                <tr>
                                    <td style="min-width: auto; padding-right: 10px">
                                        <asp:LinkButton ID="scr" AccessKey="s" runat="server" CssClass="btn btn-blue" OnClick="scr_Click"><i class="fa fa-search"></i> Screen Preview</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="pdf" runat="server" Text="Download PDF" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">
            <asp:Label ID="lblHeader" runat="server" Font-Size="12pt" Font-Bold="True"></asp:Label>
            <br />
            <asp:Label ID="lblSubHeader" runat="server" Font-Size="9pt" Font-Bold="True"></asp:Label>
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" Wrap="false" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" Wrap="false" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" Wrap="false" BackColor="#1E90FF" ForeColor="White">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" Wrap="false" BackColor="#1E90FF" ForeColor="White">Total</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" Wrap="false" BackColor="#1E90FF" ForeColor="White">Rincian Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="8" Wrap="false" BackColor="#1E90FF" ForeColor="White">AGING TAGIHAN</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" ColumnSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">Keterangan</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell ColumnSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">1 - 30 hari</asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">31 - 60 hari</asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">61 - 90 hari</asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">> 91 hari</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell Wrap="false" BackColor="#1E90FF" ForeColor="White">Nominal</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false" BackColor="#1E90FF" ForeColor="White">Telat (Hari)</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false" BackColor="#1E90FF" ForeColor="White">Nominal</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false" BackColor="#1E90FF" ForeColor="White">Telat (Hari)</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false" BackColor="#1E90FF" ForeColor="White">Nominal</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false" BackColor="#1E90FF" ForeColor="White">Telat (Hari)</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false" BackColor="#1E90FF" ForeColor="White">Nominal</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false" BackColor="#1E90FF" ForeColor="White">Telat (Hari)</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false" BackColor="#1E90FF" ForeColor="White" ColumnSpan="2">Info Terakhir</asp:TableHeaderCell>                
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

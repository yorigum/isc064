<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Laporan.DendaCustomer" CodeFile="DendaCustomer.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Denda Customer</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Denda Customer">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" width="100%" runat="server">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Denda Customer
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
                                    <tr>
                                        <td>
                                            <b style="padding-right: 10px; vertical-align: top">:</b>
                                            <asp:ListBox ID="lokasi" runat="server" Width="200" CssClass="ddl" Rows="10">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
<%--                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Status</p>
                                <table>
                                    <tr>                                        
                                        <td>
                                        <b style="padding-right:10px;vertical-align:top">:</b>                                        
                                        <asp:RadioButton ID="statusS" Text="SEMUA" runat="server" GroupName="status"  style="padding-right:30px"></asp:RadioButton>
                                        <asp:RadioButton ID="statusA" Text="AKTIF" runat="server" GroupName="status" style="padding-right:30px" Checked="True"></asp:RadioButton>
                                        <asp:RadioButton ID="statusB" Text="BATAL" runat="server" GroupName="status" style="padding-right:30px"></asp:RadioButton></td>
                                    </tr>
                                </table>
                            </div>
                        </div>--%>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">As Of</p>                                
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <b style="padding-right: 10px; vertical-align: top">:</b>
                                                <asp:TextBox ID="tgl" runat="server" type="text" CssClass="txt_center"></asp:TextBox>
                                                <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                                <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
<%--                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Status KPA</p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <b style="padding-right:10px;vertical-align:top">:</b>
                                                <asp:RadioButton ID="kpa1" runat="server" Text="INCLUDE TAGIHAN KPA" GroupName="kpa" Checked="True" style="padding-right:30px"></asp:RadioButton>
                                                <asp:RadioButton ID="kpa2" runat="server" Text="EXCLUDE TAGIHAN KPA" GroupName="kpa"></asp:RadioButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>--%>
                        <div class="form-inline col pparam sub">
                            <div class="ins">
                            <table>
                                <tr>
                                    <td style="min-width: auto; padding-right: 10px">
                                        <asp:LinkButton ID="scr" AccessKey="s" runat="server" CssClass="btn btn-blue" OnClick="scr_Click"><i class="fa fa-search"></i> Screen Preview</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
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
        </div>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tagihan</asp:TableHeaderCell>                
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Telat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Denda</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Benefit</asp:TableHeaderCell>                
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Realisasi Benefit</asp:TableHeaderCell>                  
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Realisasi Denda</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Putih Denda</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Saldo Denda</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Laporan.MasterTagihan" CodeFile="MasterTagihan.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Master Tagihan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Tagihan">
</head>
<body style="padding: 10px;" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Master Tagihan
                    </h1>
                    <div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Status</p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td><b style="padding-right: 10px">:</b>
                                                <asp:RadioButton ID="statusS" Text="SEMUA" runat="server" GroupName="status" Style="padding-right: 20px"></asp:RadioButton>
                                                <asp:RadioButton ID="statusA" Text="AKTIF" runat="server" GroupName="status" Style="padding-right: 20px" Checked="True"></asp:RadioButton>
                                                <asp:RadioButton ID="statusB" Text="INAKTIF" runat="server" GroupName="status" Style="padding-right: 20px"></asp:RadioButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">View By</p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td><b style="padding-right: 10px">:</b>
                                                <asp:RadioButton ID="tglkontrak" runat="server" Text="Tanggal Kontrak" Font-Size="10" GroupName="tgl" Checked="True"></asp:RadioButton>
                                            </td>
                                            <td></td>
                                            <td rowspan="2" style="vertical-align: middle">
                                                <p style="float: left; margin-top: 8px; margin-right: 10px; font-size: 13px; vertical-align: auto">Dari</p>
                                                <div class="item">
                                                    <asp:TextBox ID="dari" runat="server" type="text" CssClass="txt_center"></asp:TextBox>
                                                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                                </div>
                                                <p style="float: left; margin-top: 8px; margin-left: 10px; margin-right: 10px; font-size: 13px;">Sampai</p>
                                                <div class="item">
                                                    <asp:TextBox ID="sampai" runat="server" type="text" CssClass="txt_center"></asp:TextBox>
                                                    <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 17px">
                                                <asp:RadioButton ID="tgljt" runat="server" Text="Tanggal Jatuh Tempo" Font-Size="10" GroupName="tgl"></asp:RadioButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 17px">
                                                <br />
                                                <asp:RadioButton ID="includekpa" runat="server" Text="Include Tagihan KPR" Font-Size="10" GroupName="statuskpa" Style="padding-right: 30px" Checked="True"></asp:RadioButton>
                                                <asp:RadioButton ID="excludekpa" runat="server" Text="Exclude Tagihan KPR" Font-Size="10" GroupName="statuskpa"></asp:RadioButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <asp:CheckBox ID="tipeCheck" runat="server" Text="<b>Tipe</b><b style='padding-left:40px;font-size:12px'>:</b>" Checked="True" AutoPostBack="True" OnCheckedChanged="tipeCheck_CheckedChanged"></asp:CheckBox>
                                <asp:Label ID="tipec" runat="server" CssClass="err"></asp:Label>
                            </div>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:CheckBoxList ID="tipe" runat="server" RepeatColumns="1">
                                                <asp:ListItem Selected="True" Value="BF">BF = Booking Fee</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="DP">DP = Downpayment</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="ANG">ANG = Angsuran</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="ADM">ADM = Biaya Administrasi</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <asp:Label ID="Label2" runat="server"><b>Perusahaan</b><b style="padding-left:8px;font-size:12px">:</b></asp:Label>
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
                                <asp:Label ID="Label1" runat="server"><b>Project</b><b style="padding-left:45px;font-size:12px">:</b></asp:Label>
                            </div>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:DropDownList ID="project" runat="server" Width="200">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="form-inline col pparam sub">
                            <div class="ins">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="scr" AccessKey="s" runat="server" Width="120" CssClass="btn btn-blue" OnClick="scr_Click"><i class="fa fa-search"></i> Screen Preview</asp:LinkButton>
                                        </td>
                                        <td style="padding-left: 5px">
                                            <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" Width="120" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
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
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tgl. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No.Telp</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No.Hp</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Jatuh Tempo</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Nilai Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Pelunasan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Sisa Tagihan</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.KPA.Laporan.MasterPiutangBank" CodeFile="MasterPiutangBank.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Master Piutang Bank</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Piutang Bank">
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
                    <h1 id="judul" runat="server" class="title title-line">Laporan Master Piutang Bank
                    </h1>
                    <div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 130px; font-size: 10pt; font-weight: bold">Perusahaan<b style="padding-left: 15px;">:</b></p>
                                <asp:DropDownList ID="pers" runat="server" CssClass="select-dropdown" Width="200" Style="margin-left: 0px" Font-Size="12px" OnSelectedIndexChanged="pers_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 130px; font-size: 10pt; font-weight: bold">Project<b style="padding-left: 45px;">:</b></p>
                                <asp:DropDownList ID="project" runat="server" CssClass="select-dropdown" Width="200" Style="margin-left: 0px" Font-Size="12px" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 130px; font-size: 10pt; font-weight: bold">Status <b style="padding-left: 45px;">:</b></p>
                                <asp:RadioButton ID="statusS" runat="server" Text="SEMUA" GroupName="status"
                                    Style="padding-right: 20px; font-size: 9pt"></asp:RadioButton>
                                <asp:RadioButton ID="statusA" runat="server" GroupName="status" Checked="True"
                                            Text="AKTIF" style="margin-left:50px;padding-right:20px; font-size:9pt"></asp:RadioButton>
                                <asp:RadioButton ID="statusB" runat="server"
                                                GroupName="status" Text="INAKTIF" style="margin-left:50px;padding-right:20px; font-size:9pt"></asp:RadioButton>
                                </div>
                            </div>
                        <%--<div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Status :</p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="statusS" Text="SEMUA" runat="server" GroupName="status"></asp:RadioButton>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="statusA" Text="AKTIF" runat="server" GroupName="status" Checked="True"></asp:RadioButton>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="statusB" Text="INAKTIF" runat="server" GroupName="status"></asp:RadioButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>--%>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 130px; font-size:10pt;font-weight:bold">View By<b style="padding-left:39px;">:</b></p>
                                <asp:RadioButton ID="tglkontrak" runat="server" Text="Tanggal Kontrak" Font-Bold="True"
                                    Font-Size="9" GroupName="tgl" Checked="True" style="padding-right:44px"></asp:RadioButton>
                                <asp:RadioButton ID="tgljt" runat="server" Text="Tanggal Jatuh Tempo" Font-Bold="True"
                                    Font-Size="9" GroupName="tgl" style="padding-right:20px"></asp:RadioButton>                               
                            </div>
                        </div>
                        <div class="form-inline col">
                            <table>
                                <tr>
                                    <td style="float: none; width: 30px;">
                                        <p class="lbl" style=""></p>
                                    </td>
                                    <td style="float: none; min-width: 0px;">
                                        <p class="lbl" style="margin-left:-10px;font-size:12px">Dari</p>
                                    </td>
                                    <td style="float: none; min-width: 0px;">
                                        <asp:TextBox ID="dari" runat="server" type="text" style="margin-left:-70px"></asp:TextBox>
                                        <label for="dari" class="btn-a btn-cal" style="height:100%"><i class="fa fa-calendar"></i></label>
                                        <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                    <td style="float: none; min-width: 0px;">
                                        <p class="lbl" style="margin-left:10px;font-size:12px">Sampai</p>
                                    </td>
                                    <td style="float: none; min-width: 0px;">
                                        <asp:TextBox ID="sampai" runat="server" type="text" style="margin-left:-50px"></asp:TextBox>
                                        <label for="sampai" class="btn-a btn-cal" style="height:100%"><i class="fa fa-calendar"></i></label>
                                        <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <%--<div class="form-inline col">
                            <div class="pparam">
                                <asp:RadioButton ID="tglkontrak" runat="server" Text="Tanggal Kontrak" Font-Bold="True" Font-Size="10" GroupName="tgl" Checked="True"></asp:RadioButton>
                                <br>
                                <asp:RadioButton ID="tgljt" runat="server" Text="Tanggal Jatuh Tempo" Font-Bold="True" Font-Size="10" GroupName="tgl"></asp:RadioButton>
                                :
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p style="float: left; margin-right: 10px; font-size: 14px;">Dari</p>
                                <div class="item">
                                    <asp:TextBox ID="dari" runat="server" type="text" CssClass="txt_center"></asp:TextBox>
                                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                </div>
                                <p style="float: left; margin-left: 10px; margin-right: 10px; font-size: 14px;">Sampai</p>
                                <div class="item">
                                    <asp:TextBox ID="sampai" runat="server" type="text" CssClass="txt_center"></asp:TextBox>
                                    <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                                </div>
                            </div>
                        </div>--%>
                        <div class="form-inline col" style="display:none">
                            <div class="pparam">
                                <asp:RadioButton ID="includekpa" runat="server" Text="Include Tagihan KPR" Font-Bold="True" Font-Size="10" GroupName="statuskpa" Checked="True"></asp:RadioButton>
                                <asp:RadioButton ID="excludekpa" runat="server" Text="Exclude Tagihan KPR" Font-Bold="True" Font-Size="10" GroupName="statuskpa"></asp:RadioButton>
                                :
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="form-inline col">
                                <div class="pparam">
                                    <asp:CheckBox ID="tipeCheck" runat="server" Text="<b>Tipe :</b>" Checked="True" AutoPostBack="True" OnCheckedChanged="tipeCheck_CheckedChanged"></asp:CheckBox>
                                    <asp:Label ID="tipec" runat="server" CssClass="err"></asp:Label>
                                </div>
                                <asp:CheckBoxList ID="tipe" runat="server" RepeatColumns="1">
                                </asp:CheckBoxList>
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
                <%--<asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">No.Telp</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false">No.Hp</asp:TableHeaderCell>--%>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Jatuh Tempo</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Nilai Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Pengajuan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Pencairan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Sisa Tagihan</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

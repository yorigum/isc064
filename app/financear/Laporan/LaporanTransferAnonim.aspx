<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.Laporan.LaporanTransferAnonim" CodeFile="LaporanTransferAnonim.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Transfer Anonim</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Transfer Anonim">
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
                    <h1 id="judul" runat="server" class="title title-line">Laporan Transfer Anonim
                    </h1>
                    <div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Status</p>
                                <div class="item">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="statusA" runat="server" Text="SEMUA" Checked="True" GroupName="status"></asp:RadioButton>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="statusB" runat="server" Text="BARU" GroupName="status"></asp:RadioButton>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="statusID" runat="server" Text="IDENTIFIKASI" GroupName="status"></asp:RadioButton>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="statusS" runat="server" Text="SOLVE" GroupName="status"></asp:RadioButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                    <asp:RadioButton ID="tglinput" runat="server" Text="Tanggal Anonim" Font-Bold="True" Font-Size="14px"
                                        GroupName="tgl" Checked="True"></asp:RadioButton>
                                <br /><br />
                                <p class="lbl" style="font-size:12px;margin-top:10px">Dari</p>
                                <div class="item">
                                    <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                                        <asp:TextBox ID="dari" runat="server" type="text" CssClass="form-control" Style="width: 55%; height: 20px;font-size:12px"></asp:TextBox>
                                        <span class="input-group-btn" style="height: 34px; display: block">
                                            <label for="dari" class="btn-a btn-cal"><i class="fa fa-calendar"></i></label>
                                        </span>
                                    </div>
                                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                </div>
                                <p class="lbl" style="font-size:12px;margin-top:10px">Sampai</p>
                                <div class="item">
                                    <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                                        <asp:TextBox ID="sampai" runat="server" type="text" CssClass="form-control" Style="width: 55%; height: 20px;font-size:12px"></asp:TextBox>
                                        <span class="input-group-btn" style="height: 34px; display: block">
                                            <label for="sampai" class="btn-a btn-cal"><i class="fa fa-calendar"></i></label>
                                        </span>
                                    </div>
                                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div>
                        <p class="pparam">
                            <b>Perusahaan :</b>
                            <br />
                            <asp:DropDownList ID="pers" runat="server" Width="175" AutoPostBack="true" OnSelectedIndexChanged="pers_SelectedIndexChanged">
                                 <asp:ListItem>SEMUA</asp:ListItem>
                            </asp:DropDownList>
                        </p>
                        <p class="pparam">
                            <b>Project :</b>
                            <br />
                            <asp:DropDownList ID="project" runat="server" Width="175" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                  <asp:ListItem>SEMUA</asp:ListItem>
                            </asp:DropDownList>
                        </p>
                        <p class="pparam">
                            <b>Rekening :</b>
                            <br />
                            <asp:ListBox ID="rek" runat="server" CssClass="ddl" Width="300" Rows="12">
                                <asp:ListItem>SEMUA</asp:ListItem>
                            </asp:ListBox>
                        </p>
                    </div>
                    <br>
                    <div class="form-inline col pparam sub">
                        <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" AccessKey="s" runat="server" Text="Screen Preview" CssClass="btn btn-blue" OnClick="scr_Click"><i class="fa fa-search"></i> Screen Preview</asp:LinkButton>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                    <asp:Button ID="pdf" runat="server" Text="Download PDF" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
        </asp:Table>
    </form>
</body>
</html>

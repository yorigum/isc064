<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.KOMISI.Laporan.MasterReward" CodeFile="MasterReward.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Master Reward</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Reward">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Master Reward
                    </h1>

                    <table>
                        <tr>
                            <td><b>Project :</b></td>
                        </tr>
                        <tr>                            
                            <td>
                                <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Perusahaan :</b></td>
                        </tr>
                        <tr>                            
                            <td>
                                <asp:DropDownList ID="pers" runat="server">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <p style="display:none">
                        <b>Skema :</b>
                        <br>
                        <asp:ListBox ID="skema" runat="server" Width="200" CssClass="ddl" Rows="10">
                            <asp:ListItem>SEMUA</asp:ListItem>
                        </asp:ListBox>
                    </p>
                    <br />
                    
                    <%--<p class="pparam">
                        <b>Status Kontrak :</b>
                        <asp:RadioButton ID="statusS" runat="server" GroupName="status" Font-Size="10" Text="SEMUA" Style="padding-right: 20px"></asp:RadioButton>
                        <asp:RadioButton ID="statusA" runat="server" GroupName="status" Font-Size="10" Text="AKTIF" Style="padding-right: 20px" Checked="True"></asp:RadioButton>
                        <asp:RadioButton ID="statusB" runat="server" GroupName="status" Font-Size="10" Text="BATAL" Style="padding-right: 20px"></asp:RadioButton>
                    </p>
                    <p class="pparam">
                        <asp:RadioButton ID="tglkontrak" runat="server" Text="Tanggal Kontrak" Font-Bold="True" Font-Size="10"
                            GroupName="tgl" Checked="true"></asp:RadioButton>
                        <br />
                        <br />
                    </p>
                    <table style="margin-top: 10px">
                        <tr>
                            <td>dari</td>
                            <td>
                                <asp:TextBox ID="dari" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                                <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                            </td>
                            <td rowspan="2">&nbsp;&nbsp;</td>
                            <td>sampai</td>
                            <td>
                                <asp:TextBox ID="sampai" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                                <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label></td>
                            <td colspan="3">
                                <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label></td>
                        </tr>
                    </table>--%>

                    <br>
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" AccessKey="s" runat="server" CssClass="btn btn-blue" OnClick="scr_Click">
											<i class="fa fa-search"></i> Screen Preview
                                    </asp:LinkButton>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                    <asp:Button ID="pdf" runat="server" Text="Download PDF" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click"></asp:Button>

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
            <asp:TableRow VerticalAlign="middle">
            </asp:TableRow>
        </asp:Table>

        <script type="text/javascript">
            function popJadwalKomisi(NoKontrak) {
                openModal('../KontrakEdit.aspx?NoKontrak=' + NoKontrak, '800', '600');
            }
        </script>
    </form>
</body>
</html>

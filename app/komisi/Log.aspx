<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.KOMISI.Log" CodeFile="Log.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Log File</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Log File">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Log File</h1>
        <br>
        <table style="width: 80%; border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid; border-bottom: #dcdcdc 1px solid"
            cellspacing="5">
            <tr>
                <td><b>User</b></td>
                <td>
                    <asp:DropDownList ID="user" runat="server" Width="200">
                        <asp:ListItem Value="">Semua</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><b>Dari</b></td>
                <td style="width: 220px;">
                    <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                        <asp:TextBox ID="dari" runat="server" type="text" CssClass="form-control" Style="width: 57%; height: 20px"></asp:TextBox>
                        <span class="input-group-btn" style="height: 34px; display: block">
                            <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        </span>
                    </div>
                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                </td>
                <td><b>Sampai</b></td>
                <td>
                    <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                        <asp:TextBox ID="sampai" runat="server" type="text" CssClass="form-control" Style="width: 57%; height: 20px"></asp:TextBox>
                        <span class="input-group-btn" style="height: 34px; display: block">
                            <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        </span>
                    </div>
                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Keyword</b></td>
                <td colspan="6">
                    <asp:TextBox ID="keyword" runat="server" Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><b>Project</b></td>
                <td>
                    <asp:DropDownList ID="project" runat="server" Width="200">
                        <asp:ListItem Value="">Semua</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="8">
                    <asp:RadioButtonList ID="tb" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" Font-Bold="True" CssClass="radio" CellSpacing="5">
                        <asp:ListItem Value="REF_SKOM_LOG">SKEMA KOMISI</asp:ListItem>
                        <asp:ListItem Value="REF_SKOM_TERM_LOG">TERMIN KOMISI</asp:ListItem>
                        <asp:ListItem Value="REF_SKOM_CF_LOG">SKEMA CLOSING FEE</asp:ListItem>
                        <asp:ListItem Value="REF_SKOM_REWARD_LOG">SKEMA REWARD</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISI_LOG">KOMISI</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISIP_LOG">PENGAJUAN KOMISI</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISIR_LOG">REALISASI KOMISI</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISI_CF_LOG">CLOSING FEE</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISI_CFP_LOG">PENGAJUAN CLOSING FEE</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISI_CFR_LOG">REALISASI CLOSING FEE</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISI_REWARD_LOG">REWARD</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISI_REWARD_P_LOG">PENGAJUAN REWARD</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISI_REWARD_R_LOG">REALISASI REWARD</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:DropDownList ID="href" runat="server" Visible="False">
                        <asp:ListItem Value="REF_SKOM_LOG">SkemaKomisiEdit.aspx?NoSkema='%pk%'</asp:ListItem>
                        <asp:ListItem Value="REF_SKOM_TERM_LOG">TerminEdit.aspx?NoTermin='%pk%'</asp:ListItem>
                        <asp:ListItem Value="REF_SKOM_CF_LOG">SkemaCFEdit.aspx?NoSkema='%pk%'</asp:ListItem>
                        <asp:ListItem Value="REF_SKOM_REWARD_LOG">SkemaRewardEdit.aspx?NoSkema='%pk%'</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISI_LOG">KomisiEdit.aspx?NoKomisi='%pk%'</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISIP_LOG">KomisiPEdit.aspx?NoKomisiP='%pk%'</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISIR_LOG">KomisiREdit.aspx?NoKomisiR='%pk%'</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISI_CF_LOG">CFEdit.aspx?NoCF='%pk%'</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISI_CFP_LOG">CFPEdit.aspx?NoCFP='%pk%'</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISI_CFR_LOG">CFREdit.aspx?NoCFR='%pk%'</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISI_REWARD_LOG">RewardEdit.aspx?NoKReward='%pk%'</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISI_REWARD_P_LOG">RewardPEdit.aspx?NoRP='%pk%'</asp:ListItem>
                        <asp:ListItem Value="MS_KOMISI_REWARD_R_LOG">RewardREdit.aspx?NoRR='%pk%'</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="8">
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br>
        <asp:GridView ID="tb1" runat="server" SkinID="pager" OnPageIndexChanging="tb1_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="Tgl" DataField="Tgl" />
                <asp:BoundField HeaderText="Jam" DataField="Jam" />
                <asp:BoundField HeaderText="User" DataField="UserID" />
                <asp:BoundField HeaderText="" DataField="Nama" />
                <asp:BoundField HeaderText="Aktivitas" DataField="Aktivitas" />
                <asp:BoundField HeaderText="Referensi" DataField="Ref" />
                <asp:BoundField HeaderText="Approval" DataField="Approve" />
            </Columns>
        </asp:GridView>
        <asp:Table ID="rpt" runat="server" CellSpacing="1" Width="80%" CssClass="tb blue-skin" style="display:none">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell Width="65">Tgl</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="45">Jam</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="200" ColumnSpan="2">User</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="50">Aktivitas</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="120">Referensi</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="120">Approval</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>

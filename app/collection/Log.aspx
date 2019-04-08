<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Log" CodeFile="Log.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Log File</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Log File">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Log File</h1>
        <br />
        <asp:ScriptManager runat="server" ID="script" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="update" UpdateMode="Conditional">
            <ContentTemplate>
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td>User</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="user" runat="server" CssClass="ddl form-control" Width="180">
                        <asp:ListItem Value="">Semua</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Dari</td>
                <td>
                    <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                </td>
                <td>Sampai</td>
                <td>
                    <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Excel" Width="80px" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>Keyword</td>
                <td>:</td>
                <td colspan="15">
                    <asp:TextBox ID="keyword" runat="server" CssClass="txt" Width="250"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Project</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="project" runat="server" CssClass="ddl form-control" Width="180">
                        <asp:ListItem Value="">Semua</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Aktivitas</td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="aktivitas" runat="server" CssClass="ddl form-control" Width="180">
                        <asp:ListItem Value="">Semua</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:RadioButtonList ID="tb" runat="server" CssClass="radio" RepeatDirection="Horizontal" RepeatColumns="5" CellPadding="10" Font-Bold="True"
                         OnSelectedIndexChanged="tb_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="MS_PJT_LOG">PEMBERITAHUAN JATUH TEMPO</asp:ListItem>
                        <asp:ListItem Value="MS_TUNGGAKAN_LOG">SURAT TUNGGAKAN</asp:ListItem>
                        <asp:ListItem Value="ISC064_MARKETINGJUAL..MS_FOLLOWUP_LOG">FOLLOW UP</asp:ListItem>
                        <asp:ListItem Value="ISC064_MARKETINGJUAL..MS_REALISASIDENDA_LOG">REALISASI DENDA</asp:ListItem>
                        <asp:ListItem Value="ISC064_MARKETINGJUAL..MS_PUTIHDENDA_LOG">PEMUTIHAN DENDA</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:DropDownList ID="href" runat="server" Visible="False">
                        <asp:ListItem Value="MS_PJT_LOG">PJTEdit.aspx?NoPJT='%pk%'</asp:ListItem>
                        <asp:ListItem Value="MS_TUNGGAKAN_LOG">TunggakanEdit.aspx?NoTunggakan='%pk%'</asp:ListItem>
                        <asp:ListItem Value="ISC064_MARKETINGJUAL..MS_FOLLOWUP_LOG">FollowUpEdit.aspx?NoFU='%pk%'</asp:ListItem>
                        <asp:ListItem Value="ISC064_MARKETINGJUAL..MS_REALISASIDENDA_LOG">RealisasiDenda2.aspx?NoKontrak='%pk%'</asp:ListItem>
                        <asp:ListItem Value="ISC064_MARKETINGJUAL..MS_PUTIHDENDA_LOG">PemutihanDenda.aspx?NoKontrak='%pk%'</asp:ListItem>
                    </asp:DropDownList>
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
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" Style="width: 80%; display: none" CellSpacing="1">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell>Tgl</asp:TableHeaderCell>
                <asp:TableHeaderCell>Jam</asp:TableHeaderCell>
                <asp:TableHeaderCell ColumnSpan="2">User</asp:TableHeaderCell>
                <asp:TableHeaderCell>Aktivitas</asp:TableHeaderCell>
                <asp:TableHeaderCell>Referensi</asp:TableHeaderCell>
                <asp:TableHeaderCell>Approval</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="xls" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>

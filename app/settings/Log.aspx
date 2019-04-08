<%@ Page language="c#" Inherits="ISC064.SETTINGS.Log" CodeFile="Log.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
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
                    <asp:TextBox ID="dari" runat="server" CssClass="txt_center igroup" Width="85"></asp:TextBox>
                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                </td>
                <td>Sampai</td>
                <td>
                    <asp:TextBox ID="sampai" runat="server" CssClass="txt_center igroup" Width="85"></asp:TextBox>
                    <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Excel" CssClass="btn btn-green" Width="75px" OnClick="xls_Click"></asp:Button></td>
            </tr>
            <tr>
                <td>Keyword</td>
                <td>:</td>
                <td colspan="6">
                    <asp:TextBox ID="keyword" runat="server" CssClass="ddl form-control" Width="250"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Project</td>
                <td>:</td>
                <td colspan="6">
                    <asp:DropDownList ID="project" runat="server" CssClass="ddl form-control" Width="180">
                        <asp:ListItem Value="">Semua</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Aktivitas</td>
                <td>:</td>
                <td colspan="6">
                    <asp:DropDownList ID="aktivitas" runat="server" CssClass="ddl form-control" Width="180">
                        <asp:ListItem Value="">Semua</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:RadioButtonList ID="tb" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" Font-Bold="True" CssClass="radio" OnSelectedIndexChanged="tb_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem class="igroup-radio" Value="HTMLEDITOR_LOG">HTMLEDITOR</asp:ListItem>                        
                        <asp:ListItem class="igroup-radio" Value="ISC064_MARKETINGJUAL..REF_LOKASI_LOG">LOKASI</asp:ListItem>
                        <asp:ListItem class="igroup-radio" Value="ISC064_MARKETINGJUAL..REF_JENIS_LOG">JENIS</asp:ListItem>                        
                        <asp:ListItem class="igroup-radio" Value="ISC064_MARKETINGJUAL..REF_JENISPROPERTI_LOG">JENIS PROPERTI</asp:ListItem>
                        <asp:ListItem class="igroup-radio" Value="ISC064_MARKETINGJUAL..REF_BERKAS_PPJB_LOG">BERKAS PPJB</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:DropDownList ID="href" runat="server" Visible="False">
                        <asp:ListItem Value="HTMLEDITOR_LOG">HtmlEditorEdit.aspx?id='%pk%'</asp:ListItem>
                        <asp:ListItem Value="ISC064_MARKETINGJUAL..REF_LOKASI_LOG">LokasiEdit.aspx?id='%pk%'</asp:ListItem>
                        <asp:ListItem Value="ISC064_MARKETINGJUAL..REF_JENIS_LOG">JenisEdit.aspx?UserID='%pk%'</asp:ListItem>
                        <asp:ListItem Value="ISC064_MARKETINGJUAL..REF_JENISPROPERTI_LOG">JenisPropertiEdit.aspx?Kode='%pk%'</asp:ListItem>
                        <asp:ListItem Value="ISC064_MARKETINGJUAL..REF_BERKAS_PPJB_LOG">BerkasPPJBEdit.aspx?Kode='%pk%'</asp:ListItem>
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
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1" style="display:none">
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

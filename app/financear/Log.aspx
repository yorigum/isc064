<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.FINANCEAR.Log" CodeFile="Log.aspx.cs" %>
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
                <td colspan="6">
                    <asp:DropDownList runat="server" ID="project">
                        <asp:ListItem>Semua</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="8">
                    <asp:RadioButtonList ID="tb" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" Font-Bold="True">
                        <asp:ListItem class="radio" Value="MS_TTS_LOG">TANDA TERIMA SEMENTARA</asp:ListItem>
                        <asp:ListItem class="radio" Value="MS_ANONIM_LOG">TRANSFER ANONIM</asp:ListItem>
                        <asp:ListItem class="radio" Value="REF_ACC_LOG">ACCOUNT</asp:ListItem>
                        <asp:ListItem class="radio" Value="MS_KASMASUK_LOG">KAS MASUK</asp:ListItem>
                        <asp:ListItem class="radio" Value="MS_KASKELUAR_LOG">KAS KELUAR</asp:ListItem>
                        <asp:ListItem class="radio" Value="MS_CASHBACK_LOG">CASHBACK</asp:ListItem>
                        <asp:ListItem class="radio" Value="MS_MEMO_LOG">MEMO CASHBACK</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:DropDownList ID="href" runat="server" Visible="False">
                        <asp:ListItem Value="MS_TTS_LOG">TTSEdit.aspx?NoTTS='%pk%'</asp:ListItem>
                        <asp:ListItem Value="MS_ANONIM_LOG">TransferAnonimEdit.aspx?NoAnonim='%pk%'</asp:ListItem>
                        <asp:ListItem Value="REF_ACC_LOG">AccEdit.aspx?Acc='%pk%'</asp:ListItem>
                        <asp:ListItem Value="MS_KASMASUK_LOG">KasMasukEdit.aspx?NoVoucher='%pk%'</asp:ListItem>
                        <asp:ListItem Value="MS_KASKELUAR_LOG">KasKeluarEdit.aspx?NoVoucher='%pk%'</asp:ListItem>
                        <asp:ListItem Value="MS_CASHBACK_LOG">CBEdit.aspx?Nocb='%pk%'</asp:ListItem>
                        <asp:ListItem Value="MS_MEMO_LOG">MemoCashback.aspx?NoVoucher='%pk%'</asp:ListItem>
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

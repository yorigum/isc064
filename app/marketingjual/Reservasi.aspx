<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Reservasi" CodeFile="Reservasi.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Master Reservasi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reservasi">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Master Reservasi</h1>
        <br>
        <table style="border: 1px solid #DCDCDC" cellspacing="10">
            <tr>
                <td>&nbsp;</td>
                <td colspan="2">
                    <input type="button" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Daftar Reservasi' modal-url='DaftarReservasi.aspx' id="search"
                        runat="server" name="search" accesskey="s">
                </td>
            </tr>
            <tr>
                <td><b>View by</b></td>
                <td>
                    <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                        <asp:ListItem>Project :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="thnReservasi" runat="server" Width="200">
                        <asp:ListItem Value="">Periode Reservasi :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="denganttr" runat="server" Width="200">
                        <asp:ListItem Value="">Nilai Pengikatan :</asp:ListItem>
                        <asp:ListItem Value="1">Dengan Nilai</asp:ListItem>
                        <asp:ListItem Value="2">Tanpa Nilai</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:DropDownList ID="jenis" runat="server" Width="200">
                        <asp:ListItem Value="">Jenis :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="lokasi" runat="server" Width="200">
                        <asp:ListItem Value="">Lokasi :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="carabayar" runat="server" Width="200">
                        <asp:ListItem Value="">Cara Bayar :</asp:ListItem>
                        <asp:ListItem>KPR</asp:ListItem>
                        <asp:ListItem>CASH BERTAHAP</asp:ListItem>
                        <asp:ListItem>CASH KERAS</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td colspan="2">
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br>
        <div class="peach">
            Status : A = Aktif / E = Expire
        </div>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. Reservasi" DataField="No" />
                <asp:BoundField HeaderText="Status" DataField="Status" />
                <asp:BoundField HeaderText="Unit" DataField="Unit" />
                <asp:BoundField HeaderText="No. Urut" DataField="NoUrut" ItemStyle-Font-Size="15" />
                <asp:BoundField HeaderText="Tgl Reservasi" DataField="Tgl" />
                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                <asp:BoundField HeaderText="Batas Waktu" DataField="BatasWaktu" />                
                <asp:BoundField HeaderText="Project" DataField="Project" /> 
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
            function call(nomor) {
                popEditReservasi(nomor);
            }
        </script>
    </form>
</body>
</html>

<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.TTR" CodeFile="TTR.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Tanda Terima Reservasi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tanda Terima Reservasi">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Tanda Terima Reservasi</h1>
        <br>
        <table style="border: 1px solid #DCDCDC" cellspacing="10">
            <tr>
                <td><b>Search by</b></td>
                <td>
                    <input type="text" style="width: 200px" /></td>
                <td>
                    <input type="button" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Daftar TTR' modal-url='DaftarTTR.aspx' id="search" runat="server"
                        name="search" accesskey="s">
                </td>
            </tr>
            <tr>
                <td><b>View by</b></td>
                <td>
                    <asp:DropDownList ID="user" runat="server" Width="200">
                        <asp:ListItem Value="">Kasir :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="carabayar" runat="server" Width="200">
                        <asp:ListItem Value="">Cara Bayar :</asp:ListItem>
                        <asp:ListItem Value="TN">TN = Tunai</asp:ListItem>
                        <asp:ListItem Value="KK">KK = Kartu Kredit</asp:ListItem>
                        <asp:ListItem Value="KD">KD = Kartu Debit</asp:ListItem>
                        <asp:ListItem Value="TR">TR = Transfer Bank</asp:ListItem>
                        <asp:ListItem Value="BG">BG = Cek Giro</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><b>Dari</b></td>
                <td>
                    <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="160"></asp:TextBox>
                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                </td>
                <td><b>Sampai</b>
                    <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="160"></asp:TextBox>
                    <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Status</b>
                </td>
                <td colspan="3">
                    <asp:RadioButton ID="statusA" runat="server" Text="SEMUA" class="radio" Checked="True" GroupName="status"></asp:RadioButton>
                    <asp:RadioButton ID="statusB" runat="server" Text="BARU" class="radio" GroupName="status"></asp:RadioButton>
                    <asp:RadioButton ID="statusP" runat="server" Text="POST" class="radio" GroupName="status"></asp:RadioButton>
                    <asp:RadioButton ID="statusV" runat="server" Text="VOID" class="radio" GroupName="status"></asp:RadioButton>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br>
        <div class="peach">
            <p style="padding: 3px; font: 8pt">
                Cara Bayar : TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer 
				    Bank / BG = Cek Giro
            </p>
        </div>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. TTR" DataField="TTR" />
                <asp:BoundField HeaderText="Tgl. / Kasir" DataField="Tgl" />
                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
                <asp:BoundField HeaderText="Cara Bayar" DataField="CaraBayar" />
                <asp:BoundField HeaderText="Total" DataField="Total" />                
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
            function call(nomor) {
                popEditTTR(nomor);
            }
        </script>
    </form>
</body>
</html>

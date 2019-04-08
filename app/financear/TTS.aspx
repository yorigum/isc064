<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.TTS" CodeFile="TTS.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Tanda Terima Sementara</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tanda Terima Sementara">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Kuitansi</h1>
        <br>
        <table style="border: 1px solid #DCDCDC" cellspacing="5" width="65%">
            <tr>
                <td></td>
                <td>
                    <asp:DropDownList ID="user" runat="server" Width="180">
                        <asp:ListItem Value="">Kasir :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><b>Dari</b></td>
                <td>
                    <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="100"></asp:TextBox>
                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                </td>
                <td><b>Sampai</b></td>
                <td>
                    <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="100"></asp:TextBox>
                    <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:DropDownList ID="carabayar" runat="server" Width="180">
                        <asp:ListItem Value="">Cara Bayar :</asp:ListItem>
                        <asp:ListItem Value="TN">TN = Tunai</asp:ListItem>
                        <asp:ListItem Value="KK">KK = Kartu Kredit</asp:ListItem>
                        <asp:ListItem Value="KD">KD = Kartu Debit</asp:ListItem>
                        <asp:ListItem Value="TR">TR = Transfer Bank</asp:ListItem>
                        <asp:ListItem Value="BG">BG = Cek Giro</asp:ListItem>
                        <asp:ListItem Value="UJ">UJ = Uang Jaminan</asp:ListItem>
                        <asp:ListItem Value="DN">DN = Diskon</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="4">
                    <b>Status</b>
                    <asp:RadioButton ID="statusA" runat="server" class="radio" Text="SEMUA" Checked="True" GroupName="status"></asp:RadioButton>
                    <asp:RadioButton ID="statusB" runat="server" class="radio" Text="BARU" GroupName="status"></asp:RadioButton>
                    <asp:RadioButton ID="statusP" runat="server" class="radio" Text="POST" GroupName="status"></asp:RadioButton>
                    <asp:RadioButton ID="statusV" runat="server" class="radio" Text="VOID" GroupName="status"></asp:RadioButton>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:DropDownList ID="tipe" runat="server" Width="180">
                        <asp:ListItem Value="">Tipe :</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td></td>
                <td>
                    <asp:DropDownList ID="project" runat="server" Width="180">
                        <asp:ListItem Value="SEMUA">Project :</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <input type="button" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Daftar TTS' modal-url='DaftarTTS.aspx?dd=a' id="search" runat="server"
                        name="search" accesskey="s">
                </td>
            </tr>
        </table>
        <br>
        <p class="peach" style="width: 90%;">
            <b>Cara Bayar </b>TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer 
				Bank / BG = Cek Giro / UJ = Uang Jaminan / DN = Diskon
        </p>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. TTS" DataField="TTS" />
                <asp:BoundField HeaderText="Tgl. " DataField="Tgl" />
                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
                <asp:BoundField HeaderText="Cara Bayar" DataField="CaraBayar" />
                <asp:BoundField HeaderText="Total" DataField="Total" ItemStyle-HorizontalAlign="Right" />                
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
            function call(nomor) {
                popEditTTS(nomor);
            }
        </script>
    </form>
</body>
</html>

<%@ Page Language="c#" Inherits="ISC064.financear.VAUpload2" CodeFile="VAUpload2.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Upload Transaksi Virtual Account</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Virtual Account - Upload Transaksi (Hal. 2)">
</head>
<body>
    <form class="cnt" id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1>Upload Transaksi Virtual Account</h1>
        <br />
        <p class="feed">
            <asp:Label ID="feed" runat="server" />
        </p>
        <div id="div1" runat="server">
            <table cellspacing="5" class="tb">
                <tr>
                    <th align="center">#
                    </th>
                    <th align="left" width="80">No. VA
                    </th>
                    <th align="left" width="75">Tgl. TXN
                    </th>
                    <th align="left" width="80">No. Kontrak
                    </th>
                    <th align="left" width="200">Customer
                    </th>
                    <th align="left" width="80">Unit
                    </th>
                    <th align="right" width="90">Nilai
                    </th>
                    <th align="left" width="150">Tagihan
                    </th>
                    <th>Tipe
                    </th>
                    <th width="75">Jatuh Tempo
                    </th>
                    <th align="right" width="120">Sisa Tagihan
                    </th>
                    <th align="right">Nilai Pembayaran
                    </th>
                </tr>
                <asp:PlaceHolder ID="ph" runat="server" />
            </table>
            <table style="height: 50">
                <tr>
                    <td>
                        <asp:Button ID="save" runat="server" Width="75" CssClass="btn" Text="OK" OnClick="save_Click" />
                    </td>
                    <td>
                        <input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href = 'VAUpload.aspx'"
                            type="button" value="Cancel" name="cancel" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="div2" runat="server">
            <asp:Table ID="rpt" runat="server" CssClass="tb" CellSpacing="3">
                <asp:TableRow HorizontalAlign="Left">
                    <asp:TableHeaderCell Width="80">No. VA</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="80">Tgl. TXN</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="80">No. TTS</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="200">Customer</asp:TableHeaderCell>
                    <asp:TableHeaderCell Width="90" HorizontalAlign="Right">Total</asp:TableHeaderCell>
                </asp:TableRow>
            </asp:Table>
        </div>

        <script type="text/javascript">
            function tagihan(no, nilai, foo) {
                if (foo.checked)
                    document.getElementById('lunas_' + no).value = nilai;
                else
                    document.getElementById('lunas_' + no).value = "";
            }
            function call(nomor) {
                popEditTTS(nomor);
            }
        </script>

    </form>
</body>
</html>

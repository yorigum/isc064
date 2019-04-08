<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.CBRegistrasiAlokasi" CodeFile="CBRegistrasiAlokasi.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Registrasi Refund Lebih Bayar</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Registrasi Refund Lebih Bayar">
</head>
<body class="body-padding" onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <div style="display: none">
            <asp:CheckBox ID="dariReminder" runat="server"></asp:CheckBox>
        </div>
        <div id="frm" runat="server">
            <h1 class="title title-line">Registrasi Refund Lebih Bayar</h1>
            <p>
                Halaman 2 dari 2
            </p>
            <br>
            <table cellspacing="5">
                <tr>
                    <td>No. Kontrak
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="nokontrakl" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Unit
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Customer
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Sales
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
            <br>
            <table cellspacing="5">

                <tr>
                    <td>Sisa Tagihan
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="sisa" runat="server" CssClass="txt_num" Width="100">0</asp:TextBox>
                        <asp:Label ID="sisac" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Lebih Bayar
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="lb" runat="server" CssClass="txt_num" Width="100" ReadOnly="true">0</asp:TextBox>
                        <asp:Label ID="lbc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
            </table>

            <br />
            <table class="tb blue-skin">
                <tr>
                    <th>No. Tagihan</th>
                    <th>Tagihan</th>
                    <th>Tipe</th>
                    <th>Jatuh Tempo</th>
                    <th>Sisa Tagihan</th>
                    <th>Nilai Pembayaran</th>
                    <th>Tgl. Pembayaran</th>
                    <th></th>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                <tr>
                    <td colspan="5">
                        <b>Grand Total</b>
                    </td>
                    <td>
                        <asp:TextBox ID="totalBayar" runat="server" CssClass="txt_num"></asp:TextBox>
                    </td>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="SaveAlokasi_Click">
                            <i class="fa fa-share"></i> 
                            OK
                        </asp:LinkButton>
                        &nbsp;
                    <input type="button" onclick="location.href = 'CBRegistrasi1.aspx'" class="btn btn-red" value="Cancel" style="width: 75px"
                        id="cancel2" runat="server">
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function tagihan(no, nilai, foo) {
                if (foo.checked)
                    document.getElementById(no).value = nilai;
                else
                    document.getElementById(no).value = "";

                hitunggt();
            }
            function hitunggt() {
                foogt = document.getElementById('totalBayar');
                grandtotal = 0 * 1;

                eof = false;
                i = 0 * 1;
                while (!eof) {
                    foo = document.getElementById('bayar_' + i);
                    if (!foo) {
                        eof = true;
                        break;
                    }
                    else {
                        total = cvtnum(foo.value);
                        if (!isNaN(total))
                            grandtotal = grandtotal + (total * 1);
                        i++;
                    }
                }

                finalnet = Math.round(100 * grandtotal) / 100;
                eval("foogt.value = FinalFormat('" + finalnet + "')");
            }
            function cvtnum(foo) {
                return foo.replace(/,/gi, "");
            }
            function call(nokontrak) {
                document.getElementById('nokontrak').value = nokontrak;
                document.getElementById('next').click();
            }
        </script>
    </form>
</body>
</html>

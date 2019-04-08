<%@ Reference Page="~/Customer.aspx" %>
<%@ Reference Page="~/Unit.aspx" %>
<%@ Reference Page="~/Skema.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.HoldUnitDaftar" CodeFile="HoldUnitDaftar.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Hold Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Pendaftaran Hold Unit">
    <style type="text/css">
        H3 {
            font-size: 9pt;
        }
    </style>
</head>
<body>

    <script type="text/javascript" language="javascript" src="/Js/NumberFormat.js"></script>

    <form class="cnt" id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1>Hold Unit</h1>
        <p><b><i>Halaman 1 dari 2 </i></b></p>
        <br />
        <div id="dclosing" runat="server">
            <table cellpadding="2">
                <tr>
                    <td colspan="3">
                        <h3>
                            <span style="width: 30px">1.</span> DATA PEMESAN</h3>
                    </td>
                </tr>
                <tr style="display:none">
                    <td>Sumber Data
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="sumberdata" runat="server" CssClass="ddl">
                            <asp:ListItem>WALK IN</asp:ListItem>
                            <asp:ListItem>CALL IN</asp:ListItem>
                            <asp:ListItem>CANVAS</asp:ListItem>
                            <asp:ListItem>IKLAN</asp:ListItem>
                            <asp:ListItem>BUYER GET BUYER</asp:ListItem>
                            <asp:ListItem>REFERENSI</asp:ListItem>
                            <asp:ListItem>PEMBELI LAMA</asp:ListItem>
                            <asp:ListItem>PAMERAN</asp:ListItem>
                            <asp:ListItem>LAINNYA</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Lokasi Kontrak
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="lokasikontrak" runat="server" CssClass="ddl igroup">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Tipe
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:RadioButton ID="perorangan" runat="server" Text="PERORANGAN" GroupName="tipe"
                            Checked="True"></asp:RadioButton>
                        <asp:RadioButton ID="badanhukum" runat="server" Text="BADAN HUKUM" GroupName="tipe"></asp:RadioButton>
                    </td>
                </tr>
                <tr>
                    <td>Kewarganegaraan
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:RadioButton ID="wni" runat="server" Text="WNI" GroupName="wn" Checked="True"></asp:RadioButton>
                        <asp:RadioButton ID="wna" runat="server" Text="WNA" GroupName="wn"></asp:RadioButton>
                        <asp:RadioButton ID="kori" runat="server" Text="KORPORASI INDONESIA" GroupName="wn"></asp:RadioButton>
                        <asp:RadioButton ID="kora" runat="server" Text="KORPORASI ASING" GroupName="wn"></asp:RadioButton>
                    </td>
                </tr>
                <tr>
                    <td>Nama Lengkap
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="nama" runat="server" Width="250px" MaxLength="100" CssClass="txt"></asp:TextBox><asp:Label
                            ID="namac" runat="server" CssClass="err">*</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>No. Telp
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="telp" runat="server" MaxLength="50" CssClass="txt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>No. HP
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="hp" runat="server" MaxLength="50" CssClass="txt" Height="20px"></asp:TextBox><asp:Label
                            ID="telpc" runat="server" CssClass="err">*</asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td>No. KTP/Paspor
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="noktp" runat="server" Width="200" MaxLength="50" CssClass="txt"></asp:TextBox>
                        <asp:Label ID="noktpc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Alamat (Sesuai Identitas)
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:TextBox ID="ktp1" runat="server" Width="250" MaxLength="50" CssClass="txt"></asp:TextBox>
                        <br>
                        <asp:TextBox ID="ktp2" runat="server" Width="150" MaxLength="50" CssClass="txt"></asp:TextBox>
                        <br>
                        <asp:TextBox ID="ktp3" runat="server" Width="150" MaxLength="50" CssClass="txt"></asp:TextBox>
                        <br />
                        <asp:TextBox ID="ktp4" runat="server" Width="150" MaxLength="50" CssClass="txt"></asp:TextBox>
                        <br>
                    </td>
                </tr>

                <tr>
                    <td colspan="3">
                        <br>
                        <h3>
                            <span style="width: 30px">2.</span> UNIT YANG DIPESAN</h3>
                    </td>
                </tr>
                <tr>
                    <td>Tanggal Hold
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="tglhold" runat="server" Width="100px" CssClass="txt_center" ReadOnly="False"
                            Height="20px"></asp:TextBox>&nbsp;<input class="btn" onclick="openCalendar('tglhold')"
                                type="button" value="...">
                        </nobr>
                    <asp:Label ID="tglholdc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Batas Waktu
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="tglholdexp" runat="server" Width="150" CssClass="txt_center"></asp:TextBox>
                        <button class="btn" onclick="openCalendar('tglholdexp')" type="button" value="...">
                            <i class="fa fa-calendar"></i>
                        </button>
                        <asp:Label ID="tglholdexpc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Kode Unit
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="unit" runat="server" Width="100" CssClass="txt" ReadOnly="true"></asp:TextBox>
                        <div style="display: none">
                            <asp:TextBox ID="nostock" runat="server"></asp:TextBox>
                            <asp:TextBox ID="nohold" runat="server"></asp:TextBox>
                        </div>
                        <input visible="false" class="btn" id="btnUnit" onclick="popDaftarUnit2('a')" type="button"
                            value="..." name="btnUnit" runat="server" />
                        <asp:Label ID="unitc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br>
                        <h3>
                            <span style="width: 30px">3.</span> SALES PERSON</h3>
                    </td>
                </tr>
                <tr>
                    <td>Kode Sales
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="agent" runat="server" Width="400" CssClass="ddl">
                        </asp:DropDownList>
                        <asp:Label ID="agentc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
            </table>

            <table height="50">
                <tr>
                    <td>
                        <asp:Button ID="ok" runat="server" Width="75" CssClass="btn" Text="OK" OnClick="ok_Click"></asp:Button>
                    </td>
                    <td>
                        <input class="btn btn-red" id="cancel" style="width: 75px" type="button" value="Cancel" name="cancel"
                            runat="server" onclick="javascript: history.go(-1)">
                    </td>
                </tr>
            </table>
        </div>

        <script language="javascript">
            function call(nomor, nounit) {
                document.getElementById('nostock').value = nomor;
                document.getElementById('unit').value = nounit;
            }
            function callSource(nomor, source) {
                document.getElementById('spgabungan').value = nomor;
            }
            function popdiskon(d1, d2) {
                foo1 = document.getElementById(d1);
                foo2 = document.getElementById(d2);
                openModal('SkemaDiskon.aspx?t1=' + foo1.value + '&t2=' + foo2.value + '&d1=' + d1 + '&d2=' + d2, '450', '360');
            }
            function popbunga(d1, d2) {
                foo1 = document.getElementById(d1);
                foo2 = document.getElementById(d2);
                openModal('SkemaBunga.aspx?t1=' + foo1.value + '&t2=' + foo2.value + '&d1=' + d1 + '&d2=' + d2, '450', '360');
            }
            function recaldisc(discTxt) {
                disc = discTxt.value.split("+");

                discTxt.value = "";

                for (i = 0; i < disc.length; i++) {
                    if (!isNaN(disc[i]) && disc[i] != "") {
                        if (discTxt.value != "") discTxt.value = discTxt.value + "+";
                        discTxt.value = discTxt.value + disc[i];
                    }
                }
            }
            function cvtnum(foo) {
                return foo.replace(/,/gi, "");
            }
            function recal() {

            }
        </script>

    </form>
</body>
</html>

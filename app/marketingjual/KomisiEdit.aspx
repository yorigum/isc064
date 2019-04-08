<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.KomisiEdit" CodeFile="KomisiEdit.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Edit Jadwal Komisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Komisi - Edit Jadwal Komisi">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Kembali ke halaman jadwal komisi?')) document.getElementById('cancel').click()">

    <script type="text/javascript" src="/Js/Common.js"></script>

    <script type="text/javascript" src="/Js/NumberFormat.js"></script>

    <form id="Form1" method="post" runat="server" class="cnt">
        <h1 class="title title-line">Edit Jadwal Komisi
        </h1>
        <table cellpadding="0" cellspacing="0">
            <tr valign="top">
                <td width="400">
                    <table cellspacing="5">
                        <tr>
                            <td>No. Kontrak
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label ID="nokontrak" runat="server" Font-Bold="True"></asp:Label>
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
                    <p class="feed" style="padding-left: 5">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
                <td>
                    <table cellspacing="5" style="border: 1 inset;">
                        <tr>
                            <td width="250">Skema :
                            <br>
                                <asp:Label ID="skema" runat="server" Font-Underline="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Nilai DPP:
                            <br>
                                <asp:Label ID="nilai" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Pelunasan sudah mencapai :
                            <br>
                                <asp:Label ID="persenlunas" runat="server" Font-Bold="True" Font-Size="18"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br>
        <table cellspacing="1" class="tb blue-skin">
            <tr align="left">
                <th>Termin
                </th>
                <th>Tipe
                </th>
                <th>Nama Sales
                </th>
                <th>Nilai Komisi
                </th>
                <th>Nilai Bayar
                </th>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </table>
        <table height="50">
            <tr>
                <td>
                    <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a"
                        OnClick="save_Click"><i class="fa fa-check"></i>Apply</asp:LinkButton>
                </td>
                <td>
                    <input id="cancel" type="button" onclick="" class="btn btn-red" value="Cancel" runat="server"
                        style="width: 75px">
                </td>
                <td style="padding-left: 10px; font: 8pt">
                    <asp:Label ID="noedit" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
        </table>

        <script type="text/javascript">
            function hapus(nokontrak, nourut, baris) {
                if (confirm('Apakah anda ingin menghapus komisi : ' + nokontrak + '.' + nourut + '?\nPerhatian bahwa data akan dihapus secara PERMANEN.')) {
                    location.href = 'KomisiDel.aspx?NoKontrak=' + nokontrak + '&NoUrut=' + nourut + '&Baris=' + baris;
                }
            }
        </script>

    </form>
</body>
</html>

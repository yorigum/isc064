<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClosingNUP2.aspx.cs" Inherits="ISC064.LAUNCHING.ClosingNUP2" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Batavianet Business Application :: Launching</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <%--<link href="/Media/Style.css" type="text/css" rel="stylesheet">--%>
    <meta name="ctrl" content="1">
    <meta name="sec" content="Closing NUP 2">

    <script language="javascript" src="/Js/Pop.js"></script>

</head>
<style>
    .nav, .navsub {
        border: 0px;
        background-color: #EEEEEE;
        font: 8pt Trebuchet MS;
        padding-left: 7;
        text-align: left;
        width: 190;
        height: 18px;
    }

    .nav2 {
        border: 0px;
        background-color: #EEEEEE;
        font: 14pt Trebuchet MS;
        padding-left: 7;
        text-align: left;
        width: 200;
        height: 30px;
    }

    input.search {
        background: url(Image/lisearch.gif) center center no-repeat;
        border: 0px;
        width: 25px;
    }
</style>
<body class="body-padding" onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <div style="float: left; width: 40%;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 20px">
                        <a href="Index.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_prev_c.png" style="width: 80px; height: 80px;"></a>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: right; width: 10%;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 20px">
                        <a href="/Gateway.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_gateway2.png" style="width: 80px; height: 80px;"></a>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px">
                        <a href="/SignOut.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_out.png" style="width: 80px; height: 80px;"></a>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <div id="pilih" runat="server">
            <h1>NUP</h1>
            <p>
                Halaman 2 dari 2
            </p>
            <br>
            <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid; border-bottom: #dcdcdc 1px solid"
                cellspacing="5">
                <tr>
                    <td style="font-size: medium; font-weight: bold;">Daftar NUP Customer
                    <asp:Label ID="nama" runat="server"></asp:Label>
                    </td>
                    <%--<td style="font-size: medium; font-weight: bold;">
                        <input type="button" value="Lihat Tabel Stok" onclick="javascript: popPeta()" />
                    </td>
                    <td style="font-size: medium; font-weight: bold;">
                        <input type="button" value="Lihat Floor Plan" onclick="javascript: popPeta2()" />
                    </td>--%>
                </tr>
                <tr>
                    <table cellspacing="5" class="tb">
                        <tr align="left">
                            <th>No.
                            </th>
                            <th>No. NUP
                            </th>
                            <th>Nilai NUP
                            </th>
                            <th style="display: none">No. Unit
                            </th>
                            <th>Unit
                            </th>
                            <th>Tipe Unit
                            </th>
                        </tr>
                        <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                    </table>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="save" runat="server" CssClass="btn" Text="Save" Width="75" AccessKey="a"
                            OnClick="save_Click"></asp:Button><asp:Label ID="error" runat="server" Style="color: red;"></asp:Label>
                    </td>
                </tr>
                <tr id="trSave" runat="server" style="display: none;">
                    <td>Keterangan :
                    <asp:TextBox ID="ketubah" runat="server" Width="300" CssClass="txt"></asp:TextBox>
                        <asp:Button ID="submitubah" runat="server" CssClass="btn" Text="Submit" Width="75"
                            OnClick="submitubah_Click1"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <%--<asp:DropDownList ID="ddlharga" runat="server" />--%>

        <script language="javascript" type="text/javascript">


            function popPeta() {
                openPopUp('/launching/TabelStokView2.aspx?Nama=TOWER%20A&Lokasi=A', '1600', '650')
            }
            function popPeta2() {
                openPopUp('/launching/TabelStokView2.aspx?Nama=RUKO&Lokasi=R', '1600', '650')
            }
            function popKalkulator(ctrl1, ctrl2, ctrl3, ctrl4) {
                openPopUp('/launching/KalkulatorSkema.aspx?NoStock=' + ctrl1 + '&Tipe=' + ctrl2 + '&NoNUP=' + ctrl3 + '&Tipe=' + ctrl4, '800', '650')
                //openPopUp('/launching/KalkulatorSkema.aspx?NoStock=' + ctrl1 + '&Tipe=' + ctrl2 + '&NoNUP=' + ctrl3 + '&Tipe=' + ctrl4 + '&CB=' + ctrl5, '800', '650')
            }
        </script>

    </form>
</body>
</html>

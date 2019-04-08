<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClosingNUP3.aspx.cs" Inherits="ISC064.LAUNCHING.ClosingNUP3" %>

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
    <meta name="sec" content="Closing NUP 3">
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
        <div style="float: left; width: 40%">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <%--<td style="width: 20px">
                    <img src="/Media/icon_prev_c.gif" style="width: 30px; height: 30px;">
                </td>--%>
                    <tr>
                        <td style="width: 20px">
                            <a href="Index.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                                <img src="/Media/icon_prev_c.png" style="width: 80px; height: 80px;"></a>
                        </td>
                        <%--<asp:Button ID="btnBack" style="width: 80px;" runat="server" CssClass="nav2" 
                        Text="Back" onclick="btnBack_Click">
                            </asp:Button>--%>
                        <%--<input type="button" value="Back" id="btnBack" runat="server" class="nav2" style="width: 80px;" onmouseover="over(this)"
                         onmouseout="out(this)">--%>
                    </tr>
                </tr>
            </table>
        </div>
        <div style="float: right; width: 10%;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <tr>
                        <td style="width: 20px">
                            <a href="/Gateway.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                                <img src="/Media/icon_gateway2.png" style="width: 80px; height: 80px;"></a>
                        </td>
                    </tr>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;
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
        <div id="pilih" runat="server">
            <h1>Closing NUP</h1>
            <p>
                Halaman 3 dari 3
            </p>
            <br>
            <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid; border-bottom: #dcdcdc 1px solid"
                cellspacing="5">
                <tr>
                    <td style="font-size: medium; font-weight: bold;">Daftar NUP Customer
                    <asp:Label ID="nama" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <table cellspacing="5" class="tb">
                        <tr align="left">
                            <th>No.
                            </th>
                            <th>No. NUP
                            </th>
                            <th>Nama Customer
                            </th>
                            <th>Unit
                            </th>
                            <th>Cara Bayar
                            </th>
                        </tr>
                        <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                    </table>
                </tr>
            </table>
        </div>

        <script language="javascript" type="text/javascript">
            function openSite(ctrl1, ctrl2) {
                foo1 = document.getElementById(ctrl1);
                foo2 = document.getElementById(ctrl2);

                var rl = window.showModalDialog(
                            "PopUnitPetaDetil.aspx?f=GEM CITY&ctrl=" + ctrl1, "",
                            "center:yes;dialogWidth:1024px;dialogHeight:840px;help:no;status:no;");

                if (rl != null) {
                    x = rl.indexOf(';');
                    foo1.value = rl.substring(0, x);
                    foo2.value = rl.substring(x + 1, rl.length);
                }
            }

        </script>

    </form>
</body>
</html>

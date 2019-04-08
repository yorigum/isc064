<%@ Page Language="c#" Inherits="ISC064.NUP.TabelStokView" CodeFile="TabelStokView.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>TABLE STOCK</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <meta http-equiv="Refresh" content="60">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <head>
        <title>Tower</title>
        <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
        <meta name="CODE_LANGUAGE" content="C#">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <meta http-equiv="Refresh" content="5">
        <link href="/Media/Style.css" type="text/css" rel="stylesheet">
        <style type="text/css">
            A {
                COLOR: black;
            }

                A:visited {
                    COLOR: black;
                }

                A:hover {
                    COLOR: black;
                }

            #tb {
                BORDER-COLLAPSE: collapse;
                COLOR: white;
            }

            #tb_kios {
                BORDER-COLLAPSE: collapse;
            }

            #tb_food {
                BORDER-COLLAPSE: collapse;
            }

            #tb TD {
                FONT: 8pt lucida;
                WIDTH: 23px;
                TEXT-ALIGN: center;
            }

            #tb_kios TD {
                FONT: 8pt lucida;
                WIDTH: 23px;
                TEXT-ALIGN: center;
            }

            #tb_food TD {
                FONT: 8pt lucida;
                WIDTH: 23px;
                TEXT-ALIGN: center;
            }

            #tb .h {
                COLOR: white;
                BACKGROUND-COLOR: gray;
            }

            #tb_kios .h {
                COLOR: white;
                BACKGROUND-COLOR: gray;
            }

            #tb .lt {
                COLOR: black;
            }

            #tb .ket {
                COLOR: black;
                font-size: 12pt;
                font-weight: bold;
                width: auto;
            }

            #tb .ket2 {
                COLOR: black;
                font-size: 11pt;
            }
        </style>
    </head>
    <body class="body-padding">
        <form id="Form1" method="post" runat="server">
            <div style="float: left; width: 40%;">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 20px">
                            <a href="Index.html" style="text-align: center; vertical-align: middle; font-size: x-large;">
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
            <h1>Pilih UNIT</h1>
            <br />
            <br />
            <br />
            <div align="Left">
                <asp:PlaceHolder ID="list" runat="server">
                </asp:PlaceHolder>
            </div>
        </form>
    </body>
</html>

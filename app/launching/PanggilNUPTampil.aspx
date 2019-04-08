<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.PanggilNUPTampil" CodeFile="PanggilNUPTampil.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NUP Panggil - Primary</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <%--<link href="/Media/Style.css" type="text/css" rel="stylesheet">--%>
    <meta http-equiv="Refresh" content="5">
    <meta name="ctrl" content="1">
    <meta name="sec" content="NUP - Panggil NUP">
    <style type="text/css">
        .sm TD {
            font-weight: normal;
            font-size: 8pt;
            line-height: normal;
            font-style: normal;
            font-variant: normal;
        }

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

        img {
            opacity: 0.8;
            filter: alpha(opacity=40); /* For IE8 and earlier */
        }

        .blink_me {
            animation: blinker 1s linear infinite;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }
    </style>
</head>
<body style="height: auto !important; font-size: 14px; line-height: 1.42857143; background: #fff url(../Media/perumnasbackground.jpg) no-repeat; background-size: cover;">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div style="width: 100%; padding-top: 10px; padding-left: 80px" align="center">
            <table style="margin: 0;" width="100%">
                <tr>
                    <td>
                        <%--<img src="../Media/LogoPerum.jpg" />--%>
                    </td>
                    <td style="vertical-align: top; font-size: 40px; text-align: center">DAFTAR AKTIVASI NPU</td>
                    <td>
                        <%--<img src="../Media/logo-kai.png" width="250px" height="80px" />--%>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;" colspan="3">
                        <asp:Table ID="nupberjalan" runat="server" CssClass="tb" HorizontalAlign="Center"
                            CellSpacing="25">
                        </asp:Table>
                    </td>
                </tr>

            </table>
        </div>
        <div style="padding-left: 200px">
            <table style="margin: 0; font-size: 30px;">
                <tr>
                    <td style="vertical-align: top;">NPU Pilih Unit :<br />
                        <span style="font-weight: bold; font-size: 50px; padding-left: 50px">
                            <asp:Label ID="nuppilihunit" runat="server"></asp:Label></span> NPU
                    </td>
                    <td style="width: 5px; vertical-align: top;"></td>
                    <td rowspan="2">
                        <asp:PlaceHolder runat="server" ID="list"></asp:PlaceHolder>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">NPU Closing :
                        <br />
                        <span style="font-weight: bold; font-size: 50px; padding-left: 50px">
                            <asp:Label ID="nupclosing" runat="server"></asp:Label></span> NPU
                    </td>
                    <td style="width: 5px; vertical-align: top;"></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

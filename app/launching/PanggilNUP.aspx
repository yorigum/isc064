<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.PanggilNUP" CodeFile="PanggilNUP.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NUP Pemanggilan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="NUP - Pemanggilan Nomor (Voice Call)">
</head>
<style type="text/css">
    .sm TD
    {
        font-weight: normal;
        font-size: 8pt;
        line-height: normal;
        font-style: normal;
        font-variant: normal;
    }
    .nav, .navsub
    {
        border: 0px;
        background-color: #EEEEEE;
        font: 8pt Trebuchet MS;
        padding-left: 7;
        text-align: left;
        width: 190;
        height: 18px;
    }
    .nav2
    {
        border: 0px;
        background-color: #EEEEEE;
        font: 14pt Trebuchet MS;
        padding-left: 7px;
        text-align: left;
        width: 200px;
        height: 30px;
    }
</style>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
     <div>
          <div style="float: left; width: 40%;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tbody><tr>
                    <td style="width: 20px">
                        <a href="Index.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_prev_c.png" style="width: 80px; height: 80px;" alt=""></a>
                    </td>
                </tr>
            </tbody></table>
        </div>
        <div style="float: right; width: 10%;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tbody><tr>
                    <td style="width: 20px">
                        <a href="/Gateway.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_gateway2.png" style="width: 80px; height: 80px;" alt="" /></a>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px">
                        <a href="/SignOut.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_out.png" style="width: 80px; height: 80px;" alt="" /></a>
                    </td>
                </tr>
            </tbody></table>
        </div>
         <br style="clear:both;"/>
     </div>
        <div>
            <div>
                <h1></h1>
            </div>
        </div>
        <center><table align="center">
            <tr>
                <td colspan="3">
                    <center>Nomor sekarang</center>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center" style="font-size:30px;font-weight:bold;padding:10px;"><asp:Label runat="server" ID="nomor" Text="-"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Button runat="server" ID="call" Text="Panggil" class="btn btn-blue" OnClick="call_Click"/></td>
                <td>
                    <asp:Button runat="server" ID="next" Text="Nomor Selanjutnya" class="btn btn-blue" OnClick="next_Click"/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <center><asp:Label runat="server" ID="err"></asp:Label></center>
                </td>
            </tr>
        </table>
            <table>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="datang" Text="Customer Datang" class="btn btn-green" OnClick="datang_Click" />
                    </td>
                    <td>
                        <asp:Button runat="server" ID="tidakdatang" Text="Customer Tidak Datang"  class="btn btn-red" OnClick="tidakdatang_Click" />
                    </td>
                </tr>
            </table>
        </center>
   
    </form>
</body>
</html>

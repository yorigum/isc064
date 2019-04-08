<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HitungNUP.aspx.cs" Inherits="ISC064.LAUNCHING.HitungNUP" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Batavianet Business Application :: Launching</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
<meta http-equiv="Refresh" Content="1">
<style>
	td 
{
	font-size:50pt;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>PLP Sudah Aktivasi</td>
                    <td>: </td>
                    <td>
                        <asp:Label ID="aktivasi" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>PLP Sudah Pilih Unit</td>
                    <td>: </td>
                    <td>
                        <asp:Label ID="pilih" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>PLP Sudah Closing</td>
                    <td>: </td>
                    <td>
                        <asp:Label ID="closing" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>


        </div>
    </form>
</body>
</html>

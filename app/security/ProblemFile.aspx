<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProblemFile.aspx.cs" Inherits="ISC064.SECURITY.ProblemFile" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Problem File</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Problem File">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Problem File</h1>
        <div>
            <table>
                <tr>
                    <td style="vertical-align:top">
                        Halaman
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:Label ID="halaman" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align:top">
                        Keterangan
                    </td>
                    <td style="vertical-align:top">:</td>
                    <td>
                        <asp:Label ID="keterangan" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

<%@ Page Language="c#" Inherits="ISC064.SETTINGS.TandaTangan" CodeFile="TandaTangan.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Tanda Tangan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tanda Tangan">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <h1 class="title title-line">Tanda Tangan</h1>
    <br>
    <table cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="250">
                <ul id="aktif" runat="server" class="plike">
                </ul>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoryFU.aspx.cs" Inherits="ISC064.COLLECTION.HistoryFU" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html>
<head>
    <title>History Pemberitahuan Follow Up</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="P. Jatuh Tempo - History Pemberitahuan Follow Up(Marketing)">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin membatalkan proses registrasi?')) document.getElementById('cancel').click();">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">History Follow Up </h1>
        <br />
        <table class="tb blue-skin" cellspacing="1">
		    <tr align="left" valign="bottom">
				<th>No.</th>
				<th>Tgl Follow Up</th>
				<th>Kategori</th>
				<th>Keterangan</th>
                <th>Tgl. Janji Bayar</th>
                <th>Collector</th>
			</tr>
            <asp:placeholder id="list" runat="server"></asp:placeholder>
		</table>

    </form>
</body>
</html>

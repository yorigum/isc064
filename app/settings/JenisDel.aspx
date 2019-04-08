<%@ Reference Page="~/Peta.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.SETTINGS.JenisDel" CodeFile="JenisDel.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Delete Jenis Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Jenis Unit - Delete Jenis Unit">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27) history.back(-1)">
    <form id="Form1" method="post" runat="server">
        <div id="frm" runat="server">
            <input type="text" style="display: none">
            <h1 class="title title-line">Delete Tipe Unit</h1>
            <br>
            Keterangan :
        <asp:TextBox ID="ket" runat="server" CssClass="txt" Width="400"></asp:TextBox>
            <asp:Button ID="delbtn" runat="server" CssClass="btn btn-red" Text="Delete" OnClick="delbtn_Click"></asp:Button>
        </div>
        <asp:Label ID="nodel" runat="server" Visible="false">
				<h1>
					Jenis Unit Tidak Dapat Dihapus
				</h1>
				<br>
				<div class="plike">
					<h2>Kemungkinan Terjadi Karena:</h2>
					<ul>
						
						<li>
							Jenis Unit tersebut sudah memiliki unit dan kontrak
						</li>
					</ul>
				</div>
        </asp:Label>
    </form>
</body>
</html>

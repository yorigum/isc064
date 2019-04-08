<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PemutihanDenda1.aspx.cs" Inherits="ISC064.COLLECTION.PemutihanDenda1" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<!DOCTYPE html >
<html>
	<head>
		<title>Pemutihan Denda</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Pemutihan Denda">
	</head>
	<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}"
		ms_positioning="GridLayout">
		<form class="cnt" id="Form1" method="post" runat="server">
			<uc1:head id="Head" runat="server"></uc1:head><input style="DISPLAY: none">
			<h1 class="title title-line">Pemutihan Denda</h1>
			<p>Halaman 1 dari 2</p>
			<br>
			<table style="border: solid silver 1px" cellspacing="5">
				<tr>
					<td><b>No. Kontrak :</b></td>
					<td>
                        <asp:TextBox ID="nokontrak" runat="server" CssClass="" Width="100"></asp:TextBox>
                        <button class="btn btn-orange" id="btnpop" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a&dd=1' type="button" value="..." name="btnpop" runat="server">
                            <i class="fa fa-search"></i>
                        </button>
                        <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" Text="Next..." OnClick="next_Click1"> Next
                                <i class="fa fa-arrow-right"></i>
                        </asp:LinkButton>
                    </td>
				</tr>
			</table>
			<p class="feed" style="PADDING-LEFT:5px">
				<asp:label id="feed" runat="server"></asp:label>
			</p>
			<script type="text/javascript">
			function call(nokontrak)
			{
				document.getElementById('nokontrak').value = nokontrak;
				document.getElementById('next').click();
			}
			</script>
		</form>
	</body>
</html>

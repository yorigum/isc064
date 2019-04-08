<%@ Page language="c#" Inherits="ISC064.FINANCEAR.NoVAUpload" CodeFile="NoVAUpload.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html >
<html>
	<head>
		<title>Upload No VA</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Virtual Account - Upload No VA">
		<style type="text/css">
		.sm  {font:8pt}
		</style>
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<input type="text" style="display:none"/>
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Upload No VA</h1>
			<br/>
            <asp:DropDownList runat="server" ID="project" AutoPostBack="true"></asp:DropDownList>
			<h2>Standard Pengisian</h2>
			<asp:table id="rule" runat="server" cellspacing="1" cssclass="tb blue-skin">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell>No.</asp:tableheadercell>
					<asp:tableheadercell width="150">Kolom</asp:tableheadercell>
					<asp:tableheadercell width="75">Format</asp:tableheadercell>
					<asp:tableheadercell>Panjang</asp:tableheadercell>
					<asp:tableheadercell width="350">Keterangan</asp:tableheadercell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>1.</asp:tablecell>
					<asp:tablecell>No. VA</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>2.</asp:tablecell>
					<asp:tablecell>No. Unit</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>20</asp:tablecell>
					<asp:tablecell cssclass="sm">No. Unit sesuai dengan project yang di pilih.</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>3.</asp:tablecell>
					<asp:tablecell>Bank</asp:tablecell>
					<asp:tablecell>RANGE</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
			</asp:table>
			<br />
            <a href="Template\NoVA.xls" type="button" >Download Template</a>	
			<br/>
            <br />
			<table cellspacing="5">
				<tr>
					<td><h3>File Excel</h3></td>
					<td>
                        <input id="txt" class="input-text"  type = "text" value ="" style="width:40%" />
                        <button type="button" class="btn-submit button-submit2" onclick ="javascript:document.getElementById('file').click();">Upload</button>
                        <input runat="server" name="file" id = "file" type="file" style='visibility: hidden;' onchange="ChangeText(this, 'txt');"/>
					</td>
				</tr>
			</table>
			<table style="height:50px">
				<tr>
					<td>
						<asp:LinkButton id="upload" runat="server" cssclass="btn btn-blue t-white" width="75" onclick="upload_Click">
							<i class="fa fa-share"></i> OK
						</asp:LinkButton>
					</td>
					<td style="padding-left:10px">
						<p class="feed">
							<asp:label id="feed" runat="server"></asp:label>
						</p>
					</td>
				</tr>
			</table>
			<br/>
			<h3>Gagal Upload :</h3>
			<asp:table id="gagal" runat="server"></asp:table>
		</form>
	</body>
    <script type="text/javascript">
        function ChangeText(oFileInput, sTargetID) {
            document.getElementById(sTargetID).value = oFileInput.value;            
        }
    </script> 
</html>

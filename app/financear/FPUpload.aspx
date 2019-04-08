<%@ Page language="c#" Inherits="ISC064.FINANCEAR.FPUpload" CodeFile="FPUpload.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Upload Faktur Pajak</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Faktur Pajak - Upload Faktur Pajak">
		<style type="text/css">


		.sm
        {
            font:8pt;
            font-family:'Times New Roman', Times, serif;
		}
		</style>        
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<input type="text" style="display:none"/>
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Upload Faktur Pajak</h1>
			<br/>
			<h3>Standard Pengisian</h3>
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
					<asp:tablecell>Reff. Surat Permintaan FP</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>2.</asp:tablecell>
					<asp:tablecell>Reff. Surat Pemberian FP</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>3.</asp:tablecell>
					<asp:tablecell>Tgl Diajukan</asp:tablecell>
					<asp:tablecell>DATE</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm">Tanggal Pengajuan Stock Faktur Pajak</asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>4.</asp:tablecell>
					<asp:tablecell>Lampiran SPT PPN-Masa Pajak</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>5.</asp:tablecell>
					<asp:tablecell>Total FP I</asp:tablecell>
					<asp:tablecell>ANGKA</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>6.</asp:tablecell>
					<asp:tablecell>Total FP II</asp:tablecell>
					<asp:tablecell>ANGKA</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>7.</asp:tablecell>
					<asp:tablecell>Total FP III</asp:tablecell>
					<asp:tablecell>ANGKA</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>8.</asp:tablecell>
					<asp:tablecell>Total Maksimal</asp:tablecell>
					<asp:tablecell>ANGKA</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>9.</asp:tablecell>
					<asp:tablecell>Jumlah FP Diterima</asp:tablecell>
					<asp:tablecell>ANGKA</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>10.</asp:tablecell>
					<asp:tablecell>Nomor FP</asp:tablecell>
					<asp:tablecell>TEKS</asp:tablecell>
					<asp:tablecell>50</asp:tablecell>
					<asp:tablecell cssclass="sm"></asp:tablecell>
				</asp:tablerow>
				<asp:tablerow>
					<asp:tablecell>11.</asp:tablecell>
					<asp:tablecell>Tgl Terima FP</asp:tablecell>
					<asp:tablecell>DATE</asp:tablecell>
					<asp:tablecell></asp:tablecell>
					<asp:tablecell cssclass="sm">Tanggal Penerimaan Stok Faktur Pajak</asp:tablecell>
				</asp:tablerow>
			</asp:table>
			<br />
				<a class="" href="Template\FP2.xls" type="button" >Download Template</a>			                                
			<br />
            <br />
			<table cellspacing="5">
                <tr>
					<td><h3>Project</h3></td>
                    <td>
                        <asp:DropDownList runat="server" ID="project"></asp:DropDownList>                        
                    </td>
                </tr>
                <tr><td colspan="2">&nbsp;</td></tr>
				<tr>
					<td><h3>File Excel</h3></td>
					<td>
                        <input id="txt" type = "text" value ="" style="width:40%" />
                        <button type="button" class="btn btn-blue" onclick ="javascript:document.getElementById('file').click();">Upload</button>
                        <input runat="server" name="file" id = "file" type="file" style='visibility: hidden;' onchange="ChangeText(this, 'txt');"/>
					</td>
				</tr>
			</table>
			<table height="50">
				<tr>                    
					<td>
						<asp:Linkbutton id="upload" runat="server" cssclass="btn btn-blue t-white" width="75" onclick="upload_Click">
							<i class="fa fa-share"></i> OK
						</asp:Linkbutton>
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

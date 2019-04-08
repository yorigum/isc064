<%@ Page Language="c#" Inherits="ISC064.financear.VAImporBNI3" CodeFile="VAImporBNI3.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
	<title>Upload Transaksi Virtual Account</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<link href="/Media/Style.css" type="text/css" rel="stylesheet">
	<meta name="ctrl" content="1">
	<meta name="sec" content="Virtual Account - Impor Data Transaksi BNI (Hal. 3)">
</head>
<body>
	<form class="cnt" id="Form2" method="post" runat="server">
	<uc1:Head ID="Head1" runat="server"></uc1:Head>
	<h1>
		Upload Transaksi Virtual Account</h1>
	<br />
	<p class="feed">
		<asp:Label ID="feed" runat="server" />
	</p>
	<div id="div1" runat="server">
	<table class="tb" id="tb2">
	        <tr>
	            <th width="80">No. VA</th>
	            <th width="80">Tgl. TXN</th>
	            <th width="80">No. TTS</th>
	            <th width="200">Customer</th>
	            <th width="90" align="right">Total</th>
	            <th width="80">No. Kwitansi</th>
	        </tr>
	        <asp:PlaceHolder ID="ph" runat="server" />
	    </table>
	    <asp:Button ID="save1" runat="server" Width="75" CssClass="btn" Text="SAVE" 
            onclick="save1_Click" />
     </div>
     <script language="javascript">
	
		function call(nomor) {
			popEditTTS(nomor);
		}
	</script>
    </form>
</body>
</html>

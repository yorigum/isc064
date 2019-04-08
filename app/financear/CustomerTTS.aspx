<%@ Page language="c#" Inherits="ISC064.FINANCEAR.CustomerTTS" CodeFile="CustomerTTS.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadCIF" Src="HeadCIF.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavCIF" Src="NavCIF.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Customer Information File (Tanda Terima Sementara)</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Customer Information File - Tanda Terima Sementara">
	</head>
	<body onkeyup="if(event.keyCode==27) window.close()">
		<form id="Form1" method="post" runat="server">
            <div class="content-header">
			    <uc1:navcif id="NavCIF1" runat="server" aktif="2"></uc1:navcif>
            </div>
			<div class="tabdata">
				<div class="pad">
					<uc1:headcif id="HeadCIF1" runat="server"></uc1:headcif>
					<br>
                    <div class="peach">
					    <p class="feed" style="padding:3px;font:8pt">
						    Cara Bayar : TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer 
						    Bank / BG = Cek Giro / UJ = Uang Jaminan / DN = Diskon
					    </p>
                    </div>
					<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
						<asp:tablerow horizontalalign="Left">
							<asp:tableheadercell >No. TTS</asp:tableheadercell>
							<asp:tableheadercell width="110">Tgl. / Kasir</asp:tableheadercell>
							<asp:tableheadercell width="200">Keterangan</asp:tableheadercell>
							<asp:tableheadercell width="130">Cara Bayar</asp:tableheadercell>
							<asp:tableheadercell width="90" horizontalalign="Right">Total</asp:tableheadercell>
						</asp:tablerow>
					</asp:table>
				</div>
			</div>
		</form>
	</body>
</html>

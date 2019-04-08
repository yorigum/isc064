<%@ Reference Control="~/PrintPJTTemplate.ascx" %>
<%@ Page language="c#" Inherits="ISC064.COLLECTION.PrintPJTBatch" CodeFile="PrintPJTBatch1.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Print Pemberitahuan Jatuh Tempo</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Print.css" type="text/css" rel="stylesheet">
        <link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Print Pemberitahuan Jatuh Tempo (Batch)">
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup layar print ini?')) window.close();">
		<form id="Form1" method="post" runat="server">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="display:none">
			<%--<div id="reprint" runat="server">
				<h1 class="title" style="border-bottom:1px solid silver">Print Invoice</h1>
				<p style="padding:5px"><b>Tanggal :</b></p>
				<table>
					<tr>
						<td style="padding-top:20px;"><b>Periode</b></td>
						<td>
							<div class="input-group input-medium">
                                <asp:textbox id="dari" runat="server" type="text" cssclass="form-control" style="width:50%" Height="20"></asp:textbox>
                                <span class="input-group-btn" style="height:34px;display:block">
                                    <button class="btn-a default" runat="server" onclick="openCalendar('dari');" type="button" style="height:100%">
                                        <i class="fa fa-calendar"></i>
                                    </button>
                                </span>
                            </div>
						</td>
					</tr>
					<tr>
						<td colspan="3"><asp:label id="daric" runat="server" cssclass="err"></asp:label></td>
					</tr>
				</table>
				<br>
				<div>
					<table>
						<tr>
							<td>
								<asp:button id="print" runat="server" cssclass="btn-submit button-submit2" text="Print" width="75" accesskey="p" onclick="print_Click"></asp:button>
							</td>
						</tr>
					</table>
				</div>
				<br>
				<p style="padding:5px"><u>Pemberitahuan Jatuh Tempo Belum Print</u></p>
				<asp:table id="belumprint" runat="server" cssclass="tb" cellspacing="5"></asp:table>
			</div>--%>
			<asp:placeholder id="list" runat="server"></asp:placeholder>
		</form>
	</body>
</html>

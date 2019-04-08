<%@ Reference Page="~/Acc.aspx" %>
<%@ Page language="c#" Inherits="ISC064.SETTINGS.AccEdit" CodeFile="AccEdit.aspx.cs" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Edit Rekening</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="5">
		<meta name="sec" content="Kas Bank - Edit Rekening">
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27) window.close()">
		<script type="text/javascript" src="/Js/Common.js"></script>
		<script type="text/javascript" src="/Js/NumberFormat.js"></script>
		<form id="Form1" method="post" runat="server">
            <h1 class="title title-line">Edit Rekening</h1>			
				<table cellspacing="5">
					<tr>
						<td width="100%"></td>
						<td>
                            <label class="ibtn ibtn-file">
							    <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
								    accesskey="l">
                            </label>
						</td>
						<td>
                            <label class="ibtn ibtn-remove">
							    <input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel"
								    accesskey="d">
                            </label>
						</td>
					</tr>
				</table>
				<table cellspacing="5">
					<tr>
						<td><b>No. Account</b></td>
						<td>:</td>
						<td>
							<asp:textbox id="acc" runat="server" width="160" maxlength="20" cssclass="txt"></asp:textbox>
							<asp:label id="accc" runat="server" cssclass="err"></asp:label>
						</td>
					<tr>
						<td><b>SubID</b></td>
						<td>:</td>
						<td>
							<asp:textbox id="subid" runat="server" width="150" cssclass="txt" maxlength="50"></asp:textbox>
							<asp:label id="subidc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
                    
                        <tr>
                            <td>
                                Project
                            </td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="project" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
					<tr>
						<td><b>Rekening</b></td>
						<td>:</td>
						<td>
							<asp:textbox id="rekening" runat="server" width="150" cssclass="txt" maxlength="50"></asp:textbox>
						</td>
					</tr>
					<tr>
						<td><b>Bank</b></td>
						<td>:</td>
						<td>
							<asp:textbox id="bank" runat="server" width="200" cssclass="txt" maxlength="50"></asp:textbox><asp:label id="bankc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
					<tr>
						<td><b>Cabang</b></td>
						<td>:</td>
						<td>
							<asp:textbox id="cabang1" runat="server" width="200" cssclass="txt" maxlength="50"></asp:textbox>
						</td>
					</tr>
					<tr>
						<td><b>Atas Nama</b></td>
						<td>:</td>
						<td>
							<asp:textbox id="atasnama" runat="server" width="200" cssclass="txt" maxlength="50"></asp:textbox>
						</td>
					</tr>
					<tr>
						<td><b>Saldo Awal</b></td>
						<td>:</td>
						<td>
							<asp:textbox id="saldoawal" runat="server" cssclass="txt_num"></asp:textbox>
							<asp:label id="saldoawalc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
				</table>
				<table style="height:50px">
					<tr>
						<td>
                            <asp:LinkButton id="ok" runat="server" cssclass="btn btn-blue" width="75" onclick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
						</td>
                        <td>							
						    <asp:Linkbutton id="save" runat="server" cssclass="btn btn-orange" width="75" accesskey="a" onclick="save_Click"><i class="fa fa-check"></i> Apply </asp:Linkbutton>
                        </td>
						<td>
							<input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel">
						</td>						
						<td style="padding-left:10px">
							<p class="feed">
								<asp:label id="feed" runat="server"></asp:label>
							</p>
						</td>
					</tr>
				</table>			
		</form>
	</body>
</html>

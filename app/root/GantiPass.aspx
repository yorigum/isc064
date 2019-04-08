<%@ Page language="c#" Inherits="ISC064.GantiPass" CodeFile="GantiPass.aspx.cs" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Ganti Password</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="2">
		<meta name="sec" content="Ganti Password">
		<meta http-equiv="pragma" content="no-cache">
		<base target="_self">
        <script type="text/javascript">
        //function CheckPassword() 
        //{ 
        //    var inputtxt = document.getElementById('passbaru');
        //    var decimal=  /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$/;
        //    if(inputtxt.value.match(decimal)) 
        //    { 
        //        document.getElementById("save").disabled = false;
        //        document.getElementById("passc").innerHTML = "";
        //        return true;
        //    }
        //    else
        //    { 
        //        document.getElementById("save").disabled = true;
        //         document.getElementById("passc").innerHTML = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Special Characters and 1 Number";
        //        return false;
        //     }
        // }
    </script>
	</head>
	<body class="body body-padding pop" onkeyup="if(!document.getElementById('dariLogin').checked){if(event.keyCode==27)window.close()}">
		<script type="text/javascript" src="/Js/MD5.js"></script>
		<form id="Form1" method="post" runat="server">
			<div style="display:none">
				<asp:checkbox id="dariLogin" runat="server"></asp:checkbox>
			</div>
			<table>
				<tr>
					<td>
						<b>Nama User : <u>
								<asp:label id="namauser" runat="server"></asp:label></u></b>
					</td>
				</tr>
			</table>
			<table>
				<tr>
					<td>Password Sekarang</td>
					<td>:</td>
					<td>
						<asp:textbox id="passold" runat="server" cssclass="input-text" textmode="Password" width="200"></asp:textbox>
					</td>
					<td>
						<asp:button id="next" runat="server" cssclass="btn btn-blue" text=" Next... " onclick="next_Click"></asp:button>
					</td>
				</tr>
				<tr>
					<td></td>
					<td></td>
					<td><asp:label id="salah" runat="server" cssclass="err"></asp:label></td>
					<td>
						<br>
					</td>
				</tr>
				<tr>
					<td>Password Baru</td>
					<td>:</td>
					<td>
						<asp:textbox id="passbaru" runat="server" cssclass="input-text" textmode="Password" width="200" onblur="CheckPassword();"></asp:TextBox>
                            <asp:Label ID="passc" runat="server" CssClass="err"></asp:Label>						
					</td>
				</tr>
				<tr>
					<td>Confirm Password Baru</td>
					<td>:</td>
					<td>
						<asp:textbox id="passconfirm" runat="server" cssclass="input-text" textmode="Password" width="200"></asp:textbox>
					</td>
				</tr>
			</table>
			<table">
				<tr>
					<td>
						<asp:button id="save" runat="server" text="OK" cssclass="btn btn-blue" width="75" onclick="save_Click"></asp:button>
					</td>
					<td>
						<input type="button" value="Cancel" class="btn" onclick="window.close()" id="cancel" runat="server"
							name="cancel" style="width:75px">
					</td>
				</tr>
			</table>
			<p class="feed">
				<asp:label id="feed" runat="server"></asp:label>
			</p>
		</form>
	</body>
</html>

<%@ Page language="c#" Inherits="ISC064.LAUNCHING.CancelReception" CodeFile="CancelReception.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>NUP - Cancel Aktivasi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<%--<link href="/Media/Style.css" type="text/css" rel="stylesheet">--%>
		<meta name="ctrl" content="1">
		<meta name="sec" content="NUP - Cancel Aktivasi (Hal. 1 dari 2)">
	</head>
	<style type="text/css">
        .sm TD
        {
            font-weight: normal;
            font-size: 8pt;
            line-height: normal;
            font-style: normal;
            font-variant: normal;
        }
        .nav, .navsub
        {
            border: 0px;
            background-color: #EEEEEE;
            font: 8pt Trebuchet MS;
            padding-left: 7;
            text-align: left;
            width: 190;
            height: 18px;
        }
        .nav2
        {
            border: 0px;
            background-color: #EEEEEE;
            font: 14pt Trebuchet MS;
            padding-left: 7;
            text-align: left;
            width: 200;
            height: 30px;
        }
        
    </style>
	<body>
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			 <div style="float: left; width: 40%;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 20px">
                        <a href="Index.html" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_prev_c.png" style="width: 80px; height: 80px;"></a>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: right; width: 10%;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 20px">
                        <a href="/Gateway.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_gateway2.png" style="width: 80px; height: 80px;"></a>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px">
                        <a href="/SignOut.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_out.png" style="width: 80px; height: 80px;"></a>
                    </td>
                </tr>
            </table>
        </div>
            <br />
            <br />
            <br />
            <br />
            <h1>
                Cancel Aktivasi</h1>
            <p style="font-size: 8pt; color: #666;">
                Halaman 1 dari 2</p>
            <br>
			<table style="border:1px solid #DCDCDC" cellspacing="5">
				<tr>
					<td>
						Nama Customer / NUP :
					</td>
					<td>
						<asp:TextBox ID="keyword" runat="server" Width="200px"></asp:TextBox>
					</td>
					<td>
						<asp:button id="display" runat="server" cssclass="btn" text="Display" onclick="display_Click"></asp:button>
					</td>
				</tr>
			</table>
			<br />
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow horizontalalign="Left" verticalalign="Bottom">
					<asp:tableheadercell width="50">NUP</asp:tableheadercell>
					<asp:tableheadercell width="300">Customer</asp:tableheadercell>
					<asp:tableheadercell width="250">Sales Agent</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<script language="javascript">
			function call(nomor)
			{
				popUnit(nomor);
			}
			</script>
		</form>
	</body>
</html>

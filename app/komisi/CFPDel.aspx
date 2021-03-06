﻿<%@ Page language="c#" Inherits="ISC064.KOMISI.CFPDel" CodeFile="CFPDel.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Delete Pengajuan Pencairan Closing Fee</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Closing Fee - Delete Pengajuan Pencairan Closing Fee">
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27) history.back(-1)">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
		<form id="Form1" method="post" runat="server">
            <h1 class="title title-line">Clear False Pengajuan Closing Fee</h1>
            <br>

            <table style="border: 1px solid #DCDCDC" cellspacing="5">
                <tr>
                    <td>
                        <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                            <asp:ListItem>Pilih Project :</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="tipesales" runat="server" Width="300">
                            <asp:ListItem Value="0">Tipe Marketing :</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table style="border: 1px solid #DCDCDC" cellspacing="5">
                <tr>
                    <td>Tgl. Pengajuan</td>
                    <td><b>Dari</b></td>
                    <td>
                        <asp:TextBox ID="dari" runat="server" CssClass="txt_center tgl" Width="100"></asp:TextBox>
                        <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                    </td>
                    <td><b>Sampai</b></td>
                    <td>
                        <asp:TextBox ID="sampai" runat="server" CssClass="txt_center tgl" Width="100"></asp:TextBox>
                        <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                    </td>
                    <td colspan="3">
                        <asp:label id="projectc" runat="server" cssclass="err"></asp:label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td colspan="5" style="padding-left: 10px">
                        <p class="feed">
                            <asp:Label ID="feed" runat="server"></asp:Label>
                        </p>
                    </td>
                </tr>
            </table>
        <br />
        <%--<div class="peach">
            Status : A = Aktif / B = Batal
        </div>--%>
        
        <table class="tb blue-skin" cellspacing="1">
            <tr align="left" valign="bottom">
                <th></th>
                <th>No. Pengajuan</th>
                <th>Tgl. Pengajuan</th>
                <th>Tipe</th>
                <th>Penerima</th>
            </tr>
            <tr>
                <td colspan="7">
                    <ul class="floatsm">
                        <li><a href="javascript:checkCtrl('cb','true')">Check&nbsp;&nbsp;&nbsp;&nbsp;</a></li>
                        <li><a href="javascript:checkCtrl('cb','false')">Uncheck</a></li>
                    </ul>
                    <br />
                </td>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </table>
            <table cellspacing="5">
            <tr valign="top">
				<td width="20%">Alasan</td>
				<td width="1%">:</td>
				<td>
					<asp:textbox id="alasan" runat="server" Width="350" Height="150" TextMode="MultiLine" cssclass="txt"></asp:textbox>
				</td>
			</tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="del" runat="server" CssClass="btn btn-red" Text="Delete" OnClick="delbtn_Click"></asp:Button>
                </td>
            </tr>
        </table>
			<%--<div id="frm" runat="server">
				<input type="text" style="display:none">
				<h1 class="title title-line">Delete Pengajuan Pencairan Closing Fee</h1>
				<br>
				Keterangan :
				<asp:textbox id="ket" runat="server" cssclass="txt" width="400"></asp:textbox>
				<asp:button id="delbtn" runat="server" cssclass="btn btn-red" text="Delete" onclick="delbtn_Click"></asp:button>
			</div>
            <br />
            <asp:label id="warning" runat="server" cssclass="err" font-bold="True" font-size="12pt"></asp:label>
			<asp:label id="nodel" runat="server" visible="false">
				<h1>
					Pengajuan Pencairan Closing Fee Tidak Dapat Dihapus
				</h1>
				<br>
				<div class="plike">
					<h2>Kemungkinan Terjadi Karena:</h2>
					<ul>
						<li>
							Realisasi Pencairan Closing Fee sudah dilakukan
						</li>
					</ul>
				</div>
			</asp:label>--%>
		</form>

        <script type="text/javascript">
            function checkCtrl(foo, n) {
                var x = true; var i = 0;
                while (x) {
                    if (document.getElementById(foo + "_" + i)) {
                        if (!document.getElementById(foo + "_" + i).disabled) {
                            if (n == "true")
                                document.getElementById(foo + "_" + i).checked = true;
                            else
                                document.getElementById(foo + "_" + i).checked = false;
                        }
                        i++;
                    } else { x = false; }
                }
            }
        </script>

	</body>
</html>

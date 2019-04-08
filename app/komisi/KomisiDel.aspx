<%@ Page Language="c#" Inherits="ISC064.KOMISI.KomisiDel" CodeFile="KomisiDel.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Clear False Komisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Komisi - Clear False Komisi">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Clear False Komisi</h1>
        <br>
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td><b>Dari</b></td>
                <td>
                    <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="100"></asp:TextBox>
                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                </td>
                <td><b>Sampai</b></td>
                <td>
                    <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="100"></asp:TextBox>
                    <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="sales" runat="server" Width="200">
                        <asp:ListItem Value="">Sales :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br>
        <%--<div class="peach">
            Status : A = Aktif / B = Batal
        </div>--%>
        <table class="tb blue-skin" cellspacing="1">
            <tr align="left" valign="bottom">
                <th>Sales</th>
                <th>No. Kontrak</th>
                <th>Unit</th>
                <th>Customer</th>
                <th>Skema</th>
                <th class="right">Dasar Perhitungan</th>
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
                    <asp:Button ID="del" runat="server" CssClass="btn btn-red" Text="Delete" OnClick="del_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

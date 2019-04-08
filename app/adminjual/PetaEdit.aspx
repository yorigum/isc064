<%@ Page language="c#" Inherits="ISC064.ADMINJUAL.PetaEdit" CodeFile="PetaEdit.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Peta Floor Plan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Edit Peta">
        <style>
            
            .img{
                border:solid 1px #bbbbbb;
            }
        </style>
	</head>
	<body class="default-content">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1>Edit Peta Floor Plan</h1>
			
            <table style="float:left;">
                <tr>
                    <td>Nama </td>
                    <td>:</td>
                    <td> <asp:TextBox ID="nama" runat="server" Width="300"></asp:TextBox></td>                    
					<td rowspan="20" valign="top" style="padding-left:10px;padding-right:10px;">
                        <img src="/Media/line_vert.gif" />
					</td>
                </tr>
                <tr id="tr1" runat="server">
                    <td valign="top">Peta Dasar </td>
                    <td valign="top">:</td>
                    <td>
                        <asp:Image ID="imgdasar" runat="server" width="300" Height="200" ImageUrl="/Media/noimg.jpg" CssClass="img"/>
                    </td>
                </tr>
                <tr  id="tr2" runat="server">
                    <td></td>
                    <td></td>
                    <td>
                        <asp:FileUpload ID="file1" runat="server" /><br />
                        <asp:Label runat="server" ID="file1c" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <%--<tr id="tr3" runat="server">
                    <td valign="top">Peta Transparent </td>
                    <td valign="top">:</td>
                    <td>
                        <asp:Image ID="imgtransparent" runat="server" width="300" Height="200" ImageUrl="/Media/noimg.jpg" CssClass="img"/>
                    </td>
                </tr>
                <tr id="tr4" runat="server">
                    <td></td>
                    <td></td>
                    <td>
                        <asp:FileUpload ID="file2" runat="server" /><br />
                        <asp:Label runat="server" ID="file2c" CssClass="err"></asp:Label>
                    </td>
                </tr>--%>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="save" runat="server" Text="Save Change" OnClick="save_Click" />
                        <asp:Button ID="Button1" runat="server" Text="Delete" OnClick="Button1_Click"  />
                        <p class="feed">
				            <asp:label id="feed" runat="server"></asp:label>
			            </p>
                    </td>
                </tr>
            </table>
            
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3" style="float:left;">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell width="120">No. Unit</asp:tableheadercell>
					<asp:tableheadercell width="550">Koordinat</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LunasRegistrasi2.aspx.cs" Inherits="ISC064.COLLECTION.LunasRegistrasi2" %>

<!DOCTYPE html>
<html>
<head>
    <title>Registrasi Surat Keterangan Lunas</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Registrasi Surat Keterangan Lunas 2">
</head>
<body class="body-padding">
    <form id="form1" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div id="frm" runat="server">
            <h1 class="title title-line">Registrasi Surat Keterangan Lunas</h1>
            <p>Halaman 2 dari 2</p>
            <br>
            <table cellspacing="5">
                <tr>
                    <td><b>No. Kontrak</b></td>
                    <td><b>:</b></td>
                    <td>
                        <asp:Label ID="nokontrakl" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Unit</b></td>
                    <td><b>:</b></td>
                    <td>
                        <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Customer</b></td>
                    <td><b>:</b></td>
                    <td>
                        <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Sales</b></td>
                    <td><b>:</b></td>
                    <td>
                        <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Pelunasan</b></td>
                    <td><b>:</b></td>
                    <td>
                        <asp:Label ID="persenlunas" runat="server" Font-Bold="True"></asp:Label>% 
							&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:Label ID="lunasinfo" runat="server" CssClass="err" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
            <br>
            <table cellspacing="5">
                <tr>
				    <td valign="top"><b>Nomor yang Digunakan</b></td>
				    <td valign="top"><b>:</b></td>
				    <td>
				       <asp:RadioButtonList ID="nod" runat="server" OnSelectedIndexChanged="nod_SelectedIndexChanged" AutoPostBack="true">
                           <asp:ListItem Value="0" Selected="True">System</asp:ListItem>									            
                           <asp:ListItem Value="1">Manual</asp:ListItem>									            
				       </asp:RadioButtonList>
					</td>
			    </tr>
                <tr>
                    <td><b>No. Surat Keterangan Lunas Manual</b></td>
                    <td><b>:</b></td>
                    <td>
                        <asp:TextBox ID="nosklm" runat="server" CssClass="input-text" Width="120" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><b>Tgl Surat Keterangan Lunas</b></td>
                    <td><b>:</b></td>
                    <td>
                        <asp:TextBox ID="tgl" runat="server" type="text" CssClass="txt_center"></asp:TextBox>
                        <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table height="50">
                <tr>
                    <td>
                        <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                    </td>
                    <td><br /></td>
                    <td>
                        <input type="button" onclick="location.href = '?'" class="btn btn-red" value="Cancel" style="width: 75px"
                            id="cancel" runat="server">
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

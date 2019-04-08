<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NumeratorFile.aspx.cs" Inherits="ISC064.SETTINGS.NumeratorFile" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="~/Head.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>Numerator File</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="/Media/Style.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css" />
    <meta name="ctrl" content="1">
    <meta name="sec" content="Numerator File">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27){window.close()}"
    onload="document.getElementById('keyword').select()">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head" runat="server" />
        <h1 class="title title-line">Numerator File</h1>
        <asp:TextBox ID="nol" runat="server" CssClass="bug" Visible="false">0</asp:TextBox>
        <div id="content">
            <table class="tb">
                <tr>
                    <td class="label">Project</td>
                    <td>:</td>
                    <td><b><asp:TextBox runat="server" ID="project" ReadOnly="true"></asp:TextBox></b></td>
                </tr>
                <tr>
                    <td class="label">Kode Prefix </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="prefix" runat="server" Width="100" MaxLength="50" ToolTip="Kode yang digunakan sebagai pengenal transaksi di dalam sistem." />
                        <asp:RequiredFieldValidator ID="prefixr" ControlToValidate="prefix" runat="server" />
                    </td>
                </tr>
                <tr class="top">
                    <td class="label">Reset Numerator </td>
                    <td>:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="resetnum" runat="server" CssClass="cb">
                            <asp:ListItem class="radio" Value="0">Tanpa Reset</asp:ListItem>
                            <asp:ListItem class="radio" Value="1">Reset Bulan</asp:ListItem>
                            <asp:ListItem class="radio" Value="2">Reset Tahun</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="label">Digit Numerator </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="digitnum" runat="server" Width="30" CssClass="center" />
                        <asp:RequiredFieldValidator ID="digitnumr" ControlToValidate="digitnum" runat="server" />
                        <asp:CompareValidator ID="digitnumc" ControlToValidate="digitnum" runat="server"
                            SkinID="intpos" Type="Integer" Operator="GreaterThanEqual" ControlToCompare="nol" />
                    </td>
                </tr>
                <tr class="top">
                    <td class="label">Format Bulan </td>
                    <td>:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="formatbulan" runat="server" CssClass="cb">
                            <asp:ListItem class="radio" Value="0">Angka 2 Digit</asp:ListItem>
                            <asp:ListItem class="radio" Value="1">Romawi</asp:ListItem>
                            <asp:ListItem class="radio" Value="2">Alfabet</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr class="top">
                    <td class="label">Format Tahun </td>
                    <td>:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="formattahun" runat="server" CssClass="cb">
                            <asp:ListItem class="radio" Value="0">Angka 2 Digit</asp:ListItem>
                            <asp:ListItem class="radio" Value="1">Angka 4 Digit</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="label">Pemisah </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="pemisah" runat="server" Width="30" CssClass="center" MaxLength="1"
                            ToolTip="Karakter pemisah antar bagian kode." />
                        <asp:RequiredFieldValidator ID="pemisahr" ControlToValidate="pemisah" runat="server" />
                    </td>
                </tr>
                <tr class="top">
                    <td class="label">Komposisi </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="komposisi" runat="server" Width="300"></asp:TextBox><br />
                        Contoh :
                        <label>{Prefix}{Thn}{Bln}{Num}{Project}</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" />
                    <td>
                        <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click">
                            <i class="fa fa-share"></i> 
                            OK
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

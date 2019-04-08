<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.HoldUnitDaftarDone" CodeFile="HoldUnitDaftarDone.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Hold Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Pendaftaran Hold Unit Selesai">
</head>
<body>
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Pendaftaran Hold Unit</h1>
        <p><b><i>Halaman 2 dari 2 </i></b></p>
        <br />
        <br />
        <h2 style="color: Brown; border: 1 solid silver; padding: 10">Hold Unit Selesai
        </h2>
        <br />
        <table cellspacing="5">
            <tr>
                <td>No. Hold</td>
                <td>:</td>
                <td>
                    <asp:Label ID="nohold" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Unit</td>
                <td>:</td>
                <td>
                    <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Customer</td>
                <td>:</td>
                <td>
                    <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Sales</td>
                <td>:</td>
                <td>
                    <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Tgl Hold</td>
                <td>:</td>
                <td>
                    <asp:Label ID="tglhold" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Tgl Hold Expired</td>
                <td>:</td>
                <td>
                    <asp:Label ID="tglholdexp" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>

            <tr>
                <td colspan="3">
                    <p style="padding: 5px">
                        <b>Prosedur 
					Lanjutan :</b>
                        <h1><a id="aclosing" runat="server">Closing</a></h1>
                    </p>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisFP.aspx.cs" Inherits="ISC064.FINANCEAR.RegisFP" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Registrasi Faktur Pajak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Registrasi Faktur Pajak">
</head>
<body>
    <form class="cnt" id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input style="display: none" type="text">
        <h1>Registrasi Faktur Pajak</h1>
        <br>
        <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid; border-bottom: #dcdcdc 1px solid"
            cellspacing="5">
            <tr>
                <td style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">Customer 
						/ Unit / Dokumen :</td>
                <td colspan="3">
                    <asp:TextBox ID="keyword" runat="server" Width="300" CssClass="txt"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">Dari</td>
                <td>
                    <asp:TextBox ID="dari" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label></td>
                <td>Sampai</td>
                <td>
                    <asp:TextBox ID="sampai" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                    <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label></td>
            </tr>
            <tr>
                <td align="right">jumlah</td>
                <td colspan="2">
                    <asp:TextBox ID="jumlah" runat="server"></asp:TextBox>
                    <asp:Label ID="jumlahc" runat="server" CssClass="err"></asp:Label></td>
                <td>
                    <asp:Button ID="search" AccessKey="s" runat="server" CssClass="btn" Text="Search"></asp:Button></td>
            </tr>
        </table>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label></p>
        <table>
            <tr>
                <td>Start From No. Faktur Pajak :</td>
                <td>
                    <asp:TextBox ID="nohead" runat="server" Width="50" CssClass="center" Enabled="false">010</asp:TextBox>
                    <asp:TextBox ID="nopt" runat="server" Width="50" CssClass="center"></asp:TextBox><asp:Label ID="noptc" runat="server" CssClass="err"></asp:Label>-
						<asp:TextBox ID="tahunfp" runat="server" Width="50" CssClass="center"></asp:TextBox><asp:Label ID="tahunfpc" runat="server" CssClass="err"></asp:Label>
                    <asp:TextBox ID="nofp" runat="server" CssClass="center"></asp:TextBox><asp:Label ID="nofpc" runat="server" CssClass="err"></asp:Label></td>
            </tr>
        </table>
        <table class="tb">
            <tr>
                <th>&nbsp;
                </th>
                <th>No. TTS
                </th>
                <th>Tgl. / Kasir
                </th>
                <th>Customer
                </th>
                <th>Keterangan
                </th>
                <th>Cara Bayar
                </th>
                <th>DPP
                </th>
                <th>PPN
                </th>
                <th>Total
                </th>
            </tr>
            <tr>
                <td colspan="8" style="padding-bottom: 5px;">

                    <a href="javascript:checkCtrl('tts','true')" style="padding-right: 10px;">Check</a>
                    <a href="javascript:checkCtrl('tts','false')">Uncheck</a>

                </td>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
            <tr>
                <td colspan="9">
                    <strong>Jumlah :
							<asp:Label ID="jmlrow" runat="server"></asp:Label></strong>
                </td>
            </tr>
            <tr>
                <td colspan="9">
                    <asp:Button ID="save" runat="server" Text="Save" Width="75" AccessKey="s"
                        OnClick="save_Click" />

                    <asp:Button ID="upload" runat="server" Text="Upload" Width="75" AccessKey="s" />
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function call(nomor) {
                popEditTTS(nomor);
            }

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
    </form>
</body>
</html>

<%@ Reference Control="~/PrintTTSTemplate.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.PrintTTSMulti" CodeFile="PrintTTSMulti.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Print Multiple Receipt</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Print.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="3">
    <meta name="sec" content="Print Multiple Receipt">
</head>
<body onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup layar print ini?')) window.close();">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="head" runat="server"></uc1:Head>
        <div id="a" runat="server">
            <h1>Print Multiple Receipt</h1>
            <br>
            <table cellpadding="0" cellspacing="0">
                <tr valign="top">
                    <td>
                        <p style="font-size: 8pt">
                            <strong>No. TTS</strong>
                            <br>
                            Dari
								<asp:TextBox ID="tbDari" runat="server" Width="85" CssClass="txt_center" ReadOnly="True"></asp:TextBox><input type="button" value="..." onclick="popDaftarTTS('tbDari');" class="btn">
                            <asp:Label ID="lblDariErr" runat="server" CssClass="err"></asp:Label>
                            &nbsp;&nbsp; Sampai
								<asp:TextBox ID="tbSampai" runat="server" Width="85" CssClass="txt_center" ReadOnly="True"></asp:TextBox><input type="button" value="..." onclick="popDaftarTTS('tbSampai');" class="btn">
                            <asp:Label ID="lblSampaiErr" runat="server" CssClass="err"></asp:Label>
                        </p>
                    </td>
                    <td width="20"></td>
                </tr>
            </table>
            <br>
            <div style="border-right: silver 1px solid; padding-right: 5px; border-top: silver 1px solid; padding-left: 5px; padding-bottom: 5px; border-left: silver 1px solid; padding-top: 5px; border-bottom: silver 1px solid; background-color: #efefef">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="ok" runat="server" Text="OK" CssClass="btn" Width="100" AccessKey="p" OnClick="ok_Click"></asp:Button>
                            <input type="button" value="Cancel" class="btn" style="width: 100px" onclick="window.close();">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="b" runat="server">
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </div>
        <script type="text/javascript">
            function callSource(nomor, source) {
                document.getElementById(source).value = nomor;
            }
        </script>
    </form>
</body>
</html>

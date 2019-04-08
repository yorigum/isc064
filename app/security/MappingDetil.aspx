<%@ Page Language="c#" Inherits="ISC064.SECURITY.MappingDetil" CodeFile="MappingDetil.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Edit Mapping Program</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Setup Mapping Program - Edit Mapping Program">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="body-padding pop" onkeyup="if(event.keyCode==27)window.close()">
    <form id="Form1" method="post" runat="server">
        <table class="tb" cellspacing="5">
            <tr>
                <td>Halaman</td>
                
                <td>
                    <asp:Label ID="halaman" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Modul</td>
                
                <td>
                    <asp:Label ID="modul" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Keterangan</td>
                
                <td>
                    <asp:Label ID="nama" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Tgl. Pasang</td>
                
                <td>
                    <asp:Label ID="tgl" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <p style="padding-left: 7px">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <p style="padding-left: 7px; font-size: 10pt; font-weight: bold">
            Konfigurasi Security Level
        </p>
        <p style="padding-left: 7px">
            <a href="javascript:clickall()">grant all...</a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<a href="javascript:unclickall()">deny all...</a>
        </p>
        <table class="tb blue-skin" cellspacing="3">
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </table>
        <br>
        <p style="padding-left: 7px; font-size: 10pt; font-weight: bold">
            Konfigurasi Khusus
        </p>
        <p style="padding-left: 7px">
            <a href="javascript:if(confirm('Reset semua konfigurasi khusus?')){resetnormal()}">reset 
					ke normal...</a>
        </p>
        <table class="tb blue-skin" cellspacing="3">
            <asp:PlaceHolder ID="list2" runat="server"></asp:PlaceHolder>
        </table>
        <table height="50">
            <tr>
                <td>
                    <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-check"></i> Apply </asp:LinkButton>
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function clickall() {
                if(confirm('Reset semua konfigurasi security level ke GRANT ?'))
                {
                    for(i=0;i<<%printIndex();%>;i++)
				        document.getElementById("sl_"+i).checked=true;
						
				    document.getElementById('ok').click();
				}
            }
            function unclickall() {
                if(confirm('Reset semua konfigurasi security level ke DENY ?'))
                {
                    for(i=0;i<<%printIndex();%>;i++)
				        document.getElementById("sl_"+i).checked=false;
						
				    document.getElementById('ok').click();
				}
            }
            function resetnormal() {
                for(i=0;i<<%printIndexKhusus();%>;i++)
			        document.getElementById("no_"+i).checked=true;
				
			    document.getElementById('ok').click();
			}
        </script>
    </form>
</body>
</html>

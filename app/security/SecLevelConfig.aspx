<%@ Page Language="c#" Inherits="ISC064.SECURITY.SecLevelConfig" CodeFile="SecLevelConfig.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadSecLevel" Src="HeadSecLevel.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavSecLevel" Src="NavSecLevel.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Edit Security Level (Konfigurasi)</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Setup Security Level - Edit Security Level (Konfigurasi)">
</head>
<body onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup layar edit ini?')) document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavSecLevel ID="NavSecLevel1" runat="server" Aktif="2"></uc1:NavSecLevel>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadSecLevel ID="HeadSecLevel1" runat="server"></uc1:HeadSecLevel>
                <p style="padding: 7px">
                    <asp:ListBox ID="daftarmodul" runat="server" CssClass="ddl" Width="500" Rows="10">
                        <asp:ListItem Value="">Semua Modul...</asp:ListItem>
                    </asp:ListBox>
                    <br />
                    <br />
                    <input id="display" runat="server" type="button" class="btn btn-blue" value="Display" style="width: 75px">
                    <input id="cancel" onclick="window.close()" type="button" class="btn btn-red" value="Cancel" style="width: 75px"
                        name="cancel">
                </p>
                <div id="data" runat="server">
                    <div style="padding-left: 3px">
                        <p class="feed">
                            <asp:Label ID="feed" runat="server"></asp:Label>
                        </p>
                    </div>
                    <table class="tb blue-skin" cellspacing="1">
                        <tr align="left">
                            <th>&nbsp;</th>
                            <th width="450">Keterangan</th>
                            <th width="150">Halaman</th>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <a href="javascript:clickall()">grant all...</a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<a href="javascript:unclickall()">deny all...</a>
                            </td>
                        </tr>
                        <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                    </table>
                    <table style="height: 50px">
                        <tr>
                            <td>
                                <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-check"></i> Apply </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            function clickall() {
                if(confirm('Jalankan proses untuk memberikan akses penuh kepada security level ini (GRANT)?'))
                {
                    for(i=0;i<<%printIndex();%>;i++)
                        document.getElementById("p_"+i).checked=true;
						
                    document.getElementById('save').click();
                }
            }
            function unclickall() {
                if(confirm('Jalankan proses untuk mencabut seluruh akses dari security level ini (DENY)?'))
                {
                    for(i=0;i<<%printIndex();%>;i++)
                        document.getElementById("p_"+i).checked=false;
						
                    document.getElementById('save').click();
                }
            }
            function gantiModul(kode,foo) {
                if(foo.selectedIndex!=-1)
                    location.href='SecLevelConfig.aspx?Kode='+kode+'&Modul='+foo.options[foo.selectedIndex].value;
            }
        </script>
    </form>
</body>
</html>

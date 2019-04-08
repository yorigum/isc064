<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.Nav" CodeFile="Nav.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Navigasi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="0">
    <meta name="sec" content="Navigasi">
</head>
<body onunload="browserClose()" style="width: 97%">
    <form id="Form1" method="post" runat="server">
        <script type="text/javascript" src="/Js/Pop.js"></script>
        <div class="sidebar">
            <div class="sidebar-header" style="background: #18aba2">
                <p id="comp" runat="server" style="border-bottom: solid white 2px; margin-bottom: 5px;"></p>
                <div class="sidebar-logo">
                    <img src="/Media/icon_launching.png">
                    <p>LAUNCHING</p>
                </div>
            </div>
            <div class="sidebar-content">
                <ul class="sidebar-menu">
                    <li>
                        <i class="fa fa-money sb-ico"></i>
                        <input value="NUP" onclick="go(this, 'NUP.aspx'); opensub(this, 'subNUP')" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subNUP')"></span>
                        <ul class="nav-submenu" style="display: none" id="subNUP">
                            <li>
                                <input value="Registrasi NUP" onclick="go(this, 'NUPDaftarCustomer.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Pembayaran" onclick="go(this, 'NUPDaftarBayar.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Pelunasan" onclick="go(this, 'NUPLunasBayar.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="TTS Belum diprint" onclick="go(this, 'TTSBelumPrint.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <li>
                        <i class=""></i>
                        <input value="Launching" onclick="go(this, 'Index.html')" class="navmenu" type="button">
                    </li>
                </ul>
    </div>
        </div>
    <div class="pad" style="display: none">
        <p style="font: bold 8pt; color: gray; padding-left: 5; padding-bottom: 3">
            Sales
        </p>
        <table>
            <tr id="d8" style="display: none">
                <%--    <td align="right">
							<input value="Generate Komisi..." onclick="go(this,'KomisiGen.aspx')" class="navmenu" type="button"
								onmouseover="over(this)" onmouseout="out(this)">
						</td>--%>
            </tr>
            <tr>
                <td align="right"></td>
            </tr>
            <tr>
                <td align="right">
                    <table cellspacing="0">
                        <tr>
                            <td>
                                <img src="/Media/icon_out.gif">
                            </td>
                            <td>
                                <input type="button" value="Sign-Out" class="nav" onclick="if (confirm('Apakah anda ingin melakukan sign-out?\nProgram dan absensi aktif anda akan ditutup.')) { top.location.href = 'SignOut.aspx' }"
                                    style="width: 65" onmouseover="over(this)" onmouseout="out(this)">
                            </td>
                            <td width="100%"></td>
                            <td>
                                <img src="/Media/icon_gateway.gif">
                            </td>
                            <td>
                                <input type="button" value="Gateway" class="nav" onclick="top.location.href = '/Gateway.aspx'"
                                    style="width: 65" onmouseover="over(this)" onmouseout="out(this)">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <hr size="1" noshade style="color: #DCDCDC">
                    <table>
                        <tr>
                            <td style="font: 8pt">User
                            </td>
                            <td>:
                            </td>
                            <td style="font: 8pt">
                                <% Response.Write(ISC064.Act.UserID); %>
                            </td>
                        </tr>
                        <tr>
                            <td style="font: 8pt">Sec. Level
                            </td>
                            <td>:
                            </td>
                            <td style="font: 8pt">
                                <% Response.Write(ISC064.Act.SecLevel); %>
                            </td>
                        </tr>
                        <tr>
                            <td style="font: 8pt">IP Addr.
                            </td>
                            <td>:
                            </td>
                            <td style="font: 8pt">
                                <% Response.Write(ISC064.Act.IP); %>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        function opensub(sender, id) {
            // console.log(id);
            var sub = document.getElementById(id);
            var parent = sender.parentElement.getElementsByTagName('span')[0];
            // console.log(parent);
            if (sub.style.display === 'none') {
                sub.style.display = "block";
                parent.className = "";
                parent.className = "fa fa-minus-square addons"
            } else {
                sub.style.display = "none";
                parent.className = "";
                parent.className = "fa fa-plus-square addons";
            }
        }
        function go(foo, href) {
            bold(foo);
            top.content.location.href = href;
        }
        function bold(foo) {
            for (i = 1; i < document.Form1.elements.length - 2; i++) {
                document.Form1.elements[i].style.fontWeight = 'normal';
            }
            foo.style.fontWeight = 'bold';
        }
    </script>

    </form>
</body>
</html>

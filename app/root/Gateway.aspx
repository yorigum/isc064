<%@ Page Language="c#" Inherits="ISC064.Gateway" CodeFile="Gateway.aspx.cs" Debug="true" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Batavianet Business Application</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="2">
    <meta name="sec" content="Gateway">
    <style>
        .shadow-hand-lightfury {
            -webkit-box-shadow: 0 1px 5px rgba(0, 0, 0, 1);
            -moz-box-shadow: 0 1px 5px rgba(0, 0, 0, 1);
            -ms-box-shadow: 0 1px 5px rgba(0, 0, 0, 1);
            box-shadow: 0 1px 5px rgba(0, 0, 0, 1);
        }
    </style>
</head>
<body style="text-align: center;" onkeyup="if(event.keyCode==27)document.getElementById('so').click();"
    onunload="browserClose()" onload="if(window.location.search=='')dismissModal()">

    <form id="Form1" method="post" runat="server">
        <script type="text/javascript" src="/Js/Pop.js"></script>
        <script type="text/JavaScript" src="/Js/MD5.js"></script>

        <div class="modal modalopen" id="GantiPass">
            <div class="modal-dialog modal-small">
                <div class="modal-header">
                    <h4 class="modal-title">Ganti password</h4>
                    <div class="close">
                        <span class="fa fa-times" onclick="dismissModal()"></span>
                    </div>
                </div>
                <div class="modal-body">
                    <div style="display: none">
                        <asp:CheckBox ID="dariLogin" runat="server"></asp:CheckBox>
                    </div>
                    <div class="formgroup">
                        <label class="formlabel">Nama User</label>
                        <asp:Label ID="namauser" runat="server"></asp:Label>
                    </div>
                    <div class="formgroup">
                        <label class="formlabel">Password Sekarang</label>
                        <asp:TextBox ID="passold" runat="server" CssClass="txt" TextMode="Password" Width="200"></asp:TextBox>
                        <asp:Button ID="next" runat="server" CssClass="btn btn-blue" Text=" Next... " OnClick="next_Click"></asp:Button>
                    </div>
                    <asp:Label ID="salah" runat="server" CssClass="err"></asp:Label>
                    <div class="formgroup">
                        <label class="formlabel">Password Baru</label>
                        <asp:TextBox ID="passbaru" runat="server" CssClass="txt" TextMode="Password" Width="200"></asp:TextBox>
                    </div>
                    <div class="formgroup">
                        <label class="formlabel">Konfirmasi Password Baru</label>
                        <asp:TextBox ID="passconfirm" runat="server" CssClass="txt" TextMode="Password" Width="200"></asp:TextBox>
                    </div>
                    <div class="formgroup">
                        <label class="formlabel" style="visibility: hidden;">a</label>
                        <asp:Button ID="save" runat="server" Text="OK" CssClass="btn" Width="75" OnClick="save_Click"></asp:Button>
                        <input type="button" value="Cancel" class="btn" style="background: silver" id="cancel" runat="server"
                            name="cancel" onclick="dismissModal()">
                    </div>
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>

                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
        <div class="main-header">
            <div id="background"></div>
            <div id="labels">
                <div class="logo-header">
                    <span class="v-helper"></span>
                    <img src="/Media/logo.png">
                </div>
                <div class="user-thumb" onclick="usermenu()">
                    <span class="v-helper"></span>
                    <img src="/Media/user.png">
                    <div class="user-menu" id="usermenu">
                        <p onclick="openModal('GantiPass');"><i class="fa fa-lock user-icon" aria-hidden="true"></i>&nbsp;Ganti Password</p>
                        <p onclick="if(confirm('Apakah Anda ingin melakukan sign-out?\nProgram dan absensi aktif Anda akan ditutup.')){location.href='/SignOut.aspx'}"><i class="fa fa-power-off user-icon" aria-hidden="true"></i>&nbsp;Log Out</p>
                    </div>
                </div>
                <div class="user-info" onclick="usermenu()">
                    <p><% Response.Write(ISC064.Act.UserID); %> - <% Response.Write(ISC064.Act.SecLevel); %></p>
                    <p>IP Address : <% Response.Write(ISC064.Act.IP); %></p>
                </div>
                <h2 id="comp2" runat="server" style="display: none;"></h2>
            </div>
        </div>
        <div class="gateway-wrapper">
            <div class="gateway-info">
                <h4 class="dashboard">HOMEPAGE <span class="dashboard-addon" id="comp" runat="server"></span></h4>
                <label style="font-weight: 500; margin: 0px;">
                    Selamat Datang
                    <asp:Label ID="nama" runat="server" Style="font-weight: bold"></asp:Label>.</label><br>
                <label style="font-weight: bolder">
                    Login terakhir Anda adalah pada hari
                    <asp:Label ID="tglLogin" runat="server"></asp:Label></label>
            </div>
            <div class="gateway-menu" id="listmodul">
                <div id="settings" runat="server">
                    <a id="aSettings" runat="server" class="abig">
                        <div class="menu-container shadow-hand-lightfury" style="background: #6576f1">
                            <div>
                                <img class="menu-icon" src="/Media/icon_settings.png"><br>
                                <p class="menu-caption">SETTINGS</p>
                            </div>
                        </div>
                    </a>
                </div>
                <div id="security" runat="server">
                    <a id="aSecurity" runat="server" class="abig">
                        <div class="menu-container shadow-hand-lightfury" style="background: #f36d54">
                            <div>
                                <img class="menu-icon" src="/Media/icon_security.png"><br>
                                <p class="menu-caption">SECURITY</p>
                            </div>
                        </div>
                    </a>
                </div>
                <div id="adminjual" runat="server">
                    <a id="aAdminjual" runat="server" class="abig">
                        <div class="menu-container shadow-hand-lightfury" style="background: #32aee2">
                            <div>
                                <img class="menu-icon" src="/Media/icon_setupsales.png">
                                <p class="menu-caption">SETUP SALES</p>
                            </div>
                        </div>
                    </a>
                </div>
                <div style="display: none;">
                    <a>
                        <div class="menu-container shadow-hand-lightfury" style="background: #9450a3">
                            <div>
                                <img class="menu-icon" src="/Media/icon_setupleasing.png">
                                <p class="menu-caption">SETUP LEASING</p>
                            </div>
                        </div>
                    </a>
                </div>
                <div id="marketingjual" runat="server">
                    <a id="aMarketingjual" runat="server">
                        <div class="menu-container shadow-hand-lightfury" style="background: #f09635">
                            <div>
                                <img class="menu-icon" src="/Media/icon_sales.png">
                                <p class="menu-caption">SALES</p>
                            </div>
                        </div>
                    </a>
                </div>
                <div style="display: none;">
                    <a>
                        <div class="menu-container shadow-hand-lightfury" style="background: #2d9c4d">
                            <div>
                                <img class="menu-icon" src="/Media/icon_leasing.png">
                                <p class="menu-caption">LEASING</p>
                            </div>
                        </div>
                    </a>
                </div>
                <div id="collection" runat="server">
                    <a id="aCollection" runat="server" class="abig">
                        <div class="menu-container shadow-hand-lightfury" style="background: #d8689a">
                            <div>
                                <img class="menu-icon" src="/Media/icon_collection.png">
                                <p class="menu-caption">COLLECTION</p>
                            </div>
                        </div>
                    </a>
                </div>
                <div id="financear" runat="server">
                    <a id="aFinanceAR" runat="server" class="abig">
                        <div class="menu-container shadow-hand-lightfury" style="background: #6eb123">
                            <div>
                                <img class="menu-icon" src="/Media/icon_finance.png">
                                <p class="menu-caption">FINANCE & AR</p>
                            </div>
                        </div>
                    </a>
                </div>

                <div id="kpa" runat="server">
                    <a id="aKpa" runat="server" class="abig">
                        <div class="menu-container shadow-hand-lightfury" style="background: #6576f1">
                            <div>
                                <img class="menu-icon" src="/Media/icon_kpr.png">
                                <p class="menu-caption">KPA</p>
                            </div>
                        </div>
                    </a>
                </div>

                <div id="legal" runat="server">
                    <a id="aLegal" runat="server" class="abig">
                        <div class="menu-container" style="background:#2d9c4d">
                            <div>
                                <img class="menu-icon" src="/Media/icon_legal.png">
                                <p class="menu-caption">LEGAL</p>
                            </div>
                        </div>
                    </a>
                </div>
				
				<div id="komisi" runat="server">
                    <a id="aKomisi" runat="server" class="abig">
                        <div class="menu-container" style="background:#2d9c4d">
                            <div>
                                <img class="menu-icon" src="/Media/icon_komisi.png">
                                <p class="menu-caption">KOMISI</p>
                            </div>
                        </div>
                    </a>
                </div>

                <div id="nup" runat="server">
                    <a id="aNup" runat="server" class="abig">
                        <div class="menu-container shadow-hand-lightfury" style="background: #D15FEE">
                            <div>
                                <img class="menu-icon" src="/Media/icon_nup.png">
                                <p class="menu-caption">NUP</p>
                            </div>
                        </div>
                    </a>
                </div>
                <div id="launching" runat="server">
                    <a id="aLaunching" runat="server" class="abig">
                        <div class="menu-container shadow-hand-lightfury" style="background: #18aba2">
                            <div>
                                <img class="menu-icon" src="/Media/icon_launching.png">
                                <p class="menu-caption">LAUNCHING</p>
                            </div>
                        </div>
                    </a>
                </div>
               <div id="approval" runat="server">
                    <a id="aApp" runat="server" class="abig">
                        <div class="menu-container" style="background: #C0A545">
                            <div>
                                <img class="menu-icon" src="/Media/icon_check.png">
                                <p class="menu-caption">APPROVAL</p>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
        </div>
        <div class="main-footer" style="display: none;">
            <div class="copyright">
                Copyright &copy; 2017 Batavianet. All Rights Reserved.
            </div>
            <div class="contact">
                <b style="letter-spacing: 1px">CONTACTS</b> Phone: +62 21 29020111, 54356161 (hunting) | Email : support@batavianet.com
            </div>
        </div>
        <div class="dashed-separation" style="display: none">
            <p style="padding-bottom: 20px; padding-left: 40px; padding-right: 40px; padding-top: 20px; clear: both;">
                Security level anda adalah <b>
                    <asp:Label ID="seclevel" runat="server"></asp:Label></b>.
            <br>
            </p>
        </div>
        <script type="text/javascript">
            function OpenGantiPass() {
                if (navigator.userAgent.indexOf("MSIE") != -1) openModal('GantiPass.aspx', '500', '310');
                window.open('GantiPass.aspx', 'Ganti Password', "width=500px, height=310px, top=100px;, left=200px");
            }
            function usermenu() {
                var submenu = document.getElementById('usermenu');
                if (submenu) {
                    submenu.style.display = submenu.style.display == "block" ? "" : "block";
                }
            }
            function openModal(id) {
                var modal = document.getElementById(id);
                modal.className += " modalopen";
                modal.style.display = 'block';
            }
            function dismissModal() {
                var a = document.getElementsByClassName('modalopen');
                while (a.length > 0) {
                    a[0].style.display = 'none';
                    a[0].className = 'modal';
                }
            }
            document.addEventListener('click', function (e) {
                e = e || window.event;
                var target = e.target || e.srcElement;
                if (target.classList.contains('modal')) {
                    dismissModal();
                };
            }, false);
        </script>

    </form>
</body>
</html>

<%@ Page Language="C#" CodeFile="UnitPilih1.aspx.cs" Inherits="ISC064.LAUNCHING.UnitPilih1" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Booking Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit Launching - Pilih Unit">
    <style type="text/css">
        .style1 {
            width: 250px;
        }

        .style2 {
            width: 326px;
        }
    </style>
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div class="tabdata">
            <div class="pad">
                <table cellpadding="0" cellspacing="0" style="margin-left: 50px; margin-right: 50px;">
                    <tr valign="top">
                        <td>
                            <img src="Image/logosavasa.png" style="width: 300px; height: 100px;" />
                        </td>
                        <td>
                            <table cellspacing="5" cellpadding="5">
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td class="style1">
                                        <p style="font-size: 20px;">
                                            No. NUP
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td class="style2">
                                        <p style="font-size: 20px;">
                                            <b>
                                                <asp:Label ID="priority" runat="server"></asp:Label></b>
                                            <asp:Label ID="priorityc" runat="server" CssClass="err"></asp:Label>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td class="style1">
                                        <p style="font-size: 20px;">
                                            Nama Customer
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td class="style2">
                                        <p style="font-size: 20px;">
                                            <b>
                                                <asp:Label ID="namacust" runat="server"></asp:Label></b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td class="style1">
                                        <p style="font-size: 20px;">
                                            No. Unit
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td class="style2">
                                        <p style="font-size: 20px;">
                                            <b>
                                                <asp:Label ID="nomorunit" runat="server"></asp:Label></b>
                                            <asp:Label ID="nomorunitc" runat="server" CssClass="err"></asp:Label>
                                        </p>
                                    </td>
                                    <asp:TextBox ID="nostock2" runat="server" Style="display: none"></asp:TextBox>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="style1">
                                        <p style="font-size: 20px;">
                                            Type Unit
                                        </p>
                                    </td>
                                    <td>:</td>
                                    <td class="style2">
                                        <p style="font-size: 20px;">
                                            <b>
                                                <asp:Label ID="typeunit" runat="server" />
                                            </b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td class="style1">
                                        <p style="font-size: 20px;">
                                            Status Unit
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td class="style2">
                                        <p style="font-size: 20px;">
                                            <b>
                                                <asp:Label ID="statusunit" runat="server"></asp:Label></b>
                                            <asp:Label ID="statusunitc" runat="server" CssClass="err"></asp:Label>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td class="style1">
                                        <p style="font-size: 20px;">
                                            Cara Bayar
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td class="style2">
                                        <p style="font-size: 20px;">
                                            <asp:DropDownList ID="crbyt" runat="server" AutoPostBack="true" Width="300" OnSelectedIndexChanged="crbyt_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td class="style1">
                                        <p style="font-size: 20px;">
                                            Nominal
                                        </p>
                                    </td>
                                    <td>:
                                    </td>
                                    <td class="style2">
                                        <p style="font-size: 20px;">
                                            Rp.
                                            <asp:Label ID="nml" runat="server" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td class="style1"></td>
                                    <td></td>
                                    <td class="style2">
                                        <asp:Label ID="btncek" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table height="50" cellpadding="5" cellspacing="5">
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td colspan="6" style="font-size: medium">Apakah Anda yakin memilih unit
                                    <asp:Label ID="unitpilih" runat="server"></asp:Label>
                                        ?
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td style="display: none;">
                                        <asp:Button ID="ok" runat="server" CssClass="btn" Text="OK" Width="75" OnClick="ok_Click"></asp:Button>
                                    </td>
                                    <td id="tdsave" runat="server">
                                        <asp:Button ID="save" runat="server" CssClass="btn btn-blue" Text="Yes" Width="75" AccessKey="a"
                                            OnClick="save_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="cancel" runat="server" CssClass="btn btn-red" Text="No" Width="75" AccessKey="n"
                                            OnClick="cancel_Click"></asp:Button>
                                    </td>
                                    <td style="padding-left: 10px">
                                        <p class="feed">
                                            <asp:Label ID="feed" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <script src="/Js/Jquery.min.js"></script>
        <script src="/Js/jquery.signalR-2.2.3.min.js"></script>
        <script src="signalr/hubs" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                var userid = "<%=ISC064.Act.UserID %>";
                var nostock = $("#nostock2").val();
                var test = $.connection.closingHub;
                $.connection.hub.qs = "UserID=" + userid + "&NoStock=" + nostock;

                test.client.broadcastMsg = function (user, nostock) {
                    console.log(user);
                    console.log(nostock);
                };

                $.connection.hub.start().done(function () {
                    test.server.hello(userid, nostock);
                });
            });
            function popKalkul(ctrl1, ctrl2, ctrl3, ctrl4, ctrl5) {
                openPopUp('/launching/KalkulatorSkema.aspx?NoStock=' + ctrl1 + '&NoNUP=' + ctrl2 + '&Tipe=' + ctrl3 + '&cby=' + ctrl5 + '&project=' + ctrl4, '800', '650')
            }
            function call(nomor, stock, tipe, cb, project) {
                popKalku2(nomor, stock, tipe, cb, project);
            }
        </script>
    </form>
</body>
</html>

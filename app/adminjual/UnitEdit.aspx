<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.UnitEdit" CodeFile="UnitEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadUnit" Src="HeadUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavUnit" Src="NavUnit.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Edit Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Unit - Edit Unit">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavUnit ID="NavUnit1" runat="server" Aktif="1"></uc1:NavUnit>
        </div>
        <asp:ScriptManager ID="scriptmanager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadUnit ID="HeadUnit1" runat="server"></uc1:HeadUnit>
                <table cellspacing="5">
                    <tr>
                        <td width="100%"></td>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="Log" id="btnlog" runat="server" name="btnlog" accesskey="l">
                            </label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td>
                            <label class="ibtn ibtn-remove">
                                <input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel" accesskey="d">
                            </label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="stamp">Input :
                        <asp:Label ID="tglInput" runat="server"></asp:Label>
                        </td>
                        <td class="stamp">Edit :
                        <asp:Label ID="tglEdit" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0">
                    <tr valign="top">
                        <td width="400">
                            <table cellspacing="5">
                                <tr>
                                    <td colspan="3">
                                        <p>
                                            <b>Dokumen</b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <a id="aKey" runat="server">No. Stock</a>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nostock" runat="server" CssClass="txt" Width="150" ReadOnly="True"></asp:TextBox>
                                        <asp:Label ID="nostockc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Project
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="project" runat="server" Width="200" CssClass="ddl" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Kategori Unit
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                <asp:RadioButtonList ID="kategori" Enabled="false" runat="server" RepeatColumns="3">
                                    <asp:ListItem Value="FLPP" class="radio">FLPP</asp:ListItem>
                                    <asp:ListItem Value="REAL ESTATE" class="radio">Real Estate</asp:ListItem>
                                    <asp:ListItem Value="KOMERSIL" class="radio">Komersil</asp:ListItem>
                                </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br>
                                        <p>
                                            <b>Unit Properti</b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Lokasi
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="lokasi" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Blok
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="lantai" runat="server" CssClass="txt" Width="120" MaxLength="6"></asp:TextBox><font
                                            style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">Contoh format : L01</font>
                                        <asp:Label ID="lantaic" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nomor
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nomor" runat="server" CssClass="txt" Width="120" MaxLength="6"></asp:TextBox><font
                                            style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">Contoh format : 001</font>            
                                        <asp:Label ID="nomorc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>Luas
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="luas" runat="server" CssClass="txt_num" Width="75"></asp:TextBox>
                                        m<sup>2</sup>
                                        <asp:Label ID="luasc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Luas Tanah
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="luassg" runat="server" CssClass="txt_num" Width="75"></asp:TextBox>
                                        m<sup>2</sup>
                                        <asp:Label ID="luassgc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Luas Lebih Tanah
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="luaslbh" runat="server" CssClass="txt_num" Width="75"></asp:TextBox>
                                        m<sup>2</sup>
                                        <asp:Label ID="luaslbhc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Luas Bangunan
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="luasnett" runat="server" CssClass="txt_num" Width="75"></asp:TextBox>
                                        m<sup>2</sup>
                                        <asp:Label ID="luasnettc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table height="50">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-check"></i> Apply </asp:LinkButton>
                                    </td>
                                    <td>
                                        <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel"
                                            style="width: 75px">
                                    </td>
                                    <td style="padding-left: 10px">
                                        <p class="feed">
                                            <asp:Label ID="feed" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table cellspacing="5">
                                <tr>
                                    <td>
                                        <a id="aStatus" runat="server">Status</a>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="status" runat="server" Font-Bold="True" Font-Size="14"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br>
                            <table cellspacing="5">
                                <tr>
                                    <td colspan="3">
                                        <h2>Price List</h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Default
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="pl" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Minimum
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="plmin" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Price List Rumah
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="plrumah" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Price List Kavling
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="plkav" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Biaya BPHTB
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="bphtb" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Biaya Surat
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="bsurat" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Biaya Proses
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="bproses" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Biaya Lain-lain
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="blain" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Harga Tanah
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="htanah" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br>
                            <h2 style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">Tipe Unit :
                            </h2>
                            <asp:RadioButtonList ID="jenis" runat="server">
                            </asp:RadioButtonList>
                            <br>
                            <h2 style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">Tipe Properti :
                            </h2>
                                    <asp:RadioButtonList ID="tipe" runat="server">
                                        <asp:ListItem class="radio">RUMAH</asp:ListItem>
                                        <asp:ListItem class="radio">RUKAN</asp:ListItem>
                                        <asp:ListItem class="radio">RUKO</asp:ListItem>
                                        <asp:ListItem class="radio">KAVLING</asp:ListItem>
										<asp:ListItem class="radio">VILLA</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <h2 style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">Sifat PPN :
                                    </h2>
                                    <asp:RadioButtonList ID="ppn" runat="server">
                                        <asp:ListItem class="radio" Value="1" Enabled="false">YA</asp:ListItem>
                                        <asp:ListItem class="radio" Value="0" Enabled="false">TIDAK</asp:ListItem>
                                    </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="save" />
                <asp:PostBackTrigger ControlID="ok" />
            </Triggers>
        </asp:UpdatePanel>
    <script src="/Js/Jquery.min.js"></script>
    <script src="/Js/jquery.signalR-2.2.3.min.js"></script>
    <script src="signalr/hubs" type="text/javascript"></script>
    <script src="/Js/iwc-all.min.js"></script>
    <script src="/Js/signalr-patch.js"></script>
    <script src="/Js/iwc-signalr.js"></script>
        <script type="text/javascript">
            var echoHub = SJ.iwc.SignalR.getHubProxy('unitHub', {
                client: {
                    broadcastStatus: function (NoStock) {
                    }
                }
            });

            SJ.iwc.SignalR.start().done(function () {
                echoHub.server.invokeStatus('<%=Request.QueryString["NoStock"]%>').done(function () {
                    console.log('sent');
                }).fail(function (jqXHR, textStatus) {
                    console.log('failed ' + textStatus);
                });

                console.log('akhir');
            });
        </script>
    </form>
</body>
</html>

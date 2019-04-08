<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.LokasiEdit" CodeFile="LokasiEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadLokasi" Src="HeadLokasi.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavLokasi" Src="NavLokasi.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Edit Lokasi Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Lokasi Unit - Edit Lokasi Unit">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavLokasi id="NavUnit1" runat="server" aktif="1"></uc1:NavLokasi>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadLokasi id="HeadUnit1" runat="server"></uc1:HeadLokasi>
                <table cellspacing="5">
                    <tr>
                        <td width="100%"></td>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="Log" id="btnlog" runat="server" name="btnlog" accesskey="l">
                            </label>
                        </td>
                        <td>
                            <label class="ibtn ibtn-remove">
                                <input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel" accesskey="d">
                            </label>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr style="display: none">
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
                                    <td>No. Lokasi
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nolokasi" runat="server" CssClass="txt" Width="150" ReadOnly="True"></asp:TextBox>
                                        <asp:Label ID="nolokasic" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Project</td>
                                    <td>:</td>
                                    <td>
                                        <asp:DropDownList ID="project" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br>
                                        <p>
                                            <b>Lokasi Unit Properti</b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Lokasi Unit
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="lokasi" runat="server" Width="75" CssClass="txt" MaxLength="20"></asp:TextBox>
                                        <asp:Label ID="lokasic" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nama Lokasi Unit
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="namalokasi" runat="server" Width="200" CssClass="txt" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="namalokasic" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <i>*Note: Perubahan data akan berpengaruh pada data unit yang ada.</i>
                                    </td>
                                </tr>
                            </table>
                            <table style="height: 50px">
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
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>

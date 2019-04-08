<%@ Page Language="c#" Inherits="ISC064.KOMISI.TerminKomisiEdit" CodeFile="TerminKomisiEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadSkomTermin" Src="HeadSkomTermin.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavSkomTermin" Src="NavSkomTermin.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Edit Termin Komisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Setup Termin Komisi - Edit Termin Komisi">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavSkomTermin ID="NavSkomTermin" runat="server" Aktif="1"></uc1:NavSkomTermin>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadSkomTermin ID="HeadSkomTermin" runat="server"></uc1:HeadSkomTermin>
                <uc1:Head ID="Head" runat="server"></uc1:Head>
                <table cellspacing="5">
                    <tr>
                        <td width="100%"></td>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                    accesskey="l">
                            </label>
                        </td>
                        <td>
                            <label class="ibtn ibtn-remove">
                                <input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel"
                                    accesskey="d">
                            </label>
                        </td>
                    </tr>
                </table>
                <p class="feed" style="padding-left: 5">
                    <asp:Label ID="feed" runat="server"></asp:Label>
                </p>
                <table cellspacing="5">
                    <tr>
                        <td>
                            <asp:RadioButton ID="aktif" runat="server" Text="Aktif" Font-Size="12" Font-Bold="True" ForeColor="green"
                                GroupName="status"></asp:RadioButton>
                            <asp:RadioButton ID="inaktif" runat="server" Text="Inaktif" Font-Size="12" Font-Bold="True" ForeColor="red"
                                GroupName="status"></asp:RadioButton>
                        </td>
                        <td width="20"></td>
                        <td>Nama</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="nama" runat="server" Width="250" MaxLength="100" CssClass="txt"></asp:TextBox>
                            <asp:Label ID="namac" runat="server" CssClass="err" Width="50"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <b>Tipe Marketing :</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="tipe" runat="server" Width="200" AutoPostBack="true"
                                OnSelectedIndexChanged="gantitipe">
                                <asp:ListItem Value="0">Tipe Marketing :</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="tipec" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Project :</b></td>
                        <td>
                            <asp:DropDownList ID="project" runat="server" Width="200" Enabled="false">
                            </asp:DropDownList>
                    </tr>
                    <tr>
                        <td>
                            <b>Cara Bayar :</b>
                        </td>
                        <td>
                            <asp:RadioButtonList RepeatDirection="Horizontal" ID="carabayar" runat="server">
                                <asp:ListItem Value="ALL" Selected="True" Class="igroup-radio">Semua</asp:ListItem>
                                <asp:ListItem Value="CASH KERAS" Class="igroup-radio">Cash Keras</asp:ListItem>
                                <asp:ListItem Value="CASH BERTAHAP" Class="igroup-radio">Cash Bertahap</asp:ListItem>
                                <asp:ListItem Value="KPR" Class="igroup-radio">KPA/KPR</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Table ID="tbRumus1" runat="server" CssClass="tb blue-skin" CellSpacing="1">
                    <asp:TableRow>
                        <asp:TableHeaderCell ColumnSpan="15"><span style="font-size:large;">Kondisi Termin</span></asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell RowSpan="2">Level</asp:TableHeaderCell>
                        <asp:TableHeaderCell RowSpan="2">Nama</asp:TableHeaderCell>
                        <asp:TableHeaderCell RowSpan="2">% Cair</asp:TableHeaderCell>
                        <asp:TableHeaderCell ColumnSpan="7">Syarat Cair</asp:TableHeaderCell>
                        <asp:TableHeaderCell RowSpan="2">Tipe Syarat Cair</asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>% Lunas</asp:TableHeaderCell>
                        <asp:TableHeaderCell>% BF</asp:TableHeaderCell>
                        <asp:TableHeaderCell>% DP</asp:TableHeaderCell>
                        <asp:TableHeaderCell>% ANG</asp:TableHeaderCell>
                        <asp:TableHeaderCell>PPJB</asp:TableHeaderCell>
                        <asp:TableHeaderCell>AJB</asp:TableHeaderCell>
                        <asp:TableHeaderCell>AKAD</asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:tablerow horizontalalign="Left">
                        <asp:TableCell ColumnSpan="15">
                            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                        </asp:TableCell>
                    </asp:tablerow>
                    <asp:tablerow horizontalalign="Left">
                        <asp:TableCell width="75" ColumnSpan="5">
                            <asp:Button ID="add" runat="server" Text="Tambah Baris" Width="150" class="btn btn-blue" OnClick="add_Click"
                            AccessKey="s" />
                            &nbsp;
                            <asp:Button ID="del" runat="server" Text="Hapus Baris" Width="150" class="btn btn-blue" OnClick="del_Click"
                            AccessKey="s" />
                        </asp:TableCell>                    
                    </asp:tablerow>
                </asp:Table>
                <br />
                <table height="50">
                    <tr>
                        <td>
                            <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK
                            </asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a"
                                OnClick="save_Click"><i class="fa fa-check"></i>Apply</asp:LinkButton>
                        </td>
                        <td>
                            <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel" style="width: 75px">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <script language="javascript">
            function hapusbaris(nomor, baris) {
                if (confirm('Hapus satu baris detail ini dari skema?\nPerhatian bahwa data akan dihapus secara PERMANEN.')) {
                    location.href = 'TerminKomisiEdit.aspx?Act=del&Tipe=Skema&Nomor=' + nomor + '&Baris=' + baris;
                }
            }
        </script>
    </form>
</body>
</html>

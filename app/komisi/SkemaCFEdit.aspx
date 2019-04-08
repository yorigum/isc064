<%@ Page Language="c#" Inherits="ISC064.KOMISI.SkemaCFEdit" CodeFile="SkemaCFEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadSkomCF" Src="HeadSkomCF.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavSkomCF" Src="NavSkomCF.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Edit Skema Closing Fee</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Setup Skema Closing Fee - Edit Skema Closing Fee">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavSkomCF ID="NavSkomCF" runat="server" Aktif="1"></uc1:NavSkomCF>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadSkomCF ID="HeadSkomCF" runat="server"></uc1:HeadSkomCF>
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
                        <td colspan="5"><b>Periode Closing Fee</b></td>
                    </tr>
                    <tr>
                        <td>dari</td>
                        <td>
                            <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                            <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        </td>
                        <td rowspan="2">&nbsp;&nbsp;</td>
                        <td>sampai</td>
                        <td>
                            <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                            <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label></td>
                        <td colspan="3">
                            <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <asp:Label ID="tglc" runat="server" Visible="false" CssClass="err"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Project :</b></td>
                        <td>
                            <asp:DropDownList ID="project" runat="server" Width="200" Enabled="false">
                            </asp:DropDownList>
                    </tr>
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
                        <td>
                            <b>Rumus Komisi :</b>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rumus" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="param_SelectedIndexChanged">
                                <asp:ListItem class="igroup-radio" Value="UNIT">Satuan Per Unit</asp:ListItem>
                                <asp:ListItem class="igroup-radio" Value="KUMULATIF">Target Kumulatif</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Label ID="rumusc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Dasar Perhitungan :</b>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="dasarhitung" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem class="igroup-radio" Value="DPP">DPP</asp:ListItem>
                                <asp:ListItem class="igroup-radio" Value="KONTRAK">Nilai Kontrak</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Label ID="dasarhitungc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Table ID="tbRumus1" runat="server" CssClass="tb blue-skin" CellSpacing="1">
                    <asp:TableRow>
                        <asp:TableHeaderCell ColumnSpan="4"><span style="font-size:large;">Rumus Closing Fee</span></asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>Level</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Tipe Tarif</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Tarif Komisi</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Potong Komisi</asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:tablerow horizontalalign="Left">
                        <asp:TableCell ColumnSpan="4">
                            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                        </asp:TableCell>
                    </asp:tablerow>
                    <asp:tablerow horizontalalign="Left">
                        <asp:TableCell width="75" ColumnSpan="4">
                            <asp:Button ID="add" runat="server" Text="Tambah Baris" Width="150" class="btn btn-blue" OnClick="add_Click"
                            AccessKey="s" />
                            &nbsp;
                            <asp:Button ID="del" runat="server" Text="Hapus Baris" Width="150" class="btn btn-blue" OnClick="del_Click"
                            AccessKey="s" />
                        </asp:TableCell>                    
                    </asp:tablerow>
                </asp:Table>
                <br />
                <asp:Table ID="tbRumus2" runat="server" CssClass="tb blue-skin" CellSpacing="1">
                    <asp:TableRow>
                        <asp:TableHeaderCell ColumnSpan="7"><span style="font-size:large;">Rumus Closing Fee</span></asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>Level</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Tipe Target</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Target Bawah</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Target Atas</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Tipe Tarif</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Tarif Komisi</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Potong Komisi</asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:tablerow horizontalalign="Left">
                        <asp:TableCell ColumnSpan="7">
                            <asp:PlaceHolder ID="list2" runat="server"></asp:PlaceHolder>
                        </asp:TableCell>
                    </asp:tablerow>
                    <asp:tablerow horizontalalign="Left">
                        <asp:TableCell width="75" ColumnSpan="7">
                            <asp:Button ID="add2" runat="server" Text="Tambah Baris" Width="150" class="btn btn-blue" OnClick="add2_Click"
                            AccessKey="s" />
                            &nbsp;
                            <asp:Button ID="del2" runat="server" Text="Hapus Baris" Width="150" class="btn btn-blue" OnClick="del2_Click"
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
                            <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel"
                                style="width: 75px">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <script language="javascript">
            function hapusbaris(nomor, baris) {
                if (confirm('Hapus satu baris detail ini dari skema?\nPerhatian bahwa data akan dihapus secara PERMANEN.')) {
                    location.href = 'SkemaCFEdit.aspx?Act=del&Tipe=Skema&Nomor=' + nomor + '&Baris=' + baris;
                }
            }
            function hapusbarisTerm(nomor, baris) {
                if (confirm('Hapus satu baris termin ini dari skema?\nPerhatian bahwa data akan dihapus secara PERMANEN.')) {
                    //SkomEdit2.aspx?Act=del&Tipe=Term&Nomor="+ Nomor +"&Baris="+ d["Baris"] +"'
                    location.href = 'SkemaCFEdit.aspx?Act=del&Tipe=Term&Nomor=' + nomor + '&Baris=' + baris;
                }
            }
        </script>
    </form>
</body>
</html>

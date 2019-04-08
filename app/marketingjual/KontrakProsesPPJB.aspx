<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakProsesPPJB" CodeFile="KontrakProsesPPJB.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrak" Src="NavKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>Proses PPJB</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Kontrak - Proses PPJB">
</head>
<body onkeyup="if(event.keyCode==27) document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div class="content-header">
            <uc1:NavKontrak ID="NavKontrak1" runat="server" Aktif="6"></uc1:NavKontrak>
        </div>
        <div class="tabdata">
            <uc1:HeadKontrak ID="Head2" runat="server"></uc1:HeadKontrak>
            <table width="100%">
                <tr>
                    <td width="50%" valign="top">
                        <h2>Status PPJB</h2>
                        <%--   B = BELUM
                    D = SUDAH REGIS
                    T = PROSES TTD
                    S = SELESAI--%>
                        <asp:RadioButtonList ID="stat" runat="server" Enabled="false">
                            <asp:ListItem Value="B" Selected="True">Belum di proses</asp:ListItem>
                            <asp:ListItem Value="D">Print</asp:ListItem>
                            <asp:ListItem Value="T">Proses Tanda Tangan</asp:ListItem>
                            <asp:ListItem Value="S">Selesai</asp:ListItem>
                        </asp:RadioButtonList>
                        <br />
                        <table cellspacing="3" cellpadding="2">
                            <tr>
                                <td>
                                    <h5><i>No PPJB System </i></h5>
                                </td>
                                <td width="6px">:
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="noppjb"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <h5><i>No PPJB Manual </i></h5>
                                </td>
                                <td width="6px">:
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="noppjbm"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <h5><i>Tanggal PPJB </i></h5>
                                </td>
                                <td width="6px">:
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="TglPPJB"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <h5><i>Tanggal Cetak</i> </h5>
                                </td>
                                <td width="6px">:
                                </td>
                                <td>
                                    <asp:TextBox ID="tglcetak" runat="server" Width="85" CssClass="txt_center" Enabled="false"></asp:TextBox>
                                    <label for="tglcetak" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <h5><i>Tanggal Tanda Tangan </i></h5>
                                </td>
                                <td width="6px">:
                                </td>
                                <td>
                                    <asp:TextBox ID="tglttd" runat="server" Width="85" CssClass="txt_center" Enabled="false"></asp:TextBox>
                                    <label for="tglttd" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                </td>
                            </tr>
                        </table>
                        <span id="jbm" runat="server">
                            <h5>Nomor PPJB yang digunakan :</h5>
                            <asp:RadioButtonList ID="ppjbused" runat="server" Enabled="false">
                                <asp:ListItem Value="1">Manual</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">System</asp:ListItem>
                            </asp:RadioButtonList>
                        </span>
                    </td>
                    <td width="50%" valign="top">
                        <h2>Kelengkapan Berkas</h2>
                        <br />
                        <table>
                            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script language="javascript" type="text/javascript">
        function call(nova) {
            document.getElementById('nova').value = nova;
        }
    </script>

</body>
</html>

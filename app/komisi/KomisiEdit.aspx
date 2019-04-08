<%@ Page Language="c#" Inherits="ISC064.KOMISI.KomisiEdit" CodeFile="KomisiEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadKom" Src="HeadKom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKom" Src="NavKom.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Edit Komisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Komisi - Edit Komisi">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavKom ID="NavKom" runat="server" Aktif="1"></uc1:NavKom>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadKom ID="HeadKom" runat="server"></uc1:HeadKom>
                <table cellspacing="5">
                    <tr>
                        <td width="100%"></td>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                    accesskey="l">
                            </label>
                        </td>
                    </tr>
                </table>
                <p class="feed" style="padding-left: 5">
                    <asp:Label ID="feed" runat="server"></asp:Label>
                </p>
                <table>
                    <tr>
                        <td><b>Project</b></td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="project" runat="server" Width="250" CssClass="txt" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Skema</b></td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="skema" runat="server" Width="250" CssClass="txt" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Termin</b></td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="termin" runat="server" Width="250" CssClass="txt" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><b>No. Kontrak</b></td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="nokontrak" runat="server" Width="250" CssClass="txt" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><b>No. Unit</b></td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="nounit" runat="server" Width="250" CssClass="txt" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Sales</b></td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="sales" runat="server" Width="250" CssClass="txt" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Customer</b></td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="cust" runat="server" Width="250" CssClass="txt" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Tgl. Generate</b></td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="tgl" runat="server" Width="250" CssClass="txt" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Table ID="tb" runat="server" CssClass="tb blue-skin" CellSpacing="1">
                    <asp:TableRow>
                        <asp:TableHeaderCell ColumnSpan="5"><span style="font-size:large;">Detail</span></asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>No.</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Nama</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="right">Nilai</asp:TableHeaderCell>
                    </asp:TableRow>
                </asp:Table>
                <br />
                <table height="50">
                    <tr>
                        <td>
                            <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>

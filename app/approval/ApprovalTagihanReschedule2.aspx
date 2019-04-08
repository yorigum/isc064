<%@ Page Language="c#" Inherits="ISC064.APPROVAL.ApprovalTagihanReschedule2"
    CodeFile="ApprovalTagihanReschedule2.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Approval Reschedule Invoice</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Approval Reschedule Invoice (2)">
</head>
<body>
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Approval Reschedule Tagihan</h1>        
        <br>
        <table cellspacing="0" cellpadding="0">
            <tr valign="top">
                <td>
                    <table>
                        <tr>
                            <td>No. Kontrak
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label ID="nokontrak" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Total Tagihan<br />
                            </td>
                            <td>:
                            </td>
                            <td colspan="6">Rp.
                                <asp:Label ID="netto" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>Tanggal Approval</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="tglot" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                <%--<input type="button" value="&#xf073;" style="font-family: 'fontawesome'" class="btn" onclick="openCalendar('tglot')"/>--%>
                                <label for="tglot" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                <asp:Label ID="tglotc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">Note</td>
                            <td style="vertical-align: top">:</td>
                            <td>
                                <asp:TextBox ID="note" runat="server" TextMode="MultiLine" Height="70px" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
        </table>        <br>
        <p style="padding: 5;">
            <u>Jadwal tagihan yang belum di setujui</u>
        </p>
        <table width="100%">
            <tr>
                <td>
                    <br>
                    <div id="divTagihanold" runat="server" style="float: left">
                        <p style="font: 8pt; padding-left: 3">
                        </p>
                        <asp:Table ID="rptTagihanOld" runat="server" CssClass="tb" CellSpacing="3">
                            <asp:TableRow>
                                <asp:TableHeaderCell>No.</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Tipe</asp:TableHeaderCell>
                                <asp:TableHeaderCell Width="150">Nama Tagihan</asp:TableHeaderCell>
                                <asp:TableHeaderCell Width="60">Tanggal</asp:TableHeaderCell>
                                <asp:TableHeaderCell Width="150">Nilai Tagihan (Rp)</asp:TableHeaderCell>
                            </asp:TableRow>
                        </asp:Table>
                        <br />
                        <div>
                            Total : Rp. <b>
                                <asp:Label ID="grandTotalold" runat="server" /></b>
                        </div>
                    </div>
                    <div id="divTagihannew" runat="server" style="float: right">
                        <p style="font: 8pt; padding-left: 3">
                        </p>
                        <asp:Table ID="rptTagihanNew" runat="server" CssClass="tb" CellSpacing="3">
                            <asp:TableRow>
                                <asp:TableHeaderCell>No.</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Tipe</asp:TableHeaderCell>
                                <asp:TableHeaderCell Width="150">Nama Tagihan</asp:TableHeaderCell>
                                <asp:TableHeaderCell Width="60">Tanggal</asp:TableHeaderCell>
                                <asp:TableHeaderCell Width="150">Nilai Tagihan (Rp)</asp:TableHeaderCell>
                            </asp:TableRow>
                        </asp:Table>
                        <br />
                        <div>
                            Total : Rp. <b>
                                <asp:Label ID="grandTotalnew" runat="server" /></b>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <div>
            <table>
                <tr>
                    <td>
                        <%--<asp:Button ID="save" runat="server" CssClass="btn btn-blue" Text="OK" Width="100" OnClick="save_Click">
                    </asp:Button>--%>
                    <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-share"></i> Approve </asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="cancel" runat="server" CssClass="btn btn-red" Width="75" AccessKey="a" OnClick="Batal_Click"><i class="fa fa-share"></i> Reject </asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

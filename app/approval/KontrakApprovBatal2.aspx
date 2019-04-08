<%@ Page Language="C#" CodeFile="KontrakApprovBatal2.aspx.cs" Inherits="ISC064.APPROVAL.KontrakApprovBatal2" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html>
<head>
    <title>Approval Pembatalan Kontrak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Approval Pembatalan Kontrak">
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none" />
        <div id="frm" runat="server">
            <h1 class="title title-line">Approval Pembatalan Kontrak</h1>
            <br />
            <%--            <asp:ScriptManager ID="scriptmanager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
            <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
            <div>
                <p class="feed">
                    <asp:Label ID="feed" runat="server"></asp:Label>
                </p>
                <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid; border-bottom: #dcdcdc 1px solid">
                    <%--<tr>
                        <td><b>Tanggal Approval</b></td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="tglot" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                            <label for="tglot" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                            <asp:Label ID="tglotc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Project</b></td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="project">
                            </asp:DropDownList>
                        </td>
                    </tr>--%>
                </table>
            </div>
            <br />
            <div>
                <table cellspacing="5">
                    <tr>
                        <td>No. Kontrak</td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="nokontrak" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Unit</td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="unit" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Customer</td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="customer" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Sales</td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="agent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Alasan Batal</td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="alasan" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Keterangan</td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="ket" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Biaya Administrasi
                        </td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="biayaadmin" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Total Pelunasan
                        </td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="totallunas" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Tgl Pengembalian</td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="tglkembali" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Total Pengembalian</td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="totalkembali" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Nilai Klaim</td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="nilaiklaim" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">Note</td>
                        <td style="vertical-align: top">:</td>
                        <td>
                            <asp:TextBox ID="note" runat="server" TextMode="MultiLine" Height="70px" Width="300px"></asp:TextBox></td>
                    </tr>
                </table>
                <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click" AccessKey="s">
					<i class="fa fa-share"></i> Approve
                </asp:LinkButton>
                <asp:LinkButton ID="reject" runat="server" Width="75" OnClick="reject_Click" CssClass="btn btn-red"
                    AccessKey="d">
                    <i class="fa fa-share"></i> Reject
                </asp:LinkButton>
            </div>
            <%--</ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="save" />
                    <asp:PostBackTrigger ControlID="reject" />
                </Triggers>
            </asp:UpdatePanel>--%>
        </div>
        <script type="text/javascript">
            function call(NoKontrak) {
                popLog2(NoKontrak);
            }
        </script>
    </form>
</body>
</html>

<%@ Page Language="C#" CodeFile="KontrakApprovCustomTagihan2.aspx.cs" Inherits="ISC064.APPROVAL.KontrakApprovCustomTagihan2" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html>
<head>
    <title>Approval Customize Tagihan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Approval Customize Tagihan">
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none" />
        <div id="frm" runat="server">
            <h1 class="title title-line">Approval Customize Tagihan</h1>
            <br />
            <table width="100%">
                <tr>
                    <td style="width: 10%">
                        <table>
                            <tr>
                                <td>
                                    <label class="ibtn ibtn-file" style="display: none">
                                        <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                            accesskey="l">
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table cellspacing="5">
                <tr>
                    <td>No. Kontrak</td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="nokontrak" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Unit
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Customer
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Sales
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Skema Sebelum
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="skemabfr" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Skema Setelah
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="skemaaft" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Cara Bayar Sebelum
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="carabayarbfr" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Cara Bayar Setelah
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="carabayaraft" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">Tanggal Pengalihan Hak</td>
                    <td style="vertical-align: top">:</td>
                    <td>
                        <asp:Label ID="tglpengajuan" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td style="vertical-align: top">Note</td>
                    <td style="vertical-align: top">:</td>
                    <td>
                        <asp:TextBox ID="note" runat="server" TextMode="MultiLine" Height="70px" Width="300px"></asp:TextBox></td>
                </tr>
            </table>
            <asp:LinkButton ID="save" runat="server" Width="75" OnClick="save_Click" CssClass="btn btn-blue" AccessKey="s"><i class="fa fa-share"></i> Approve</asp:LinkButton>
            <asp:LinkButton ID="reject" runat="server" Width="75" OnClick="reject_Click" CssClass="btn btn-red" AccessKey="d"><i class="fa fa-share"></i> Reject</asp:LinkButton>
        </div>
        <script type="text/javascript">
            function checkCtrl(foo, n) {
                var x = true; var i = 0;
                while (x) {
                    if (document.getElementById(foo + "_" + i)) {
                        if (!document.getElementById(foo + "_" + i).disabled) {
                            if (n == "true")
                                document.getElementById(foo + "_" + i).checked = true;
                            else
                                document.getElementById(foo + "_" + i).checked = false;
                        }
                        i++;
                    } else { x = false; }
                }
            }
            init();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(init);
        </script>
    </form>
</body>
</html>

<%@ Page Language="c#" Inherits="ISC064.KOMISI.RewardPRegis1" CodeFile="RewardPRegis1.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Pengajuan Pencairan Reward</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Pengajuan Pencairan Reward - Registrasi">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Pengajuan Pencairan Reward</h1>
        <br>
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td><b>Dari</b></td>
                <td>
                    <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="100"></asp:TextBox>
                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                </td>
                <td><b>Sampai</b></td>
                <td>
                    <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="100"></asp:TextBox>
                    <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="project" runat="server" Width="200">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br>
        <%--<div class="peach">
            Status : A = Aktif / B = Batal
        </div>--%>
        <table class="tb blue-skin" cellspacing="1">
            <tr align="left" valign="bottom">
                <th></th>
                <th>Kode</th>
                <th>Sales</th>
                <th>Nama Skema</th>
                <th>Periode</th>
                <th>Reward</th>
            </tr>
            <tr>
                <td colspan="6">
                    <ul class="floatsm">
                        <li><a href="javascript:checkCtrl('cb','true')">Check&nbsp;&nbsp;&nbsp;&nbsp;</a></li>
                        <li><a href="javascript:checkCtrl('cb','false')">Uncheck</a></li>
                    </ul>
                    <br />
                </td>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </table>
        <table cellspacing="5">
            <tr valign="top">
                <td width="20%">Tgl. Pengajuan</td>
                <td width="1%">:</td>
                <td>
                    <nobr>
						<asp:textbox id="tgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
                        <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>                                            
					</nobr>
                    <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr valign="top">
                <td width="20%">Keterangan</td>
                <td width="1%">:</td>
                <td>
                    <asp:TextBox ID="ket" runat="server" Width="350" Height="150" TextMode="MultiLine" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
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
        </script>
    </form>
</body>
</html>

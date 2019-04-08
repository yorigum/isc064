<%@ Page Language="c#" Inherits="ISC064.KOMISI.CFPRegis1" CodeFile="CFPRegis1.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Pengajuan Pencairan Closing Fee</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Pengajuan Pencairan Closing Fee - Registrasi">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Pengajuan Pencairan Closing Fee</h1>
        <br />
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td>
                    <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                            <asp:ListItem>Pilih Project :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="tipesales" runat="server" Width="300" AutoPostBack="true" OnSelectedIndexChanged="tipesales_SelectedIndexChanged">
                        <asp:ListItem Value="0">Tipe Marketing :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td id="trSales" runat="server">
                    <asp:DropDownList ID="sales" runat="server" Width="200">
                        <asp:ListItem Value="0">Nama :</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3"><asp:label id="projectc" runat="server" cssclass="err"></asp:label></td>
            </tr>
        </table>

        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td>Tgl. Generate : </td>
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
                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>&nbsp;
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>

        <table>
            <tr>
                <td colspan="5" style="padding-left: 10px">
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
            </tr>
        </table>
        <br>
        <%--<div class="peach">
            Status : A = Aktif / B = Batal
        </div>--%>
        <table id="tbHead" runat="server" cellspacing="5" visible="false">
            <tr>
                <td style="width:8%">Periode</td>
                <td style="width:2%">:</td>
                <td><asp:Label ID="headperiode" runat="server"></asp:Label></td>
            </tr>
            <tr id="trTipeSales" runat="server" visible="false">
                <td>Tipe</td>
                <td>:</td>
                <td><asp:Label ID="headtipe" runat="server"></asp:Label></td>
            </tr>
            <tr id="trNama" runat="server" visible="false">
                <td>Nama</td>
                <td>:</td>
                <td><asp:Label ID="headnama" runat="server"></asp:Label></td>
            </tr>
        </table>

        <br />
        <table class="tb blue-skin" cellspacing="1">
            <tr align="left" valign="bottom">
                <th></th>
                <th>No. Generate Closing Fee</th>
                <th>No. Kontrak</th>
                <th>Unit</th>
                <th>Customer</th>
                <th>Sales</th>
                <th class="right">Nilai</th>
            </tr>
            <tr>
                <td colspan="7">
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

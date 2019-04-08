<%@ Page Language="c#" Inherits="ISC064.KOMISI.CFRegis1" CodeFile="CFRegis1.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Generate Closing Fee</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Closing Fee - Generate Closing Fee (Step 1)">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Generate Closing Fee</h1>
        <br>
        <asp:ScriptManager runat="server" ID="script" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="update" UpdateMode="Conditional">
            <ContentTemplate>

                <table style="border: 1px solid #DCDCDC" cellspacing="5">
                    <tr>
                        <td>
                            <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                    <asp:ListItem>Pilih Project :</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="tipesales" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="tipesales_SelectedIndexChanged">
                                <asp:ListItem Value="0">Tipe Marketing :</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="sales" runat="server" Width="250">
                                <asp:ListItem Value="0">Nama :</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"><asp:label id="projectc" runat="server" cssclass="err"></asp:label></td>
                    </tr>
                </table>

                <br />
                <table style="border: 1px solid #DCDCDC" cellspacing="5">
                    <tr>
                        <td>
                            <asp:DropDownList ID="skema" runat="server" Width="375" AutoPostBack="true" OnSelectedIndexChanged="skema_SelectedIndexChanged">
                                <asp:ListItem Value="0">Skema :</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>

                <br />
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
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-left: 10px">
                            <p class="feed">
                                <asp:Label ID="feed" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>
                </table>
                <br />

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

                <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
                    <asp:TableRow HorizontalAlign="Left" VerticalAlign="Bottom">
                        <asp:TableHeaderCell>Sales</asp:TableHeaderCell>
                        <asp:TableHeaderCell>No. Kontrak</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Unit</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Customer</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Skema</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="right">Dasar Perhitungan</asp:TableHeaderCell>
                    </asp:TableRow>
                </asp:Table>
                <table cellspacing="5">
                    <tr>
                        <td>
                            <asp:Button ID="hitung" runat="server" CssClass="btn btn-blue" Text="Hitung" OnClick="hitung_Click"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="alert" runat="server" CssClass="err"></asp:Label></td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="hitung" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>

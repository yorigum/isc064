<%@ Page Language="c#" Inherits="ISC064.KOMISI.CFDel" CodeFile="CFDel.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Clear False Closing Fee</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Closing Fee - Clear False Closing Fee">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Clear False Closing Fee</h1>
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
                            <asp:DropDownList ID="tipesales" runat="server" Width="300" AutoPostBack="true" OnSelectedIndexChanged="tipesales_SelectedIndexChanged">
                                <asp:ListItem Value="0">Tipe Marketing :</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td id="trSales" runat="server">
                            <asp:DropDownList ID="sales" runat="server" Width="200">
                                <asp:ListItem Value="">Nama :</asp:ListItem>
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
                        <td colspan="5">
                            <asp:DropDownList ID="skema" runat="server" Width="375" AutoPostBack="true" OnSelectedIndexChanged="skema_SelectedIndexChanged">
                                <asp:ListItem Value="0">Skema :</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
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
                <%--<div class="peach">
                    Status : A = Aktif / B = Batal
                </div>--%>
                <br />
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
                <table class="tb blue-skin" cellspacing="1">
                    <tr align="left" valign="bottom">
                        <th>Sales</th>
                        <th>No. Kontrak</th>
                        <th>Unit</th>
                        <th>Customer</th>
                        <th>Skema</th>
                        <th class="right">Dasar Perhitungan</th>
                    </tr>
                    <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                </table>
                <table cellspacing="5">
                    <tr valign="top">
                        <td width="20%">Alasan</td>
                        <td width="1%">:</td>
                        <td>
                            <asp:TextBox ID="alasan" runat="server" Width="350" Height="150" TextMode="MultiLine" CssClass="txt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="del" runat="server" CssClass="btn btn-red" Text="Delete" OnClick="del_Click"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"><asp:Label ID="alert" runat="server" CssClass="err"></asp:Label></td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="display" />
                <asp:PostBackTrigger ControlID="del" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>

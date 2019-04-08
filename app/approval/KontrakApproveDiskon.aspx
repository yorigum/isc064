<%@ Page Language="c#" Inherits="ISC064.APPROVAL.KontrakApproveDiskon" CodeFile="KontrakApproveDiskon.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Approval Kontrak Diskon</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Approval Pengalihan Hak">
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none" />
        <div id="frm" runat="server">
            <h1 class="title title-line">Approval Kontrak Diskon</h1>
            <br />
            <asp:ScriptManager ID="scriptmanager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
            <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="border: 1px solid #DCDCDC" cellspacing="5">
                        <tr style="display: none;">
                            <td><b>Search by</b></td>
                            <td>
                                <input type="button" class="btn btn-blue" value="Search" onclick="popDaftarApprov();" id="search"
                                    runat="server" name="search" accesskey="s"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td><b>View by</b></td>
                            <td>
                                <asp:DropDownList ID="thnKontrak" runat="server" Width="200">
                                    <asp:ListItem Value="">Periode Kontrak :</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="lokasi" runat="server" Width="200">
                                    <asp:ListItem Value="">Lokasi :</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="project" runat="server" Width="200" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
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
                    <asp:Label ID="feed" runat="server" />
                    <br>
                    <div class="peach">
                        Status : A = Aktif / B = Batal
                    </div>
                    <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="" DataField="Nav" />
                            <asp:BoundField HeaderText="No. Approval" DataField="Approval" />
                            <%--<asp:BoundField HeaderText="No. Kontrak" DataField="Kontrak" />--%>
                            <asp:BoundField HeaderText="Tgl. Kontrak" DataField="Tgl" />
                            <asp:BoundField HeaderText="Unit " DataField="Unit" />
                            <asp:BoundField HeaderText="Customer" DataField="Customer" />
                            <asp:BoundField HeaderText="Agent" DataField="Agent" />
                            <asp:BoundField HeaderText="Project" DataField="Project" />
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>

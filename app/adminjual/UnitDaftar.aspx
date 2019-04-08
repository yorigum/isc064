<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.UnitDaftar" CodeFile="UnitDaftar.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Pendaftaran Unit Baru</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Pendaftaran Unit Baru">
</head>
<body class="body-padding">
    <form class="cnt" id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Pendaftaran Unit Baru</h1>
        <br>
        <asp:ScriptManager ID="scriptmanager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        <table cellspacing="0">
            <tr valign="top">
                <td style="padding-right: 10px">
                    <p>
                        <b>Terbaru :</b>
                    </p>
                    <asp:ListBox ID="baru" runat="server" CssClass="ddl" Width="200" Rows="25"></asp:ListBox>
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
                <td style="padding-right: 10px; padding-left: 15px; padding-bottom: 0px; padding-top: 5px">
                    <img src="/Media/line_vert.gif">
                </td>
                <td>
                    <asp:Label ID="norefjenis" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label><br />
                    <asp:Label ID="noreflokasi" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                    <table cellspacing="5">
                        <tr>
                            <td colspan="3">
                                <p>
                                    <b>Dokumen</b>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Stock
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nostock" runat="server" CssClass="txt" Width="65" Font-Bold="True"
                                    Text="#AUTO#" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Project
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <br />
                                <p>
                                    <b>Tipe Unit</b>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:RadioButtonList ID="jenis" runat="server" RepeatColumns="2">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <br />
                                <p>
                                    <b>Tipe Properti</b>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:RadioButtonList ID="tipe" runat="server" RepeatColumns="2">
               
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <br />
                                <p>
                                    <b>Kategori Unit</b>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:RadioButtonList ID="kategori" runat="server" RepeatColumns="2">
                                    <asp:ListItem Value="FLPP" class="radio" Selected="True">FLPP</asp:ListItem>
                                    <asp:ListItem Value="KOMERSIL" class="radio">Komersil</asp:ListItem>
                                    <asp:ListItem Value="REAL ESTATE" class="radio">Real Estate</asp:ListItem>
                                    <asp:ListItem Value="HIGH RISE" class="radio">High Rise</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>Lokasi
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:DropDownList ID="lokasi" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>Unit
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nounit" runat="server" CssClass="txt" Width="120" MaxLength="6" MinLength="6"></asp:TextBox><font
                                    style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">Contoh format : LG/A/001</font>
                                <%--<asp:RegularExpressionValidator display="dynamic" ControlToValidate="nounit" ID="nounitr" ValidationExpression="^[\s\S]{6,6}$" runat="server" ErrorMessage="Minimum 6 And Maximum 6 characters required."></asp:RegularExpressionValidator>
                                <asp:Label ID="nounitc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>Blok
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="lantai" runat="server" CssClass="txt" Width="120" MaxLength="6"></asp:TextBox><font
                                    style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">Contoh format : A01</font>
                                <asp:Label ID="lantaic" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Nomor
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nomor" runat="server" CssClass="txt" Width="120" MaxLength="6"></asp:TextBox><font
                                    style="font-weight: normal; font-size: 8pt; line-height: normal; font-style: normal; font-variant: normal">Contoh format : 001</font>            
                                <asp:Label ID="nomorc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Luas Tanah
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="luassg" runat="server" CssClass="txt_num" Width="75"></asp:TextBox>m2
                            <asp:Label ID="luassgc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Luas Lebih Tanah
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="luaslbh" runat="server" CssClass="txt_num" Width="75"></asp:TextBox>m2
                            <asp:Label ID="luaslbhc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Luas Bangunan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="luasnett" runat="server" CssClass="txt_num" Width="75"></asp:TextBox>m2
                            <asp:Label ID="luasnettc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="height: 50px">
                        <tr>
                            <td>
                                <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="save" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>

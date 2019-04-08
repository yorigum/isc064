<%@ Page Language="c#" Inherits="ISC064.SETTINGS.HtmlEditorEdit" CodeFile="HtmlEditorEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Html Editor - Edit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="/Media/Style.css" type="text/css" rel="stylesheet" />
    <meta name="ctrl" content="1">
    <meta name="sec" content="Html Editor - Edit">
    <script type="text/javascript" src="/Js/ckeditor/ckeditor.js"></script>
    <meta http-equiv="pragma" content="no-cache" />
    <base target="_self" />
</head>
<body class="body-padding">
    <form id="form1" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Html Editor - Edit</h1>
					<table cellspacing="5">
						<tr>
							<td width="100%"></td>
							<td>
                                <label class="ibtn ibtn-file">
								    <input type="button" class="btn btn-blue btn-ico" value="Log" id="btnlog" runat="server" name="btnlog" accesskey="l">
                                </label>
							</td>
						</tr>
					</table>
        <table>
            <tr>
                <td style="min-width: 80px">Halaman
                </td>
                <td>:</td>
                <td>
                    <asp:Label ID="halaman" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Project</td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="project" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Modul
                </td>
                <td>:</td>
                <td>
                    <asp:Label ID="modul" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">Isi
                </td>
                <td style="vertical-align: top">:</td>
                <td>
                    <asp:TextBox ID="html" runat="server" TextMode="MultiLine" CssClass="ckeditor"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
                <td>
                    <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" AccessKey="a"
                        OnClick="save_Click"><i class="fa fa-share"></i>OK</asp:LinkButton>
                    <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel"
                        style="width: 75px">
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
            </tr>
        </table>
        <table class="tb blue-skin">
            <tr>
                <th>Parameter</th>
                <th>Keterangan</th>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </table>
    </form>
</body>
</html>

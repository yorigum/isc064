<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.ExecutiveSummary" CodeFile="ExecutiveSummary.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Management Report</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Management Report">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" class="title title-line" runat="server">Management Report
                    </h1>
                    <br />
                    <div class="content">
                        <table>
                            <tr>
                                <td>Project</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="project" runat="server" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Perusahaan</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="pers" runat="server">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" AccessKey="s" runat="server" CssClass="btn btn-blue" OnClick="scr_Click">
											<i class="fa fa-search"></i> Screen Preview
                                    </asp:LinkButton>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>                                    
                                    <asp:LinkButton ID="pdf" runat="server" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click">
											Download PDF
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <asp:PlaceHolder ID="rpt" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>

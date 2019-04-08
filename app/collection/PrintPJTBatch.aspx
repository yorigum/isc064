<%@ Reference Control="~/PrintPJTTemplate.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.COLLECTION.PrintPJTBatch" CodeFile="PrintPJTBatch.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Print Pemberitahuan Jatuh Tempo</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Print.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Print Pemberitahuan Jatuh Tempo (Batch)">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup layar print ini?')) window.close();">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <div id="reprint" runat="server">
            <h1 class="title" style="border-bottom: 1px solid silver">Print Invoice</h1>
            <table>
                <tr>
                    <td style="padding-top: 20px;"><b>Tanggal Periode :</b></td>
                    <td>
                        <div class="input-group input-medium">
                            <asp:TextBox ID="dari" runat="server" type="text" CssClass="form-control" Style="width: 50%" Height="20"></asp:TextBox>
                            <span class="input-group-btn" style="height: 34px; display: block">
                                <button class="btn-a default" runat="server" type="button" style="height: 100%">
                                    <i class="fa fa-calendar"></i>
                                </button>
                            </span>
                        </div>
                    </td>
                    <td style="padding-top: 20px;"><b>Project :</b></td>
                    <td>
                        <div class="input-group input-medium">
                            <asp:DropDownList runat="server" ID="project" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label></td>
                </tr>
            </table>
            <br>
            <br />
            <br />
            <br />
            <div class="ins">
                <asp:Button ID="print" runat="server" CssClass="btn-submit button-submit2" Text="Print" Width="75" AccessKey="p" OnClick="print_Click"></asp:Button>
            </div>
            <br>
            <p style="padding: 5px"><u>Pemberitahuan Jatuh Tempo Belum Print</u></p>
            <asp:Table ID="belumprint" runat="server" CssClass="tb" CellSpacing="5"></asp:Table>
        </div>
        <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>

<%@ Page Language="c#" Inherits="ISC064.SECURITY.ApprovalDetail" CodeFile="ApprovalDetail.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Approval</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Setup - Approval Detail">
    <style type="text/css">
        .list td {
            width: 220px;
            padding-left: 50px;
        }

        h2 {
            font: bold 10pt
        }

        .pedit {
            font: bold 14pt
        }
    </style>
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Setup Approval</h1>
        <h2 id="tipeapproval" runat="server"></h2>
        <a id="anew" runat="server" visible="false">Registrasi</a>
        <br>
        <div style="font-size: large; margin: 5px; padding: 20px">
            <asp:Table ID="rpt" runat="server" CssClass="blue-skin tb" CellSpacing="1">
                <asp:TableRow HorizontalAlign="Left">
                    <asp:TableHeaderCell>Level</asp:TableHeaderCell>
                    <asp:TableHeaderCell>User ID</asp:TableHeaderCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>

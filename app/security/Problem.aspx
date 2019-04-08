<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Problem.aspx.cs" Inherits="ISC064.SECURITY.Problem" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Problem</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Problem">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Problem</h1>
        <div>
            <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="ID" />
                    <asp:BoundField HeaderText="Halaman" DataField="Halaman" />
                    <asp:BoundField HeaderText="Tanggal" DataField="Tanggal" />
                </Columns>
            </asp:GridView>
        </div>
        <script type="text/javascript">
            function call(id) {
                openPopUp('ProblemFile.aspx?id=' + id, 1000, 600);
            }
        </script>
    </form>
</body>
</html>

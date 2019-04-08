<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.Migrated_Peta" CodeFile="Peta.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Peta Floor Plan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Setup Peta Floor Plan">
    <style>
        .listitem ul {
            list-style: square;
            font-size: 16px;
            margin-left: 3px !important;
        }

        .listitem a {
            text-decoration: none;
        }

        .listitem .link {
            font-weight: bold;
            color: #494949 !important;
        }
    </style>
</head>
<body class="default-content">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1>Peta Site Plan</h1>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <p style="font: bold 10pt">
            <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
            </asp:DropDownList>
        </p>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb" CellSpacing="3">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell Width="300">File Floor Plan</asp:TableHeaderCell>
                <asp:TableHeaderCell></asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <div class="listitem">
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </div>
        <script language="javascript">

            function New(nomor) {
                openPopUp('/adminjual/PetaDaftar.aspx?ParentID=' + nomor, '500', '400')
            }
            function Edit(nomor) {
                openPopUp('/adminjual/PetaEdit.aspx?id=' + nomor, '900', '650')
            }
            function call(f) {
                popPeta(f);
            }
            function hapus(f) {
                if (confirm('Hapus Floor Plan ini dan file gambarnya dari server?\nPerhatian bahwa proses ini TIDAK bisa dibalik.'))
                    location.href = 'PetaDel.aspx?f=' + f;
            }
        </script>
    </form>
</body>
</html>

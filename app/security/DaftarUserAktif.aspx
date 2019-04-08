<%@ Page language="c#" Inherits="ISC064.SECURITY.DaftarUserAktif" CodeFile="DaftarUserAktif.aspx.cs" %>
<!DOCTYPE html>
<html>
<head>
    <title>Daftar User Aktif</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="(Pop-Up) Daftar User Aktif">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="body-padding" class="pop" onkeyup="if(event.keyCode==27)window.close()">
    <form id="Form1" method="post" runat="server">
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="Nama" DataField="Nama" />
                <asp:BoundField HeaderText="Kode " DataField="Kode" />
                <asp:BoundField HeaderText="Sec. Level " DataField="SecLevel" />
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
            function call(userid) {
                window.opener.call(userid);
                window.close();
            }
        </script>
    </form>
</body>
</html>

<%@ Page language="c#" Inherits="ISC064.SECURITY.Absensi" CodeFile="Absensi.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Absensi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Absensi">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Tabel Absensi Masih Aktif</h1>
        <h2>
            <asp:Label ID="date" runat="server"></asp:Label></h2>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="Jam Masuk" DataField="Jam" />
                <asp:BoundField HeaderText="Kode User" DataField="Kode" />
                <asp:BoundField HeaderText="Nama User" DataField="Nama" />
                <asp:BoundField HeaderText="Security Level " DataField="SecLevel" />
                <asp:BoundField HeaderText="IP Address" DataField="IP" />
                <asp:BoundField HeaderText="" DataField="Kick" />
            </Columns>
        </asp:GridView>
        <p style="padding-left: 3">
            <a href="#" onclick="openPopUp('TabelAbsensi.aspx', '955', '650');">Tabel Absensi 
					Lengkap...</a>
        </p>
        <br>
        <p style="font: 8pt">
            Tips : Proses KICK akan menon-aktifkan satu buah absensi secara manual.
        </p>
        <script type="text/javascript">
            function kick(nomor) {
                if (confirm('Lanjutkan proses KICK username?\nUser tersebut akan sign-out otomatis.')) {
                    location.href = 'Kick.aspx?LogID=' + nomor;
                }
            }
        </script>
    </form>
</body>
</html>

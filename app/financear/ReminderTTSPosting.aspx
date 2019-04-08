<%@ Page language="c#" Inherits="ISC064.FINANCEAR.ReminderTTSPosting" CodeFile="ReminderTTSPosting.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Reminder Posting Tanda Terima Sementara</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder - Posting Tanda Terima Sementara">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder Posting Tanda Terima Sementara</h1>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <br />
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. TTS" DataField="TTS" ItemStyle-VerticalAlign="Top"/>
                <asp:BoundField HeaderText="Tgl. / Kasir" DataField="Tgl" ItemStyle-VerticalAlign="Top"/>
                <asp:BoundField HeaderText="Customer" DataField="Cs" ItemStyle-VerticalAlign="Top"/>
                <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" ItemStyle-VerticalAlign="Top"/>
                <asp:BoundField HeaderText="Cara Bayar" DataField="Bayar" ItemStyle-VerticalAlign="Top"/>
                <asp:BoundField HeaderText="Total" DataField="Total" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" />
            </Columns>
        </asp:GridView>
        <label runat="server" id="kosong"></label>
        <table height="50">
            <tr>
                <td>
                    <a href="" runat="server" id="ok" type="button" class="btn btn-blue t-white" style="width: 75px">
                        <i class="fa fa-share"></i>OK
                    </a>
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function call(nomor) {
                popEditTTS(nomor);
            }
        </script>
		</form>
	</body>
</html>
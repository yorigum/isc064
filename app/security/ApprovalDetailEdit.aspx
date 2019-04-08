<%@ Page Language="c#" Inherits="ISC064.SECURITY.ApprovalDetailEdit" CodeFile="ApprovalDetailEdit.aspx.cs" %>

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
    <meta name="sec" content="Setup - Approval Detail Edit">
    <style type="text/css">
        .list td {
            width: 220px;
            padding-left: 50px;
        }

        h2 {
            font: bold 10pt;
        }

        .pedit {
            font: bold 14pt;
        }
    </style>
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Setup Approval</h1>
        <h2 id="keterangan" runat="server"></h2>
        <div style="font-size: large; margin: 5px; padding: 20px">
            <table cellspacing="5" class="tb">
                <tr align="left">
                    <th>UserID</th>
                    <th colspan="2">Action</th>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                <tr>
                    <td>
                        <p>
                            <asp:LinkButton ID="add" runat="server" OnClick="add_Click" AccessKey="t">+ Tambah Baris</asp:LinkButton>
                            <asp:LinkButton ID="add3" runat="server" OnClick="add3_Click" AccessKey="t">+ Tambah 3 Baris</asp:LinkButton>
                        </p>
                    </td>
                </tr>
            </table>
            <asp:Button ID="next" runat="server" CssClass="btn btn-blue" Text="Next" OnClick="next_Click"></asp:Button>
            <asp:Button ID="finish" runat="server" CssClass="btn btn-blue" Text="Finish" OnClick="finish_Click"></asp:Button>
        </div>
        <script type="text/javascript">
            function popDaftarAktif2(ctrl1, ctrl2) {
                foo1 = document.getElementById(ctrl1);
                foo2 = document.getElementById(ctrl2);

                var rl = openModal(
                    "/security/DaftarUserAktif2.aspx?ctrl=" + ctrl1 + "&ctrl2=" + ctrl2, "",
                    "center:yes;dialogWidth:500px;dialogHeight:400px;help:no;status:no;");

            }
            function pilih(x, x2, ctrl1, ctrl2) {
                document.getElementById(ctrl1).value = x;
                document.getElementById(ctrl2).value = x2;
            }
            function hapusbaris(userid, tipe, lvl) {
                if (confirm('Hapus satu baris detail ini dari approval?\nPerhatian bahwa data akan dihapus secara PERMANEN.')) {
                    location.href = 'ApprovalDetailDel.aspx?userid=' + userid + '&tipe=' + tipe + '&lvl=' + lvl;
                }
            }
        </script>
    </form>
</body>
</html>


<%@ Page Language="c#" Inherits="ISC064.financear.VAUpload" CodeFile="VAUpload.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Import Transaksi Virtual Account</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Virtual Account - Import Transaksi">
</head>
<body class="body-padding">
    <form class="cnt" id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <span class="title title-line">Import Transaksi Virtual Account</span>
        <br />
        <table cellspacing="5">
            <tr>
                <td>
                    <b>Rekening Bank</b>
                </td>
                <td>:</td>
                <td>
                    <asp:DropDownList ID="bank" runat="server" Width="300">
                        <asp:ListItem Selected="True">- Pilih Rekening Bank -</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="bankc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>File Teks</b>
                </td>
                <td>:</td>
                <td>
                    <input id="txt" type="text" value="" style="width: 40%" />
                    <button type="button" class="btn-submit button-submit2" onclick="javascript:document.getElementById('file').click();">Upload</button>
                    <input runat="server" name="file" id="file" type="file" style='visibility: hidden;' onchange="ChangeText(this, 'txt');" />
                </td>
            </tr>
        </table>
        <table style="height: 50px">
            <tr>
                <td>
                    <asp:LinkButton ID="upload" runat="server" Width="75" CssClass="btn btn-blue" OnClick="upload_Click">Next <i class="fa fa-arrow-right"></i>
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </form>
</body>
<script type="text/javascript">
    function ChangeText(oFileInput, sTargetID) {
        document.getElementById(sTargetID).value = oFileInput.value;
    }
</script>
</html>

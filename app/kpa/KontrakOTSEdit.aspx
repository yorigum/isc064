<%@ Reference Page="~/Log.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.KPA.KontrakOTSEdit" CodeFile="KontrakOTSEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Input OTS</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Input OTS">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Kembali ke halaman proses KPR?')) document.getElementById('cancel').click()">

    <script type="text/javascript" src="/Js/Common.js"></script>

    <script type="text/javascript" src="/Js/NumberFormat.js"></script>

    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <div id="pilih" runat="server">
        <h1 class="title title-line">
            Input OTS</h1>
        <p>
            Halaman 1 dari 2</p>
        <br />
        <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid;
            border-bottom: #dcdcdc 1px solid" cellspacing="5">
            <tr>
                <td>
                    No. Kontrak :
                </td>
                <td>
                    <asp:TextBox ID="nokontrak" runat="server" Width="100" CssClass="txt"></asp:TextBox>
                    <input class="btn btn-orange" id="btnpop" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a&amp;kpr=1' type="button" value="&#xf002;" style="font-family: 'fontawesome'"
								name="btnpop" runat="server" />
                </td>
                <td>
                    <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" OnClick="next_Click">Next<i class="fa fa-arrow-right"></i>
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
    </div>
    <input type="text" style="display: none">
    <div id="frm" runat="server">
        <h1 class="title title-line">
            Input OTS</h1>
        <div id="pageof" runat="server">
            <p>
                Halaman 2 dari 2</p>
            <br />
        </div>
        <table cellspacing="5">
            <tr>
                <td>
                    No. Kontrak
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="kontrakno" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Unit
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Customer
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <br>
        <table cellspacing="5">
            <tr>
                <td>
                    Status
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:RadioButtonList ID="status" runat="server" CssClass="radio" RepeatDirection="Horizontal" AutoPostBack="True"
                        OnSelectedIndexChanged="status_SelectedIndexChanged">
                        <asp:ListItem style="padding-right: 30px">BELUM DITENTUKAN</asp:ListItem>
                        <asp:ListItem style="padding-right: 30px">DIJADWALKAN</asp:ListItem>
                        <asp:ListItem style="padding-right: 30px">SELESAI</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <table id="dijadwalkan" runat="server" cellspacing="5">
            <tr>
                <td>
                    Target OTS
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="target" runat="server" CssClass="txt_center tgl" Width="75" Font-Size="8"></asp:TextBox>
                    <label for="target" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="targetc" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="selesai" runat="server" cellspacing="5">
            <tr>
                <td>
                    Tgl. OTS
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="tgl" runat="server" CssClass="txt_center tgl" Width="75" Font-Size="8"></asp:TextBox>
                    <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="tglc" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Hasil OTS
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:RadioButtonList ID="hasil" runat="server" CssClass="radio" RepeatDirection="Horizontal">
                        <asp:ListItem Value="TOLAK" Selected="True" style="padding-right: 30px">  TOLAK</asp:ListItem>
                        <asp:ListItem Value="SETUJU" style="padding-right: 30px">  SETUJU</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    Keterangan
                </td>
                <td valign="top">
                    :
                </td>
                <td>
                    <asp:TextBox ID="ket" runat="server" CssClass="input-text" TextMode="MultiLine" Rows="5"
                        Columns="100"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table height="50">
            <tr>
                <td>
                    <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click">
                    <i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
                <td>
                    <input class="btn btn-red" id="cancel" style="width: 100px" type="button" value="Cancel" name="cancel"
                        runat="server">
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        function call(nokontrak) {
            document.getElementById('nokontrak').value = nokontrak;
            document.getElementById('next').click();
        }

    </script>

    </form>
</body>
</html>

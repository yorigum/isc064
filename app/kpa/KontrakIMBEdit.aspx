<%@ Reference Page="~/Log.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.KPA.KontrakIMBEdit" CodeFile="KontrakIMBEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Edit IMB</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Edit IMB">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Kembali ke halaman proses KPR?')) document.getElementById('cancel').click()">

    <script type="text/javascript" src="/Js/Common.js"></script>

    <script type="text/javascript" src="/Js/NumberFormat.js"></script>

    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input style="display: none">
        <div id="pilih" runat="server">
            <h1 class="title title-line">Edit IMB</h1>
            <p>
                Halaman 1 dari 2
            </p>
            <br />
            <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid; border-bottom: #dcdcdc 1px solid"
                cellspacing="5">
                <tr>
                    <td>No. Kontrak :
                    </td>
                    <td>
                        <asp:TextBox ID="nokontrak" runat="server" Width="100" CssClass="txt"></asp:TextBox>
                        <input type="button" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" onclick="popDaftarKontrak('a&amp;kpr=1')"
                            id="Button1" runat="server" name="btnpop" />
                    </td>
                    <td>
                        <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" OnClick="next_Click"> Next <i class="fa fa-arrow-right"></i>
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
            <p class="feed">
                <asp:Label ID="feed" runat="server"></asp:Label>
            </p>
        </div>
        <div id="frm" runat="server">
            <h1 class="title title-line">Edit IMB</h1>
            <p>
                Halaman 2 dari 2
            </p>
            <br />
            <table cellspacing="5">
                <tr>
                    <td>No. Kontrak
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="kontrakno" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Unit
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Customer
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
            <br>
            <table cellspacing="5">
                <tr>
                    <td>Status
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                            OnSelectedIndexChanged="rblStatus_SelectedIndexChanged">
                            <asp:ListItem Value="0">BELUM DIKELUARKAN</asp:ListItem>
                            <asp:ListItem Value="1">SELESAI</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <table id="selesai" runat="server" cellspacing="5">
                <tr>
                    <td>Tgl. IMB
                    </td>
                    <td>:
                    </td>
                    <td>
                        <div class="input-group input-medium">
                            <asp:TextBox ID="tbTgl" runat="server" type="text" CssClass="form-control" Style="width: 50%" Height="34"></asp:TextBox>
                            <span class="input-group-btn" style="height: 34px; display: block">
                                <button class="btn-a default" runat="server" onclick="openCalendar('tbTgl');" type="button" style="height: 100%">
                                    <i class="fa fa-calendar"></i>
                                </button>
                            </span>
                        </div>
                        <asp:Label ID="lblTgl" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>No. IMB
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="tbNoIMB" runat="server" CssClass="input-text" MaxLength="20" Width="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Keterangan IMB
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:TextBox ID="keteranganimb" runat="server" CssClass="input-text" MaxLength="1000" Width="300"
                            Height="80"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table height="50">
                <tr>
                    <td>
                        <asp:LinkButton ID="ok" runat="server" Width="50" CssClass="btn btn-blue" OnClick="ok_Click">
                    <i class="fa fa-share"></i> OK
                        </asp:LinkButton>
                    </td>
                    <td>
                        <input class="btn btn-red" id="cancel" style="width: 75px" type="button" value="Cancel" name="cancel"
                            runat="server">
                    </td>
                </tr>
            </table>
        </div>

        <script language="javascript" type="text/javascript">
            function call(nokontrak) {
                document.getElementById('nokontrak').value = nokontrak;
                document.getElementById('next').click();
            }

        </script>

    </form>
</body>
</html>

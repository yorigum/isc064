<%@ Reference Page="~/Log.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.KPA.KontrakAkadEdit" CodeFile="KontrakAkadEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Input Akad</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Input Akad">

    <script type="text/javascript" src="/Js/NumberFormat.js"></script>

</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Kembali ke halaman proses KPA?')) document.getElementById('cancel').click()">

    <script type="text/javascript" src="/Js/Common.js"></script>

    <script type="text/javascript" src="/Js/NumberFormat.js"></script>

    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div id="pilih" runat="server">
            <h1 class="title title-line">Input Akad</h1>
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
                        <input class="btn btn-orange" id="btnpop" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a&amp;kpr=1' type="button" value="&#xf002;" style="font-family: 'fontawesome'"
                            name="btnpop" runat="server" />
                    </td>
                    <td>
                        <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" OnClick="next_Click"> Next
                        <i class="fa fa-arrow-right"></i>
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
            <p class="feed">
                <asp:Label ID="feed" runat="server"></asp:Label>
            </p>
        </div>
        <input style="display: none">
        <asp:ScriptManager runat="server" ID="script" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="update" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="frm" runat="server">
                    <h1 class="title title-line">Input Akad
                    </h1>
                    <div id="pageof" runat="server">
                        <p>
                            Halaman 2 dari 2
                        </p>
                        <br />
                    </div>
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
                                <asp:RadioButtonList ID="rblStatus" runat="server" CssClass="radio" RepeatDirection="Horizontal" AutoPostBack="True"
                                    OnSelectedIndexChanged="rblStatus_SelectedIndexChanged">
                                    <asp:ListItem style="padding-right: 30px">BELUM DITENTUKAN</asp:ListItem>
                                    <asp:ListItem style="padding-right: 30px">DIJADWALKAN</asp:ListItem>
                                    <asp:ListItem style="padding-right: 30px">SELESAI</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>Kategori Retensi</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="retensi" CssClass="select-dropdown" runat="server">
                                    <asp:ListItem Value="0">Pilih Retensi :</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table id="dijadwalkan" runat="server" cellspacing="5" style="width: 400px">
                        <tr>
                            <td style="width: 100px">Target Akad
                            </td>
                            <td style="width: 5px">:
                            </td>
                            <td>
                                <asp:TextBox ID="tbTarget" runat="server" CssClass="txt_center tgl" Width="75" Font-Size="8"></asp:TextBox>
                                <label for="tbTarget" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                <asp:Label ID="lblTarget" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table id="selesai" runat="server" cellspacing="5" style="width: 400px">
                        <tr>
                            <td style="width: 100px">Tgl. Akad
                            </td>
                            <td style="width: 5px">:
                            </td>
                            <td>
                                <asp:TextBox ID="tbTgl" runat="server" CssClass="txt_center tgl" Width="75" Font-Size="8"></asp:TextBox>
                                <label for="tbTgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                <asp:Label ID="lblTgl" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Debitur
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="tbNoAkad" runat="server" CssClass="input-text" MaxLength="20" Width="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Keterangan
                            </td>
                            <td valign="top">:
                            </td>
                            <td>
                                <asp:TextBox ID="tbKet" runat="server" CssClass="input-text" TextMode="MultiLine" Rows="5"
                                    Columns="100" Width="250"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Realisasi Akad
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nilai" runat="server" CssClass="input-text"></asp:TextBox>
                                <asp:Label ID="nilaic" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table height="50">
                        <tr>
                            <td>
                                <asp:LinkButton ID="ok" runat="server" Width="75" CssClass="btn btn-blue" OnClick="ok_Click"><i class="fa fa-share"></i> OK
                                </asp:LinkButton>
                            </td>
                            <td>
                                <input class="btn btn-red" id="cancel" style="width: 100px" type="button" value="Cancel" name="cancel"
                                    runat="server">
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ok" />
            </Triggers>
        </asp:UpdatePanel>

        <script type="text/javascript">
            function call(nokontrak) {
                document.getElementById('nokontrak').value = nokontrak;
                document.getElementById('next').click();
            }
        </script>

    </form>
</body>
</html>

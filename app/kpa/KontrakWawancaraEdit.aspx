<%@ Reference Page="~/Log.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.KPA.KontrakWawancaraEdit" CodeFile="KontrakWawancaraEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Input Wawancara</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Input Wawancara">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Kembali ke halaman proses KPA?')) document.getElementById('cancel').click()">

    <script type="text/javascript" src="/Js/Common.js"></script>

    <script type="text/javascript" src="/Js/NumberFormat.js"></script>

    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div id="pilih" runat="server">
            <h1 class="title title-line">Input Wawancara</h1>
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
        <asp:ScriptManager runat="server" ID="scriptmanager" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="update" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="frm" runat="server">
                    <h1 class="title title-line">Input Wawancara
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
                    <br />
                    <table cellspacing="5">
                        <tr>
                            <td>Bank KPA
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAcc" runat="server" CssClass="select-dropdown" Width="300px">
                                    <asp:ListItem Value="">- Pilih Bank KPA -</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblBank" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
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
                                    <asp:ListItem>SELESAI</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <table id="dijadwalkan" runat="server" cellspacing="5" style="">
                        <tr>
                            <td>Target Wawancara
                            </td>
                            <td style="width: 5px">:
                            </td>
                            <td style="width: 265px">
                                <asp:TextBox ID="tbTarget" runat="server" CssClass="txt_center tgl" Width="75" Font-Size="8"></asp:TextBox>
                                <label for="tbTarget" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                <asp:Label ID="lblTarget" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <%--<tr>
                    <td>Lokasi Wawancara
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblLokasi" CssClass="radio" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" style="padding-right: 30px">ON THE SPOT</asp:ListItem>
                            <asp:ListItem style="padding-right: 30px">BANK</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>--%>
                        <tr>
                            <td>Lokasi Wawancara
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="tbLokasi" runat="server" CssClass="txt_num" Width="150"></asp:TextBox>
                                <asp:Label ID="lblLokasi" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Nilai Pengajuan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nilaipengajuan" runat="server" CssClass="txt_num" Width="150" ReadOnly="true"></asp:TextBox>
                                <asp:Label ID="nilaipengajuanc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table id="selesai" runat="server" cellspacing="5" style="width: 400px">
                        <tr>
                            <td style="width: 137px">Tgl. Wawancara
                            </td>
                            <td style="width: 5px">:
                            </td>
                            <td style="width: 258px">
                                <asp:TextBox ID="tbTgl" runat="server" CssClass="txt_center tgl" Width="75" Font-Size="8"></asp:TextBox>
                                <label for="tbTgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                <asp:Label ID="lblTgl" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Keterangan
                            </td>
                            <td valign="top">:
                            </td>
                            <td>
                                <asp:TextBox ID="tbKet" runat="server" CssClass="txt" TextMode="MultiLine" Rows="5"
                                    Columns="40"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table height="50">
                        <tr>
                            <td>
                                <asp:LinkButton ID="ok" runat="server" Width="75" CssClass="btn btn-blue" OnClick="ok_Click">
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

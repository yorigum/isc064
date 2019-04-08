<%@ Reference Page="~/Customer.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.TransferAnonimEdit" CodeFile="TransferAnonimEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Edit Transfer Anonim</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Tanda Terima Sementara - Edit Transfer Anonim">
    <script type="text/javascript" src="/Js/NumberFormat.js"></script>
</head>
<body onkeyup="if(event.keyCode==27)window.close()">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div class="pad">
            <table cellspacing="5">
                <tr valign="top">
                    <td style="white-space: nowrap">No. : <b id="no" runat="server" style="font-size: 11pt"></b></td>
                    <td width="100%"></td>
                    <td>
                        <label class="ibtn ibtn-file">
                            <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                accesskey="l">
                        </label>
                    </td>
                    <td>
                        <label class="ibtn ibtn-remove">
                            <input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel">
                        </label>
                    </td>
                </tr>
            </table>
            <table cellspacing="5">
                <tr>
                    <td>Tanggal</td>
                    <td>:</td>
                    <td>
                        <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                            <asp:TextBox ID="tgl" runat="server" type="text" CssClass="form-control" Style="width: 65%; height: 20px"></asp:TextBox>
                            <span class="input-group-btn" style="height: 34px; display: block">
                                <label for="tgl" class="btn-a default btn-cal"><i class="fa fa-calendar"></i></label>
                            </span>
                        </div>
                        <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Bank</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="bank"></asp:DropDownList>
                        <asp:Label ID="bankc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Nilai</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="nilai" runat="server" CssClass="txt_num" Width="100"></asp:TextBox>
                        <asp:Label ID="nilaic" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Project</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="project"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br>
                        <p><b>Identifikasi</b></p>
                    </td>
                </tr>
                <tr>
                    <td>No.Kontrak</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="nokontrak" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                        <button class="btn btn-orange" id="btnpop" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a' type="button" value="..." name="btnpop" runat="server">
                            <i class="fa fa-search"></i>
                        </button>
                    </td>
                </tr>
                <tr id="trunit" runat="server">
                    <td>No.Unit</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="unit" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trcs" runat="server">
                    <td>Customer</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="customer" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Keterangan<br>
                        Pembayaran</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="ket" runat="server" CssClass="txt" Width="400" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="height: 50px">
                <tr>
                    <td>
                        <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-check"></i> Apply </asp:LinkButton>
                    </td>
                    <td>
                        <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel" style="width: 75px">
                    </td>
                    <td style="padding-left: 10px">
                        <p class="feed">
                            <asp:Label ID="feed" runat="server"></asp:Label>
                        </p>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function call(nokontrak) {
        document.getElementById('nokontrak').value = nokontrak;

    }
    //function call2(nounit, customer) {
    //    document.getElementById('unit').value = nounit;
    //    document.getElementById('customer').value = customer;
    //}
</script>

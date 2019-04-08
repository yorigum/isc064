<%@ Reference Page="~/Customer.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.CBEdit" CodeFile="CBEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Edit Cash Back</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Refund Lebih Bayar - Edit">

    <script type="text/javascript" src="/Js/NumberFormat.js"></script>

</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)window.close()">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div class="pad">
            <span class="title title-line">Edit Refund Lebih Bayar </span>
            <table cellspacing="5">
                <tr valign="top">
                </tr>
            </table>
            <table cellspacing="5">
                <tr>
                    <td>
                        Tipe
                    </td>
                    <td>:</td>
                    <td id="tipe" runat="server">

                    </td>
                </tr>
                <tr>
                    <td><label id="labeltglkembali" runat="server">Tanggal Pengembalian</label>
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="tgl" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                        <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Sisa Tagihan
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="sisa" runat="server" CssClass="txt_num" Width="100">0</asp:TextBox>
                        <asp:Label ID="sisac" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Lebih Bayar
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="lb" runat="server" CssClass="txt_num" Width="100" ReadOnly="true">0</asp:TextBox>
                        <asp:Label ID="lbc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>Cash Back
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="cb" runat="server" CssClass="txt_num" Width="100" ReadOnly="true">0</asp:TextBox>
                        <asp:Label ID="cbc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><label id="labelbangkeluar" runat="server">Bank Keluar</label>
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="bk" runat="server" CssClass="txt_num" Width="100">0</asp:TextBox>
                        <asp:Label ID="bkc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr id="trbank" runat="server">
                    <td>Rekening Bank
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="bank" runat="server" Width="250">
                            <asp:ListItem Selected="True">- Pilih Rekening Bank -</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="bankc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <%--<tr>
                <td>
                    Rekening
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="bank" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                    <asp:Label ID="bankc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>--%>
            </table>
            <table height="50">
                <tr>
                    <td>
                        <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click">
                        <i class="fa fa-share"></i> OK
                        </asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Text="Apply" Width="75" AccessKey="a"
                            OnClick="save_Click">
                        <i class="fa fa-check"></i> Apply 
                        </asp:LinkButton>
                    </td>
                    <td>
                        <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel"
                            style="width: 75px">
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

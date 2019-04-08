<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.VARegis" CodeFile="VARegis.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Registrasi Virtual Account</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Virtual Account - Registrasi VA">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Registrasi Virtual Account</h1>
        <br>
        <table cellspacing="0" cellpadding="1">
            <tr valign="top">
                <td width="">
                    <h3>Dokumen</h3>
                    <table cellspacing="5">
                        <tr>
                            <td>
                                <b>No. VA</b>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="start" runat="server" Width="100" MaxLength="50" Style="margin-left: 30px;"></asp:TextBox>
                                <asp:Label ID="startc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Project</b>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="project" runat="server" Style="margin-left: 30px; width: 200px" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Label ID="projectc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><b>No. Unit</b></td>
                            <td>:</td>
                            <td>
                                <div class="input-group input-medium">
                                    <asp:TextBox ID="nounit" runat="server" type="text" CssClass="form-control" Style="width: 65%; margin-bottom: 10px;" AutoPostBack="true"></asp:TextBox>

                                    <button class="btn-a btn-orange" runat="server" id="btnpop2" show-modal='#ModalPopUp' modal-title='Daftar Unit' type="button">
                                        <i class="fa fa-search"></i>
                                    </button>
                                </div>
                                <asp:Label ID="nostockc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Bank</b>
                            </td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="bank" runat="server" Style="margin-left: 30px; width: 200px">
                                    <asp:ListItem Selected="True">- Pilih Rekening Bank -</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="bankc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="height: 50px">
                        <tr>
                            <td>
                                <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue t-white" Width="75" OnClick="ok_Click">
                                <i class="fa fa-share"></i> OK
                                </asp:LinkButton>
                            </td>
                            <td style="padding-left: 10px">
                                <p class="feed">
                                    <asp:Label ID="feed" runat="server"></asp:Label>
                                </p>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function call(no) {
                document.getElementById('nounit').value = no;
            }
        </script>
    </form>
</body>
</html>

<%@ Reference Page="~/SecLevel.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.SECURITY.Pendaftaran" CodeFile="Pendaftaran.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Pendaftaran Username Baru</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <script type="text/javascript" src="/Js/JQuery.min.js"></script>
    <meta name="ctrl" content="1">
    <meta name="sec" content="Username - Pendaftaran Username Baru">

</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Pendaftaran Username Baru</h1>
        <br>
        <table cellspacing="0" cellspacing="0">
            <tr valign="top">
                <td style="padding-right: 10">
                    <p><b>Terbaru :</b></p>
                    <asp:ListBox ID="baru" Rows="25" runat="server" Width="200" CssClass="ddl"></asp:ListBox>
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
                <td style="padding: 5 10 0 15">
                    <img src="/Media/line_vert.gif"></td>
                <td>
                    <table cellspacing="5">
                        <tr>
                            <td colspan="3">
                                <p><b>Identitas Username</b></p>
                            </td>
                        </tr>
                        <tr>
                            <td >Kode</td>

                            <td>
                                <asp:TextBox ID="userid" runat="server" CssClass="txt igroup" Width="75" MaxLength="20"></asp:TextBox>
                                <asp:Label ID="useridc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >Nama Lengkap</td>

                            <td>
                                <asp:TextBox ID="nama" runat="server" CssClass="txt igroup" Width="300" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Email</td>
                            <td>
                                <asp:TextBox ID="email" runat="server" CssClass="txt igroup" Width="300" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="emailc" runat="server" CssClass="err"></asp:Label>
                                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="email" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td >Security Level</td>

                            <td>
                                <asp:ListBox ID="seclevel" runat="server" CssClass="ddl" Width="250"></asp:ListBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>Kode Sales</td>

                            <td>
                                <asp:DropDownList ID="agent" runat="server" Width="400" CssClass="ddl">
                                    <asp:ListItem Value="0">Sales :</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <br>
                                <p><b>Aturan Password</b></p>
                            </td>
                        </tr>
                        <tr>
                            <td >Password Awal</td>

                            <td>
                                <asp:TextBox ID="pass" runat="server" CssClass="txt igroup" Width="100"></asp:TextBox>
                                <div style="display: none">
                                    <asp:TextBox ID="passMD5" runat="server"></asp:TextBox>
                                </div>
                                <asp:Label ID="passc" runat="server" CssClass="err"></asp:Label>
                                <asp:RegularExpressionValidator ID="Regex3" runat="server" ControlToValidate="pass"
                                    ValidationExpression="^(?=.{8,})" ForeColor="Red" EnableClientScript="true">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:CheckBox Checked="True" ID="gantipass" runat="server" Text="User harus rubah password pada saat Log-In pertama"></asp:CheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">User diwajbkan ganti password setiap
									<asp:TextBox ID="rotasipass" runat="server" CssClass="txt_center igroup" Width="50">6</asp:TextBox>
                                bulan sekali. &nbsp;
									<asp:Label ID="rotasipassc" runat="server" CssClass="err"></asp:Label>
                                <p style="font: 7pt">(0 = Password tidak pernah expire)</p>
                            </td>
                        </tr>
                        <tr>
                            <td>Upload Gambar :</td>
                            <td><input type="file" id="file" class="txt" runat="server" style="WIDTH:568px" name="file" /></td>
                        </tr>
                    </table>
                    <table height="50">
                        <tr>
                            <td>
                                <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    <script type="text/javascript">
        function CheckEmail() {
            var inputtxt = document.getElementById('email');
            var decimal = /^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$/;
            
            if (inputtxt.value.match(decimal)) {
                document.getElementById("save").disabled = false;
                document.getElementById("emailc").innerHTML = "";
                return true;
            }
            else {
                document.getElementById("save").enabled = false;
                document.getElementById("emailc").innerHTML = "Email harus sesuai format.";
                return false;
            }
        }
        function CheckPassword() {
            var inputtxt = document.getElementById('pass');
            var decimal = /^(?=.{8,})/;
            
            if (inputtxt.value.match(decimal)) {
                document.getElementById("save").disabled = false;
                document.getElementById("passc").innerHTML = "";
                return true;
            }
            else {
                document.getElementById("save").disabled = true;
                document.getElementById("passc").innerHTML = "Password baru harus tediri dari minimal 8 karakter.";
                return false;
            }
        }
        $("#email").keyup(function () {
            CheckEmail();
        });        
		$("#pass").keyup(function () {
            CheckPassword();
        });
    </script>
    </form>
</body>
</html>

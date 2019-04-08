<%@ Page Language="c#" Inherits="ISC064.SETTINGS.SetPass" CodeFile="SetPass.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Set Password Baru</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Username - Set Password Baru">

    <script type="text/javascript">
        function CheckPassword() {
            var inputtxt = document.getElementById('pass');
            var decimal = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{5,}$/;
            if (inputtxt.value.match(decimal)) {
                document.getElementById("save").disabled = false;
                document.getElementById("passc").innerHTML = "";
                return true;
            }
            else {
                document.getElementById("save").disabled = true;
                document.getElementById("passc").innerHTML = "Password baru harus tediri dari minimal 5 karakter, dengan kombinasi angka dan huruf.";
                return false;
            }
        }
    </script>

</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <div id="pilih" runat="server">
            <h1 class="title title-line">Set Password Baru</h1>
            <p>Halaman 1 dari 2</p>
            <br>
            <table style="border: 1px solid #DCDCDC" cellspacing="5">
                <tr>
                    <td>Kode / Username :</td>
                    <td>
                        <asp:TextBox ID="userid" runat="server" CssClass="txt igroup" Width="100"></asp:TextBox>
                        <button type="button" value="..." class="btn btn-orange" show-modal="#ModalPopUp" modal-url="/security/DaftarUserAktif.aspx" modal-title='List User' id="btnpop" runat="server"
                            name="btnpop">
                            <i class="fa fa-search"></i>
                        </button>
                    </td>
                    <td>
                        <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" OnClick="next_Click">Next <i class="fa fa-arrow-right"></i></asp:LinkButton>
                    </td>
                </tr>
            </table>
            <p class="feed">
                <asp:Label ID="feed" runat="server"></asp:Label>
            </p>
        </div>
        <div id="frm" runat="server">
            <h1 class="title">Set Password Baru</h1>
            <p>Halaman 2 dari 2</p>
            <br>
            <table cellspacing="5">
                <tr>
                    <td>Kode</td>
                    
                    <td>
                        <asp:Label ID="useridl" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td>Nama</td>
                    
                    <td>
                        <asp:Label ID="nama" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td>Security Level</td>
                    
                    <td>
                        <asp:Label ID="seclevel" runat="server" Font-Bold="True"></asp:Label></td>
                </tr>
            </table>
            <br>
            <table cellspacing="5">
                <tr>
                    <td>Password Baru</td>
                    
                    <td>
                        <asp:TextBox ID="pass" runat="server" CssClass="txt" Width="150" onblur="CheckPassword();"></asp:TextBox>
                        <asp:Label ID="passc" runat="server" CssClass="err"></asp:Label>
                        <asp:RegularExpressionValidator ID="Regex3" runat="server" ControlToValidate="pass" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$" ForeColor="Red" EnableClientScript="true"></asp:RegularExpressionValidator>
                        <div style="display: none">
                            <asp:TextBox ID="passMD5" runat="server"></asp:TextBox>
                        </div>
                    </td>
                </tr>
            </table>
            <p style="padding: 5px">
                <asp:CheckBox ID="gantipass" runat="server" Text="User harus rubah password pada saat Log-In berikutnya"
                    Checked="True"></asp:CheckBox>
            </p>
            <table height="50">
                <tr>
                    <td>
                        <asp:Button ID="save" runat="server" CssClass="btn btn-blue" Text="OK" Width="75" OnClick="save_Click"></asp:Button>
                    </td>
                    <td>
                        <input type="button" onclick="location.href = '?cancel=1'" class="btn btn-red" value="Cancel" style="width: 75px"
                            id="cancel">
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript">
            function call(userid) {
                document.getElementById('userid').value = userid;
                document.getElementById('next').click();
            }
        </script>
    </form>
</body>
</html>

<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.AgentDaftar" CodeFile="AgentDaftar.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Pendaftaran Marketing Baru</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Sales - Pendaftaran Sales Baru">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Pendaftaran Marketing Baru</h1>
        <br>
        <table cssclass="blue-skin" cellspacing="0">
            <tr valign="top">
                <td style="padding-right: 10px">
                    <p><b>Terbaru :</b></p>
                    <asp:ListBox ID="baru" Rows="25" runat="server" Width="200" CssClass="ddl"></asp:ListBox>
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
                <td style="padding: 5px 10px 0px 15px">
                    <img src="/Media/line_vert.gif"></td>
                <td>
                    <table cellspacing="5">
                        <tr>
                            <td colspan="3">
                                <p><b>Identitas</b></p>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Marketing</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="noagent" runat="server" CssClass="txt" Width="75" ReadOnly="True" Font-Bold="True"
                                    Text="#AUTO#"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Project</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                </asp:DropDownList>
                        </tr>
                        <tr>
                            <td>Tipe</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="tipe" runat="server" Width="200" AutoPostBack="true"
                                    OnSelectedIndexChanged="tipe_SelectedIndexChanged">
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td>Level</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="level" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="level_SelectedIndexChanged">
                                </asp:DropDownList>

                                <asp:Label ID="levelc" runat="server" CssClass="err"></asp:Label>

                            </td>
                        </tr>
                        <tr style="display: none" visible="false">
                            <%--<tr--%>
                            <td>Grade</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="grade" runat="server" Width="200" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:Label ID="gradec" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trAtasan" runat="server" visible="false">
                            <td>Atasan</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="atasan" runat="server" Width="200" AutoPostBack="true">
                                </asp:DropDownList>

                                <asp:Label ID="atasanc" runat="server" CssClass="err"></asp:Label>

                            </td>
                        </tr>
                        <tr id="trAtasanM" runat="server" visible="false">
                            <td>Atasan Manager</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="atasanm" runat="server" Width="200" AutoPostBack="true">
                                </asp:DropDownList>

                                <asp:Label ID="atasanmc" runat="server" CssClass="err"></asp:Label>

                            </td>
                        </tr>
                        <tr>
                            <td>Kode Marketing</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="kodesls" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                                <asp:Label ID="kodeslsc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Nama Lengkap</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="nama" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                                <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Alamat</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="alamat" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Email</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="email1" runat="server" CssClass="txt" Width="250" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="email1c" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Telepon</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="telp" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Handphone</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="hp" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Whatsapp</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="wa" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>NPWP</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="npwp" runat="server" CssClass="txt" Width="250" MaxLength="50">00.000.000.0-000.000</asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td colspan="3">
                                <br>
                                <p><b>Rekening Bank</b></p>
                            </td>
                        </tr>
                        <tr>
                            <td>Rekening</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="rek" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Bank</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="rekbank1" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>Cabang</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="cabang1" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>No. Rek</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="norek1" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Atas Nama Rekening</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="atasnama1" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>Jabatan</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="jabatan" runat="server" Width="250" OnSelectedIndexChanged="level_SelectedIndexChanged">
                                    <asp:ListItem>--Pilih Jabatan--</asp:ListItem>
                                    <asp:ListItem>Marketing</asp:ListItem>
                                    <asp:ListItem>Supervisor</asp:ListItem>
                                    <asp:ListItem>Manager</asp:ListItem>
                                    <asp:ListItem>GM Marketing</asp:ListItem>
                                    <asp:ListItem>Supporting</asp:ListItem>
                                </asp:DropDownList>

                                <asp:Label ID="jabatanmc" runat="server" CssClass="err"></asp:Label>

                            </td>

                            <tr style="display: none;">
                                <td colspan="3">
                                    <br>
                                    <p><b>Perusahaan / Manager</b></p>
                                </td>
                            </tr>
                        <tr style="display: none;">
                            <td>Nama</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="principal" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td colspan="3">
                                <br>
                                <p><b>Skema Komisi</b></p>
                            </td>
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
                var inputtxt = document.getElementById('email1');
                var decimal = /^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$/;

                if (inputtxt.value.match(decimal)) {
                    document.getElementById("save").disabled = false;
                    document.getElementById("email1c").innerHTML = "";
                    return true;
                }
                else {
                    document.getElementById("save").enabled = false;
                    document.getElementById("email1c").innerHTML = "Email harus sesuai format.";
                    return false;
                }
            }
            $("#email1").keyup(function () {
                CheckEmail();
            });
        </script>
    </form>
</body>
</html>

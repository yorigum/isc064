<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.AgentEdit" CodeFile="AgentEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadAgent" Src="HeadAgent.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavAgent" Src="NavAgent.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Edit Sales</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Sales - Edit Sales">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavAgent ID="NavAgent1" runat="server" Aktif="1"></uc1:NavAgent>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadAgent ID="HeadAgent1" runat="server"></uc1:HeadAgent>
                <table cellspacing="5">
                    <tr>
                        <td width="100%"></td>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="Log" id="btnlog" runat="server" name="btnlog" accesskey="l">
                            </label>
                        </td>
                        <td>
                            <label class="ibtn ibtn-remove">
                                <input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel" accesskey="d">
                            </label>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="stamp">Input :
								<asp:Label ID="tglInput" runat="server"></asp:Label>
                        </td>
                        <td class="stamp">Edit :
								<asp:Label ID="tglEdit" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellspacing="5">
                    <tr>
                        <td>Status :
                        </td>
                        <td>
                            <asp:RadioButton ID="aktif" CssClass="igroup-radio" runat="server" Text="Aktif" Font-Size="12" Font-Bold="True"
                                ForeColor="green" GroupName="status"></asp:RadioButton>
                        </td>
                        <td>
                            <asp:RadioButton ID="inaktif" CssClass="igroup-radio" runat="server" Text="Inaktif" Font-Size="12" Font-Bold="True"
                                ForeColor="red" GroupName="status"></asp:RadioButton>
                        </td>
                    </tr>
                </table>
                <br>
                <table cellspacing="5">
                    <tr>
                        <td colspan="3">
                            <p>
                                <b>Identitas</b>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>No. Sales</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="noagent" runat="server" CssClass="txt" Width="75" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Kode Sales</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="kodesls1" runat="server" CssClass="txt" Width="75" Font-Bold="True" Visible="false" ></asp:TextBox>
                            <asp:TextBox ID="kodesls" runat="server" CssClass="txt" Width="75" Font-Bold="True"></asp:TextBox>
                            <asp:Label ID="kodeslsc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Nama Lengkap</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="nama" runat="server" CssClass="txt" Width="350" MaxLength="100"></asp:TextBox>
                            <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Project</td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="project" runat="server" Width="200" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Tipe</td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="tipe" runat="server" Width="200"
                                OnSelectedIndexChanged="tipe_SelectedIndexChanged" AutoPostBack="true">
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
                    <tr id="trAtasan" runat="server">
                        <td>Atasan</td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="atasan" runat="server" Width="200" AutoPostBack="true">
                            </asp:DropDownList>

                            <asp:Label ID="atasanc" runat="server" CssClass="err"></asp:Label>

                        </td>
                    </tr>
                    <tr id="trAtasanM" runat="server" visible="false" style="display:none">
                        <td>Atasan Manager</td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="atasanm" runat="server" Width="200" AutoPostBack="true">
                            </asp:DropDownList>

                            <asp:Label ID="atasanmc" runat="server" CssClass="err"></asp:Label>

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
                    <tr>
                        <td>Email</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="email1" runat="server" CssClass="txt" Width="250" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td>Jabatan</td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="jabatan" runat="server" Width="250"  OnSelectedIndexChanged="level_SelectedIndexChanged"> 
                                    <asp:ListItem>Marketing</asp:ListItem>
                                    <asp:ListItem>Supervisor</asp:ListItem>
                                    <asp:ListItem>Manager</asp:ListItem>
                                    <asp:ListItem>GM Marketing</asp:ListItem>
                                    <asp:ListItem>Supporting</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br>
                            <p><b>Rekening Bank</b></p>
                        </td>
                    </tr>
                    <tr>
                        <td>Bank</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="norek1" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td>Cabang</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="cabang1" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Nomor Rekening</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="rekbank1" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Atas Nama</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="atasnama1" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td colspan="3">
                            <br>
                            <p><b>Supervisor</b></p>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td>Nama</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="principal" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="height: 50px;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-check"></i> Apply </asp:LinkButton>
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
        </div>
    </form>
</body>
</html>

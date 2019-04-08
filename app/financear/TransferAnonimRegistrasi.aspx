<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.TransferAnonimRegistrasi" CodeFile="TransferAnonimRegistrasi.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Registrasi Transfer Anonim</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Registrasi Transfer Anonim">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Registrasi Transfer Anonim</h1>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <asp:ScriptManager runat="server" ID="script" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="update" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellspacing="5">
                    <tr>
                        <td><b>Tanggal</b></td>
                        <td>
                            <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                                <asp:TextBox ID="tgl" runat="server" type="text" CssClass="form-control" Style="width: 65%; height: 20px"></asp:TextBox>
                                <span class="input-group-btn" style="height: 34px; display: block">
                                    <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                </span>
                            </div>
                            <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Project</b></td>
                        <td>
                            <asp:DropDownList ID="project" runat="server" Width="180" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                <asp:ListItem Value="SEMUA">Project :</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Rekening Bank</b></td>
                        <td>
                            <asp:DropDownList ID="ddlAcc" runat="server" Width="300">
                                <asp:ListItem Selected="True">- Pilih Rekening Bank -</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="bankc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Keterangan Pembayaran</b></td>
                        <td>
                            <asp:TextBox ID="ket" runat="server" Width="400" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Nilai</b></td>
                        <td>
                            <asp:TextBox ID="nilai" runat="server" Width="100" CssClass="txt_num"></asp:TextBox>
                            <asp:Label ID="nilaic" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table height="50">
                    <tr>
                        <td>
                            <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue t-white" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="save" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>

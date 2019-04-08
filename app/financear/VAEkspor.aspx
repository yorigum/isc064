<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.VAEkspor" CodeFile="VAEkspor.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NUP - Pembayaran Registrasi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Export VA">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Export VA</h1>
        <p style="font-size: 8pt; color: #666;">Halaman 1 dari 3</p>
        <br />
        <br />
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td><b>Dari</b></td>
                <td>
                    <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                        <asp:TextBox ID="dari" runat="server" type="text" CssClass="form-control" Style="width: 65%; height: 20px"></asp:TextBox>
                        <span class="input-group-btn" style="height: 34px; display: block">
                            <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        </span>
                    </div>
                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                </td>
                <td><b>Sampai</b></td>
                <td>
                    <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                        <asp:TextBox ID="sampai" runat="server" type="text" CssClass="form-control" Style="width: 65%; height: 20px"></asp:TextBox>
                        <span class="input-group-btn" style="height: 34px; display: block">
                            <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        </span>
                    </div>
                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display"
                        OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No." DataField="Acc" />
                <asp:BoundField HeaderText="Bank " DataField="Bank" />
                <asp:BoundField HeaderText="" DataField="Act" />
            </Columns>
        </asp:GridView>    
    </form>
</body>
</html>

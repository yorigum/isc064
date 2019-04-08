<%@ Reference Page="~/Acc.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.KPA.RetensiEdit" CodeFile="RetensiEdit.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Edit Kategori Retensi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Setup Retensi - Edit Kategori Retensi">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">

    <script type="text/javascript" src="/Js/Common.js"></script>

    <script type="text/javascript" src="/Js/NumberFormat.js"></script>

    <form id="Form1" method="post" runat="server">
        <div class="pad">
            <table cellspacing="5">
                <tr>
                    <td width="100%"></td>
                    <td>
                        <label class="ibtn ibtn-file">
                            <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                accesskey="l">
                        </label>
                    </td>
                    <td>
                        <label class="ibtn ibtn-remove">
                            <input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel"
                                accesskey="d">
                        </label>
                    </td>
                </tr>
            </table>
            <table cellspacing="5">
                <tr>
                    <td>Kode 
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="kode" runat="server" Width="160" MaxLength="20" CssClass="txt"></asp:TextBox>
                        <asp:Label ID="kodec" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Nama Kategori Retensi
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="nama" runat="server" Width="200" CssClass="txt" MaxLength="50"></asp:TextBox>
                        <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Project
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="project"></asp:DropDownList>
                        <asp:Label ID="Label1" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
            </table>
            <table height="50">
                <tr>
                    <td>
                        <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK
                        </asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a"
                            OnClick="save_Click"><i class="fa fa-check"></i>Apply</asp:LinkButton>
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

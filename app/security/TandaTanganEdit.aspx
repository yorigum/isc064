<%@ Page Language="c#" Inherits="ISC064.SECURITY.TandaTanganEdit" CodeFile="TandaTanganEdit.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Edit Tanda Tangan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Tanda Tangan - Edit Tanda Tangan">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
    <div class="tabdata">
        <div class="pad">
            <table cellspacing="5">
                <tr>
                    <td colspan="2">
                        <h2 class="title title-line">
                            Edit Tanda Tangan - <asp:Label ID="dok" runat="server"></asp:Label></h2>
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0">
                <tr valign="top">
                    <td width="400">
                        <table cellspacing="5" class="tb">
                            <tr>
                                <th>
                                    No.</th>
                                <th>
					                Nama</th>
				                <th>
					                Jabatan</th>
                            </tr>
                            <asp:placeholder id="list" runat="server"></asp:placeholder>
                        </table>
                        <table height="50">
                            <tr>
                                <td>
									<asp:LinkButton id="ok" runat="server" cssclass="btn btn-blue" width="75" onclick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                                </td>
                                <td>
                                    <input id="cancel" type="button" onclick="window.close()" class="btn" value="Cancel"
                                        style="width: 75px">
                                </td>
                                <td>
									<asp:Linkbutton id="save" runat="server" cssclass="btn btn-orange" width="75" accesskey="a" onclick="save_Click"><i class="fa fa-check"></i> Apply </asp:Linkbutton>
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
        </div>
    </div>
    </form>
</body> 
</html>

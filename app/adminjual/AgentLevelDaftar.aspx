<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.AgentLevelDaftar" CodeFile="AgentLevelDaftar.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Pendaftaran Level Agent Baru</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Sales - Pendaftaran Level Agent">
</head>
<body class="default-content">
    <form id="Form1" method="post" runat="server">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <h1>
        Pendaftaran Level Agent</h1>
    <br />
    <table cellspacing="0">
        <tr valign="top">
            <td style="padding-right: 10px">
                <p>
                    <b>Internal :</b></p>
                <asp:listbox id="inBaru" rows="25" runat="server" width="200px" cssclass="ddl" 
                    Height="150px"></asp:listbox>
                <br />
                <br />
                <p visible="false">
                    <%--<b visible="false">External :</b>--%>

                </p>
                <asp:listbox id="exBaru" rows="25" runat="server" width="200px" cssclass="ddl" 
                    Height="150px" visible="false"></asp:listbox>
                <p class="feed" visible="false">
                    <asp:Label ID="feed" runat="server" visible="false"></asp:Label></p>
            </td>
            <td style="padding-right: 10px; padding-left: 15px; padding-bottom: 0px; padding-top: 5px">
                <img src="/Media/line_vert.gif">
            </td>
            <td>
                <table cellspacing="5">
                    <tr>
                        <td colspan="3">
                            <p>
                                <b>Dokumen</b></p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            No. Level</td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="nojenis" runat="server" CssClass="txt" Width="65" Font-Bold="True"
                                Text="#AUTO#" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
							<tr runat="server">
								<td>Tipe</td>
								<td>:</td>
								<td>
									<asp:DropDownList ID="tipe" runat="server">
									    <asp:ListItem Value="Internal">Internal</asp:ListItem>
									</asp:DropDownList>
									
								</td>
							</tr>
                    <tr>
                        <td>
                            Nama Level Agent
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="namaLevel" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                            <asp:Label ID="namaLevelc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table height="50">
                    <tr>
                        <td>
                            <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click">
                                <i class="fa fa-share"></i> OK
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

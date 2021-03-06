﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlamatEmail.aspx.cs" Inherits="ISC064.SETTINGS.AlamatEmail" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html>
<head>
    <title>Setup Email</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Setup Email">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <h1 class="title title-line">Alamat Email</h1>
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div>

            <asp:DropDownList runat="server" ID="project" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>
            <br />
            <table cellpadding="1" cellspacing="0">
                <tr valign="top">
                    <td style="width: 220px">
                        <h2>Daftar Email</h2>
                        <ul id="list" runat="server" class="plike">
                        </ul>
                    </td>
                    <td style="padding: 5px 10px 0px 15px">
                        <img src="/Media/line_vert.gif"></td>
                    <td>
                        <h2 style="padding-left: 5px; padding-bottom: 5px">Pendaftaran Email Baru</h2>
                        <table cellspacing="5">
                            <tr>
                                <td>Nama Email</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="email" runat="server" MaxLength="20"></asp:TextBox>
                                    <asp:Label ID="emailcc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="height: 50px">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
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
    </form>
</body>
</html>

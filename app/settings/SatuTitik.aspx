<%@ Page Language="c#" Inherits="ISC064.SETTINGS.SatuTitik" CodeFile="SatuTitik.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Setup Akun Satu Titik</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Setup Akun Satu Titik - Registrasi">
</head>
<body class="body-padding">
    <form id="Form2" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Setup Akun Satu Titik</h1>
        <br>
        <table cellspacing="5">
            <tr valign="">
                <td width="20%">Project</td>
                <td width="1%">:</td>
                <td>
                    <%--<asp:TextBox ID="ket" runat="server" Width="350" Height="150" TextMode="MultiLine" CssClass="txt"></asp:TextBox>--%>
                    <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr valign="">
                <td width="20%">Username</td>
                <td width="1%">:</td>
                <td>
                    <asp:TextBox ID="username" runat="server" Width="200" CssClass="txt" required="required"></asp:TextBox>
                    <asp:Label ID="usernamec" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr valign="">
                <td width="20%">Password</td>
                <td width="1%">:</td>
                <td>
                    <asp:TextBox ID="pass" runat="server" Width="200" CssClass="txt" required="required"></asp:TextBox>
                    <asp:Label ID="passc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr valign="">
                <td width="20%">Masking</td>
                <td width="1%">:</td>
                <td>
                    <asp:TextBox ID="masking" runat="server" Width="200" CssClass="txt" required="required"></asp:TextBox>
                    <asp:Label ID="maskingc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr valign="">
                <td width="20%">Divisi</td>
                <td width="1%">:</td>
                <td>
                    <asp:TextBox ID="divisi" runat="server" Width="200" MaxLength="50" CssClass="txt" required="required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:Label runat="server" ID="feed"></asp:Label>
        <script type="text/javascript">
            function checkCtrl(foo, n) {
                var x = true; var i = 0;
                while (x) {
                    if (document.getElementById(foo + "_" + i)) {
                        if (!document.getElementById(foo + "_" + i).disabled) {
                            if (n == "true")
                                document.getElementById(foo + "_" + i).checked = true;
                            else
                                document.getElementById(foo + "_" + i).checked = false;
                        }
                        i++;
                    } else { x = false; }
                }
            }
        </script>
    </form>
</body>
</html>


<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SatuTitik.aspx.cs" Inherits="SMS_SatuTitik" %>

<%@ Register Src="~/Sidebar.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="~/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/Style.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="Common/Script.js"></script>
    <meta http-equiv="pragma" content="no-cache" />
    <base target="_self" />
</head>
<body class="dashboard">
    <uc1:Header ID="header" runat="server" />
    <form id="form1" runat="server" class="page">
        <div class="page-header">
            <ol class="breadcrumb">
                <li><a href="../Default.aspx">Home</a></li>
                <li>SMS Blast</li>
                <li class="active">Setup</li>
            </ol>
            <h1 class="page-title">Akun Satu Titik</h1>
        </div>
        <div class="page-content container-fluid">
            <div class="row">
                <div class="panel">
                    <div class="panel-body">
                        <div class="tab-content">
                            <div class=" tab-pane animation-fade active">
                                <div class="panel">
                                    <div class="panel-body">
                                        <div id="content">
                                            <div class="form-horizontal">
                                                <div class="form-group form-material">
                                                    <label class="col-sm-3 control-label">Project :</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="dept" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="gantilist">
                                                    </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group form-material">
                                                    <label class="col-sm-3 control-label">Username :</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="nama" runat="server" MaxLength="50" class="form-control" required="required" />
                                                    </div>
                                                </div>
                                                <div class="form-group form-material">
                                                    <label class="col-sm-3 control-label">Password :</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="pass" runat="server" MaxLength="50" class="form-control" required="required" />
                                                    </div>
                                                </div>
                                                <div class="form-group form-material">
                                                    <label class="col-sm-3 control-label">Masking :</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="masking" runat="server" MaxLength="50" class="form-control" required="required" />
                                                    </div>
                                                </div>
                                                <div class="form-group form-material">
                                                    <label class="col-sm-3 control-label">Divisi :</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="divisi" runat="server" MaxLength="50" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-horizontal">
                                                <div class="form-group form-material">
                                                    <div class="col-sm-6">
                                                        <asp:LinkButton ID="save" runat="server" Text="Save" Width="100" OnClick="save_Click" class="btn btn-primary" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <uc2:Footer ID="Footer" runat="server" />
</body>
</html>--%>

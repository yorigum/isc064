<%@ Reference Control="~/PrintNUPTemplate2.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.NUP.PrintNUP1" CodeFile="PrintNUP1.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Print Surat Pesanan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Print.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="3">
    <meta name="sec" content="Print Surat Pesanan">
    <style type="text/css">
        #print TD {
            FONT: 6.5pt arial;
        }

        #print P {
            FONT: 6.5pt arial;
        }

        #print LI {
            FONT: 6.5pt arial;
        }

        #print DIV {
            FONT: 6.5pt arial;
        }

        #print H3 {
            FONT: bold 10pt arial;
            PADDING-TOP: 20px;
        }

        #print H2 {
            BORDER-RIGHT: 0px;
            BORDER-TOP: 0px;
            FONT: bold 14pt arial;
            BORDER-LEFT: 0px;
            BORDER-BOTTOM: 0px;
        }

        #print TH {
            FONT: bold 10pt arial;
        }
        #print hitam {
            background-color:black;
            color:white;
            width:100%;
        }
    </style>
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;document.getElementById('cancel')){document.getElementById('cancel').click()}else if(event.keyCode==27){document.getElementById('cancel2').click()}">
    <script type="text/JavaScript" src="/Js/MD5.js"></script>
    <form id="Form1" method="post" runat="server">
        <%--<div id="reprint" runat="server">
            <h1 class="title title-line">Otorisasi Reprint</h1>
            <p style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                Jenis Dokumen : <u style="font-weight: bold; font-size: 10pt; line-height: normal; font-style: normal; font-variant: normal">Surat Pesanan</u>
                <br>
                Dokumen sudah di-print sebanyak
					<asp:Label ID="count" runat="server"></asp:Label>
                kali.
            </p>
            <br>
            <table cellspacing="5">
                <tr>
                    <td>Username</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="username" runat="server" CssClass="txt" Width="150"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Password</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="pass" runat="server" CssClass="txt" Width="150" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br>
            <div class="ins">
                <table class="">
                    <tr style="">
                        <td>
                            <asp:Button ID="btn" runat="server" CssClass="btn btn-green" Text="Authorize" OnClick="btn_Click"></asp:Button>
                        </td>
                        <td>
                            <input type="button" id="cancel" runat="server" class="btn btn-red" value="Cancel" style="width: 75px"
                                name="cancel">
                        </td>
                        <td style="padding-left: 10px">
                            <asp:Label ID="salah" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="display: none">
            <input type="button" id="cancel2" runat="server" name="cancel2">
        </div>--%>
        <div id="print">
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>

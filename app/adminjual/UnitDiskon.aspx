<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.UnitDiskon" CodeFile="UnitDiskon.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadUnit" Src="HeadUnit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavUnit" Src="NavUnit.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Diskon Kontrak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Diskon Kontrak">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavUnit ID="NavUnit1" runat="server" Aktif="4"></uc1:NavUnit>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadUnit ID="HeadUnit1" runat="server"></uc1:HeadUnit>
                <div style="padding: 5px">
                    <span style="font-size: large"><b>Status</b> <b>:
                        <asp:Label ID="statusdiskon" runat="server"></asp:Label></b></span>
                    <br />
                    <br />
                    <br />
                    <p>
                        Klik tombol jika ingin mengautorisasi diskon melebihi Pricelist minimum dari unit yang bersangkutan
                    </p>
                    <br />
                    <asp:Button ID="autorisasi" runat="server" CssClass="btn" Text="Autorisasi Limit Diskon" Width="275" OnClick="autorisasi_click"></asp:Button>
                    <br />
                    <br />
                    <br />
                    <p>
                        Klik tombol jika ingin menset diskon tidak melebihi Pricelist minimum dari unit yang bersangkutan
                    </p>
                    <br />
                    <asp:Button ID="deautorisasi" runat="server" CssClass="btn" Text="Unautorisasi Limit Diskon" Width="275" OnClick="deautorisasi_click"></asp:Button>
                    <br />
                    <br />
                    <asp:Label ID="feed" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

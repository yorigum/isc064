<%@ Reference Page="~/TabelStok2.aspx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.TabelStokS" CodeFile="TabelStokS.aspx.cs" %>
<!DOCTYPE html>
<html>
    <head>
	<title></title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <meta http-equiv="Refresh" content="5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        A {
            COLOR: black
        }

            A:visited {
                COLOR: black
            }

            A:hover {
                COLOR: black
            }

        #tb {
            BORDER-COLLAPSE: collapse;
            COLOR: gray
        }

            #tb TD {
                FONT: 10pt Open Sans;
                WIDTH: 35px;
                height: 30px;
                TEXT-ALIGN: center;
                border: 2px solid #e4e4e4
            }

            #tb .h {
                COLOR: black;
                BACKGROUND-COLOR: White;
                border: 2px solid #e4e4e4
                
            }

            #tb .lt {
                COLOR: black;
                border: 2px solid #e4e4e4
            }

            #tb .ket {
                COLOR: black;
                font-size: 8pt;
                font-weight: bold;
                width: auto;
                text-align: center;
            }
    </style>
</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server">
        <h1 class="title title-line" style="text-align: center">Shop House</h1>
        <div align="center">
            <img src="/Media/Final-01.jpg" width="800px" height="100px" />
        </div>
        <div align="center">
            <div class="peach">
                <asp:Literal runat="server" ID="legend" Visible="true" />
            </div>
            <br />
            <h3 style="font-size: 10pt; margin: 5px"></h3>
            <asp:Table ID="tb" runat="server" GridLines="Both" CellPadding="1"></asp:Table>
        </div>
    </form>
	</body>
</html>

<%--<%@ Reference Page="~/TabelStok2.aspx" %>--%>

<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.ShowTableStock2" CodeFile="ShowTableStock2.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<head>
    <title>Kios A</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <meta http-equiv="Refresh" content="5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        A {
            color: black;
        }

            A:visited {
                color: black;
            }

            A:hover {
                color: black;
            }

        #tb {
            border-collapse: collapse;
            color: black;
            font-weight: bold;
        }

            #tb TD {
                font: 10pt lucida;
                width: 23px;
                text-align: center;
            }

            #tb .h {
                color: black;
                text-align: center;
                font-weight: bold;
                font-size: 12pt;
            }

            #tb .lt {
                color: black;
                background-color: white;
                text-align: center;
                font-weight: bold;
                width: 150px;
            }

            #tb .ket {
                color: black;
                font-size: 6pt;
                font-weight: bold;
                width: 200px;
            }

            #tb .ket2 {
                color: black;
                font-size: 8pt;
                font-weight: bold;
                width: 300px;
            }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table cellpadding="0" cellspacing="0" style="margin: auto;">
            <tr style="height: 70px;">
                <td></td>
            </tr>
            <tr>
                <td>
                    <div align="center" style="margin: 0 20 20 20;">
                        <h3 style="margin: 5px"></h3>
                        <asp:Table ID="tb" runat="server" GridLines="Both" CellPadding="1">
                        </asp:Table>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

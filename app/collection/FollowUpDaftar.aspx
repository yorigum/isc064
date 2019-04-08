<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FollowUpDaftar.aspx.cs" Inherits="ISC064.COLLECTION.FollowUpDaftar" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<!DOCTYPE html>

<html>
<head>
    <title>Input Follow Up</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="P. Jatuh Tempo - Daftar Pemberitahuan Follow Up(Marketing)">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin membatalkan proses registrasi?')) document.getElementById('cancel').click();">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Registrasi Follow Up </h1>        
        <br>
            <table cellspacing="5">
            <tr>
                <td><b>Tgl. Follow UP</b></td>
                <td><b>:</b></td>
                <td>					
                    <asp:textbox id="tglfu" runat="server" type="text" cssclass="txt_center" ReadOnly="true"></asp:textbox>
                    <label for="tgful" class="btn btn-cal"><i class="fa fa-calendar"></i></label>                     
                    <asp:label id="tglfuc" runat="server" cssclass="err"></asp:label>					
                </td>
            </tr>           
            <tr>
                <td><b>Kategori</b></td>
                <td><b>:</b></td>
                <td>
                    <asp:RadioButtonList ID="rblgrup" GrupName="Status" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>                    
                    <asp:label id="rblgrupc" runat="server" cssclass="err"></asp:label>
                </td>
            </tr>
            <tr>
                <td><b>Tgl. Janji Bayar</b></td>
                <td><b>:</b></td>
                <td>
                    <asp:textbox id="tgl" runat="server" type="text" cssclass="txt_center"></asp:textbox>
                    <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>                     
                    <asp:label id="tglc" runat="server" cssclass="err"></asp:label>					
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top"><b>Keterangan</b></td>
                <td style="vertical-align:top"><b>:</b></td>
                <td>
                    <asp:TextBox ID="keterangan" runat="server" CssClass="input-text" Width="300px" Height="100" TextMode="MultiLine"></asp:TextBox>
                    <asp:label id="keteranganc" runat="server" cssclass="err"></asp:label>
                </td>                
            </tr>
            <tr></tr>
            <tr>
                <td>
                    <asp:LinkButton ID="save" runat="server" Width="75" CssClass="btn btn-blue" OnClick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton></td>
                <td>
                    <br />
                </td>
                <td>
                    <input class="btn btn-red" id="cancel" style="width: 75px" onclick="window.close();"
                        type="button" value="Cancel" name="cancel" runat="server">
                </td>
            </tr>

        </table>
    </form>
</body>
</html>

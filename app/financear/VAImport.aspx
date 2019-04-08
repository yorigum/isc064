<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VAImport.aspx.cs" Inherits="ISC064.FINANCEAR.VAImport" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html >
<html>
<head>
    <title>Virtual Account</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Import Virtual Account</h1>
        <br />
        <ul class="plike">
            <li>
                <a href="VAUpload.aspx">Permata</a></li>
            <li>
                <a href="VAImporBRI.aspx">BRI</a></li>
            <li>
                <a href="VAImporBNI.aspx">BNI</a></li>
            <li>
                <a href="VAImporMandiri.aspx">Mandiri</a></li>
        </ul>
    </form>
</body>
</html>

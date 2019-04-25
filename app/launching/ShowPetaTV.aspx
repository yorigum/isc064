<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.ShowPetaTV" CodeFile="ShowPetaTV.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Peta Site Plan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta http-equiv="Refresh" content="5">

    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Peta Floor Plan Detil">
    <meta http-equiv="pragma" content="no-cache">
    <link href="../Media/Style.css" type="text/css" rel="stylesheet" />
    <link href="css/Custom.css" type="text/css" rel="stylesheet" />
    <script src="/Js/JQuery.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body onkeyup="if(event.keyCode==27) history.back(-1)">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div style="width: 100%;">
            <div class="row">
                <div class="col s6">
                    <div class="row" style="margin-top: 20px; text-align: center; margin-left: 100px;">
                        <div class="col-md-4" style="text-align: center; background-color: pink; color: white;">
                            <h5>Sedang Pemilihan Unit</h5>
                        </div>
                        <div class="col-md-4" style="text-align: center; background-color: red; color: white;">
                            <h5>Sold</h5>
                        </div>
                        <div class="col-md-4" style="text-align: center; background-color: gray; color: white;">
                            <h5>Hold</h5>
                        </div>
                    </div>
                    <div>
                        <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                    </div>
                    
                </div>
                <div class="col s6" style="margin-left: 950px; margin-top: 20px">
                    <div class="container">
                        <div class="panel-group">
                            <div class="panel panel-default" style="width: 40%;">
                                <div class="panel-heading" style="background-color: black; color: white; text-align: center">
                                    <h5>TOTAL UNIT CLUSTER ASA</h5>
                                </div>
                                <div class="panel-body" style="color: black; text-align: center">
                                    <h4><span id="total" runat="server"></span></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="container">
                        <div class="panel-group">
                            <div class="panel panel-default" style="width: 40%;">
                                <div class="panel-heading" style="background-color: gray; color: white; text-align: center">
                                    <h5>BELUM DIPASARKAN</h5>
                                </div>
                                <div class="panel-body" style="color: black; text-align: center">
                                    <h4><span id="belum" runat="server"></span></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="container">
                        <div class="panel-group">
                            <div class="panel panel-default" style="width: 40%;">
                                <div class="panel-heading" style="background-color: #77DD77; color: white; text-align: center">
                                    <h5>STOK</h5>
                                </div>
                                <div class="panel-body" style="color: black; text-align: center">
                                    <h4><span id="stok" runat="server"></span></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="container">
                        <div class="panel-group">
                            <div class="panel panel-default" style="width: 40%;">
                                <div class="panel-heading" style="background-color: #ff6961; color: white; text-align: center">
                                    <h5>TERJUAL</h5>
                                </div>
                                <div class="panel-body" style="color: black; text-align: center">
                                    <h4><span id="terjual" runat="server"></span></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="container">
                        <div class="panel-group">
                            <div class="panel panel-default" style="width: 40%;">
                                <div class="panel-heading" style="background-color: #fdfd96; color: black; text-align: center">
                                    <h5>JUMLAH DIPASARKAN</h5>
                                </div>
                                <div class="panel-body" style="color: black; text-align: center">
                                    <h4><span id="jumlah" runat="server"></span></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

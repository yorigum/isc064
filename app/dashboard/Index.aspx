<%@ Page Language="c#" Inherits="ISC064.DASHBOARD.Index" CodeFile="Index.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Dashboard</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Dashboard">
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/assets/css/StyleSheet.css" rel="stylesheet" />
    <link href="/assets/css/light-bootstrap-dashboard.css" rel="stylesheet" />
    <script type="text/javascript">
        var forecolor = ["1DC7EA", "FB404B", "FFA534", "9368E9", "87CB16", "1F77D0", "5e5e5e", "dd4b39", "35465c", "e52d27", "55acee", "cc2127", "1769ff", "6188e2", "a748ca", "", "", "", "", "", "", "", "", "", "", ""];
    </script>
    <link href="http://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
    <script src="/assets/js/amcharts.js" type="text/javascript"></script>
    <script src="/assets/js/serial.js" type="text/javascript"></script>
    <script src="/assets/js/pie.js" type="text/javascript"></script>
    <script src="/assets/js/export.min.js" type="text/javascript"></script>
    <script src="/assets/js/light.js" type="text/javascript"></script>
    <link href="/assets/css/export.css" rel="stylesheet" type="text/css" media="all" />
    <style>
        .shadow-hand-lightfury {
            -webkit-box-shadow: 0 1px 5px rgba(0, 0, 0, 1);
            -moz-box-shadow: 0 1px 5px rgba(0, 0, 0, 1);
            -ms-box-shadow: 0 1px 5px rgba(0, 0, 0, 1);
            box-shadow: 0 1px 5px rgba(0, 0, 0, 1);
        }

        .shadow {
            -webkit-box-shadow: 0 1px 5px rgba(0, 0, 0, 0.3);
            -moz-box-shadow: 0 1px 5px rgba(0, 0, 0, 0.3);
            -ms-box-shadow: 0 1px 5px rgba(0, 0, 0, 0.3);
            box-shadow: 0 1px 5px rgba(0, 0, 0, 0.3);
        }

        .amcharts-graph-label {
            font-size: 18px;
        }
    </style>
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <div class="main-header">
	        <div id="background"></div>
	        <div id="labels">
	            <div class="logo-header" onclick="location.href='../gateway.aspx'">
	                <span class="v-helper"></span>
	                <img src="/Media/logo.png">
	            </div>
	            <div class="user-thumb" onclick="usermenu()">
	                <span class="v-helper"></span>
                    <img runat="server" style="border-radius:100%;width:40px;height:40px" id="gambar" src="">
	                <div class="user-menu" id="usermenu">
	                    <p onclick="OpenGantiPass();"><i class="fa fa-lock user-icon" aria-hidden="true"></i> &nbsp;Ganti Password</p>
                        <p onclick="OpenGantiFoto();"><i class="fa fa-lock user-icon" aria-hidden="true"></i> &nbsp;Ganti Foto</p>
	                    <p onclick="if(confirm('Apakah Anda ingin melakukan sign-out?\nProgram dan absensi aktif Anda akan ditutup.')){location.href='/SignOut.aspx'}"><i class="fa fa-power-off user-icon" aria-hidden="true"></i> &nbsp;Log Out</p>
	                </div>
	            </div>
	            <div class="user-info" onclick="usermenu()">
                    <p><% Response.Write(ISC064.Act.UserID); %> - <% Response.Write(ISC064.Act.SecLevel); %></p>
                    <p>IP Address : <% Response.Write(ISC064.Act.IP); %></p>
	            </div>
	        </div>
	    </div>
        <br />
        <br />
        <br />
        <br />
            <div class="content">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card" runat="server">
                                <div class="header">
                                    <h4 class="title">Penjualan Tahun <label style="font-size:25pt" class="title" id="labelthn" runat="server"/></h4>
                                    <p class="category"></p>
                                    <div class="item" style="text-align:right">
                                        <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged" Width="110"></asp:DropDownList>
                                        <asp:DropDownList ID="tahun" runat="server" AutoPostBack="true" Width="110"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="content">
                                    <div id="chart22" class="ct-chart" style="height: 500px;"></div>
                                    <div class="footer" style="text-align: left;">
                                        <div class="legend">
                                            <i class="fa fa-circle text-danger"></i> Penjualan : <label id="penjualanl" runat="server"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card" runat="server">
                                <div class="header">
                                    <h4 class="title">Omset Penjualan Tahun <label style="font-size:25pt" class="title" id="labelthn2" runat="server"/></h4>
                                    <p class="category"></p>
                                    <div class="item" style="text-align:right;">
                                        <asp:DropDownList ID="project2" runat="server" AutoPostBack="true" Width="110"></asp:DropDownList>
                                        <asp:DropDownList ID="tahun2" runat="server" AutoPostBack="true" Width="110"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="content">
                                    <div id="chartLinePenjualan" class="ct-chart" style="height: 500px;"></div>
                                    <div class="footer" style="text-align: left;">
                                        <div class="legend">
                                            <i class="fa fa-circle text-danger"></i> Omset Penjualan : <label id="omsetpenjualanl" runat="server"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="header">
                                    <h4 class="title">Master Stock</h4>
                                </div>
                                <div class="content">
                                    <div id="chartPieSetupSales" class="ct-chart ct-perfect-fourth"></div>
                                     <div class="footer">
                                    <div class="legend">
                                        <i class="fa fa-circle text-info"></i> Available
                                        <i class="fa fa-circle text-danger"></i> Sold
                                        <i class="fa fa-circle" style="color:#00e600"></i> Hold
                                        </div>
                                </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="header">
                                    <h4 class="title">Penjualan</h4>
                                </div>
                                <div class="content">
                                    <div id="chartCaraBayar" class="ct-chart ct-perfect-fourth"></div>
                                     <div class="footer">
                                    <div class="legend">
                                        <i class="fa fa-circle text-info"></i> KPA
                                        <i class="fa fa-circle text-danger"></i> CASH KERAS
                                        <i class="fa fa-circle" style="color:#00e600"></i> CASH BERTAHAP
                                        </div>
                                </div>
                                </div>
                            </div>
							<div class="card " runat="server">
                                <div class="header">
                                    <h4 class="title">Penjualan</h4>
                                    <p class="category"></p>
                                </div>
                                <div class="content">
                                    <div id="chart31" class="ct-chart" style="height: 500px;"></div>
                                    <div class="footer" style="text-align: left;">
                                        <div class="legend">
                                            <i class="fa fa-circle text-info"></i> Penjualan : <label id="penjualanlegend" runat="server"></label>
                                            <i class="fa fa-circle text-danger"></i> Batal : <label id="batallegend" runat="server"></label>
                                            <i class="fa fa-circle" style="color:#00e600"></i> Penjualan Bersih : <label id="penjualanblegend" runat="server"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
							<div class="card " runat="server">
                                <div class="header">
                                    <h4 class="title">Collection</h4>
                                    <p class="category"></p>
                                </div>
                                <div class="content">
                                    <div id="chart30" class="ct-chart" style="height: 500px;"></div>
                                    <div class="footer" style="text-align: left;">
                                        <div class="legend">
                                            <i class="fa fa-circle text-info"></i> Piutang : <label id="piutanglegend" runat="server"></label>
                                            <i class="fa fa-circle text-danger"></i> Pelunasan : <label id="pelunasanlegend" runat="server"></label>
                                            <i class="fa fa-circle" style="color:#00e600"></i> Sisa : <label id="sisalegend" runat="server"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
							<div class="card">
                                <div class="header">
                                    <h4 class="title">Collection</h4>
                                </div>
                                <div class="content">
                                    <div id="chartPieColl" class="ct-chart ct-perfect-fourth"></div>
                                     <div class="footer">
                                    <div class="legend">
                                        <i class="fa fa-circle text-info"></i> Piutang
                                        <i class="fa fa-circle text-danger"></i> Pelunasan
                                        <i class="fa fa-circle" style="color:#00e600"></i> Sisa
                                        </div>
                                </div>
                                </div>
                            </div>
                            <div class="card " runat="server">
                                <div class="header">
                                    <h4 class="title">Pelunasan Piutang <label style="font-size:25pt" class="title" id="labelthn3" runat="server"/></h4>
                                    <p class="category"></p>
                                    <div class="item" style="text-align:right;">
                                        <asp:DropDownList ID="project3" runat="server" AutoPostBack="true" Width="110"></asp:DropDownList>
                                        <asp:DropDownList ID="tahun3" runat="server" AutoPostBack="true" Width="110"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="content">
                                    <div id="chartBarFin" class="ct-chart" style="height: 500px;"></div>
                                    <div class="footer" style="text-align: left;">
                                        <div class="legend">
                                            <i class="fa fa-square text-danger"></i> Memo : <label id="memolegend" runat="server"></label>
                                            <i class="fa fa-square text-info"></i> Kuitansi : <label id="ttslegend" runat="server"></label>
                                            <i class="fa fa-square" style="color:#FFD700"></i> Total Pelunasan : <label id="totallegend" runat="server"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
							<div class="card" style="display:none">
                                <div class="header">
                                    <h4 class="title">TTS</h4>
                                </div>
                                <div class="content">
                                    <div id="chartPieFin" class="ct-chart ct-perfect-fourth"></div>
                                    <div id="as"></div>
                                    <%-- <div class="footer">
                                    <div class="legend">
                                        <i class="fa fa-circle text-info"></i> Done
                                        <i class="fa fa-circle text-danger"></i> On Progress
                                        </div>
                                </div>--%>
                                </div>
                            </div>
                            <div class="card " runat="server" style="display:none">
                                <div class="header">
                                    <h4 class="title">Setup Sales</h4>
                                    <p class="category"></p>
                                </div>
                                <div class="content">
                                    <div id="chartBarSetupSales" class="ct-chart" style="height: 500px;"></div>
                                   <%-- <div class="footer" style="text-align: left;">
                                        <div class="legend">
                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        <div class="main-footer clear">
			<div class="copyright">
				2018 &copy; Batavianet. All Rights Reserved.
			</div>
			<div class="contact">
				<b style="letter-spacing: 1px">CONTACTS</b> Phone: +62 21 29020111, 54356161 (hunting) | Email : support@batavianet.com
			</div>
		</div>

        <script src="/assets/js/jquery-1.10.2.js" type="text/javascript"></script>
        <script src="/assets/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="/assets/js/chartist.min.js"></script>
        <script src="/assets/js/Chart.js">
        </script>

        <script type="text/javascript">

            AmCharts.ready(function () {
                var chart;

                // SERIAL CHART
                chart = new AmCharts.AmSerialChart();
                chart.dataProvider = chartz22;
                chart.categoryField = "month";
                chart.startDuration = 0.5;
                chart.balloon.color = "#000000";

                var legend = new AmCharts.AmLegend();
                legend = new AmCharts.AmLegend();
                legend.position = "bottom";
                legend.align = "left";
                legend.markerType = "square";
                legend.valueText = "";
                chart.addLegend(legend);

                // AXES
                // category
                var categoryAxis = chart.categoryAxis;
                categoryAxis.fillAlpha = 1;
                categoryAxis.fillColor = "#FAFAFA";
                categoryAxis.gridAlpha = 0;
                categoryAxis.axisAlpha = 0;
                categoryAxis.gridPosition = "start";
                categoryAxis.position = "bottom";

                // value
                var valueAxis = new AmCharts.ValueAxis();
                //valueAxis.title = "Place taken";
                valueAxis.dashLength = 5;
                valueAxis.axisAlpha = 0;
                //valueAxis.minimum = 1;
                //valueAxis.maximum = 6;
                valueAxis.integersOnly = true;
                valueAxis.gridCount = 10;
                valueAxis.reversed = false; // this line makes the value axis reversed
                chart.addValueAxis(valueAxis);

                // GRAPHS
                // realisasi graph
                var graph = new AmCharts.AmGraph();
                graph.title = "Penjualan";
                graph.valueField = "penjualan";
                graph.hidden = false; // this line makes the graph initially hidden
                graph.balloonText = "Penjualan, Bulan [[category]]: [[value]]";
                graph.lineColor = "#FF0F00";
                graph.lineAlpha = 1;
                graph.bullet = "round";
                chart.addGraph(graph);

                // CURSOR
                var chartCursor = new AmCharts.ChartCursor();
                chartCursor.cursorPosition = "mouse";
                chartCursor.zoomable = false;
                chartCursor.cursorAlpha = 0;
                chart.addChartCursor(chartCursor);

                // LEGEND
                //var legend = new AmCharts.AmLegend();
                //legend.useGraphSettings = true;
                //chart.addLegend(legend);
                //$("#legendtarget").addLegend(legend);

                // WRITE
                chart.write("chart22");
            });
        </script>

        <script type="text/javascript">

            AmCharts.ready(function () {
                var chart;

                // SERIAL CHART
                chart = new AmCharts.AmSerialChart();
                chart.dataProvider = chartLinePenjualan;
                chart.categoryField = "month";
                chart.startDuration = 0.5;
                chart.balloon.color = "#000000";

                var legend = new AmCharts.AmLegend();
                legend = new AmCharts.AmLegend();
                legend.position = "bottom";
                legend.align = "left";
                legend.markerType = "square";
                legend.valueText = "";
                chart.addLegend(legend);

                // AXES
                // category
                var categoryAxis = chart.categoryAxis;
                categoryAxis.fillAlpha = 1;
                categoryAxis.fillColor = "#FAFAFA";
                categoryAxis.gridAlpha = 0;
                categoryAxis.axisAlpha = 0;
                categoryAxis.gridPosition = "start";
                categoryAxis.position = "bottom";

                // value
                var valueAxis = new AmCharts.ValueAxis();
                //valueAxis.title = "Place taken";
                valueAxis.dashLength = 5;
                valueAxis.axisAlpha = 0;
                //valueAxis.minimum = 1;
                //valueAxis.maximum = 6;
                valueAxis.integersOnly = true;
                valueAxis.gridCount = 10;
                valueAxis.reversed = false; // this line makes the value axis reversed
                chart.addValueAxis(valueAxis);

                // GRAPHS
                // realisasi graph
                var graph = new AmCharts.AmGraph();
                graph.title = "Omset Penjualan";
                graph.valueField = "penjualan";
                graph.hidden = false; // this line makes the graph initially hidden
                graph.balloonText = "Omset Penjualan, Bulan [[category]]: [[value]]";
                graph.lineColor = "#FF0F00";
                graph.lineAlpha = 1;
                graph.bullet = "round";
                chart.addGraph(graph);

                // CURSOR
                var chartCursor = new AmCharts.ChartCursor();
                chartCursor.cursorPosition = "mouse";
                chartCursor.zoomable = false;
                chartCursor.cursorAlpha = 0;
                chart.addChartCursor(chartCursor);

                // LEGEND
                //var legend = new AmCharts.AmLegend();
                //legend.useGraphSettings = true;
                //chart.addLegend(legend);
                //$("#legendtarget").addLegend(legend);

                // WRITE
                chart.write("chartLinePenjualan");
            });
        </script>
        
        <script type="text/javascript">
            AmCharts.ready(function () {
                var chart;
                var legend;


                // PIE CHART
                chart = new AmCharts.AmPieChart();
                chart.dataProvider = chartCaraBayar;
                chart.categoryField = "periode";
                chart.titleField = "title";
                chart.valueField = "value";
                chart.colorField = "color";
                chart.outlineColor = "#FFFFFF";
                chart.outlineAlpha = 0.8;
                chart.outlineThickness = 2;
                chart.balloonText = "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>";
                // this makes the chart 3D
                chart.depth3D = 15;
                chart.angle = 30;
                chart.creditsPosition = "top-right";
                // WRITE
                chart.write("chartCaraBayar");
            });

        </script>
		<script type="text/javascript">
            AmCharts.ready(function () {
                var chart;
                var legend;


                // PIE CHART
                chart = new AmCharts.AmPieChart();
                chart.dataProvider = PieChartColl;
                chart.categoryField = "periode";
                chart.titleField = "title";
                chart.valueField = "value";
                chart.colorField = "color";
                chart.outlineColor = "#FFFFFF";
                chart.outlineAlpha = 0.8;
                chart.outlineThickness = 2;
                chart.balloonText = "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>";
                // this makes the chart 3D
                chart.depth3D = 15;
                chart.angle = 30;
                chart.creditsPosition = "top-right";
                // WRITE
                chart.write("chartPieColl");
            });

        </script>
		<script type="text/javascript">
            AmCharts.ready(function () {
            var chart;
            // SERIAL CHART
            chart = new AmCharts.AmSerialChart();
            chart.dataProvider = chartz30;
            chart.categoryField = "periode";
            // the following two lines makes chart 3D
            chart.depth3D = 20;
            chart.angle = 30;

            var legend = new AmCharts.AmLegend();
            legend = new AmCharts.AmLegend();
            legend.position = "bottom";
            legend.align = "left";
            legend.markerType = "square";
            legend.valueText = "";
            chart.addLegend(legend);

            // AXES
            // category
            var categoryAxis = chart.categoryAxis;
            categoryAxis.labelRotation = 0;
            categoryAxis.fillColor = "#0D8ECF";
            //categoryAxis.dashLength = 5;
            categoryAxis.gridPosition = "start";

            // value
            var valueAxis = new AmCharts.ValueAxis();
            valueAxis.title = "";
            valueAxis.dashLength = 5;
            chart.addValueAxis(valueAxis);

            // GRAPH
            var graph = new AmCharts.AmGraph();
            graph.title = "Piutang";
            graph.valueField = "piutang";
            graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
            graph.type = "column";
            graph.lineAlpha = 0;
            graph.lineColor = "#0D8ECF";
            graph.fillAlphas = 1;
            chart.addGraph(graph);
			
			// GRAPH
            var graph = new AmCharts.AmGraph();
            graph.title = "Pelunasan";
            graph.valueField = "pelunasan";
            graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
            graph.type = "column";
            graph.lineAlpha = 0;
            graph.lineColor = "#FF0F00";
            graph.fillAlphas = 1;
            chart.addGraph(graph);
			
			// GRAPH
            var graph = new AmCharts.AmGraph();
            graph.title = "Sisa";
            graph.valueField = "sisa";
            graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
            graph.type = "column";
            graph.lineAlpha = 0;
            graph.lineColor = "#00e600";
            graph.fillAlphas = 1;
            chart.addGraph(graph);
			
			
            // CURSOR
            var chartCursor = new AmCharts.ChartCursor();
            chartCursor.cursorAlpha = 0;
            chartCursor.zoomable = false;
            chartCursor.categoryBalloonEnabled = false;
            chart.addChartCursor(chartCursor);
            chart.creditsPosition = "top-right";
            // WRITE
            chart.write("chart30");
        });
    </script>
		
		<script type="text/javascript">
            AmCharts.ready(function () {
            var chart;
            // SERIAL CHART
            chart = new AmCharts.AmSerialChart();
            chart.dataProvider = chartz31;
            chart.categoryField = "periode";
            // the following two lines makes chart 3D
            chart.depth3D = 20;
            chart.angle = 30;

            var legend = new AmCharts.AmLegend();
            legend = new AmCharts.AmLegend();
            legend.position = "bottom";
            legend.align = "left";
            legend.markerType = "square";
            legend.valueText = "";
            chart.addLegend(legend);

            // AXES
            // category
            var categoryAxis = chart.categoryAxis;
            categoryAxis.labelRotation = 0;
            categoryAxis.fillColor = "#0D8ECF";
            //categoryAxis.dashLength = 5;
            categoryAxis.gridPosition = "start";

            // value
            var valueAxis = new AmCharts.ValueAxis();
            valueAxis.title = "";
            valueAxis.dashLength = 5;
            chart.addValueAxis(valueAxis);

            // GRAPH
            var graph = new AmCharts.AmGraph();
            graph.title = "Penjualan";
            graph.valueField = "penjualan";
            graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
            graph.type = "column";
            graph.lineAlpha = 0;
            graph.lineColor = "#0D8ECF";
            graph.fillAlphas = 1;
            chart.addGraph(graph);
			
			// GRAPH
            var graph = new AmCharts.AmGraph();
            graph.title = "Batal";
            graph.valueField = "batal";
            graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
            graph.type = "column";
            graph.lineAlpha = 0;
            graph.lineColor = "#FF0F00";
            graph.fillAlphas = 1;
            chart.addGraph(graph);
			
			// GRAPH
            var graph = new AmCharts.AmGraph();
            graph.title = "Penjualan Bersih";
            graph.valueField = "penjualanb";
            graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
            graph.type = "column";
            graph.lineAlpha = 0;
            graph.lineColor = "#00e600";
            graph.fillAlphas = 1;
            chart.addGraph(graph);
			
			
            // CURSOR
            var chartCursor = new AmCharts.ChartCursor();
            chartCursor.cursorAlpha = 0;
            chartCursor.zoomable = false;
            chartCursor.categoryBalloonEnabled = false;
            chart.addChartCursor(chartCursor);
            chart.creditsPosition = "top-right";
            // WRITE
            chart.write("chart31");
        });
    </script>

        <script type="text/javascript">
            AmCharts.ready(function () {
                var chart;
                var legend;


                // PIE CHART
                chart = new AmCharts.AmPieChart();
                chart.dataProvider = chartPieFin;
                chart.categoryField = "periode";
                chart.titleField = "title";
                chart.valueField = "value";
                chart.colorField = "color";
                chart.outlineColor = "#FFFFFF";
                chart.outlineAlpha = 0.8;
                chart.outlineThickness = 2;
                chart.balloonText = "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>";
                // this makes the chart 3D
                chart.depth3D = 15;
                chart.angle = 30;
                chart.creditsPosition = "top-right";
                // WRITE
                chart.write("chartPieFin");
            });

        </script>
		<script type="text/javascript">
            AmCharts.ready(function () {
            var chart;
            // SERIAL CHART
            chart = new AmCharts.AmSerialChart();
            chart.dataProvider = chartBarFin;
            chart.categoryField = "periode";
            // the following two lines makes chart 3D
            chart.depth3D = 20;
            chart.angle = 30;

            var legend = new AmCharts.AmLegend();
            legend = new AmCharts.AmLegend();
            legend.position = "bottom";
            legend.align = "left";
            legend.markerType = "square";
            legend.valueText = "";
            chart.addLegend(legend);

            // AXES
            // category
            var categoryAxis = chart.categoryAxis;
            categoryAxis.labelRotation = 0;
            categoryAxis.fillColor = "#0D8ECF";
            categoryAxis.dashLength = 10;
            categoryAxis.gridPosition = "start";

            // value
            var valueAxis = new AmCharts.ValueAxis();
            valueAxis.title = "";
            valueAxis.dashLength = 5;
            chart.addValueAxis(valueAxis);
			
			// GRAPH
            var graph = new AmCharts.AmGraph();
            graph.title = "Memo";
            graph.valueField = "memo";
            graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
            graph.type = "column";
            graph.lineAlpha = 0;
            graph.lineColor = "#FB404B";
            graph.fillAlphas = 1;
            chart.addGraph(graph);
			
            // GRAPH
            var graph = new AmCharts.AmGraph();
            graph.title = "Kuitansi";
            graph.valueField = "kuitansi";
            graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
            graph.type = "column";
            graph.lineAlpha = 0;
            graph.lineColor = "#0D8ECF";
            graph.fillAlphas = 1;
            chart.addGraph(graph);

            // GRAPH
            var graph = new AmCharts.AmGraph();
            graph.title = "Total Pelunasan";
            graph.valueField = "total";
            graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
            graph.type = "column";
            graph.lineAlpha = 0;
            graph.lineColor = "#FFD700";
            graph.fillAlphas = 1;
            chart.addGraph(graph);

            // CURSOR
            var chartCursor = new AmCharts.ChartCursor();
            chartCursor.cursorAlpha = 0;
            chartCursor.zoomable = false;
            chartCursor.categoryBalloonEnabled = false;
            chart.addChartCursor(chartCursor);
            chart.creditsPosition = "top-right";
            // WRITE
            chart.write("chartBarFin");
        });
    </script>
        <script type="text/javascript">
            AmCharts.ready(function () {
                var chart;
                var legend;


                // PIE CHART
                chart = new AmCharts.AmPieChart();
                chart.dataProvider = chartPieSetupSales;
                chart.categoryField = "periode";
                chart.titleField = "title";
                chart.valueField = "value";
                chart.colorField = "color";
                chart.outlineColor = "#FFFFFF";
                chart.outlineAlpha = 0.8;
                chart.outlineThickness = 2;
                chart.balloonText = "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>";
                // this makes the chart 3D
                chart.depth3D = 15;
                chart.angle = 30;
                chart.creditsPosition = "top-right";
                // WRITE
                chart.write("chartPieSetupSales");
            });

        </script>
		<script type="text/javascript">
            AmCharts.ready(function () {
            var chart;
            // SERIAL CHART
            chart = new AmCharts.AmSerialChart();
            chart.dataProvider = chartBarSetupSales;
            chart.categoryField = "periode";
            // the following two lines makes chart 3D
            chart.depth3D = 20;
            chart.angle = 30;

            var legend = new AmCharts.AmLegend();
            legend = new AmCharts.AmLegend();
            legend.position = "bottom";
            legend.align = "left";
            legend.markerType = "square";
            legend.valueText = "";
            chart.addLegend(legend);

            // AXES
            // category
            var categoryAxis = chart.categoryAxis;
            categoryAxis.labelRotation = 0;
            categoryAxis.fillColor = "#0D8ECF";
            //categoryAxis.dashLength = 5;
            categoryAxis.gridPosition = "start";

            // value
            var valueAxis = new AmCharts.ValueAxis();
            valueAxis.title = "";
            valueAxis.dashLength = 5;
            chart.addValueAxis(valueAxis);
			
			// GRAPH
            var graph = new AmCharts.AmGraph();
            graph.title = "Available";
            graph.valueField = "available";
            graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
            graph.type = "column";
            graph.lineAlpha = 0;
            graph.lineColor = "#0D8ECF";
            graph.fillAlphas = 1;
            chart.addGraph(graph);

            // GRAPH
            var graph = new AmCharts.AmGraph();
            graph.title = "Sold";
            graph.valueField = "sold";
            graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
            graph.type = "column";
            graph.lineAlpha = 0;
            graph.lineColor = "#FB404B";
            graph.fillAlphas = 1;
            chart.addGraph(graph);

            // GRAPH
            var graph = new AmCharts.AmGraph();
            graph.title = "Total";
            graph.valueField = "total";
            graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
            graph.type = "column";
            graph.lineAlpha = 0;
            graph.lineColor = "#787878";
            graph.fillAlphas = 1;
            chart.addGraph(graph);
			
			
            // CURSOR
            var chartCursor = new AmCharts.ChartCursor();
            chartCursor.cursorAlpha = 0;
            chartCursor.zoomable = false;
            chartCursor.categoryBalloonEnabled = false;
            chart.addChartCursor(chartCursor);
            chart.creditsPosition = "top-right";
            // WRITE
            chart.write("chartBarSetupSales");
        });
    </script>
        
	<script type="text/javascript">
        function OpenGantiPass() {
            if (navigator.userAgent.indexOf("MSIE") != -1) openModal('../GantiPass.aspx', '500', '310');
            window.open('../GantiPass.aspx', 'Ganti Password', "width=500px, height=310px, top=100px;, left=200px");
        }
        function OpenGantiFoto() {
            if (navigator.userAgent.indexOf("MSIE") != -1) openModal('../GantiFoto.aspx', '500', '310');
            window.open('../GantiFoto.aspx', 'Ganti Gambar', "width=500px, height=310px, top=170px;, left=430px");
        }
        function usermenu() {
            var submenu = document.getElementById('usermenu');
            if (submenu) {
                submenu.style.display = submenu.style.display == "block" ? "" : "block";
            }
        }
        function openModal(id) {
            var modal = document.getElementById(id);
            modal.className += " modalopen";
            modal.style.display = 'block';
        }
        function dismissModal() {
            var a = document.getElementsByClassName('modalopen');
            while (a.length > 0) {
                a[0].style.display = 'none';
                a[0].className = 'modal';
            }
        }
        document.addEventListener('click', function (e) {
            e = e || window.event;
            var target = e.target || e.srcElement;
            if (target.classList.contains('modal')) {
                dismissModal();
            };
        }, false);
	</script>
    </form>
</body>
</html>


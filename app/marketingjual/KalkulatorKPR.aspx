<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KalkulatorKPR" CodeFile="KalkulatorKPR.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Kalkulator KPR</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Unit - Kalkulator KPR">
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Kalkulator KPR</h1>
			<br>
			<table border="0" cellspacing="1" cellpadding="5">
				<tr>
					<td>Nilai KPR</td>
					<td>:</td>
					<td><input type="text" class="txt_num" id="loan" size="15" runat="server"> Rupiah</td>
				</tr>
				<tr>
					<td>Periode</td>
					<td>:</td>
					<td><input type="text" class="txt_center" id="months" size="5" runat="server"> Bulan</td>
				</tr>
				<tr>
					<td>Effective Rate</td>
					<td>:</td>
					<td><input type="text" class="txt_center" id="rate" size="5" runat="server"> %</td>
				</tr>
				<tr>
					<td colspan="3">
						<input type="button" class="btn btn-blue" onclick="showpay()" value="Hitung" name="button">
					</td>
				</tr>
				<tr>
					<td colspan="3">
						Installment Bulanan :
						<br>
						<input type="text" class="txt" id="pay" readonly="true" style="border:0px;font-size:20pt;font-weight:bold">
					</td>
				</tr>
			</table>
			<script type="text/JavaScript">
			function showpay() {
				if ((document.getElementById('loan').value == null || document.getElementById('loan').value.length == 0) ||
		     		(document.getElementById('months').value == null || document.getElementById('months').value.length == 0) ||
		     		(document.getElementById('rate').value == null || document.getElementById('rate').value.length == 0))
					
					{ 	alert ("Note:\nTidak bisa kalkulasi. Data tidak lengkap.");
						document.getElementById('loan').focus();
						document.getElementById('loan').select();
					}
			
				else
			
					{	a = cvtnum(document.getElementById('loan').value)	
						c = cvtnum(document.getElementById('months').value)
		   				d = cvtnum(document.getElementById('rate').value)
						
						e = a
				
						//f= ( e + (e*(d/100)) ) / (c*12)
						h1 = a;
						h2 = 1 + (d / 1200);
						h3 = Math.pow(h2,c);
						h4 = 1 / h3;
						h5 = 1 - h4;
						f = (h1 * (d / 1200)) / h5;
						f = Math.round(f);
						
						finalnet = Math.round(100*f)/100;
						eval("document.getElementById('pay').value = FinalFormat('"+f+"')");
					}
					// payment = principle * monthly interest/(1 - (1/(1+MonthlyInterest)*Months))
			}
			function cvtnum(foo){
				return foo.replace(/,/gi ,"");
			}
			</script>
		</form>
	</body>
</html>

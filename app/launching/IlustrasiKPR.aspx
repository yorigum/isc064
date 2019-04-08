<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.IlustrasiKPR" CodeFile="IlustrasiKPR.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Informasi KPR</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Ilustrasi KPR">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <h1>
        Bank Financing: Monthly Payment Estimation</h1>
    <br>
    <table border="0" cellspacing="1" cellpadding="5">
        <tr>
            <td>
                Annual Interest Rate
            </td>
            <td>
                :
            </td>
            <td>
                <input type="text" class="txt_center" id="rate" size="5" runat="server">
                %
            </td>
        </tr>
        <tr>
            <td>
                Number of Months payment
            </td>
            <td>
                :
            </td>
            <td>
                <input type="text" class="txt_center" id="months" size="5" runat="server">
                Bulan
            </td>
        </tr>
        <tr>
            <td>
                Ammount of Loan
            </td>
            <td>
                :
            </td>
            <td>
                <input type="text" class="txt_num" id="loan" size="15" runat="server">
                Rupiah
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <input type="button" class="btn" onclick="showpay()" value="Hitung" name="button"
                    id="btnhitung">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                Payment Per Month :
                <br>
                <input type="text" class="txt" id="pay" readonly="true" style="border: 0px; font-size: 20pt;
                    font-weight: bold">
            </td>
        </tr>
    </table>

    <script language="JavaScript">
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
						document.getElementById('btnhitung').style.display="none";
						
						window.print();
					}
					
			}
			function cvtnum(foo){
				return foo.replace(/,/gi ,"");
			}
    </script>

    </form>
</body>
</html>

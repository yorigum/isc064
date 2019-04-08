<%@ Page Language="C#" CodeFile="KontrakApprovBatal.aspx.cs" Inherits="ISC064.MARKETINGJUAL.KontrakApprovBatal" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html>
    <head>
        <title>Approval Pembatalan Kontrak</title>
        <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
	    <meta name="CODE_LANGUAGE" content="C#">
	    <meta name="vs_defaultClientScript" content="JavaScript">
	    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
	    <meta name="ctrl" content="1">
	    <meta name="sec" content="Kontrak - Approval Pembatalan Kontrak">
    </head>
    <body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="DISPLAY:none"/>
			<div id="frm" runat="server" >
				<h1 class="title title-line">Approval Pembatalan Kontrak</h1>
                <br />
				<div>
				 <p class="feed">
			        <asp:label id="feed" runat="server"></asp:label>
		        </p>
		        <table style="BORDER-RIGHT:#dcdcdc 1px solid; BORDER-TOP:#dcdcdc 1px solid; BORDER-LEFT:#dcdcdc 1px solid; BORDER-BOTTOM:#dcdcdc 1px solid">
					<tr>
						<td><b>Tanggal Approval</b></td>
						<td>:</td>
						<td>
							<asp:textbox id="tglot" runat="server" cssclass="txt_center" width="85"></asp:textbox>
                            <label for="tglot" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
							<asp:label id="tglotc" runat="server" cssclass="err"></asp:label>
						</td>
					</tr>
		        </table>
			    </div>
			    <br />
			    <div>
				  <table class="tb blue-skin" border="0">
				    <tr>
					    <th />
					    <th>
						    No. Kontrak
					    </th>
					    <th>
					        Unit
					    </th>
					    <th>
						    Customer
					    </th>
					    <th>
						    Sales
					    </th>
					    <th>
						    Alasan Batal
					    </th>
					    <th>
					        Keterangan
					    </th>
					    <th class="right">
						    Biaya Administrasi
					    </th>
					    <th class="right">
						    Total Pelunasan
					    </th>
					    <th class="right">
						    Tgl Pengembalian
					    </th>
					    <th class="right">
						    Total Pengembalian
					    </th>
					    <th class="right">
						    Nilai Klaim
					    </th>
				    </tr>
				    <tr>
					    <td colspan="11">
						    <ul class="floatsm">
							    <li><a href="javascript:checkCtrl('nokontrak','true')">Check &nbsp;&nbsp;&nbsp;&nbsp;</a></li>
							    <li><a href="javascript:checkCtrl('nokontrak','false')">Uncheck</a></li>
						    </ul>
						    <br/>
					    </td>
				    </tr>
				    <asp:PlaceHolder ID="list" runat="server" />
			    </table>
				<asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click" AccessKey="s">
					<i class="fa fa-share"></i> OK
				</asp:LinkButton>

			</div>
			</div>
			<script type="text/javascript">
			    function checkCtrl(foo, n) {
			        var x = true; var i = 0;
			        while (x) {
			            if (document.getElementById(foo + "_" + i)) {
			                if (!document.getElementById(foo + "_" + i).disabled) {
			                    if (n == "true")
			                        document.getElementById(foo + "_" + i).checked = true;
			                    else
			                        document.getElementById(foo + "_" + i).checked = false;
			                }
			                i++;
			            } else { x = false; }
			        }
			    }
			</script>
		</form>
	</body>
</html>

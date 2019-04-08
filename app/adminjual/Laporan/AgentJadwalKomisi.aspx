<%@ Page Language="C#" Inherits="ISC064.ADMINJUAL.AgentJadwalKomisi" CodeFile="AgentJadwalKomisi.aspx.cs"%>
<%@ Register src="NavAgent.ascx" tagname="NavAgent" tagprefix="uc1" %>
<%@ Register src="HeadAgent.ascx" tagname="HeadAgent" tagprefix="uc2" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Jadwal Komisi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="C#"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link href="/Media/Style.css" type="text/css" rel="stylesheet"/>
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1"/>
		<meta name="sec" content="Sales - Jadwal Komisi"/>
	</head>
    <body onkeyup="if(event.keyCode==27) window.close()">
		<form id="Form1" method="post" runat="server">
            
		    <uc1:NavAgent ID="NavAgent1" runat="server" />
			<%--<uc2:navagent id="NavAgent1" runat="server" aktif="5"></uc2:navagent>--%>
			<div class="tabdata">
				<div class="pad">
  					<%--<uc1:headagent id="HeadAgent1" runat="server"></uc1:headagent>--%>
                    
            <uc2:HeadAgent ID="HeadAgent1" runat="server" />
			        <table style="BORDER-BOTTOM:#dcdcdc 1px solid; BORDER-LEFT:#dcdcdc 1px solid; BORDER-TOP:#dcdcdc 1px solid; BORDER-RIGHT:#dcdcdc 1px solid"
				        cellspacing="5">
				        <tr>
					        <td><b>Periode Komisi :</b>
					        </td>					            
					        <td>
					           <asp:DropDownList ID="ddlPeriode" runat="server" AutoPostBack="true"
                                onselectedindexchanged="ddlPeriode_SelectedIndexChanged">
                                   <asp:ListItem>Periode Komisi</asp:ListItem>
                               </asp:DropDownList>
					        </td>
				        </tr>
			        </table>
                    <br />				
                <asp:table id="rpt" runat="server" cssclass="blue-skin tb" cellspacing="0">
					<asp:tablerow horizontalalign="Left">
						<asp:tableheadercell width="100">No. Kontrak</asp:tableheadercell>
						<asp:tableheadercell width="100">Unit</asp:tableheadercell>
						<asp:tableheadercell width="100">Periode Komisi</asp:tableheadercell>
						<asp:tableheadercell>Skema Cair</asp:tableheadercell>
						<asp:tableheadercell width="100">Nilai Komisi</asp:tableheadercell>
						<asp:tableheadercell>% Pencairan</asp:tableheadercell>
						<asp:tableheadercell>&nbsp;</asp:tableheadercell>
					</asp:tablerow>
				</asp:table>
					
				</div>
			</div>
		    <script type="text/javascript">
			    function popKomisiBayar(NoKontrak, NoAgent, BtnID, Periode) {
				    openModal('KomisiBayar.aspx?NoKontrak=' + NoKontrak + '&NoAgent=' + NoAgent + '&id='+BtnID , '575', '345');
				}
			</script>
			<script type="text/javascript">
			    function call(nomor) {
			        popEditKontrak(nomor);
			    }
			</script>
		</form>
	</body>
</html>

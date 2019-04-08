<%@ Reference Page="~/Customer.aspx" %>
<%@ Page language="c#" Inherits="ISC064.COLLECTION.SKLEdit" CodeFile="SKLEdit.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadSKL" Src="HeadSKL.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavSKL" Src="NavSKL.ascx" %>

<!DOCTYPE html>
<html>
	<head>
		<title>Edit Surat Keterangan Lunas</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="5">
		<meta name="sec" content="P. Surat Keterangan Lunas - Edit Surat Keterangan Lunas">
	</head>
	<body class="" onkeyup="if(event.keyCode==27)window.close()">
		<form id="Form1" method="post" runat="server">
            <div class="content-header">
            <uc1:NavSKL ID="NavPJT1" runat="server" Aktif="1"></uc1:NavSKL>
            </div>
			<div class="tabdata">
				<div class="pad">
					<uc1:headskl id="HeadSKL1" runat="server"></uc1:headskl>
					<table cellspacing="5">
						<tr>
							<td style="width:5%"><b>Print :</b></td>
							<td style="width:25%" class=""><a id="printSKL" runat="server"><b>Surat Keterangan Lunas</b></a></td>
							<td style="width:70%"></td>
							<td style="">
                                <label class="ibtn ibtn-file">
								    <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
									accesskey="l">
                                </label>
							</td>
						</tr>
					</table>
					<table cellpadding="0" cellspacing="0">
						<tr valign="top">
							<td>
								<table cellspacing="5">
									<tr>
										<td colspan="3">
											<p><b>Data Collection</b></p>
										</td>
									</tr>
									<tr>
										<td style="padding-top:25px"><b>Tgl. Surat Keterangan Lunas</b></td>
										<td style="padding-top:25px"><b>:</b></td>
										<td>
                                            <div class="input-group input-medium">
                                                <asp:textbox id="tgl" runat="server" type="text" cssclass="form-control" style="width:50%" Height="20"></asp:textbox>
                                                <span class="input-group-btn" style="height:34px;display:block">
                                                    <button class="btn-a default" runat="server" type="button" style="height:100%">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                            </div>
						                    <asp:label id="tglc" runat="server" cssclass="err"></asp:label>
											<%--<asp:label id="tgl" runat="server" font-bold="True" font-size="12"></asp:label>--%>
										</td>
									</tr>
									<tr>
										<td><b>No. Surat Keterangan Lunas System</b></td>
										<td><b>:</b></td>
										<td>
											<asp:label id="nosys" runat="server" font-bold="True" font-size="12"></asp:label>
										</td>
									</tr>
									<tr>
										<td><b>No. Surat Keterangan Lunas Manual</b></td>
										<td><b>:</b></td>
										<td>
											<asp:TextBox id="nom" runat="server" font-bold="True" font-size="12"></asp:TextBox>
										</td>
									</tr>
									<tr>
									    <td valign="top"><b>Nomor yang Digunakan</b></td>
									    <td valign="top"><b>:</b></td>
									    <td>
									        <asp:RadioButtonList ID="nod" CssClass="radio" runat="server">
                                                <asp:ListItem Value="0" Selected="True">System</asp:ListItem>									            
                                                <asp:ListItem Value="1">Manual</asp:ListItem>									            
									        </asp:RadioButtonList>
									    </td>
									</tr>
								</table>
								<table height="50">
									<tr>
										<td>
											<asp:LinkButton id="ok" runat="server" cssclass="btn btn-blue" width="75" onclick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
										</td>
                                        <td>
                                            <br />
                                        </td>
										<td>
											<asp:LinkButton id="save" runat="server" cssclass="btn btn-orange" width="75" accesskey="a" onclick="save_Click"><i class="fa fa-check"></i> Apply</asp:LinkButton>
										</td>
                                        <td>
                                            <br />
                                        </td>
                                        <td>
											<input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel">
										</td>
										<td style="padding-left:10">
											<p class="feed">
												<asp:label id="feed" runat="server"></asp:label>
											</p>
										</td>
									</tr>
								</table>
							</td>
							
						</tr>
					</table>
				</div>
			</div>
		</form>
	</body>
</html>

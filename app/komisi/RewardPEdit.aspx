<%@ Page Language="c#" Inherits="ISC064.KOMISI.RewardPEdit" CodeFile="RewardPEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadRewardP" Src="HeadRewardP.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavRewardP" Src="NavRewardP.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Edit Pengajuan Pencairan Reward</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Pengajuan Pencairan Reward - Edit">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavRewardP ID="NavRewardP" runat="server" Aktif="1"></uc1:NavRewardP>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadRewardP ID="HeadRewardP" runat="server"></uc1:HeadRewardP>
                <table cellspacing="5">
                    <tr>
                        <td width="100%"></td>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                    accesskey="l">
                            </label>
                        </td>
                        <td>
                            <label class="ibtn ibtn-remove">
                                <input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel"
                                    accesskey="d">
                            </label>
                        </td>
                    </tr>
                </table>
                <p class="feed" style="padding-left: 5">
                    <asp:Label ID="feed" runat="server"></asp:Label>
                </p>
                <table>
                    <tr>
                        <td><b>Project</b></td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="project" runat="server" Width="250" CssClass="txt" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Tgl. Pengajuan</b></td>
                        <td>:</td>
                        <td>
                            <nobr>
						        <asp:textbox id="tgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
                                <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>                                            
					        </nobr>
                            <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Keterangan</b></td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="ket" runat="server" Width="350" Height="150" TextMode="MultiLine" CssClass="txt"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Table ID="tb" runat="server" CssClass="tb blue-skin" CellSpacing="1">
                    <asp:TableRow>
                        <asp:TableHeaderCell ColumnSpan="5"><span style="font-size:large;">Detail</span></asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableHeaderCell>No.</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Sales</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Nama Skema</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Periode</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Reward</asp:TableHeaderCell>
                    </asp:TableRow>
                </asp:Table>
                <br />
                <table height="50">
                    <tr>
                        <td>
                            <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK
                            </asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a"
                                 OnClick="save_Click"><i class="fa fa-check"></i>Apply</asp:LinkButton>
                        </td>
                        <td>
                            <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel"
                                style="width: 75px">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>

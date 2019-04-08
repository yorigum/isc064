<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.TransferAnonim" CodeFile="TransferAnonim.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Transfer Anonim</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Transfer Anonim">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <span class="title">Transfer Anonim</span>
        <hr />
        <br>
        <asp:ScriptManager runat="server" ID="script" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="update" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="border: 1px solid #DCDCDC;" cellspacing="5">
                    <tr>
                        <td><b>Search by</b></td>
                        <td colspan="3">
                            <asp:DropDownList ID="rek" runat="server" Width="200">
                                <asp:ListItem Value="">Rekening :</asp:ListItem>
                            </asp:DropDownList>
                            <input type="button" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Master Transfer Anonim' modal-url='DaftarTA.aspx' id="search" runat="server"
                                name="search" accesskey="s">
                        </td>
                    </tr>
                    <tr>
                        <td><b>Dari</b></td>
                        <td>
                            <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                                <asp:TextBox ID="dari" runat="server" type="text" CssClass="form-control" Style="width: 65%; height: 20px"></asp:TextBox>
                                <span class="input-group-btn" style="height: 34px; display: block">
                                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                </span>
                            </div>
                            <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                        </td>
                        <td><b>Sampai</b></td>
                        <td>
                            <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                                <asp:TextBox ID="sampai" runat="server" type="text" CssClass="form-control" Style="width: 65%; height: 20px"></asp:TextBox>
                                <span class="input-group-btn" style="height: 34px; display: block">
                                    <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                </span>
                            </div>
                            <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Status</b></td>
                        <td colspan="4">
                            <asp:RadioButton ID="statusA" runat="server" class="radio" Text="SEMUA" Checked="True" GroupName="status"></asp:RadioButton>
                            <asp:RadioButton ID="statusB" runat="server" class="radio" Text="BARU" GroupName="status"></asp:RadioButton>
                            <asp:RadioButton ID="statusID" runat="server" class="radio" Text="IDENTIFIKASI" GroupName="status"></asp:RadioButton>
                            <asp:RadioButton ID="statusS" runat="server" class="radio" Text="SOLVE" GroupName="status"></asp:RadioButton>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Project</b></td>
                        <td>
                            <asp:DropDownList ID="project" runat="server" Width="180" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                <asp:ListItem Value="SEMUA">Project :</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
                <br>
                <p style="padding: 3px; font: 8pt">
                </p>
                <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="No. Anonim" DataField="Anonim" />
                        <asp:BoundField HeaderText="Tgl. " DataField="Tgl" />
                        <%--<asp:BoundField HeaderText="Customer" DataField="Customer" ItemStyle-Width="200px" />--%>
                        <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Total" DataField="Total" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField HeaderText="Project" DataField="Project" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script type="text/javascript">
            function call(nomor) {
                popEditTA(nomor);
            }
        </script>
    </form>
</body>
</html>

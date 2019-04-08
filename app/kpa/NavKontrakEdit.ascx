<%@ Control Language="c#" Inherits="ISC064.KPA.NavKontrakEdit" CodeFile="NavKontrakEdit.ascx.cs" %>
<style>
/*.tab
{
    margin-left: 5px;
    border: none;
    background: #5c9bd1;
    width: 100px;
    height: 30px;
    float: left;
    margin: 0;
    margin-top:5px;
    margin-left: 10px;
    box-sizing: border-box;
    text-align: center;
    vertical-align:middle;
}
.tab a{
	color:white;
}*/
</style>
<div class="tab" id="editkontrak" runat="server" style="LEFT:5px">
	<a id="edit" runat="server"><b>Edit Kontrak</b></a>
</div>
<div id="detilkpa" runat="server" class="tab" style="LEFT:5px">
	<a id="detil" runat="server"><b>Detil KPR</b></a>
</div>
<div id="tagihankpa" runat="server" class="tab" style="LEFT:125px">
	<a id="tagihan" runat="server"><b>Tagihan KPR</b></a>
</div>
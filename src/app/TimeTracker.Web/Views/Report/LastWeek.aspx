<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ReportOutput>" %>
<%@ Import Namespace="TimeTracker.DTO"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Weekly Report
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Weekly Report</h2>
<table>
<thead><tr><th>Project</th><th>Task</th><th>Type</th>
<%
	for (int i = 0; i < 7; i++)
	{
		%>
		<th><%=Model.StartDate.AddDays(i).ToString("ddd, MMM d") %></th>
		<%
	}
	 %>
	<th>Total</th>
</tr></thead>
<tbody>
<%
	var totalHours = new double[7];
	foreach (var detail in Model.ReportDetails)
	{
		%>
		<tr>
		
		<td><%=detail.Project %></td>
		<td><%=detail.Task %></td>
		<td><%=detail.Type %></td>
		<%
		for (int i = 0; i < 7; i++)
		{
			%>
			<td><%=detail.Hours[i] > 0 ? detail.Hours[i].ToString("0.00") : "&nbsp;" %></td>
			<%
			totalHours[i] += detail.Hours[i];
		}
		 %>
		 <td><%=detail.Hours.Sum().ToString("0.00")%></td>
		 </tr>
		<%
	}
	 %>
<tfoot>
	<tr>
		<th>&nbsp;</th>
		<th>&nbsp;</th>
		<th>Total:</th>
		<%
		for (int i = 0; i < 7; i++)
		{
			%>
			<th><%=totalHours[i] > 0 ? totalHours[i].ToString("0.00") : "&nbsp;"%></th>
			<%
		}
		 %>
		 <th><%=totalHours.Sum().ToString("0.00")%></th>
	</tr>
</tfoot>
</tbody>
</table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

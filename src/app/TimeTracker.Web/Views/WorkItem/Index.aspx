<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<WorkItemListItem>>" %>
<%@ Import Namespace="MvcContrib.UI.Pager"%>
<%@ Import Namespace="MvcContrib.UI.Grid.ActionSyntax"%>
<%@ Import Namespace="MvcContrib.UI.Grid"%>
<%@ Import Namespace="MvcContrib.Pagination"%>
<%@ Import Namespace="TimeTracker.DTO"%>
<%@ Import Namespace="TimeTracker.Web.Controllers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    
	 <%using (Html.BeginForm())
	 {
	 %>
		Search: <%=Html.TextBox("search")%> <input type="submit" value="Search" />
	 <%
	 }
	 %>
    <%
		 var pageable = Model.AsPagination((int)ViewData["page"], 10);
		 var sort = (string)ViewData["sort"];
    	
    	Html.Grid(pageable)
			.Columns(c =>
			         	{
			         		c.For(w => 
									Html.ActionLink("Edit", "Edit", new { id=w.Id }) + " | " +
									Html.ActionLink("Details", "Details", new { id=w.Id }))
									.Named("").DoNotEncode();
			         		c.For(w => w.Name).HeaderAction(() => {
									var field = "Name";
									if (sort == field)
									{
										field += " DESC";
									}
									%>
			         			<th><%=Html.ActionLink("Name", "Index", new { sort = field })%></th>
			         		<%});
			         		c.For(w => w.ProjectTaskTypeName).Named("Project Task Type");
								c.For(w => w.LastStartTime).Action(w => {%>
									<td><%=(w.LastStartTime.Year == DateTime.MaxValue.Year) ? 
                	"Not Started" : w.LastStartTime.ToString() %></td>
								<%});
			         	}
			).Render();
    %>
    <%=Html.Pager(pageable) %>
    
    <%=Html.ActionLink<WorkItemController>(x => x.Create(), "Create Work Item") %>

</asp:Content>

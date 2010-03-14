<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Tag>>" %>
<%@ Import Namespace="Blog.Core" %>
<h1> Subjects</h1>
<ul class="tags">
    <% foreach(var tag in Model){ %>
    <li>
        <%=Html.ActionLink<PostController>(x=>x.PostsByTag(tag.TagName), tag.TagName) %>
    </li>
    <% } %>
</ul>

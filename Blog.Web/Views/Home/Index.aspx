<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Blog.Models.HomeModel>" %>
<%@ Import Namespace="Blog.Models"%>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
   <%=Model.Settings.Title %> -  <%=Model.Settings.Description %>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="posts">
    <% 
    foreach(var post in Model.Posts){
        Html.RenderPartial("Post",post);
    }
    %>
    </div>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ArchiveModel>" %>
<%@ Import Namespace="Blog.Web.Code"%>
<%@ Import Namespace="Blog.Models"%>
<%@ Import Namespace="System.Globalization"%>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
  Archive
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="archive">
            <%
                var previousMonth="";
                
                for(var i=0; i<Model.Posts.Count(); i++) {
                    var post = Model.Posts[i];
                    var currentMonth = CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(post.Created.Month);
                      
            %>
                <% if(currentMonth!=previousMonth){ %>
                <h2><%= currentMonth + " " + post.Created.Year %></h2>
                <%
                    previousMonth = currentMonth;
                   } %>
                <a href="<%=Html.PostUrl(post) %>"><%=post.Title %></a><br />
            <% } %>
    </div>
    
    


</asp:Content>

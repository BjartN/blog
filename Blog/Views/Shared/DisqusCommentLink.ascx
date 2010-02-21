<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Post>" %>
<%@ Import Namespace="Blog.Web.Code"%>
<%@ Import Namespace="Blog.Core" %>

<a class="dsq-comment-count" href="<% =Html.PostUrl(Model)%>#disqus_thread">Comments</a> 
    
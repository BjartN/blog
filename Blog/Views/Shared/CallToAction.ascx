<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Post>" %>
<%@ Import Namespace="Blog.Web.Code"%>
<%@ Import Namespace="Blog.Core" %>    

<a class="twitter-icon" href="<%=Html.TwitterUrl(Model) %>">
    <img alt="Share this post Twitter" src="../../Content/tweet-this.png" />
</a>
<a  id="fb_share" 
    title="Share this post on Facebook" 
    href="<%=Html.FacebookUrl(Model) %>">
F Share
</a>

<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Blog.Models.LogOnModel>" %>


<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Log On
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Log On</h2>
    <p>
        Please enter your username and password. 
    </p>
    <%= Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend>Account Information</legend>
                <ul>
                    <li>
                        <%= Html.LabelFor(m => m.UserName) %>
                    </li>
                    <li>
                        <%= Html.TextBoxFor(m => m.UserName) %>
                        <%= Html.ValidationMessageFor(m => m.UserName) %>
                    </li>
                    
                    <li>
                        <%= Html.LabelFor(m => m.Password) %>
                    </li>
                    <li>
                        <%= Html.PasswordFor(m => m.Password) %>
                        <%= Html.ValidationMessageFor(m => m.Password) %>
                    </li>
                    
                    <li>
                        <%= Html.CheckBoxFor(m => m.RememberMe) %>
                        <%= Html.LabelFor(m => m.RememberMe) %>
                    </li>
                    <li> 
                        <input type="submit" value="Log On" />
                    </li>
               </ul>
            </fieldset>
        </div>
    <% } %>
</asp:Content>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="menucontainer">
            
    <ul id="menu">              
        <% foreach (var verb in JBSolutions.Common.Web.Application.GetVerbsForCategory("Navigation")) { %>
            <li><%: Html.ActionLink(verb.Name, verb.Action, verb.Controller) %></li>
         <% } %>
    </ul>
            
</div>


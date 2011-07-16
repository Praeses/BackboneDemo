<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MasterPage.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="<%: Url.Content("~/Scripts/Site/Demo/Ticket.js") %>" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="TicketCollection">
        <ul>
        </ul>
        <button id="NewTicket">Add A Ticket</button>
    </div>

    <script type="text/x-handlebars-template" id="ticket-template">
        <fieldset>
            <div class='add_field'>
                <label for="NewField">Add Field</label>
                <input type="text" id='NewField' name="NewField">
            </div>
        </fieldset>
    </script>

    <script type="text/x-handlebars-template" id="new-field-template">
        <div>
            <label for="{{ id }}">{{ id }}</label>
            <input type="text" id='{{ id }}' name="{{ id }}">
        </div>
    </script>

</asp:Content>



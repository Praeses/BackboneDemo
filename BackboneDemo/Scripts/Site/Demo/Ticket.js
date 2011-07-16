var Ticket = Backbone.Model.extend({
});

var Tickets = Backbone.Collection.extend({
    url: "../../Demo/Ticket"
});


$(function () {

    window.TicketsView = Backbone.View.extend({
        el: $("#TicketCollection"),

        initialize: function () {
            this.model = new Tickets();
            this.model.view = this;
            this.render();
        },

        AddTicket: function () {
            var ticket = new Ticket();
            this.model.add(ticket);
            this.$('ul').append(new TicketView({ model: ticket }).el);
        },

        events: { "click #NewTicket": "AddTicket" },

        render: function () {
        }

    });


    window.TicketView = Backbone.View.extend({
        tagName: 'li',

        initialize: function () {
            this.model.view = this;
            this.render();
        },

        events: {
            "keydown #NewField": "NewField",
            "keydown input:not(#NewField)": "inputChanged"
        },

        NewField: function (e) {
            if (e && e.keyCode == 13) {
                var text = $(e.target).val();
                if ($("#" + text).length == 0) {
                    this.AddField(text);
                }
            }
        },

        inputChangedCallback: function () { this.model.save( this.model.toJSON() ); },

        AddField: function (text) {
            this.$('fieldset').append(this.newFieldTemplate({ id: text }));
        },

        template: Handlebars.compile($("#ticket-template").html()),
        newFieldTemplate: Handlebars.compile($("#new-field-template").html()),

        render: function () {
            $(this.el).html(this.template(this.model));
            window.temp = this.model;
        }

    });




    new TicketsView();
});
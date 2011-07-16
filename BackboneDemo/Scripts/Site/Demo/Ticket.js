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
            _.bindAll(this, "DataReady");
            this.render();
        },

        events: { "click #NewTicket": "AddTicket" },

        AddTicket: function () {
            var ticket = new Ticket();
            this.model.add(ticket);
            this.DisplayTicket(ticket);
        },

        DisplayTicket: function (ticket) {
            this.$('ul').append(new TicketView({ model: ticket }).el);
        },

        DataReady: function (collection) {
            _.each(collection.models, this.DisplayTicket);
        },

        render: function () {
            this.model.fetch({ success: this.DataReady });
        }

    });


    window.TicketView = Backbone.View.extend({
        tagName: 'li',

        initialize: function () {
            this.model.view = this;
            _.bindAll(this, "AddField", "refresh");
            this.render();
        },

        events: {
            "keyup #NewField": "NewField",
            "keyup input:not(#NewField)": "inputChanged",
            "blur input": "valueChanged"
        },

        NewField: function (e) {
            if (e && e.keyCode == 13) {
                var text = $(e.target).val();
                if (this.$("#" + text).length == 0) {
                    this.AddField(text);
                }
            }
        },

        valueChanged: function () { this.model.save(this.model.toJSON()); },

        AddField: function (text) {
            this.$('fieldset').append(this.newFieldTemplate({ id: text }));
            this.bindField(this.$("#" + text)[0], this);
        },

        template: Handlebars.compile($("#ticket-template").html()),
        newFieldTemplate: Handlebars.compile($("#new-field-template").html()),

        createFieldsFromModel: function () {
            var keys = _.map(JSON.stringify(this.model.toJSON()).match(/"[^"]*":/g)
                , function (raw) { return raw.match(/"(.+)":/)[1] });
            var keys = _.without(keys, "id");
            _.each(keys, this.AddField);
        },

        refresh:function(){
            if( $('input:focus').length == 0) {this.model.fetch(); }
        },

        render: function () {
            $(this.el).html(this.template(this.model));
            this.createFieldsFromModel();
            var model = this.model;
            setInterval( this.refresh, 1000);
        }

    });


    new TicketsView();
});
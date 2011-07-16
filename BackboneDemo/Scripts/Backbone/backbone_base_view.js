Backbone.View.prototype.bindFields = function (root) {
    var view = this;
    if (typeof (root) != 'undefined') {
        $('input, select, span, textarea', root).each(function (i, c) { view.bindField(c, view); });
    }
    else {
        view.$('input, select, span, textarea').each(function (i, c) { view.bindField(c, view);});
    }
};


Backbone.View.prototype.bindField = function (c,view) {
    view.model.bind("change:" + c.id, function () {
        if ($(c).get(0).tagName == 'SPAN') { $(c).text(this.get(c.id)); }
        else if ($(c).get(0).tagName == "INPUT"
            && !_.isEmpty(c.type) && c.type == "checkbox") {
            c.checked = this.get(c.id)
        }
        else { $(c).val(this.get(c.id)); }
    });
    view.model.trigger("change:" + c.id, this.model)
};


Backbone.Model.prototype.print = function () {
    console.log(JSON.stringify(this.toJSON()));
};



Backbone.View.prototype._updateModelByTagId = function (e) {
    var field = e.target.attributes['id'].value;
    var value = null;
    if (!_.isEmpty(e.target.type) && e.target.type == "checkbox") { value = e.target.checked; }
    else { value = $.trim($(e.target).val()); }
    var obj = jQuery.parseJSON('{"' + field + '": ' + JSON.stringify(value) + '}');
    this.model.set(obj);
}

Backbone.View.prototype.textareaChanged = function (e) {
    this._updateModelByTagId(e);
    if (typeof (this.textareaChangedCallback) == "function") { this.textareaChangedCallback(); }
};


Backbone.View.prototype.inputChanged = function (e) {
    this._updateModelByTagId(e);
    if (typeof (this.inputChangedCallback) == "function") { this.inputChangedCallback(); }
};

Backbone.View.prototype.selectChanged = function (e) {
    var field = e.target.attributes['id'].value;
    var value = $.trim($("option:selected", e.target).val());
    if (!isNaN(value)) { value = parseFloat(value); }
    var field_display = e.target.attributes['id'].value.match(/(.+)UID$/)[1] + "Name";
    var value_display = $.trim($("option:selected", e.target).text());
    var obj = jQuery.parseJSON('{"' + field + '": ' + JSON.stringify(value)
        + ',"' + field_display + '": ' + JSON.stringify(value_display) + '  }');
    this.model.set(obj);
    if (typeof (this.selectChangedCallback) == "function") { this.selectChangedCallback(); }
};




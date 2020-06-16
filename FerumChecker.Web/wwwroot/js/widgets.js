if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
                ? args[number]
                : match
                ;
        });
    };
}

var BasicWidgets = {}

BasicWidgets.confirmWindow = function (message, header = "") {
    var content = '<div class="modal" tabindex="-1" role="dialog">' +
        '<div class="modal-dialog" role = "document" >' +
        '<div class="modal-content">' +
        '<div class="modal-header">' +
        '<h5 class="modal-title">' + header + '</h5>' +
        '</div>' +
        '<div class="modal-body"> ' +
        '<p>' + message + '</p>' +
        '</div>' +
        '<div class="modal-footer">' +
        '<button type="button" class="btn btn-success confirm-btn">Ок</button>' +
        '<button type="button" class="btn btn-secondary close-btn" data-dismiss="modal">Закрити</button>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>';

    var dialog = $(content).appendTo(document.body);


    $(dialog).modal("show");

    return dialog;
}


BasicWidgets.loading = function (elem, flag) {
    if (flag) {
        elem.addClass('disabledelement');
        var height = elem.height();
        var width = elem.width();
        var loader = "<div class='loader'></div>";
        loader = $(loader).appendTo(elem);
        loader.css("position", "absolute");
        loader.css("top", elem.position().top);
        loader.css("left", elem.position().left);
    } else {
        elem.removeClass('disabledelement');
        elem.find('.loader').remove();
    }
}
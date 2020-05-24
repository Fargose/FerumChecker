EditableDataTable = function () {
    this.TableContainer = null;
    this.Url = null;
}

EditableDataTable.prototype.init = function (tableContainer, url) {
    var self = this;
    this.TableContainer = tableContainer;
    this.Url = url;

    this.TableContainer.on("click", ".create-link", function (e) {
        self.createItem();
    })

    this.TableContainer.on("click", ".delete-link", function (e) {
        self.deleteItem(e);
    })

    this.TableContainer.on("click", ".edit-link", function (e) {
        self.editItem(e);
    })
}

EditableDataTable.prototype.getRowTemplate = function () {
    return '<tr data-id="{0}">' +
        '<td>{1}</td>' + 
        '<td>' + 
            '<a class="edit-link"><i class="fas fa-edit"></i></a>' + 
            '<a class="delete-link"><i class="fas fa-trash"></i></a>' + 
        '</td>' +
    '</tr>'
}

EditableDataTable.prototype.getEditTemplate = function () {
    return "<tr><td colspan = '2'><div class='table-edit-container'></td></tr>"
}

EditableDataTable.prototype.createItem = function () {
    var self = this;
    var template = this.getEditTemplate();

    //this.TableContainer.find("tbody").prepend(template);
    var insertedRow = $(template).prependTo(this.TableContainer.find("tbody"));

    $.ajax({
        url: "/" + self.Url + "/Create",
        type: "GET",
        dataType: "html",
        success: function (content) {
            $(insertedRow).find('.table-edit-container').html(content);
            $(insertedRow).find("form").on("submit", function (e) {
                saveItem(e)
            });
        }
    });

    function saveItem(e) {
        e.preventDefault();
        BasicWidgets.loading($(".catalog-list-container"), true);
        //var formData = $(e.target).serialize();
        var formData = new FormData(e.target);
        $.ajax({
            url: "/" + self.Url + "/Create",
            type: "POST",
            data: formData,
            processData: false,
            contentType : false,
            success: function (data, textStatus, jqXHR) {
                if (data.id) {
                    BasicWidgets.loading($(".catalog-list-container"), false);
                    $(insertedRow).remove();
                    self.TableContainer.find("tbody").prepend(self.getRowTemplate().format(data.id, data.fullName));
                }
                else {
                    BasicWidgets.loading($(".catalog-list-container"), false);
                    $(insertedRow).find('.table-edit-container').html(data);
                    $(insertedRow).find("form").on("submit", function (e) {
                        saveItem(e)
                    });
                }
            }
        });
    }
}


EditableDataTable.prototype.deleteItem = function (e) {
    var self = this;
    var row = $(e.target).closest("tr");
    //alert($(row).attr("data-id"));
    var confirm = BasicWidgets.confirmWindow("Ви дійсно хочете видалити цей елемент?");
    console.log(confirm);
    $(confirm).find(".confirm-btn").on("click", function () {
        BasicWidgets.loading($(".catalog-list-container"), true);
        $.ajax({
            url: "/" + self.Url + "/Delete/" + $(e.target).closest("tr").attr("data-id") ,
            type: "POST",
            success: function (data) {
                BasicWidgets.loading($(".catalog-list-container"), false);
                $(row).remove();
                $(confirm).modal("hide");
                $(confirm).remove();
            }
        });
    })

}


EditableDataTable.prototype.editItem = function (e) {

    var self = this;
    var template = this.getEditTemplate();

    //this.TableContainer.find("tbody").prepend(template);
    var insertedRow = $(template).prependTo(this.TableContainer.find("tbody"));

    var row = $(e.target).closest("tr");
    $(row).hide();

    $.ajax({
        url: "/" + self.Url + "/Edit",
        type: "GET",
        data: { id: $(e.target).closest("tr").attr("data-id")},
        dataType: "html",
        success: function (content) {
            $(insertedRow).find('.table-edit-container').html(content);
            $(insertedRow).find("form").on("submit", function (e) {
                updateItem(e)
            });
        }
    });

    function updateItem(e) {
        e.preventDefault();
        var formData = new FormData(e.target);
        BasicWidgets.loading($(".catalog-list-container"), true);
        $.ajax({
            url: "/" + self.Url + "/Edit",
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                if (data.id) {
                    BasicWidgets.loading($(".catalog-list-container"), false);
                    $(row.find("td")[0]).html(data.fullName);
                    $(row).show();
                    $(insertedRow).remove();
                }
                else {
                    BasicWidgets.loading($(".catalog-list-container"), false);
                    $(insertedRow).find('.table-edit-container').html(data);
                    $(insertedRow).find("form").on("submit", function (e) {
                        updateItem(e)
                    });
                }
            }
        });
    }

    //var self = this;
    //var row = $(e.target).closest("tr");
    //var name = $(row.find("td")[0]).html().trim();
    //var editRow = this.getEditTemplate().format(name);
    //editRow = $(editRow).insertAfter(row);
    //row.hide();
    //$(editRow).find(".add-confirm").click(function () {
    //    var newName = $(editRow).find('.name-input').val();
    //    if (name == newName) {
    //        $(editRow).remove();
    //        row.show();
    //    } else {
    //        $.ajax({
    //            url: "/" + self.Url + "/Edit",
    //            data: { Id: $(e.target).closest("tr").attr("data-id"), Name: newName },
    //            type: "POST",
    //            success: function (data) {
    //                $(editRow).remove();
    //                $(row.find("td")[0]).html(data.name);
    //                row.show();
    //            }
    //        });
    //    }
    //})
}
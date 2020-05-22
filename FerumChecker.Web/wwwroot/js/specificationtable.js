SpecificationTable = function () {
    this.TableContainer = null;
    this.Url = null;
}

SpecificationTable.prototype.init = function (tableContainer, url) {
    var self = this;
    this.TableContainer = tableContainer;
    this.Url = url;

    this.TableContainer.on("click", ".create-link", function (e) {
        self.createSpecification();
    })

    this.TableContainer.on("click", ".delete-link", function (e) {
        self.deleteSpecification(e);
    })

    this.TableContainer.on("click", ".edit-link", function (e) {
        self.editSpecification(e);
    })
}

SpecificationTable.prototype.getRowTemplate = function () {
    return '<tr data-id="{0}">' +
        '<td>{1}</td>' + 
        '<td>' + 
            '<a class="edit-link"><i class="fas fa-edit"></i></a>' + 
            '<a class="delete-link"><i class="fas fa-trash"></i></a>' + 
        '</td>' +
    '</tr>'
}

SpecificationTable.prototype.getEditTemplate = function () {
    return "<tr><td><input type='text' class='form-control name-input' value='{0}'></input></td><td><a class='add-confirm'><i class='fas fa-save'></i></a></td></tr>"
}

SpecificationTable.prototype.createSpecification = function () {
    var self = this;
    var template = this.getEditTemplate().format('');

    //this.TableContainer.find("tbody").prepend(template);
    var insertedRow = $(template).prependTo(this.TableContainer.find("tbody"));

    insertedRow.find(".add-confirm").click(function () {
        var name = $(insertedRow).find('.name-input').val();

        $.ajax({
            url: "/" + self.Url + "/Create",
            data: { Name: name },
            type: "POST",
            success: function (data) {
                insertedRow.remove();
                var rowTemplate = self.getRowTemplate();
                self.TableContainer.find("tbody").prepend(rowTemplate.format(data.id, data.name));
            }
        });
    })
}


SpecificationTable.prototype.deleteSpecification = function (e) {
    var self = this;
    var row = $(e.target).closest("tr");
    //alert($(row).attr("data-id"));
    var confirm = BasicWidgets.confirmWindow("Ви дійсно хочете видалити цей елемент?");
    console.log(confirm);
    $(confirm).find(".confirm-btn").on("click", function () {
        $.ajax({
            url: "/" + self.Url + "/Delete/" + $(e.target).closest("tr").attr("data-id") ,
            type: "POST",
            success: function (data) {
                $(row).remove();
                $(confirm).modal("hide");
                $(confirm).remove();
            }
        });
    })

}


SpecificationTable.prototype.editSpecification = function (e) {
    var self = this;
    var row = $(e.target).closest("tr");
    var name = $(row.find("td")[0]).html().trim();
    var editRow = this.getEditTemplate().format(name);
    editRow = $(editRow).insertAfter(row);
    row.hide();
    $(editRow).find(".add-confirm").click(function () {
        var newName = $(editRow).find('.name-input').val();
        if (name == newName) {
            $(editRow).remove();
            row.show();
        } else {
            $.ajax({
                url: "/" + self.Url + "/Edit",
                data: { Id: $(e.target).closest("tr").attr("data-id"), Name: newName },
                type: "POST",
                success: function (data) {
                    $(editRow).remove();
                    $(row.find("td")[0]).html(data.name);
                    row.show();
                }
            });
        }
    })
}
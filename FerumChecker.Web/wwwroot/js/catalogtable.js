CatalogTable = function () {
    this.TableContainer = null;
    this.Url = null;
}

CatalogTable.prototype.init = function (tableContainer, url) {
    var self = this;
    this.TableContainer = tableContainer;
    this.Url = url;

    this.TableContainer.on("click", ".delete-link", function (e) {
        self.deleteSpecification(e);
    })
}


CatalogTable.prototype.deleteSpecification = function (e) {
    var self = this;
    var row = $(e.target).closest("tr");
    //alert($(row).attr("data-id"));
    var confirm = BasicWidgets.confirmWindow("Ви дійсно хочете видалити цей елемент?");
    console.log(confirm);
    $(confirm).find(".confirm-btn").on("click", function () {
        $.ajax({
            url: "/" + self.Url + "/Delete/" + $(e.target).closest("tr").attr("data-id"),
            type: "POST",
            success: function (data) {
                $(row).remove();
                $(confirm).modal("hide");
                $(confirm).remove();
            }
        });
    })

}
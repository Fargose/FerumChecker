AssemblyViewer = function () {
    this.TableContainer = null;
    this.Url = null;
    this.ComputerAssemblyId = null;
}

AssemblyViewer.prototype.init = function (id) {

    var self = this;
    this.ComputerAssemblyId = id;


    $('[data-toggle="tooltip"]').tooltip();



    $(".editor-container").on("contextmenu", ".hardware-slot", function (e) {
        e.preventDefault();
        var id = $(this).attr("data-id");
        var type = $(this).attr("data-type");
        if (id) {
            self.showDescription(id, type);
        }
    });
}



AssemblyViewer.prototype.showDescription = function (id, type) {
    $.ajax({
        url: "/" + type + "/PartialDetails",
        data: { id: id },
        success: function (content) {
            ;
            var confirm = BasicWidgets.confirmWindow(content);
            $(confirm).find(".confirm-btn").remove();
            $(confirm).find(".modal-content").width(1200);
            $(confirm).css("left", "-300px");
            $(confirm).find(".cancel-btn").on("click", function () {
                $(confirm).modal("hide");
                $(confirm).remove();
            });
        }
    });
}


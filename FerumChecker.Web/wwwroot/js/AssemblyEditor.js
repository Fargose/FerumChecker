AssemblyEditor = function () {
    this.TableContainer = null;
    this.Url = null;
    this.ComputerAssemblyId = null;
}

AssemblyEditor.prototype.init = function (id) {

    var self = this;
    this.ComputerAssemblyId = id;

    var categories = $(".category-container");
    for (var i = 0; i < categories.length; i++) {
        self.initContainer($(categories[i]));
    }

    $('[data-toggle="tooltip"]').tooltip(); 
}


AssemblyEditor.prototype.initContainer = function (elem) {
    var self = this;
    var row = elem.find(".category-link");
    var container = elem.find(".items-container");
    container.on("dblclick", '.catalog-item', function (event) {
        self.setHardware($(event.target).closest("tr").attr("data-id"), row.attr("data-link"), $(event.target).closest("tr").attr("data-image"), $(event.target).closest("tr").find(".item-name").html());
    });
    container.on('contextmenu', '.catalog-item', function (event) {
        event.preventDefault();
        $.ajax({
            url: "/" + row.attr("data-link") + "/PartialDetails",
            data: { id: $(event.target).closest("tr").attr("data-id") },
            success: function (content) {
                var confirm = BasicWidgets.confirmWindow(content);
                $(confirm).find(".confirm-btn").remove();
                $(confirm).find(".modal-content").width(1200);
                $(confirm).css("left", "-300px");
                $(confirm).find(".cancel-btn").on("click", function () {
                    $(confirm).modal("hide");
                    $(confirm).remove();
                });
            }
        })
        
    });
    row.on("click", function (e) {
        $(".category-link").removeClass("selected");
        BasicWidgets.loading(container, true);
        $(e.target).addClass("selected");
        var loader = $.ajax({
            url: "/" + $(e.target).attr("data-link") + "/SmallList",
            type: "GET"
        });

        loader.done(function (content) {
            BasicWidgets.loading(container, false);
            container.html(content);
            self.CurrentTable = $(e.target).attr("data-link");
            $(".items-container").hide();
            container.show();
        });
    });
}

AssemblyEditor.prototype.setHardware = function (id, type, image, name) {

    $.ajax({
        url: "/" + type + "/SetHardware",
        data: { id: id, assemblyId: this.ComputerAssemblyId },
        success(data) {
            if (data.succedeed) {
                var slot = type.toLowerCase() + "-slot";
                $("." + slot).find(".hardware-img").attr("src", image);
                $("." + slot).attr("data-original-title", name);
                if (data.warning) {
                    var confirm = BasicWidgets.confirmWindow(data.warning, "Попередження");
                    $(confirm).find(".confirm-btn").remove();
                    $(confirm).find(".cancel-btn").on("click", function () {
                        $(confirm).modal("hide");
                        $(confirm).remove();
                    });
                }
            }
            else {
                var confirm = BasicWidgets.confirmWindow(data.message, "Несумісність");
                $(confirm).find(".confirm-btn").remove();
                $(confirm).find(".cancel-btn").on("click", function () {
                    $(confirm).modal("hide");
                    $(confirm).remove();
                });
            }
        }
    });
}
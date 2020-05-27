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

    $(".editor-container").on("dblclick", ".hardware-slot", function (e) {
        var id = $(this).attr("data-id");
        var type = $(this).attr("data-type");
        if (id) {
            self.removeHardware($(this));
        }
    });


    $(".editor-container").on("contextmenu", ".hardware-slot", function (e) {
        e.preventDefault();
        var id = $(this).attr("data-id");
        var type = $(this).attr("data-type");
        if (id) {
            self.showDescription(id, type);
        }
    });

    $(".recomendation-container").on("dblclick", ".catalog-item", function (e) {
        var id = $(this).attr("data-id");
        var type = $(this).attr("data-type");
        var image = $(this).attr("data-image");
        var name = $(this).attr("data-name");
        var multiple = false;
        if (type.toLowerCase() == "ram" || type.toLowerCase() == "hdd" || type.toLowerCase() == "ssd") {
            multiple = true;
        }
        if (id) {
            self.setHardware(id, type, image, name, multiple);
        }
    });

    $(".recomendation-container").on("contextmenu", ".catalog-item", function (e) {
        e.preventDefault();
        var id = $(this).attr("data-id");
        var type = $(this).attr("data-type");
        if (id) {
            self.showDescription(id, type);
        }
    });

    this.updateRecomendation();
}


AssemblyEditor.prototype.initContainer = function (elem) {
    var self = this;
    var row = elem.find(".category-link");
    var container = elem.find(".items-container");
    container.on("dblclick", '.catalog-item', function (event) {
        self.setHardware($(event.target).closest("tr").attr("data-id"), row.attr("data-link"), $(event.target).closest("tr").attr("data-image"), $(event.target).closest("tr").find(".item-name").html(), $(event.target).closest("tr").hasClass("multiple"));
    });
    container.on('contextmenu', '.catalog-item', function (event) {
        event.preventDefault();
        var type = row.attr("data-link");
        var id = $(event.target).closest("tr").attr("data-id");
        self.showDescription(id, type);


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

AssemblyEditor.prototype.setHardware = function (id, type, image, name, multiple = false) {
    var self = this;
    $.ajax({
        url: "/" + type + "/SetHardware",
        data: { id: id, assemblyId: this.ComputerAssemblyId },
        success(data) {
            if (data.succedeed) {
                if (multiple) {
                    if (type.toLowerCase() == "ssd" || type.toLowerCase() == "hdd") {
                        var elem = $(".outer-memory-slot[data-id='']")
                    }
                    else {
                        var elem = $(".hardware-slot[data-id=''][data-type='" + type + "']")
                    }
                    if (elem && elem.length) {
                        $(elem[0]).find(".hardware-img").attr("src", image);
                        $(elem[0]).attr("data-original-title", name);
                        $(elem[0]).attr("data-id", id);
                    } else {
                        if (type.toLowerCase() == "ram") {
                            $(".ram-slot-container").append('<div class="hardware-slot ram-slot multiple" data-toggle="tooltip" data-original-title="' + name + '" data-type="' + type + '" data-id="'+id+'"><img class="hardware-img" src = "' + image + '"/></div>')
                        } else {
                            $(".outermemory-slot-container").append('<div class="hardware-slot outer-memory-slot multiple" data-toggle="tooltip" data-original-title="' + name + '" data-type="' + type + '" data-id="'+id+'"><img class="hardware-img" src = "' + image + '"/></div>')
                        }
                    }

                }
                else {
                    var slot = type.toLowerCase() + "-slot";
                    $("." + slot).find(".hardware-img").attr("src", image);
                    $("." + slot).attr("data-id", id);
                    $("." + slot).attr("data-original-title", name);
                    if (data.ramFree) {
                        var size = $(".ram-slot").length;
                        if (data.ramFree >= size) {
                            for (var i = size; i < data.ramFree; i++) {
                                $(".ram-slot-container").append('<div class="hardware-slot ram-slot multiple" data-toggle="tooltip" title="Пусто" data-type="RAM" data-id=""><img class="hardware-img" src = ""/></div>')
                            }
                        } else {
                            $(".ram-slot[data-id='']").remove();
                        }
                    }
                    if (data.memoryFree) {
                        var size = $(".outer-memory-slot").length;
                        if (data.memoryFree >= size) {
                            for (var i = size; i < data.memoryFree; i++) {
                                $(".outermemory-slot-container").append('<div class="hardware-slot outer-memory-slot multiple" data-toggle="tooltip" title="Пусто" data-type="" data-id=""><img class="hardware-img" src = ""/></div>')
                            }
                        } else {
                            $(".outer-memory-slot[data-id='']").remove();
                        }
                    }
                }
                if (data.warnings && data.warnings.length > 0) {
                    var list = "<ul>";
                    for (var i = 0; i < data.warnings.length; i++) {
                        list += "<li>" + data.warnings[i] + "</li>";
                    }
                    list += "</ul>";
                    var confirm = BasicWidgets.confirmWindow(list, "Попередження");
                    $(confirm).find(".confirm-btn").remove();
                    $(confirm).find(".cancel-btn").on("click", function () {
                        $(confirm).modal("hide");
                        $(confirm).remove();
                    });
                }
            }
            else {
                if (data.messages) {
                    var list = "<ul>";
                    for (var i = 0; i < data.messages.length; i++) {
                        list += "<li>" + data.messages[i] + "</li>";
                    }
                    list += "</ul>";
                    var confirm = BasicWidgets.confirmWindow(list, "Несумісність");
                    $(confirm).find(".confirm-btn").remove();
                    $(confirm).find(".cancel-btn").on("click", function () {
                        $(confirm).modal("hide");
                        $(confirm).remove();
                    });
                }
            }
            self.updateRecomendation();
        }
    });
}

AssemblyEditor.prototype.removeHardware = function (elem) {
    var self = this;
    var id = elem.attr("data-id");
    var type = elem.attr("data-type");
    $.ajax({
        url: "/" + type + "/RemoveHardware",
        data: { assemblyId: this.ComputerAssemblyId, hardwareId: id },
        success(data) {
            if (data.succedeed) {
                elem.find(".hardware-img").attr("src", "");
                elem.attr("data-original-title", "Пусто");
                elem.attr("data-id", "");
                if (data.ramFree) {
                    var size = $(".ram-slot").length;
                    while ($(".ram-slot[data-id='']").length > data.ramFree) {
                        $(".ram-slot[data-id='']")[0].remove();
                    }
                    if ($(".ram-slot").length == 0) {
                        $(".ram-slot-container").append('<div class="hardware-slot ram-slot multiple" data-toggle="tooltip" title="Пусто" data-type="RAM" data-id=""><img class="hardware-img" src = ""/></div>')
                    }
                }
                if (data.memoryFree) {
                    var size = $(".ram-slot").length;
                    while ($(".outer-memory-slot[data-id='']").length > data.memoryFree) {
                        $(".outer-memory-slot[data-id='']")[0].remove();
                    }
                    if ($(".outer-memory-slot").length == 0) {
                        $(".outermemory-slot-container").append('<div class="hardware-slot outer-memory-slot multiple" data-toggle="tooltip" data-original-title="' + name + '" data-type="' + type + '" data-id="' + id + '"><img class="hardware-img" src = "' + image + '"/></div>')
                    }
                }
            }
            else {
                alert("Щось пішло не так");
            }
            self.updateRecomendation();
        }
    });
}

AssemblyEditor.prototype.showDescription = function (id, type) {
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

AssemblyEditor.prototype.updateRecomendation = function () {
    var self = this;
    $.ajax({
        url: "/Editor/GetRecomendation",
        data: { assemblyId: self.ComputerAssemblyId },
        success: function (content) {
            $(".recomendation-container").html(content);
        }
    })
}
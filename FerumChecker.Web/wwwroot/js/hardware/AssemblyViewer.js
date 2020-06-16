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

    $(".button-add-coment").click(function(e){
        self.createComment();
    });

    $(".comments-container").on("click", ".delete-link", function (e) {
        self.deleteItem(e);
    })

    $.ajax({
        url: "/Editor/AssemblyStat",
        data: { assemblyId: self.ComputerAssemblyId },
        success: function (content) {
            $(".stat-container-detail").find(".content").html(content);
        }
    })
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

AssemblyViewer.prototype.getRowTemplate = function () {
    return '<div class="comment-item" da-id="{0}">' +
        '<h4>{1}</h4>' +
        '<a style="float: right" class="delete-link"><i class="fas fa-trash"></i></a>' +
        '<div class="comment-text">{2}</div>' +
        '</div >'
}

AssemblyViewer.prototype.createComment = function () {
    var self = this;
    var template = "<div class='comment-item'></div>";

    //this.TableContainer.find("tbody").prepend(template);
    var insertedRow = $(template).prependTo($('.comments-container'));

    $.ajax({
        url: "/Editor/CreateComment",
        type: "GET",
        dataType: "html",
        success: function (content) {
            $(insertedRow).html(content);
            $(insertedRow).find("form").on("submit", function (e) {
                saveItem(e)
            });
        }
    });

    function saveItem(e) {
        e.preventDefault();
        BasicWidgets.loading($("body"), true);
        var formData = $(e.target).serialize();
        var text = e.target.elements["Text"].value;
        var id = self.ComputerAssemblyId;
        //var formData = new FormData(e.target);
        $.ajax({
            url: "/Editor/CreateComment",
            type: "POST",
            data: {Text : text, ComputerAssemblyId : id},
            success: function (data, textStatus, jqXHR) {
                if (data.id) {
                    BasicWidgets.loading($("body"), false);
                    $(insertedRow).remove();
                    $(".comments-container").prepend(self.getRowTemplate().format(data.id, data.owner, data.text));
                }
                else {
                    BasicWidgets.loading($("body"), false);
                    $(insertedRow).html(data);
                    $(insertedRow).find("form").on("submit", function (e) {
                        saveItem(e)
                    });
                }
            }
        });
    }
}



AssemblyViewer.prototype.deleteItem = function (e) {
    var self = this;
    var row = $(e.target).closest(".comment-item");
    //alert($(row).attr("data-id"));
    var confirm = BasicWidgets.confirmWindow("Ви дійсно хочете видалити цей коментар?");
    console.log(confirm);
    $(confirm).find(".confirm-btn").on("click", function () {
        BasicWidgets.loading($("body"), true);
        $.ajax({
            url: "/Editor/CommentDelete/" + $(e.target).closest(".comment-item").attr("data-id"),
            type: "POST",
            success: function (data) {
                BasicWidgets.loading($("body"), false);
                $(row).remove();
                $(confirm).modal("hide");
                $(confirm).remove();
            }, fail: function () {
                $(confirm).find(".modal-body").html("Cхоже шось пішло не так :(");
                $(confirm).find(".confirm-btn").remove();
            }
        });
    })

}
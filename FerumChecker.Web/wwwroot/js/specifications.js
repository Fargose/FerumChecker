CatalogPage = function () {
    this.TypingTimer = null;
    this.DoneTypingInterval = 1500;
    this.CurrentTable = null;
}

CatalogPage.prototype.init = function (startView = null) {
    var self = this;

    if (startView) {
        this.initFirstPage(startView);
    }

    $(".category-link").on("click", function (e) {
        $(".category-link").removeClass("selected");
        BasicWidgets.loading($(".catalog-list-container"), true);
        $(e.target).addClass("selected");
        var loader = $.ajax({
            url: "/" + $(e.target).attr("data-link") + "/PartialIndex",
            type: "GET"
        });

        loader.done(function (content) {
            BasicWidgets.loading($(".catalog-list-container"), false);
            $(".catalog-list-container").html(content);
            $(".сategory-name").html($(e.target).html());
            self.CurrentTable = $(e.target).attr("data-link");
            $(".search-box").show();
        });
    });


    $("#search-input").unbind("keyup").keyup(function () {
        clearTimeout(self.TypingTimer);
        self.TypingTimer = setTimeout(function () { self.searchDone() },  self.DoneTypingInterval);
    });
}



CatalogPage.prototype.searchDone = function() {
    this.updateTable($("#search-input").val());
}

CatalogPage.prototype.updateTable = function (searchString) {
    BasicWidgets.loading($(".catalog-list-container"), true);
    var self = this;
    var data = {
        search: searchString
    };
    $.ajax({
        url: "/" + self.CurrentTable + "/PartialIndex",
        type: "GET",
        data: data,
        success: function (content) {
            BasicWidgets.loading($(".catalog-list-container"), false);
            $(".catalog-list-container").html(content);
        }
    });
}


CatalogPage.prototype.initFirstPage = function (page) {
    BasicWidgets.loading($(".catalog-list-container"), true);
    var self = this;
    $("a[data-link = " + page + "]").addClass("selected");
    $(".сategory-name").html($("a[data-link = " + page + "]").html());
    $.ajax({
        url: "/" + page + "/PartialIndex",
        type: "GET",
        success: function (content) {
            BasicWidgets.loading($(".catalog-list-container"), false);
            $(".catalog-list-container").html(content);
        }
    });
}
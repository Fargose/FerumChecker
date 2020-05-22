CatalogPage = function () {
    this.TypingTimer = null;
    this.DoneTypingInterval = 1500;
    this.CurrentTable = null;
}

CatalogPage.prototype.init = function () {
    var self = this;

    $(".category-link").on("click", function (e) {
        $(".category-link").removeClass("selected");
        $(e.target).addClass("selected");
        var loader = $.ajax({
            url: "/" + $(e.target).attr("data-link") + "/PartialIndex",
            type: "GET"
        });

        loader.done(function (content) {
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
    var self = this;
    var data = {
        search: searchString
    };
    $.ajax({
        url: "/" + self.CurrentTable + "/PartialIndex",
        type: "GET",
        data: data,
        success: function (content) {
            $(".catalog-list-container").html(content);
        }
    });
 }
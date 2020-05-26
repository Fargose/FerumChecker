PCCaseEditPage = function () {
    this.Action = null;
}

PCCaseEditPage.prototype.init = function (action) {
    var self = this;
    this.Action = action

    $(".custom-file-input").on("change", function (data) {
        var fileName = $(this).val().split("\\").pop();
        console.log(data);
        if (data.currentTarget.files && data.currentTarget.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#country-image').attr('src', e.target.result)
            };

            reader.readAsDataURL(data.currentTarget.files[0]);
        }
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });

    this.initMultiBox($("#outer-memory-form-factor-select"), "PCCaseOuterMemoryFormFactors");
    this.initMultiBox($("#motherboard-form-factor-select"), "PCCaseMotherBoardFormFactors");

    $("form").submit(function (e) {
        e.preventDefault();
        var formData = new FormData(e.target);
        self.addArrayToFormData(formData, self.PCCaseOuterMemoryFormFactors, "PCCaseOuterMemoryFormFactors");
        self.addArrayToFormData(formData, self.PCCaseMotherBoardFormFactors, "PCCaseMotherBoardFormFactors");
        $.ajax({
            url: "/PCCase/" + self.Action,
            type: "POST",
            dataType: "html",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                if (data.id) {
                    window.location.href = "/Catalog/Index";
                } else {
                    $(document.body).html(data);
                }
            }
        });
    });
}


PCCaseEditPage.prototype.initMultiBox = function (elem, propertyName) {
    var self = this;
    this.clickInit(elem, propertyName);
}

PCCaseEditPage.prototype.clickInit = function (elem, propertyName) {
    var self = this;
    var select = elem.find(".combo-select");
    var list = elem.find(".selected-list");
    this[propertyName] = [];
    var items = list.find("li");
    for (var i = 0; i < items.length; i++) {
        this[propertyName].push({ Id: $(items[i]).attr("data-id") });
    }
    elem.find(".add-item").click(function (e) {
        self.addItem(select, list, propertyName);
    });
    list.on("click", ".remove-item", function (e) {
        self.removeItem(e, propertyName);
    })
}

PCCaseEditPage.prototype.removeItem = function (e, propertyname) {
    var id = $(e.target).closest("li").attr("data-id");
    if (this[propertyname]) {
        for (var i = this[propertyname].length - 1; i > -1 ; i--) {
            if (this[propertyname][i].Id == id) {
                this[propertyname].splice(i, 1);
                break;
            }
        }
    }
    $(e.target).closest("li").remove();
}

PCCaseEditPage.prototype.addItem = function (select, list, propertyName) {
    var name = select.find("option:selected").text()
    var id = select.val();
    var item = { Id: id, Name: name };
    if (!this[propertyName]) {
        this[propertyName] = [];
    }
    this[propertyName].push(item);
    list.append("<li data-id='" + id + "'>" + name + "&nbsp;<a class='remove-item'><i class='fas fa-times'></i></a></li>");
    console.log(this[propertyName]);
}

PCCaseEditPage.prototype.addArrayToFormData = function (formData, array, name) {
    if (array) {
        for (var i = 0; i < array.length; i++) {
            formData.append(name + "[" + i + "].Id", array[i].Id);
            formData.append(name + "[" + i + "].Name", array[i].Name);
        }
    }
}
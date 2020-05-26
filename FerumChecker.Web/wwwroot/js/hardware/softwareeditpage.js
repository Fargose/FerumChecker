SoftwareEditPage = function () {
    this.Action = null;
}

SoftwareEditPage.prototype.init = function (action) {
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

    this.initMultiBox($("#minimum-videocard-select"), "SoftwareVideoCardRequirementsMinimum", "VideoCardId");
    this.initMultiBox($("#minimum-cpu-select"), "SoftwareCPURequirementsMinimum", "CPUId");
    this.initMultiBox($("#recomended-videocard-select"), "SoftwareVideoCardRequirementsRecomended", "VideoCardId");
    this.initMultiBox($("#recomended-cpu-select"), "SoftwareCPURequirementsMinimumRecomended", "CPUId");


    $("form").submit(function (e) {
        e.preventDefault();
        var formData = new FormData(e.target);
        var lastVideoCard = self.addVideoCardArrayToFormData(formData, self.SoftwareVideoCardRequirementsMinimum, "SoftwareVideoCardRequirements", 1);
        var lastCPU = self.addCPUArrayToFormData(formData, self.SoftwareCPURequirementsMinimum, "SoftwareCPURequirements", 1);
        self.addVideoCardArrayToFormData(formData, self.SoftwareVideoCardRequirementsRecomended, "SoftwareVideoCardRequirements", 2, lastVideoCard);
        self.addCPUArrayToFormData(formData, self.SoftwareCPURequirementsMinimumRecomended, "SoftwareCPURequirements", 2, lastCPU);
        $.ajax({
            url: "/Software/" + self.Action,
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


SoftwareEditPage.prototype.initMultiBox = function (elem, propertyName, idProp) {
    var self = this;
    this.clickInit(elem, propertyName, idProp);
}

SoftwareEditPage.prototype.clickInit = function (elem, propertyName, idProp) {
    var self = this;
    var select = elem.find(".combo-select");
    var list = elem.find(".selected-list");
    this[propertyName] = [];
    var items = list.find("li");
    for (var i = 0; i < items.length; i++) {
        var item = {};
        item[idProp] = $(items[i]).attr("data-id");
        item.RequirementTypeId = $(items[i]).attr("data-requirement")
        this[propertyName].push(item);
    }
    elem.find(".add-item").click(function (e) {
        self.addItem(select, list, propertyName, idProp);
    });
    list.on("click", ".remove-item", function (e) {
        self.removeItem(e, propertyName, idProp);
    })
}

SoftwareEditPage.prototype.removeItem = function (e, propertyname, idProp) {
    var id = $(e.target).closest("li").attr("data-id");
    if (this[propertyname]) {
        for (var i = this[propertyname].length - 1; i > -1 ; i--) {
            if (this[propertyname][i][idProp] == id) {
                this[propertyname].splice(i, 1);
                break;
            }
        }
    }
    $(e.target).closest("li").remove();
}

SoftwareEditPage.prototype.addItem = function (select, list, propertyName, idProp) {
    var name = select.find("option:selected").text()
    var id = select.val();
    if (!this[propertyName]) {
        this[propertyName] = [];
    }
    if (!this[propertyName].find(function (el) {
        return el[idProp] == id;
    })) {
        var item = { Name: name };
        item[idProp] = id;
        this[propertyName].push(item);
        list.append("<li data-id='" + id + "'>" + name + "&nbsp;<a class='remove-item'><i class='fas fa-times'></i></a></li>");
        console.log(this[propertyName]);
    }
}

SoftwareEditPage.prototype.addVideoCardArrayToFormData = function (formData, array, name, requirmentType, startFrom = 0) {
    if (array) {
        for (var i = 0; i < array.length; i++) {
            formData.append(name + "[" + (i + startFrom) + "].VideoCardId", array[i].VideoCardId);
            formData.append(name + "[" + (i + startFrom) + "].RequirementTypeId", requirmentType);
        }
        return i;
    }
}

SoftwareEditPage.prototype.addCPUArrayToFormData = function (formData, array, name, requirmentType, startFrom = 0) {
    if (array) {
        for (var i = 0; i < array.length; i++) {
            formData.append(name + "[" + (i + startFrom) + "].CPUId", array[i].CPUId);
            formData.append(name + "[" + (i + startFrom) + "].RequirementTypeId", requirmentType);
        }
        return i;
    }
}
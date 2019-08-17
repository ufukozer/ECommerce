var ECommerce = {
    Helper: {
        Ajax: function (method, jDto, callback) {
            var json = JSON.stringify(jDto);
            var data = new Object();
            data.Method = method;
            data.Json = json;
            $.ajax({
                method: "POST",
                url: "/api",
                data: "JSON=" + JSON.stringify(data)
            })
                .done(function (msg) {
                    if (callback) {
                        callback(msg);
                    }
            });
        },
    }, 
    Page: {
        Home: {

        },
        Category: {
            Save: function () {
                var categoryId = $("#CategoryId").val();
                var productName = $("#ProductName").val();
                var productDescription = $("#ProductDescription").val();
                var jDto = new Object();
                jDto.CategoryId = categoryId;
                jDto.ProductName = productName;
                jDto.productDescription = productDescription;
                ECommerce.Helper.Ajax("SaveProduct", jDto);
            },
            List: function () {
                var jDto = new Object();
                jDto.CategoryId = $("#CategoryId").val();
                ECommerce.Helper.Ajax("ProductsByCategoryId", jDto, ECommerce.Page.Category.Callback_List);
            },
            Callback_List: function (data) {
                console.log(data);
                var html = "";
                for (var i = 0; i < data.dynamic.length; i++) {
                    var product = data.dynamic[i];
                    var productName = product.name;
                    html += "- " + productName + "<br />";
                }
                $("#Holder-Products").html(html);
            },
        },
        Contact: {

        },
        Help: {

        },
    }
}
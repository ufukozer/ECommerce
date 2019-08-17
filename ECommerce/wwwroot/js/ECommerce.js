﻿var ECommerce = {
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
                ECommerce.Helper.Ajax("SaveProduct", jDto, ECommerce.Page.Category.Callback_Save);
            },
            Callback_Save: function (data) {
                ECommerce.Page.Category.List();
            },
            Remove: function (productId) {
                var jDto = new Object(); 
                jDto.productId = productId;
                ECommerce.Helper.Ajax("RemoveProduct", jDto, ECommerce.Page.Category.Callback_Remove);
            },
            Callback_Remove: function () {
                ECommerce.Page.Category.List();
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
                    html += "- " + productName + "<input type='button'value='sil' onclick='ECommerce.Page.Category.Remove("+ product.id +")'/><br />";
                }
                $("#Holder-Products").html(html);
            },
        },
        Contact: {
            Submit: function () {
                var nameSurname = $("#NameSurname").val();
                var eMail = $("#EMail").val();
                var message = $("#Message").val();
                var jDto = new Object();
                jDto.NameSurname = nameSurname;
                jDto.EMail = eMail;
                jDto.Message = message;
                ECommerce.Helper.Ajax("ContactSubmit", jDto, ECommerce.Page.Contact.Contact_Save);
            },
            Contact_Save: function () {
                alert("Kayıt Başarılı!");  
            }
        },
        Help: {

        },
    }
}
var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvent();
    },
    registerEvent: function () {
        $("#addToCart").off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.addItem(productId);
        })
    },
    addItem:function (productId) {
        $.ajax({
            url: "/ShoppingCart/Add",
            data{
                productId: productId
            },
            type: "POST",
            dataType: "json",
            seccess: function (response) {
                if (response.status) {
                    alert("Thêm sản phẩm thành công");
                }
            }
            
        });
    },

    loadData: function () {
        $.ajax({
            url: "/ShoppingCart/GetAll",
            type: "GET",
            dataType: "json",
            success: function (res) {
                if (res.status) {
                    var template = $('#template').html();
                    //Mustache.parse(template);
                    var html = '';
                    $.each(res.data, function (i, item) {
                        html += Mustache.render(template, {
                            ProductId: item.ProductId,
                            ProductName: item.product.Name,
                            ImgUrl: item.product.Image,
                            Price: numeral(item.product.Price).format('0,0'),
                            Quatity: item.Quantity,
                            SubPrice: numeral(item.Quantity * item.product.Price).format('0,0)
                        });
                    });
                    $("#cartBody").html(html);
                }
            }
        });
    }
}
cart.init();
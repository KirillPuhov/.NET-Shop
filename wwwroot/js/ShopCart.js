function addToCart(id) {
    console.log('add' + id);
    $.ajax({
        async: false,
        type: "POST",
        url: "/Cart/AddToCartPartial",
        data: 'action=add&id=' + id,
        error: function () {
            alert("Error");
        },
        success: function (response) {
            $(".ajaxcart").html(response);
            alert('Добавленно в корзину');
        }
    });
}

function incProduct(ProductId) {
    console.log(ProductId);
    $.ajax({
        async: false,
        type: "POST",
        url: "/Cart/IncrementProduct",
        data: "ProductId=" + ProductId,
        error: function () {
            alert("Error");
        },
        success: function (response) {
            location.reload()
        }
    });
}

function decProduct(ProductId) {
    console.log(ProductId);
    $.ajax({
        async: false,
        type: "POST",
        url: "/Cart/DecrementProduct",
        data: "ProductId="+ProductId,
        error: function () {
            alert("Error");
        },
        success: function (response) {
            location.reload()
        }
    });
}

function removeProduct(ProductId) {
    console.log(ProductId);
    $.ajax({
        async: false,
        type: "POST",
        url: "/Cart/RemoveProduct",
        data: "ProductId=" + ProductId,
        error: function () {
            alert("Error");
        },
        success: function (response) {
            location.reload()
        }
    });
}
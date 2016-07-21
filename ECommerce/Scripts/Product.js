$(function () {
    
    $(".image").click(function () {
        var image = $(this).attr("src");
        $("#big-image").attr("src", image);
    });
    $("#add-to-cart").click(function () {
        var quantity = $("select").val();
        var product = $("#product").data("product-id");
        $.post("/home/addtocart", { quantity: quantity, productid: product }, function (result) {
            console.log(result.CartId);
            $("#cart-link").attr("href", "/Home/Cart/" + result.CartId);
            $(".badge").html(result.CartCount);
        });
    });
   
});

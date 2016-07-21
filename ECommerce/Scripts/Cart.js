$(function () {
    $("#delete-item").click(function () {
        var itemId = $(this).data("item-id");
        $.post("/home/DeleteItem", { itemId: itemId }, function () {
            window.location.reload();
        });
        
    });
    
});
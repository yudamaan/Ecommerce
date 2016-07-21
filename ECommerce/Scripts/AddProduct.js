$(function () {
    var x = 1;
    $("#add-image").click(function () {        
        $("#image-div").append('<input type="file" name="image[' + x + '].image" class="form-control">');
        x++;      
    });
});
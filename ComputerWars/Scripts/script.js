$(document).ready(function () {

    $("#nameForm").submit(function (event) {
        event.preventDefault();
        var name = $("#Name").val().trim();
        $("#Name").val(name);
        $(this).unbind('submit').submit();
    });



});
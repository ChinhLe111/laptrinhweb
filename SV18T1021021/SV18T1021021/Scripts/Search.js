$(document).ready(function () {
    doSearch(@Model.Page);
$("#searchInput").submit(function (e) {
    e.preventDefault();
    doSearch(1);
    return false;
});
        });
function doSearch(page) {
    var url = $("#searchInput").prop("action");
    var input = $("#searchInput").serializeArray();
    input.push({ "name": "page", "value": page });
    //Gọi api customer/search để lấy dữ liệu
    $.ajax({
        type: "GET",
        url: url,
        data: input,
        success: function (data) {
            $("#searchResult").html(data)
        }
    });
}
async function GetData() {
    var searchText = document.getElementById("search").value;
    var url = '/kullanici-getir/' + searchText;
    $('#example').html("");
    var thead =
        '<thead class="thead-primary"><tr>' +
        '<th class="sorting_asc">Name</th>' +
        '<th class="sorting_asc">Surname</th>' +
        '<th class="sorting_asc">E-Mail</th>' +
        '<th class="sorting_asc">Password</th>' +
        '<th class="sorting_asc">Action Buttons</th>' +
        '</tr></thead>';
    $('#example').append(thead);

    $.getJSON(url, function (data) {
        var tableData = '<tbody>';
        for (var i = 0; i < data.result.length; i++) {
            tableData = tableData+
                '<tr role="row">' +
            '<td>' + data.result[i].name + '</td>' +
            '<td>' + data.result[i].surname + '</td>' +
            '<td>' + data.result[i].email + '</td>' +
            '<td>' + data.result[i].password + '</td>' +
                '<td><div class="d-flex">' +
                '<a href="/kullanici-guncelle/' + data.result[i].id + '" style="color:white" class="btn btn-primary shadow btn-xs sharp mr-1"><i class="flaticon-062-pencil"></i></a>' +
                '<a id="btnDelete" style="color:white" class="btn btn-danger shadow btn-xs sharp" data-id="' + data.result[i].id + '"><i class="flaticon-078-remove"></i></a>' +
                '</td></tr>';
        };
        tableData = tableData + '</tbody>';
        $('#example').append(tableData);

    })
}
$("#example").on("click", "#btnDelete", function () {
    var btn = $(this);
    bootbox.confirm("Silmek istediğinize emin misiniz ?", function (result) {
        if (result) {
            var ID = btn.data("id");
            $.ajax({
                type: "GET",
                url: "/kullanici-sil/" + ID,
                success: function () {
                    bootbox.alert("Kullanıcı silme işlemi başarılı.")
                    GetData();
                },
                error: function () {
                    bootbox.alert("Kullanıcı silinemedi lütfen tekrar deneyin. Sorunun devam etmesi durumunda Hasan Doğdu ile irtibat kurunuz.");
                }
            });
        }
    });
});
$(document).ready(GetData());
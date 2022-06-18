$(document).ready(function () {
    ReloadData('/Admin/DatSach/_DanhSachDatSach');
});

function ChiTietDatSach(url, id) {
    $('#dttable').hide();
    $('#frmdata').show();
    $('#btnthemsua').hide();
    $('#btnback').removeAttr('hidden');

    $.get(url + "/" + id, {}).done(function (data) {
        $('#frmdata').html(data);
        if (id == null) {
            $("#tieude").text('Add New ');
            $("#btnsumit").text('Insert');
        } else {
            $("#tieude").text('Edit ');
            $("#btnsumit").text('Update');
        };

    })
}
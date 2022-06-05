var iconLoading = document.getElementById("loading");
var showLoading = () => {
    iconLoading.style.display = "block";
}
var hideLoading = () => {
    iconLoading.style.display = "none";
}

numberOfSelectedRows = $('#numberOfSelectedRows');
function ReloadDT(idtable) {
    $(idtable).DataTable({
        dom: 'Bfrtip',
        "language": {
            url: '/Areas/Admin/Content/AdminJson/en-GB.json',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
        }
    });
}
function ReloadData(url) {
    $.get(url, {})
        .done(function (data) {
            if (data) {
                $("#data").html(data);
               $(function () { $('.check-toggle').bootstrapToggle() });
                ReloadDT('#tableLoad');
                updateNumberOfSelectedRows();
            }
        }).fail(function () {
            $body.removeClass("loading");
        })
}

function Check(id) {
    const self = $("#check-" + id);
    const isChecked = self.is(':checked');
    numberOfSelectedRows.text(0);
    var checkcount = isChecked ? 1 : -1;
    totalSelectedRows = totalSelectedRows + checkcount;
    updateNumberOfSelectedRows();
    if (checkcount == -1) {
        $('#checkall').each(function () {
            $(this).prop('checked', isChecked);
        });
    };
}

function updateNumberOfSelectedRows() {
    totalSelectedRows = 0;
    $('.check-field').each(function () {
        const self = $(this);
        const isChecked = self.is(':checked');
        if (isChecked) {
            totalSelectedRows = totalSelectedRows + 1;
        }

    })
    numberOfSelectedRows.text(totalSelectedRows > 0 ? totalSelectedRows : 0);
    if (totalSelectedRows > 0) {
        $("#show-dele").show();
    } else {
        $("#show-dele").hide();
    }

}

function SelectAll() {
    const self = $("#checkall");
    const isChecked = self.is(':checked');
    numberOfSelectedRows.text(0);
    totalSelectedRows = isChecked ? $('.check-field').length : 0;
    $('.check-field').each(function () {
        $(this).prop('checked', isChecked);
    });
    updateNumberOfSelectedRows();
    
}

//tao seo link
function toSlug(str) {
    str = str.toLowerCase();

    str = str
        .normalize('NFD')
        .replace(/[\u0300-\u036f]/g, '');

    str = str.replace(/[đĐ]/g, 'd');

    str = str.replace(/([^0-9a-z-\s])/g, '');

    str = str.replace(/(\s+)/g, '-');

    str = str.replace(/-+/g, '-');

    str = str.replace(/^-+|-+$/g, '');

    return str;
}

function ModalThemSua(url, id) {
    if (!$('#ThemOrSuaModal').hasClass('in')) {
        $('#ThemOrSuaModal').modal('toggle');
    }

    $.ajax({
        type: "GET",
        url: url + "/" + id,
        async: false,
        success: function (data) {
            $('#themorsua').html(data);
        }, failure: function (response) {
            location.reload();
        },
    });
    if (id == null) {
        $("#tieude").text('Add New ');
        $("#btnsumit").text('Insert');
    } else {
        $("#tieude").text('Edit ');
        $("#btnsumit").text('Update');
    };


    //ckeditor
    document.querySelectorAll('.ckeditor').forEach(e => {
        ClassicEditor
            .create(e)
            .then(ckeditor => {
                ckeditor.model.document.on('change:data', () => {
                    e.value = ckeditor.getData();
                });
            })

            .catch(error => {
                console.error(error);
            });
    });
    let input = $('#name');
    let output = $('#seolink');
    input.keyup(function () {
        output.val(toSlug(input.val()));

    });
}

function OpenModal(url) {
    if (!$('#AddExcel').hasClass('in')) {
        $('#AddExcel').modal('toggle');
    }

    $.ajax({
        type: "GET",
        url: url,
        async: false,
        success: function (data) {
            $('#addexcel').html(data);
        }, failure: function (response) {
            location.reload();
        },
    });

}


// ckfinder

function onCkfider() {
    selectFileWithCKFinder('#img-preview');
}

function selectFileWithCKFinder(elementId) {
    CKFinder.popup({
        chooseFiles: true,
        width: 800,
        height: 600,
        onInit: function (finder) {
            finder.on('files:choose', function (evt) {
                var file = evt.data.files.first();
                var output = $(elementId)
                output.attr('src',file.getUrl());
            });
        }
    });
}
// thay doi trang thai
function CheckTrangThai(id, url) {

    $.ajax({
        type: "POST",
        dataType: 'json',
        async: false,
        url: url,
        data: {
            Id: id,
        },
    }).done(function (data) {
        if (data.status == "1") {
            $('#ThemOrSuaModal').modal('hide');
            $.toast({
                heading: 'Update',
                text: data.message,
                showhidetransition: 'fade',
                icon: 'success'
            });
            ReloadData(url);
        }
        if (data.status == "0") {
            $.toast({
                heading: 'Error',
                text: data.message,
                showhidetransition: 'fade',
                icon: 'error'
            });
        }
    })

}



//xoa du lieu
function XoaData(url, urlload) {
    if (totalSelectedRows > 0) {
        Swal.fire({
            icon: 'warning',
            text: 'Do you want delete ' + totalSelectedRows + ' row of data ?',
            showDenyButton: true,
            showLoaderOnConfirm: true,
            confirmButtonText: '<i class="fa fa-check"></i> Delete',
            denyButtonText: '<i class="fa fa-ban"> Cancel',
            allowOutsideClick: false,
        })
            .then((result) => {
                if (result.isConfirmed) {
                    var data = [];
                    $('.check-field').each(function () {
                        const self = $(this);
                        const isChecked = self.is(':checked');
                        if (isChecked) {
                            const id = self.data("id");
                            data.push(id);
                        }
                    });
                    $.post(url, { data: data })
                        .done(function (data) {
                            if (data.status == "1") {
                                $.toast({
                                    heading: 'Delete',
                                    text: data.message,
                                    showhidetransition: 'fade',
                                    icon: 'success'
                                });
                                ReloadData(urlload);

                            }
                            if (data.status == "0") {
                                $.toast({
                                    heading: 'Error',
                                    text: data.message,
                                    showhidetransition: 'fade',
                                    icon: 'error'
                                });
                            }
                        })
                }
            });
    } else {
        $.toast({
            heading: 'Eror',
            text: 'You have not selected any data lines!',
            showhidetransition: 'fade',
            icon: 'error'
        });
    }
}

function getFile() {
    document.getElementById("upfile").click();
}

function sub(obj) {
    var file = obj.value;
    var fileName = file.split("\\");
    document.getElementById("yourBtn").innerHTML = fileName[fileName.length - 1];
    document.myForm.submit();
    event.preventDefault();
}




function ImportExcel(url, urlLoad) {
    if ($("#frmThemOrSua").valid()) {
        Swal.fire({
            position: 'absolute',
            icon: 'question',
            title: 'Insert',
            text:  'Are you sure you want to insert?' ,
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "POST",
                    dataType: 'json',
                    async: false,
                    url: url,
                }).done(function (data) {
                    if (data.status == "1") {
                        $('#AddExcel').modal('hide');
                        $.toast({
                            heading: 'Insert',
                            text: data.message,
                            showhidetransition: 'fade',
                            icon: 'success'
                        });
                        $.get(urlLoad, {})
                            .done(function (data) {
                                if (data) {
                                    $("#data").html(data);
                                    $(function () { $('.check-toggle').bootstrapToggle() });
                                    ReloadDT('#tableLoad');
                                    updateNumberOfSelectedRows();
                                }
                            }).fail(function () {
                                $body.removeClass("loading");
                            })

                    }
                    if (data.status == "0") {
                        $('#AddExcel').modal('hide');
                        $.toast({
                            heading: 'Error',
                            text: data.message,
                            showhidetransition: 'fade',
                            icon: 'error'
                        });
                    }
                })
            }
        })
    }
}


function ImportEx(data, url) {
    var formData = new FormData();
    formData.append('myExcelData', $('#ImportEx')[0].files[0]);
    $.ajax({
        type: 'POST',
        url: data,
        contentType: false,
        processData: false,
        data: formData,
    }).done(function (data) {
        if (data.status == "1") {
            $.toast({
                heading: 'Insert',
                text: data.message,
                showhidetransition: 'fade',
                icon: 'success'
            });
            OpenModal(url)

        }
        if (data.status == "0") {
            $.toast({
                heading: 'Error',
                text: data.message,
                showhidetransition: 'fade',
                icon: 'error'
            });
        }})
}

function DownloadFile(url) {
    window.location.href = url;
}
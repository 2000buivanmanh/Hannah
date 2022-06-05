$(document).ready(function () {
    ReloadData('/Admin/NhomTuoi/_DanhSachNhomTuoi');
});


function ThemOrSuaNhomTuoi(id,url) {
    var trangthai = $('#trangthai').val();
    var agemin = $('#name').val();
    var agemax = $('#agemax').val();
    var description = $('#description').val();
    var seocontent = $('#seocontent').val();
    var seokeyword = $('#seokeyword').val();
    var seotitle = $('#seotitle').val();
    var seolink = $('#seolink').val();
    $.validator.addMethod(
        "checkage",
        function (value) {
            if (agemin > agemax)
                return true;
        })
    $("#frmThemOrSua").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert",
        rules: {
            agemin
                : {
                required: true,
                number: true,
                maxlength: 3,
                checkage: false,
            },
            agemax: {
                required: true,
                number: true,
                maxlength: 3,
                checkage: false
            },

            seolink: {
                required: true,
            },
        },
        messages: {
            agemin: {
                required: "Please enter your Age Min",
                maxlength: "Age Min field only accepts 3 digits",
                number: "Can only enter numbers ",
                checkage: "Minimum age must not be older than maximum age",
            },
            agemax: {
                required: "Please enter your Age Max",
                maxlength: "Age Max field only accepts 3 digits",
                number: "Can only enter numbers ",
                checkage: "Minimum age must not be older than maximum age",
            },
            seolink: {
                required: "Please enter your SEO Link",
            },
        },
    })
    if ($("#frmThemOrSua").valid()) {
        Swal.fire({
            position: 'absolute',
            icon: 'question',
            title: id == 0 || id == null ? 'Insert' : 'Update',
            text: id == 0 || id == null ? 'Are you sure you want to insert?' : 'Are you sure you want to Update?',
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
                    url: '/Admin/NhomTuoi/ThemHoacSuaNhomTuoi' + "/" + id,
                    data: {
                        MaNhomTuoi: id,
                        DoTuoiMin: agemin,
                        DoTuoiMax: agemax,
                        TrangThai: trangthai,
                        MoTa: description,
                        NoiDungSeo: seocontent,
                        TuKhoaSeo: seokeyword,
                        TieuDeSeo: seotitle,
                        DuongDanSeo: seolink
                    },

                }).done(function (data) {
                    if (id == null) {
                        if (data.status == "1") {
                            $('#ThemOrSuaModal').modal('hide');
                            $.toast({
                                heading: 'Insert',
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
                    } else {
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
                    }

                })
            }
        })
    }
}

function ModalThemSuaNhomTuoi(url, id) {
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
    let a = $('#name');
    let b = $('#agemax');

    let output = $('#seolink');
    a.add(b).keyup(function () {
        output.val(toSlug(a.val() + " " + b.val() + " " + "years old"));
    });
    //ckeditor
    document.querySelectorAll('.ckeditor').forEach(e => {
        ClassicEditor
            .create(e)
            .then(editor => {
                editor.model.document.on('change:data', () => {
                    e.value = editor.getData();
                });
            })

            .catch(error => {
                console.error(error);
            });
    })
}
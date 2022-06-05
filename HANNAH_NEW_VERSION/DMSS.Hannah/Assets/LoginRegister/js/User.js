function OpenModalUser(id) {
    if (!jQuery('#ModalUser').hasClass('in')) {
        jQuery('#ModalUser').modal('toggle');
    }
    //load parialview login ra modal
    jQuery.ajax({
        type: "GET",
        url: id,
        async: false,
        success: function (data) {
            jQuery('#ModalContent').html(data);
        }, failure: function (response) {
            location.reload();
        },
    });
    var success = false;
    //username
    jQuery.validator.addMethod(
        "checkUsername",
        function (value) {
            var user = jQuery("username").val();
            jQuery.ajax({
                type: "POST",
                async: false,
                url: '/Users/KiemTraTonTaiTenNguoiDung',
                data: "username=" + value,
                dataType: "html",
                success: function (data) {
                    //If username ton tai => false 
                    if (data == 1)
                        success = true;
                    else
                        success = false;
                }
            });
            return success;
        },
    );
    //email
    jQuery.validator.addMethod(
        "checkEmail",
        function (value) {
            var email = jQuery("email").val();
            jQuery.ajax({
                type: "POST",
                async: false,
                url: '/Users/KiemTraTonTaiEmail',
                data: "email=" + value,
                dataType: "html",
                success: function (data) {
                    //If username ton tai => false 
                    if (data == 1)
                        success = true;
                    else
                        success = false;
                }
            });
            return success;
        },
    );

    //form validate
    jQuery("#frmRegister").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert",
        rules: {
            fullname: {
                required: true,
            },
            username: {
                required: true,
                checkUsername: true
            },
            email: {
                required: true,
                email: true,
                checkEmail: true
            },
            phone: {
                minlength: 10,
                maxlength: 10,
            },
            password: {
                required: true,
                minlength: 4
            },
            confirmpassword: {
                required: true,
                minlength: 4,
                equalTo: "#password"
            },
            dob: {
                required: true,
                date: true
            },
            address: {
                required: true,
            },
            sinfo: {
                required: true
            },
            sname: {
                required: true
            },
            sphone: {
                required: true,
                minlength: 10,
                maxlength: 10,
            },
            smail: {
                required: true,
                email: true
            },
            srelationship: {
                required: true
            }
        },
        messages: {
            fullname: {
                required: "Please enter your Full name",
            },
            username: {
                required: "Please enter your User name",
                checkUsername: "This Username is taken already"
            },
            email: {
                required: "Please enter your Email",
                email: "The email should be in the format: abc@domain.com",
                checkEmail: "This Email is taken already"
            },
            phone: {
                minlength: "Phone number field accept only 10 digits",
                maxlength: "Phone number field accept only 10 digits",
            },
            password: {
                required: "Please enter your Password",
            },
            confirmpassword: {
                required: "Please enter your Password again",
                equalTo: "Password does not match, please re-enter"
            },
            dob: {
                required: "Please enter your Date Of Birth",
            },
            address: {
                required: "Please enter your Address",
            },
            sname: {
                required: "Please enter Sponsor name",
            },
            sphone: {
                required: "Please enter Sponsor phone",
                minlength: "Phone number field accept only 10 digits",
                maxlength: "Phone number field accept only 10 digits",
            },
            smail: {
                required: "Please enter Sponsor mail",
                email: "The email should be in the format: abc@domain.com"
            },
            srelationship: {
                required: "Please choose your relationship With Sponsor",
            }
        },
    })

}
function OpenProFile(id) {
    if (!jQuery('#ModaProfile').hasClass('in')) {
        jQuery('#ModaProfile').modal('toggle');
    }
    //load parialview login ra modal
    jQuery.ajax({
        type: "GET",
        url: id,
        async: false,
        success: function (data) {
            jQuery('#ModalThongTin').html(data);
        }, failure: function (response) {
            location.reload();
        },
    });
}
function CapNhat() {
    hoTen = jQuery('#fullnameUpdate').val();
    ngaySinh = jQuery('#dobUpdate').val();
    matKhau = jQuery('#passwordUpdate').val();
    soDienThoai = jQuery('#phoneUpdate').val();
    diaChi = jQuery('#addressUpdate').val();
    gioiThieu = jQuery('#about').val();
    diemTichLuy = jQuery('#diemTichLuy').val();
    congKhaiThongTin = jQuery('#checkbox1').val();
    jQuery("#frmProfile").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert",
        rules: {
            fullnameUpdate: {
                required: true,
            },
            usernameUpdate: {
                required: true,
            },
            emailUpdate: {
                required: true,
                email: true,
            },
            phoneUpdate: {
                minlength: 10,
                maxlength: 10,
            },
            passwordUpdate: {
                minlength: 4,
            },
            confirmpasswordUpdate: {
                minlength: 4,
                equalTo: "#passwordUpdate"
            },
            dobUpdate: {
                date: true
            },
            addressUpdate: {
                required: true,
            },
        },
        messages: {
            fullnameUpdate: {
                required: "Please enter your Full name",
            },
            usernameUpdate: {
                required: "Please enter your User name",
                checkUsername: "This Username is taken already"
            },
            emailUpdate: {
                required: "Please enter your Email",
                email: "The email should be in the format: abc@domain.com",
                checkEmail: "This Email is taken already"
            },
            phoneUpdate: {
                minlength: "Phone number field accept only 10 digits",
                maxlength: "Phone number field accept only 10 digits",
            },
            addressUpdate: {
                required: "Please enter your Address",
            },
        },
    })
    if (jQuery("#frmProfile").valid()) {
        var errorUpload = false;
        var uploadedImage = new FormData();
        var files = jQuery("#imageUpload").get(0).files;
        if (files.length > 0) {
            uploadedImage.append("UploadedImage", files[0]);
            jQuery.ajax({
                type: "POST",
                url: "/Users/UploadAvatar",
                contentType: false,
                processData: false,
                async: false,
                data: uploadedImage,
            }).done(function (data) {
                if (data.status == "0") {
                    errorUpload = true;
                    swal({
                        title: "error",
                        text: data.message,
                        icon: "error",
                        button: false,
                        timer: 3000
                    });
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal({
                    title: "Server Error",
                    text: errorThrown,
                    icon: "error",
                    button: false,
                    timer: 1500
                });
            });
        }
        if (!errorUpload) {
            jQuery.ajax({
                type: "POST",
                url: "/Users/CapNhatNguoiDung",
                data: {
                    HoTen: hoTen,
                    NgaySinh: ngaySinh,
                    MatKhau: matKhau,
                    SoDienThoai: soDienThoai,
                    DiaChi: diaChi,
                    GioiThieu: gioiThieu,
                    DiemTichLuy: diemTichLuy,
                    CongKhaiThongTin: congKhaiThongTin
                },
            })
                .done(function (data) {
                    swal({
                        title: data.alerticon,
                        text: data.message,
                        icon: data.alerticon,
                        button: false,
                        timer: 1500
                    });

                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    swal({
                        title: "Server Error",
                        text: errorThrown,
                        icon: "error",
                        button: false,
                        timer: 1500
                    });
                });

        }
    }
}
function PhanHoi() {
    var tenNguoiGui = jQuery('#Name').val();
    var Email = jQuery('#Email').val();
    var noiDung = jQuery('#NoiDung').val();
    var SDT = jQuery('#SDT').val();
    jQuery("#frmfeedback").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert",
        rules: {
            Name: {
                required: true,
            },
            Email: {
                required: true,
                email: true,
            },
            SDT: {
                required: true,
                minlength: 10,
                maxlength: 10,
            },
            NoiDung: {
                required: true,
            },
        },
        messages: {
            Name: {
                required: "Please enter your name",
            },
            Email: {
                required: "Please enter your Email",
                email: "The email should be in the format: abc@domain.com",
                checkEmail: "This Email is taken already"
            },
            SDT: {
                required: "Please enter your phone",
                minlength: "Phone number field accept only 10 digits",
                maxlength: "Phone number field accept only 10 digits",
            },
            NoiDung: {
                required: "Please enter your message",
            },
        },
    })
    if (jQuery("#frmfeedback").valid()) {
        swal({
            title: "WARNING",
            text: "Send Feedback?",
            icon: "warning",
            buttons: true,
        }).then((yes) => {
            if (yes) {
                showLoading();
                jQuery.post('/FeedbackUser/GuiPhanHoi',
                    {
                        tenNguoiGui: tenNguoiGui,
                        Email: Email,
                        noiDung: noiDung,
                        SDT: SDT
                    })
                    .done(function (data) {
                        hideLoading();
                        if (data.status == "1") {
                            swal({
                                title: data.alerticon,
                                text: data.message,
                                icon: data.alerticon,
                                button: false,
                                timer: 1500
                            });
                            jQuery('#frmfeedback')[0].reset();
                        }
                        if (data.status == "0") {
                            swal({
                                title: data.alerticon,
                                text: data.message,
                                icon: data.alerticon,
                                button: false,
                                timer: 1500
                            });
                        }
                    })
                    .fail(function (jqXHR, textStatus, errorThrown) {
                        swal({
                            title: "Server Error",
                            text: errorThrown,
                            icon: "error",
                            button: false,
                            timer: 1500
                        });
                    });
            }
        });
    }
}

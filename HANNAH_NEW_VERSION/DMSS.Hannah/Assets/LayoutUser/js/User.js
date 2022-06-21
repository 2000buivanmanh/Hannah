function OpenModalUser(id) {
    if (!jQuery('#modalUser').hasClass('in')) {
        jQuery('#modalUser').modal('toggle');
    }
    //load parialview login ra modal
    jQuery.ajax({
        type: "GET",
        url: id,
        async: false,
        success: function (data) {
            jQuery('#modalContent').html(data);
        }, failure: function (response) {
            location.reload();
        },
    });
    jQuery("#formLogin").validate({
        rules: {
            usernameEmailLogin: {
                required: true,
            },
            passwordLogin: {
                required: true,
                minlength: 4,
            }
        },
        messages: {
            usernameEmailLogin: {
                required: "Please enter your Password !",
            },
            passwordLogin: {
                required: "Please enter your Password !",
                minlength: "Please enter min 4 character !",
            }
        }
    });
    jQuery("#formRegister").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert",
        rules: {
            readerName: {
                required: true,
            },
            readerUserName: {
                required: true,
                //checkUsername: true
            },
            readerEmail: {
                required: true,
                email: true,
                //checkEmail: true
            },
            readerPhoneNumber: {
                minlength: 10,
                maxlength: 10,
            },
            readerPassWord: {
                required: true,
                minlength: 4
            },
            readerRePassWord: {
                required: true,
                minlength: 4,
                equalTo: "#readerPassWord"
            },
            readerDOB: {
                required: true,
                date: true
            },
            readerAddress: {
                required: true,
            },
            suggesterName: {
                required: true
            },
            suggesterPhoneMail: {
                required: true,
                minlength: 10,
                maxlength: 10,
            },
            suggesterEmail: {
                required: true,
                email: true
            },
            suggestersRelation: {
                required: true
            }
        },
        messages: {
            readerName: {
                required: "Please enter your Full name",
            },
            readerUserName: {
                required: "Please enter your User name",
                checkUsername: "This Username is taken already"
            },
            readerEmail: {
                required: "Please enter your Email",
                email: "The email should be in the format: abc@domain.com",
                checkEmail: "This Email is taken already"
            },
            readerPhoneNumber: {
                minlength: "Phone number field accept only 10 digits",
                maxlength: "Phone number field accept only 10 digits",
            },
            readerPassWord: {
                required: "Please enter your Password",
            },
            readerRePassWord: {
                required: "Please enter your Password again",
                equalTo: "Password does not match, please re-enter"
            },
            readerDOB: {
                required: "Please enter your Date Of Birth",
            },
            readerAddress: {
                required: "Please enter your Address",
            },
            suggesterName: {
                required: "Please enter Sponsor name",
            },
            suggesterPhoneMail: {
                required: "Please enter Sponsor phone",
                minlength: "Phone number field accept only 10 digits",
                maxlength: "Phone number field accept only 10 digits",
            },
            suggesterEmail: {
                required: "Please enter Sponsor mail",
                email: "The email should be in the format: abc@domain.com"
            },
            suggestersRelation: {
                required: "Please choose your relationship With Sponsor",
            }
        },
    })
}
function DangNhap() {
    var tenDangNhap = jQuery('#usernameEmailLogin').val();
    var matKhau = jQuery('#passwordLogin').val();
    var success = true;
    if (success) {
        jQuery.ajax({
            type: "GET",
            url: "/Users/KiemTraDangNhap",
            data: {
                tenDangNhap: tenDangNhap,
                matKhau: matKhau
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.status == 1) {
                    if (data.isAdmin == true) {
                        jQuery.toast({
                            heading: 'success',
                            text: 'Login as Admin',
                            showhidetransition: 'fade',
                            position: 'bottom-right',
                            icon: 'success'
                        });
                        window.location.href = '/Admin/Admin/Index';
                    }
                    if (data.isAdmin == false) {
                        jQuery.toast({
                            heading: 'success',
                            text: 'Login as User',
                            position: 'bottom-right',
                            showhidetransition: 'fade',
                            icon: 'success'
                        });
                        window.location.href = '/';
                    }
                }
                if (data.status == 0) {
                    if (data.capcha == false) {
                        swal({
                            title: "error",
                            text: data.message,
                            icon: "error",
                            button: false,
                            timer: 3000
                        });
                    } else {
                        jQuery.toast({
                            heading: 'error',
                            text: data.message,
                            showhidetransition: 'fade',
                            position: 'bottom-right',
                            icon: 'error',
                        });
                    }
                }
                if (data.status == -1) {
                    swal({
                        title: "success",
                        text: "sent confirmation code",
                        icon: "success",
                        button: false,
                        timer: 1500
                    });
                    tempEmail = tenDangNhap;
                    jQuery('#modalAccount').modal('show');
                    //OpenModalUser('/NguoiDung/_ConfirmAccount');
                }
                if (data.status == 2) {
                    window.location.href = '/Users/WaitAccount';
                }
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
}
function DangKy() {
    var fullname = jQuery('#readerName').val();
    var username = jQuery('#readerUserName').val();
    var email = jQuery('#readerEmail').val();
    var phone = jQuery('#readerPhoneNumber').val();
    var password = jQuery('#readerPassWord').val();
    var confirmpassword = jQuery('#readerRePassWord').val();
    var dob = jQuery('#readerDOB').val();
    var address = jQuery('#readerAddress').val();
    var sname = jQuery('#suggesterName').val();
    var sphone = jQuery('#suggesterPhoneMail').val();
    var smail = jQuery('#suggesterEmail').val();
    var srelationship = jQuery('#suggestersRelation').val();
    if (jQuery("#formRegister").valid()) {
        //showLoading();
        jQuery.ajax({
            type: "POST",
            dataType: 'json',
            async: false,
            url: '/Users/ThemNguoiDung',
            data: {
                HoTen: fullname,
                TenDangNhap: username,
                EmailNguoiDung: email,
                SoDienThoai: phone,
                MatKhau: password,
                MatKhau: confirmpassword,
                NgaySinh: dob,
                DiaChi: address,
                TenNguoiGioiThieu: sname,
                SDTNguoiGioiThieu: sphone,
                EmailNguoiGioiThieu: smail,
                MoiQuanHe: srelationship
            },
        }).done(function (data) {
            //hideLoading();
            if (data.status == 0) {
                swal({
                    title: "error",
                    text: data.message,
                    icon: "error",
                    button: false,
                    timer: 1500
                });
            }
            if (data.status == -1) {
                swal(data.message)
                    .then((value) => {
                        OpenModalUser('/Users/_ConfirmAccount');
                        //window.location.href = '/Users/ConfirmAccount';
                        tempEmail = username;
                        success = true;
                    });
            }
            else
                success = false;
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
}
function GuiMaXacNhan() {
    jQuery.ajax({
        type: "GET",
        url: "/Users/GuiMaXacNhan",
        data: {
            tenDangNhap: tempEmail,
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.status == 0) {
                hideLoading();
                swal("", data.message, "warning");
            }
            if (data.status == 1) {
                jQuery('#some_div').hide();
                jQuery('#btnct').removeAttr('disabled');
                swal({
                    title: "success",
                    text: "sent confirmation code",
                    icon: "success",
                    button: false,
                    timer: 1500
                });
                countdownTimer();
            }
        }, failure: function (response) {
            alert(response.responseText);
        },
    });
}
function XacNhanMail() {
    var capcha = jQuery('#Inp-SecurityCode').val();
    jQuery.ajax({
        type: "GET",
        url: "/Users/XacNhanMail",
        data: {
            tenDangNhap: tempEmail,
            maXacNhan: capcha
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.status == 1) {
                window.location.href = '/Users/WaitAccount';
            }
            if (data.status == 0) {
                swal({
                    title: "error",
                    text: data.message,
                    icon: "error",
                    button: false,
                    timer: 1500
                });
            }
        }, failure: function (response) {
            alert(response.responseText);
        },
    });
}
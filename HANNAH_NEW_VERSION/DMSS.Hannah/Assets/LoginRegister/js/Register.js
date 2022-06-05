var tempEmail;
function CheckForm() {
    var fullname = jQuery('#fullname').val();
    var username = jQuery('#username').val();
    var email = jQuery('#email').val();
    var phone = jQuery('#phone').val();
    var password = jQuery('#password').val();
    var confirmpassword = jQuery('#confirmpassword').val();
    var dob = jQuery('#dob').val();
    var address = jQuery('#address').val();
    var sname = jQuery('#sname').val();
    var sphone = jQuery('#sphone').val();
    var smail = jQuery('#smail').val();
    var srelationship = jQuery('#srelationship').val();
    if (jQuery("#frmRegister").valid()) {
        showLoading();
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
        }).done(function (data){
            hideLoading();
            if (data.status == -1) {
                swal(data.message)
                    .then((value) => {
                        OpenModalUser('/Users/_ConfirmAccount');
                        tempEmail = username;
                        success = true;
                    });
            }
            else
                success = false;
        }).fail.fail(function (jqXHR, textStatus, errorThrown) {
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
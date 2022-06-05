$("#feedbackForm").validate({
    rules: {
        feedbackName: {
            required: true,
        },
        feedbackEmail: {
            required: true,
            email: true,
        },
        feedbackPhone: {
            required: true,
            minlength: 10,
            maxlength: 10,
        },
        feedbackContent: {
            required: true,
        },
    },
    messages: {
        feedbackName: {
            required: "Please enter your name",
        },
        feedbackEmail: {
            required: "Please enter your Email",
            email: "The email should be in the format: abc@domain.com",
            checkEmail: "This Email is taken already"
        },
        feedbackPhone: {
            required: "Please enter your Phone number",
            minlength: "Phone number field accept only 10 digits",
            maxlength: "Phone number field accept only 10 digits",
        },
        feedbackContent: {
            required: "Please enter your message",
        },
    },
})
function PhanHoi() {
    var feedbackName = jQuery('#feedbackName').val();
    var feedbackEmail = jQuery('#feedbackEmail').val();
    var feedbackPhone = jQuery('#feedbackPhone').val();
    var feedbackContent = jQuery('#feedbackContent').val();
    if ($("#feedbackForm").valid()) {
        Swal.fire({
            title: 'Are you sure?',
            text: "Do you want send feedback",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes'
        }).then((result) => {
            if (result.isConfirmed) {
                $.post('/PhanHoiNguoiDung/GuiPhanHoi',
                    {
                        tenNguoiGui: feedbackName,
                        Email: feedbackEmail,
                        SDT: feedbackPhone,
                        noiDung: feedbackContent
                    })
                    .done(function (data) {
                        if (data.status == "1") {
                            Swal.fire({
                                title: data.alerticon,
                                text: data.message,
                                icon: data.alerticon,
                                //button: false,
                                //timer: 15000
                            });
                            //$('#feedbackForm')[0].reset();
                        }
                        if (data.status == "0") {
                            Swal.fire({
                                title: data.alerticon,
                                text: data.message,
                                icon: data.alerticon,
                                button: false,
                                /*    timer: 1500*/
                            });
                        }
                    })
                    .fail(function (jqXHR, textStatus, errorThrown) {
                        Swal.fire({
                            title: "Server Error",
                            text: errorThrown,
                            icon: "error",
                            button: false,
                            //timer: 1500
                        });
                    });
            }
        })
    }
}

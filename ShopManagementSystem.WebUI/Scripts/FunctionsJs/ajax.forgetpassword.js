var token = $("[name='__RequestVerificationToken']").val();
$(document).ready(function () {
    $("#btn-forgot-my-password").click(function () {

        $.ajax({
            type: "POST",
            url: '/security/forgotmypassword',
            data: {
                __RequestVerificationToken: token,
                Email: document.getElementById("Email").value
            },
            dataType: 'json',
            beforeSend: function () {
                $("#ajax-loader").show();
            },
            complete: function () {
                $("#ajax-loader").hide();
            },
            success: function (result) {
                if (result.Status === "Success") {
                    $("#divResult2").show();
                    document.getElementById("divResult2").innerHTML = '<div class="alert alert-success" role="alert"><div class="alert-text"> TEBRİKLER : Yeni şifreniz başarıyla email adresinize gönderildi. Lütfen email adresinizi kontrol ediniz.</div></div>';
                    setTimeout(function () {
                        window.location.href = result.Url;
                    }, 3000);

                }
                else if (result.Status === "Fail") {
                    $("#divResult2").show();
                    document.getElementById("divResult2").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : Email adresi sistemde bulunmuyor.</div></div>';
                    setTimeout(function () {
                        $("#divResult2").hide();
                    }, 3000);
                }
                else if (result.Status === "Fail2") {
                    $("#divResult2").show();
                    document.getElementById("divResult2").innerHTML = '<div class="alert alert-brand" role="alert"><div class="alert-text"> Şifre sıfırlama talebiniz alındı. Lütfen mail adresinizi kontrol ediniz. Mail alamamanız durumunda 5 dk sonra tekrar deneyiniz.</div></div>';
                    setTimeout(function () {
                        $("#divResult2").hide();
                    }, 3000);
                }
                else if (result.Status === "NonEmail") {
                    $("#divResult2").show();
                    document.getElementById("divResult2").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Email adresi alanı boş geçilemez!</div></div>';
                    setTimeout(function () {
                        $("#divResult2").hide();
                    }, 3000);
                }
            },
            error: function () {
                $("#divResult2").show();
                document.getElementById("divResult2").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : Beklenilmeyen bir hata ile karşılaşıldı.Yeniden deneyin.Sorun devam ederse sistem yöneticinize başvurunuz.</div></div>';
                setTimeout(function () {
                    $("#divResult2").hide();
                }, 3000);
            }

        });
        return false;
    });
});


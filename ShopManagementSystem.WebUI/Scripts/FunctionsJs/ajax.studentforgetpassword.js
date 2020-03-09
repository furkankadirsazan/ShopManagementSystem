var token = $("[name='__RequestVerificationToken']").val();
$(document).ready(function () {
    $("#btn-forgot-my-password").click(function () {

        $.ajax({
            type: "POST",
            url: '/student/security/forgotmypassword',
            data: {
                __RequestVerificationToken: token,
                Email: document.getElementById("FrEmail").value
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
                    $("#divResult3").show();
                    document.getElementById("divResult3").innerHTML = '<div class="alert alert-success" role="alert"><div class="alert-text"> TEBRİKLER : Yeni şifreniz başarıyla email adresinize gönderildi. Lütfen email adresinizi kontrol ediniz.</div></div>';
                    setTimeout(function () {
                        window.location.href = result.Url;
                    }, 3000);

                }
                else if (result.Status === "Fail") {
                    $("#divResult3").show();
                    document.getElementById("divResult3").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : Email adresi sistemde bulunmuyor.</div></div>';
                    setTimeout(function () {
                        $("#divResult3").hide();
                    }, 3000);
                }
                else if (result.Status === "NonEmail") {
                    $("#divResult3").show();
                    document.getElementById("divResult3").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Email adresi alanı boş geçilemez!</div></div>';
                    setTimeout(function () {
                        $("#divResult3").hide();
                    }, 3000);
                }
            },
            error: function () {
                $("#divResult3").show();
                document.getElementById("divResult3").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : Beklenilmeyen bir hata ile karşılaşıldı.Yeniden deneyin.Sorun devam ederse sistem yöneticinize başvurunuz.</div></div>';
                setTimeout(function () {
                    $("#divResult3").hide();
                }, 3000);
            }

        });
        return false;
    });
});


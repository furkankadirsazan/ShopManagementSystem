var token = $("[name='__RequestVerificationToken']").val();
$(document).ready(function () {
    $("#btn-login").click(function () {
        $.ajax({
            type: "POST",
            url: '/admin/security/login',
            data: {
                __RequestVerificationToken: token,
                Username: document.getElementById("Username").value,
                Password: document.getElementById("Password").value,
                IsRemember: document.getElementById("IsRemember").checked
            },
            dataType: 'json',
            beforeSend: function () {
                $("#ajax-loader-login").show();
            },
            complete: function () {
                $("#ajax-loader-login").hide();
            },
            success: function (result) {
                if (result.Status === "Success") {
                    $("#divResult").show();
                    document.getElementById("divResult").innerHTML = '<div class="alert alert-success" role="alert"><div class="alert-text"> TEBRİKLER : Giriş işlemi başarılı. Yönetim paneline yönlendiriliyorsunuz. Lütfen bekleyiniz...</div></div>';
                    setTimeout(function () {
                        window.location.href = result.Url;
                    }, 3000);
                }
                else if (result.Status === "Fail") {
                    $("#divResult").show();
                    document.getElementById("divResult").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : Kullanıcı adı veya şifre hatalı!</div></div>';
                    setTimeout(function () {
                        $("#divResult").hide();
                    }, 3000);
                }
                else if (result.Status === "None") {
                    $("#divResult").show();
                    document.getElementById("divResult").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Tüm alanların doldurulması zorunludur!</div></div>';
                    setTimeout(function () {
                        $("#divResult").hide();
                    }, 3000);
                }
            },
            error: function () {
                $("#divResult").show();
                document.getElementById("divResult").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : Beklenilmeyen bir hata ile karşılaşıldı.Sayfayı yenileyip tekrar deneyin.Sorun devam ederse sistem yöneticinize başvurunuz!</div></div>';
                setTimeout(function () {
                    $("#divResult").hide();
                }, 3000);
            }
        });
        return false;
    });
});


var token = $("[name='__RequestVerificationToken']").val();
$(document).ready(function () {
    $("#btn-login").click(function () {
        $.ajax({
            type: "POST",
            url: '/student/security/login',
            data: {
                __RequestVerificationToken: token,
                Ssn: document.getElementById("Ssn").value,
                Password: document.getElementById("Password").value,
                IsRemember: document.getElementById("IsRemember").checked
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
                    $("#divResult").show();
                    document.getElementById("divResult").innerHTML = '<div class="alert alert-success" role="alert"><div class="alert-text"> TEBRİKLER : Giriş işlemi başarılı. Yönetim paneline yönlendiriliyorsunuz!</div></div>';
                    setTimeout(function () {
                        window.location.href = result.Url;
                    }, 3000);
                }
                else if (result.Status === "NotAuthenticated") {
                    $("#divResult").show();
                    document.getElementById("divResult").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> HATA : Hesabınız doğrulanmamış! Mailinize gelen linke tıklayarak doğrulama yapınız!</div></div>';
                    setTimeout(function () {
                        $("#divResult").hide();
                    }, 6000);
                }
                else if (result.Status === "Punished") {
                    $("#divResult").show();
                    document.getElementById("divResult").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> HATA : Hesabınız engellendi! Hesabınız sistem yöneticisi tarafından cezalı olduğunuz için erişime kapatıldı!</div></div>';
                    setTimeout(function () {
                        $("#divResult").hide();
                    }, 6000);
                }
                else if (result.Status === "NotActive") {
                    $("#divResult").show();
                    document.getElementById("divResult").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : Hesabınız sistem yöneticisi tarafından kapatılmıştır! Bilginiz dışındaysa yetkililer ile iletişime geçiniz.</div></div>';
                    setTimeout(function () {
                        $("#divResult").hide();
                    }, 6000);
                }
                else if (result.Status === "Fail") {
                    $("#divResult").show();
                    document.getElementById("divResult").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : Tc Kimlik No veya şifre hatalı!</div></div>';
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
                document.getElementById("divResult").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : Beklenilmeyen bir hata ile karşılaşıldı.Yeniden deneyin.Sorun devam ederse sistem yöneticinize başvurunuz!</div></div>';
                setTimeout(function () {
                    $("#divResult").hide();
                }, 3000);
            }
        });
        return false;
    });
});



$(".search").keyup(function () {
    var searchTerm = $(".search").val();
    var bulunan = 0;
    $('.results tbody tr').each(function (e) {
        var table = $(this);
        if (table.text().toLowerCase().includes(searchTerm.toLowerCase())) {
            bulunan += 1;
            table.show();
            $("#searchResult").show();
            document.getElementById("searchResult").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text text-center" style="margin-top: 10px;"><p class="counter" style="font-weight:bold;"></p></div></div>';
            $(".counter").text(bulunan + " kayıt bulundu");
            setTimeout(function () {
                $("#searchResult").hide();
            }, 5000);
            $(".no-result").css('display', 'none');
        } else {
            table.hide();
            $("#searchResult").show();
            document.getElementById("searchResult").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text text-center" style="margin-top: 10px;"><p class="counter" style="font-weight:bold;"></p></div></div>';
            $(".counter").text(bulunan + " kayıt bulundu");
            setTimeout(function () {
                $("#searchResult").hide();
            }, 5000);
            if (bulunan === 0) {
                $(".no-result").css('display', '');
            }
        }
    });
});

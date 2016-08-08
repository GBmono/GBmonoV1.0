/**
 * Created by Administrator on 2016/8/8 0008.
 */
define(function (require,modules,exports) {
    require("zepto-selector");
    require("zepto-data");
    require("gbmono");

    $(".wrapper").height($.height() - 92 - 65);


    $(".search-btn").on("click",function () {
        if($(".search-wrapper").hasClass("dnone")){
            $(".menu-list").addClass("dnone");
            $(".search-wrapper").removeClass("dnone");
        }
        else {
            $(".menu-list").removeClass("dnone");
            $(".search-wrapper").addClass("dnone");
        }
    });

});
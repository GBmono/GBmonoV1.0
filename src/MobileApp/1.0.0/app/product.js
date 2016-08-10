/**
 * Created by Administrator on 2016/8/8 0008.
 */
define(function (require,modules,exports) {
    require("zepto-selector");
    require("zepto-data");
    require("zepto-touch");
    require("gbmono");

    $(".wrapper").height($.height() - 92 - 55);


    $(".search-btn").on("tap",function () {
        if($(".search-wrapper").hasClass("dnone")){
            $(".menu-list").addClass("dnone");
            $(".search-wrapper").removeClass("dnone");
        }
        else {
            $(".menu-list").removeClass("dnone");
            $(".search-wrapper").addClass("dnone");
        }
    });

    $(".slide-list li").on("tap",function (e) {
        const currentEle = $(e.target);

        const dropDownList = currentEle.siblings();

        if(dropDownList.hasClass("dnone"))
            dropDownList.removeClass("dnone");
        else
            dropDownList.addClass("dnone");

        return false;


    });

});
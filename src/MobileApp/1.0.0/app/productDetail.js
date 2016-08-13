/**
 * Created by Administrator on 2016/8/13 0013.
 */
define(function (require,modules,exports) {
    require("zepto-selector");
    require("zepto-data");
    require("zepto-touch");
    require("gbmono");
    require("zepto-carousel");

    $(".wrapper").height($.height() - 92 - 55);

    $(".slide-list-first-name").on("tap",function (e) {
        const currentBtn = $(e.currentTarget);
        const dropDownList = currentBtn.siblings();

        if(dropDownList.hasClass("dnone"))
            dropDownList.removeClass("dnone");
        else
            dropDownList.addClass("dnone");

    });



    /*轮播图样式*/
    var mainVisualCollection = $("#visualList");
    mainVisualCollection.owlCarousel({
        items : 1, //10 items above 1000px browser width
        itemsDesktop : [1200,1], //5 items between 1000px and 901px
        itemsDesktopSmall : [1100,1], // betweem 900px and 601px
        itemsTablet: [1000,1], //2 items between 600 and 0
        itemsMobile : false,
        autoPlay:true,
        lazyLoad:true,
        pagination:true
    });

});
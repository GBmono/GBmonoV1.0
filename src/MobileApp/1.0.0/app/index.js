/**
 * Created by Administrator on 2016/8/3 0003.
 */
define(function (require,modules,exports) {
    require("zepto-selector");
    require("zepto-data");
    require("zepto-carousel");

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
$(function(){
	
	
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


    /*底部轮播图片*/
    var snapList = $("#snapList");
    snapList.owlCarousel({
        items : 4, //10 items above 1000px browser width
        itemsDesktop : [840,4], //5 items between 1000px and 901px
        itemsDesktopSmall : [740,3], // betweem 900px and 601px
        itemsTablet: [600,2], //2 items between 600 and 0
        itemsMobile : false,
        autoPlay:true,
        lazyLoad:true,
        pagination:false
    });


    $("#pageTop").on("click",function(){
        $("html,body").animate({
            scrollTop: 0
        },300);

        return false;
    });

    $(".gb-recommend").on("click",function (e) {

        var recommendBtn = $(this);
        if(!recommendBtn.hasClass("open")){
            $("body").trigger("clearMenu");
            recommendBtn.addClass("open");
        }
        else {
            recommendBtn.removeClass("open");
        }

        return false;
    });

    $(".medicine-shop").on("click",function (e) {
        var medicineShop = $(this);
        if(!medicineShop.hasClass("open")){
            $("body").trigger("clearMenu");
            medicineShop.addClass("open");
        }
        else {
            medicineShop.removeClass("open");
        }

        return false;
    });

    $(".gb-products-btn").on("click",function (e) {


        var productBtn = $(".gb-products");
        if(!productBtn.hasClass("open")){
            $("body").trigger("clearMenu");
            productBtn.addClass("open");
        }
        else {
            productBtn.removeClass("open");
        }

        return false;
        
    });

    $("body").on("clearMenu",function (e) {
        var recommendBtn = $(".gb-recommend");
        if(recommendBtn.hasClass("open"))
            recommendBtn.removeClass("open");

        var medicineShop = $(".medicine-shop");
        if(medicineShop.hasClass("open"))
            medicineShop.removeClass("open");

        var productBtn = $(".gb-products");
        if(productBtn.hasClass("open"))
            productBtn.removeClass("open");

        return false;
    });

    $("body").click(function (e) {
        $("body").trigger("clearMenu");
    });


    window.addEventListener("scroll", function(event) {
        const pageTopEl = $("#pageTop");
        const headerDomEl = $("#menuHeader");
        var scrollTop = document.documentElement.scrollTop || window.pageYOffset || document.body.scrollTop;
        if(scrollTop > 0){
            pageTopEl.show();
            headerDomEl.addClass("min-header");
        }
        else {
            headerDomEl.removeClass("min-header");
            pageTopEl.hide();
        }
    });
	
	
});
    


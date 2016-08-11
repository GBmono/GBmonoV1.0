/**
 * Created by Administrator on 2016/8/4 0004.
 */
(function($){

    $.extend($,{

        height:function(){
            return (window.webviewHeight && window.webviewHeight / window.devicePixelRatio) || (document.documentElement.offsetHeight);
        }

    });

    window.$ = Zepto;
})(Zepto);
/**
 * Created by Administrator on 2016/8/10 0010.
 */
var preLoad = "<img src='./resources/images/GBmonoLogo.jpg' style='display:none;width:0px;'/>";

preLoad +=
    '<div id="helper" class="helper">' +
        '<div id="wrapperHelper" class="wrapperHelper">' +
            '<div class="scrollerHelper">'+
                '<ul>' +
                    '<li>' +
                        '<img src="./resources/images/GBmonoLogo.jpg" />' +
                        '<p class="scan-qrcode-btn">点击扫描QR码</p>'+
                    '</li>' +
                '</ul>' +
            '</div>' +
        '</div>' +
    '</div>';


document.write(preLoad);
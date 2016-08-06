/*
 global variables & consts
*/
(function (gbmono) {
    // server name
    var server = 'http://localhost';
    // web api application name
    var apiAppName = 'gbmonoapi';
    // image server name
    var imgServer = 'http://localhost';
    var imgAppName = 'images';

    /* web api */
    gbmono.web_api_app_name = server + '/' + apiAppName + '/api';
    
    /* web page view root path */
    gbmono.app_view_path = '/gbmono/app/views';

    /* gbmono product images url */
    gbmono.img_root_path = imgServer + '/' + imgAppName + '/products/';
    /* gbmono article images url */
    gbmono.img_article_root_path = imgServer + '/' + imgAppName +  '/articles/';
    /* gbmono brand images url */
    gbmono.img_brand_root_path = imgServer + '/' + imgAppName +  '/brands/';

    /* web api bearer token entry point*/
    gbmono.api_token_url = server + '/' + apiAppName + '/token'; // bearer token end point

    // web api url routes
    gbmono.api_site_prefix = {
        // account api url
        account_api_url: gbmono.web_api_app_name + '/Accounts',
        // category api url 
        category_api_url: gbmono.web_api_app_name + '/Categories',
        // product detail url
        product_api_url: gbmono.web_api_app_name + '/Products',
        // user favorite url
        userfavorite_api_url: gbmono.web_api_app_name + '/UserFavorites',
        // retailer url
        retailer_api_url: gbmono.web_api_app_name + '/Retailers',
        // retailer shop url
        retailer_shop_api_url: gbmono.web_api_app_name + '/RetailerShops',
        // brand url
        brand_api_url: gbmono.web_api_app_name + '/Brands',
        // article url
        article_api_url: gbmono.web_api_app_name + '/Articles',
        // profile url
        profile_api_url: gbmono.web_api_app_name + '/Profiles',
        // location url
        location_api_url: gbmono.web_api_app_name + '/Locations'
    };

    // image type
    gbmono.imgType = {
        product: 1,
        descritpion: 2,
        instruction: 3,
        extra: 4
    };

})(window.gbmono = window.gbmono || {});
/*
 global variables & consts
*/
(function (gbmono) {
    /* web api dimain */
    gbmono.domain = 'http://localhost/gbmonoapi';
    /* web api root name */
    gbmono.web_api_app_name = gbmono.domain + '/api';
    
    /* web page view root path */
    gbmono.app_view_path = '/gbmono/app/views';

    /* gbmono product images url */
    gbmono.img_root_path = 'http://localhost/images/products/';
    /* gbmono article images url */
    gbmono.img_article_root_path = 'http://localhost/images/articles/';
    /* gbmono brand images url */
    gbmono.img_brand_root_path = 'http://localhost/images/brands/';

    /* web api controller route prefix */
    /* bearer token entry point*/
    gbmono.api_token_url = gbmono.domain + '/token'; // bearer token end point

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
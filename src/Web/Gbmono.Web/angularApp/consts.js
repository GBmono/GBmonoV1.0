/*
 global variables & consts
*/
(function (gbmono) {
    /* web api application name */
    //gbmono.web_api_app_name = 'http://localhost/name';
    gbmono.domain = 'http://localhost:28975/';
    gbmono.web_api_app_name = gbmono.domain + 'api';
    
    /* angularJs app view root path */
    gbmono.app_view_path = '/angularApp/views';

    /* product image root path */
    gbmono.img_root_path = 'http://localhost:2232/Files/Products/'

    /* gbmono bearer token key name */
    gbmono.LOCAL_STORAGE_TOKEN_KEY = 'gbmono_BEARER_TOKEN'; // localstorage token key name
   
    /* web api controller route prefix */
    /* bearer token entry point*/
    gbmono.api_token_url = gbmono.domain + '/Token'; // bearer token end point

    // web api url routes
    gbmono.api_site_prefix = {
        // account api url
        account_api_url: gbmono.web_api_app_name + '/Accounts',
        // category api url 
        category_api_url: gbmono.web_api_app_name + '/Categories',
        // product detail url
        product_api_url: gbmono.web_api_app_name + '/Products',
        // brand url
        brand_api_url: gbmono.web_api_app_name + '/Brands',
        // profile url
        profile_api_url: gbmono.web_api_app_name + '/Profiles',
        // banner url
        banner_api_url: gbmono.web_api_app_name + '/Banners',
        // retail url
        retail_api_url: gbmono.web_api_app_name + '/Retailers'
    };

    // image type
    gbmono.imgType = {
        product: 1,
        descritpion: 2,
        instruction: 3,
        extra: 4
    };

})(window.gbmono = window.gbmono || {});
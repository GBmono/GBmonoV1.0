/*
 global variables & consts
*/
(function (gbmono) {
    /* admin web api root */
    gbmono.web_api_app_name = 'http://localhost/adminapi/api';
    
    /* web page app view root path */
    gbmono.app_view_path = '/admin/angularApp/views';

    /* product image root path */
    gbmono.img_root_path = 'http://localhost/adminapi/Files/Products';

    /* gbmono bearer token key name */
    gbmono.LOCAL_STORAGE_TOKEN_KEY = 'gbmono_BEARER_TOKEN'; // localstorage token key name
   
    /* web api controller route prefix */
    /* bearer token entry point*/
    gbmono.api_token_url = gbmono.domain + '/Token'; // bearer token end point

    // web api url routes
    gbmono.api_site_prefix = {
        // category api url 
        category_api_url: gbmono.web_api_app_name + '/Categories',
        // product api url
        product_api_url: gbmono.web_api_app_name + '/Products',
        // product image api url
        product_image_api_url:gbmono.web_api_app_name + '/ProductImages',
        // brand api url
        brand_api_url: gbmono.web_api_app_name + '/Brands',
    };

    // notification plain text
    gbmono.notification = {
        delText: "Are you sure to delete ?",
        saveText : "Are you sure to save the changes ?"
    };

})(window.gbmono = window.gbmono || {});
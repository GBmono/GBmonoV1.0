/*
 global variables & consts
*/
(function (gbmono) {
    /* html app name */
    gbmono.html_app = 'http://localhost/admin';

    /* web api app name */
    gbmono.api_app = 'http://localhost/adminapi';

    /* web api root */
    gbmono.web_api_root = gbmono.api_app + '/api';
    
    /* web page app view root path */
    gbmono.app_view_path = '/admin/angularApp/views';

    /* product image root path */
    gbmono.img_root_path = gbmono.api_app + '/files/products';
   
    /* web api controller route prefix */
    /* bearer token entry point*/
    gbmono.api_token_url = gbmono.api_app + '/token'; // bearer token end point

    // web api url routes
    gbmono.api_site_prefix = {
        // category api url 
        category_api_url: gbmono.web_api_root + '/Categories',
        // tag api url
        tag_api_url: gbmono.web_api_root + '/Tags',
        // product api url
        product_api_url: gbmono.web_api_root + '/Products',
        // product image api url
        product_image_api_url: gbmono.web_api_root + '/ProductImages',
        //// product tag api url
        //product_tag_api_url: gbmono.web_api_root + '/ProductTags',
        // brand api url
        brand_api_url: gbmono.web_api_root + '/Brands',
        // stats api url
        stats_api_url: gbmono.web_api_root + '/Statistics'
    };

    // notification plain text
    gbmono.notification = {
        delText: "Are you sure to delete ?",
        saveText : "Are you sure to save the changes ?"
    };

})(window.gbmono = window.gbmono || {});
/*
 global variables & consts
*/
(function (gbmono) {
    /* html app name */

    //Testing
    gbmono.html_app = 'http://localhost/admin';
    /* web api app name */
    gbmono.api_app = 'http://localhost/adminapi';
    /* gbmono product images url */
    gbmono.img_product_path = 'http://localhost/images/products/'
    /* gbmono article images url */
    gbmono.img_article_path = 'http://localhost/images/articles/'

    ////Live
    //gbmono.html_app = 'http://119.9.104.196/admin';
    ///* web api app name */
    //gbmono.api_app = 'http://119.9.104.196/adminapi';
    ///* gbmono product images url */
    //gbmono.img_product_path = 'http://119.9.104.196/images/products/'
    ///* gbmono article images url */
    //gbmono.img_article_path = 'http://119.9.104.196/images/articles/'

    /* web api root */
    gbmono.web_api_root = gbmono.api_app + '/api';
    
    /* web page app view root path */
    gbmono.app_view_path = '/admin/angularApp/views';

      
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
        stats_api_url: gbmono.web_api_root + '/Statistics',
        // retailer shop api url 
        retailer_shop_api_url: gbmono.web_api_root + '/RetailerShops',
        // retailer api url
        retailer_api_url: gbmono.web_api_root + '/Retailers',
        // location api url
        location_api_url: gbmono.web_api_root + '/Locations',
        // article api url
        article_api_url: gbmono.web_api_root + '/Articles'
    };

    // notification plain text
    gbmono.notification = {
        delText: "Are you sure to delete ?",
        saveText : "Are you sure to save the changes ?"
    };

})(window.gbmono = window.gbmono || {});
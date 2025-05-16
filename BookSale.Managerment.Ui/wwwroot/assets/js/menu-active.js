(function($) {
  'use strict';
  $(function() {
    // Get current URL path
    var currentPath = window.location.pathname.replace(/^\/|\/$/g, '');
    
    // Reset all active states first
    $('.sidebar .nav-item').removeClass('active');
    $('.sidebar .nav-link').removeClass('active');
    $('.sidebar .collapse').removeClass('show');
    
    // Process all links in the sidebar
    $('.sidebar .nav-link').each(function() {
      var $link = $(this);
      var href = $link.attr('href');
      
      // Skip dropdown toggles
      if (href && href.startsWith('#')) {
        return;
      }
      
      // Clean the href
      href = (href || '').replace(/^\/|\/$/g, '');
      
      // Skip empty hrefs
      if (href === "") {
        return;
      }
      
      // Check for exact match
      if (href === currentPath) {
        // Mark this link as active
        $link.addClass('active');
        
        // Mark parent nav-item as active
        $link.parents('.nav-item').addClass('active');
        
        // If inside a submenu, expand the parent menu
        if ($link.parents('.collapse').length) {
          $link.parents('.collapse').addClass('show');
          $link.parents('.collapse').prev('a[data-toggle="collapse"]').parents('.nav-item').addClass('active');
        }
      }
    });
  });
})(jQuery);
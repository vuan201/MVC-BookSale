(function($) {
  'use strict';
  $(function() {
    var body = $('body');
    var contentWrapper = $('.content-wrapper');
    var scroller = $('.container-scroller');
    var footer = $('.footer');
    var sidebar = $('.sidebar');

    //Add active class to nav-link based on url dynamically
    //Active class can be hard coded directly in html file also as required

    function addActiveClass(element) {
      // Get the href attribute
      var href = element.attr('href');
      
      // Skip processing for dropdown toggles (they have href like "#account-management")
      if (href && href.startsWith('#')) {
        return;
      }
      
      // Clean the href by removing leading and trailing slashes
      href = (href || '').replace(/^\/|\/$/g, '');
      
      // Skip empty hrefs
      if (href === "") return;
      
      // For exact match only - compare the full URLs
      var shouldBeActive = (href === current);
      
      // Apply active class if needed
      if (shouldBeActive) {
        // Mark this link as active
        element.addClass('active');
        
        // Mark the parent nav-item as active
        element.parents('.nav-item').last().addClass('active');
      }
    }

    //var current = location.pathname.split("/").slice(-1)[0].replace(/^\/|\/$/g, '');
    var current = location.pathname.replace(/^\/|\/$/g, '');

    // First pass: mark active links based on exact URL match
    $('.nav li a', sidebar).each(function() {
      var $this = $(this);
      addActiveClass($this);
    });

    $('.horizontal-menu .nav li a').each(function() {
      var $this = $(this);
      addActiveClass($this);
    });
    
    // Second pass: expand parent menus of active items
    setTimeout(function() {
      $('.nav-item.active').each(function() {
        var $activeItem = $(this);
        
        // If this is inside a submenu, expand the parent menu
        if ($activeItem.parents('.collapse').length) {
          $activeItem.parents('.collapse').addClass('show');
          $activeItem.parents('.collapse').prev('a[data-toggle="collapse"]').parents('.nav-item').addClass('active');
        }
      });
      
      // Ensure submenus with active items are shown
      $('.sub-menu .nav-item.active').each(function() {
        $(this).closest('.collapse').addClass('show');
        $(this).closest('.collapse').prev('a[data-toggle="collapse"]').parents('.nav-item').addClass('active');
      });
    }, 100);

    //Close other submenu in sidebar on opening any

    sidebar.on('show.bs.collapse', '.collapse', function() {
      sidebar.find('.collapse.show').collapse('hide');
    });


    //Change sidebar and content-wrapper height
    applyStyles();

    function applyStyles() {
      //Applying perfect scrollbar
      if (!body.hasClass("rtl")) {
        if ($('.settings-panel .tab-content .tab-pane.scroll-wrapper').length) {
          const settingsPanelScroll = new PerfectScrollbar('.settings-panel .tab-content .tab-pane.scroll-wrapper');
        }
        if ($('.chats').length) {
          const chatsScroll = new PerfectScrollbar('.chats');
        }
        if (body.hasClass("sidebar-fixed")) {
          var fixedSidebarScroll = new PerfectScrollbar('#sidebar .nav');
        }
      }
    }

    $('[data-toggle="minimize"]').on("click", function() {
      if ((body.hasClass('sidebar-toggle-display')) || (body.hasClass('sidebar-absolute'))) {
        body.toggleClass('sidebar-hidden');
      } else {
        body.toggleClass('sidebar-icon-only');
      }
    });

    //checkbox and radios
    $(".form-check label,.form-radio label").append('<i class="input-helper"></i>');

    //fullscreen
    $("#fullscreen-button").on("click", function toggleFullScreen() {
      if ((document.fullScreenElement !== undefined && document.fullScreenElement === null) || (document.msFullscreenElement !== undefined && document.msFullscreenElement === null) || (document.mozFullScreen !== undefined && !document.mozFullScreen) || (document.webkitIsFullScreen !== undefined && !document.webkitIsFullScreen)) {
        if (document.documentElement.requestFullScreen) {
          document.documentElement.requestFullScreen();
        } else if (document.documentElement.mozRequestFullScreen) {
          document.documentElement.mozRequestFullScreen();
        } else if (document.documentElement.webkitRequestFullScreen) {
          document.documentElement.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
        } else if (document.documentElement.msRequestFullscreen) {
          document.documentElement.msRequestFullscreen();
        }
      } else {
        if (document.cancelFullScreen) {
          document.cancelFullScreen();
        } else if (document.mozCancelFullScreen) {
          document.mozCancelFullScreen();
        } else if (document.webkitCancelFullScreen) {
          document.webkitCancelFullScreen();
        } else if (document.msExitFullscreen) {
          document.msExitFullscreen();
        }
      }
    })
  });
})(jQuery);
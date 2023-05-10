// mobile navbar

//document.querySelector('.mobile-nav-toggle').addEventListener('click', function(e) {
//    document.querySelector('.navbar').classList.toggle('navbar-mobile');
//    this.classList.toggle('bi-list');
//    this.classList.toggle('bi-x');
//});

//document.querySelectorAll('.navbar .dropdown > a').forEach(function(dropdownToggle) {
//    dropdownToggle.addEventListener('click', function(e) {
//        if (document.querySelector('#navbar').classList.contains('navbar-mobile')) {
//            e.preventDefault();
//            this.nextElementSibling.classList.toggle('dropdown-active');
//        }
//    });
//});


// navbar position
$(document).ready(function() {
    var navbar = $("#menubar");
    var footer = $(".footer");
    var navbarOffset = navbar.offset().top;

    $(window).scroll(function() {
        if ($(window).scrollTop() < navbarOffset) {
            navbar.removeClass("sticky-top");
            navbar.addClass("sticky-bottom");
        } else {
            navbar.removeClass("sticky-bottom");
            navbar.addClass("sticky-top");
        }
    });

    //$(window).scroll(function() {
    //    if ($(window).scrollTop() < navbarOffset / 2) {
    //        footer.removeClass("fixed-bottom")
    //    } else {
    //        footer.addClass("fixed-bottom")
    //    }
    //})
});
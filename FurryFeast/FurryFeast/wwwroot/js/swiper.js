/**
 * Clients Slider
 */
new Swiper('.clients-slider', {
    speed: 400,
    loop: true,
    autoplay: {
        delay: 5000,
        disableOnInteraction: false
    },
    slidesPerView: 'auto',
    pagination: {
        el: '.swiper-pagination',
        type: 'bullets',
        clickable: true
    },
    breakpoints: {
        //320: {
        //    slidesPerView: 1,
            // spaceBetween: 40
       // },
        480: {
            slidesPerView: 1,
            // spaceBetween: 60
        },
        730: {
            slidesPerView: 2,
            /*spaceBetween: 30*/
        },
        992: {
            slidesPerView: 3,
            spaceBetween: 30
        }
    }
});

/**
 * Init swiper slider with 1 slide at once in desktop view
 */
new Swiper('.slides-1', {
    speed: 600,
    loop: true,
    autoplay: {
        delay: 5000,
        disableOnInteraction: false
    },
    slidesPerView: 'auto',
    pagination: {
        el: '.swiper-pagination',
        type: 'bullets',
        clickable: true
    },
    // navigation: {
    //     nextEl: '.arrow-next-icon',
    //     prevEl: '.arrow-prev-icon',
    // }
});

/**
 * Init swiper slider with 3 slides at once in desktop view
 */
new Swiper('.slides-3', {
    speed: 600,
    loop: true,
    autoplay: {
        delay: 5000,
        disableOnInteraction: false
    },
    slidesPerView: 'auto',
    pagination: {
        el: '.swiper-pagination',
        type: 'bullets',
        clickable: true
    },
    // navigation: {
    //     nextEl: '.arrow-next-icon',
    //     prevEl: '.arrow-prev-icon',
    // },
    breakpoints: {
        320: {
            slidesPerView: 1,
            spaceBetween: 0
        },

        1200: {
            slidesPerView: 3,
        }
    }
});
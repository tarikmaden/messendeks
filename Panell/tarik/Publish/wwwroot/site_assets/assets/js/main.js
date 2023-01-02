var swiperHome = new Swiper(".me-swiper-home", {
    slidesPerView: 1,
    navigation: {
        nextEl: ".me-sh-next",
        prevEl: ".me-sh-prev"
    },
    loop: true,
    pagination: {
        el: '.me-swiper-home-count',
        type: "custom",
        renderCustom: function (swiper, current, total) {
            return '<span class="swiper-pagination-current">0' + current + '</span> / ' + '<span class="swiper-pagination-total">0' + total + '</span>'; 
        }
    },
    effect: "fade"
});

var swiperHomeProgressbar = new Swiper(".me-swiper-home", {
    pagination: {
        el: '.me-swiper-home-progressbar',
        type: 'progressbar'
    },
    loop: true,
    effect: "fade"
});

swiperHome.controller.control = swiperHomeProgressbar;
swiperHomeProgressbar.controller.control = swiperHome;

var swiperMiniGallery = new Swiper(".me-swiper-mini-gallery", {
    slidesPerView: 5,
    spaceBetween: 12,
    loop: true,
    navigation: {
        nextEl: "#me-sb-next1",
        prevEl: "#me-sb-prev1"
    },
    breakpoints: {
        0: {slidesPerView: 2},
        768: {slidesPerView: 3},
        992: {slidesPerView: 5}
    }
});

var swiperMiniNews = new Swiper(".me-swiper-mini-news", {
    slidesPerView: 3,
    loop: true,
    spaceBetween: 40,
    navigation: {
        nextEl: "#me-sb-next2",
        prevEl: "#me-sb-prev2"
    },
    breakpoints: {
        0: {slidesPerView: 1},
        768: {slidesPerView: 2},
        992: {slidesPerView: 3}
    }
});


$('#jsSearchOpen').click(function() {
    $("#jsSearchArea").css('transform', 'translateY(0)');
});

$('#jsSearchOpen2').click(function() {
    $("#jsSearchArea").css('transform', 'translateY(0)');
});

$('#jsSearchClose').click(function() {
    $("#jsSearchArea").css('transform', 'translateY(-220px)');
});

$('#jsOffcanvasOpen').click(function() {
    $("#jsOffcanvasMenu").css('opacity', '1');
    $("#jsOffcanvasMenu").css('pointer-events', 'inherit');
    $("body").css('overflow', 'hidden');
    $(".bg-blur").css('opacity', '1');
    $(".bg-blur").css('pointer-events', 'auto');
});

$('#jsOffcanvasClose').click(function() {
    $("#jsOffcanvasMenu").css('opacity', '0');
    $("#jsOffcanvasMenu").css('pointer-events', 'none');
    $("body").css('overflow', 'inherit');
    $(".bg-blur").css('opacity', '0');
    $(".bg-blur").css('pointer-events', 'none');
});
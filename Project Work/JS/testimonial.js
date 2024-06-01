document.addEventListener('DOMContentLoaded', function () {
    const sliderWrapper = document.querySelector('.testimonial-wrapper');
    const slides = document.querySelectorAll('.testimonial');
    const pagination = document.querySelector('.testimonial-pagination');
    
    let currentIndex = 0;
    const totalSlides = slides.length;
    let slidesPerView;

    if(document.documentElement.clientWidth > 448){
        slidesPerView = 3;
    }else{
        slidesPerView = 1;
    }

    window.addEventListener('resize', function () {
        if (window.innerWidth <= 448) {
            slidesPerView = 1;
            sliderWrapper.innerHTML = '';
    
            clone();
            totalSlides = sliderWrapper.querySelectorAll('.testimonial').length;
            updateSlider();
        } else {
            slidesPerView = 3;
        }
    });

    function clone(){
        for (let i = 0; i < slidesPerView; i++) {
            sliderWrapper.appendChild(slides[i].cloneNode(true));
        }
    }

    clone();

    for (let i = 0; i < totalSlides; i++) {
        const bullet = document.createElement('div');
        bullet.classList.add('testimonial-pagination-bullet');
        if (i === 0) bullet.classList.add('active');
        bullet.dataset.index = i;
        pagination.appendChild(bullet);
    }

    const bullets = document.querySelectorAll('.testimonial-pagination-bullet');

    function updateSlider() {
        if (slidesPerView == 1) {
            sliderWrapper.style.transform = `translateX(-${currentIndex * 100}%)`;
        } else {
            sliderWrapper.style.transform = `translateX(-${currentIndex * (100 / slidesPerView)}%)`;
        }
        bullets.forEach(bullet => bullet.classList.remove('active'));
        if (currentIndex < totalSlides) {
            bullets[currentIndex].classList.add('active');
        } else {
            bullets[0].classList.add('active');
        }
    }
    

    function goToNextSlide() {
        currentIndex++;
        if (currentIndex >= totalSlides + slidesPerView) {
            currentIndex = 1;
            sliderWrapper.style.transition = 'none';
            sliderWrapper.style.transform = `translateX(0%)`;
            setTimeout(() => {
                sliderWrapper.style.transition = 'transform 0.5s ease';
                updateSlider();
            }, 0);
            return;
        }
        updateSlider();
    }
    

    bullets.forEach(bullet => {
        bullet.addEventListener('click', () => {
            currentIndex = parseInt(bullet.dataset.index);
            updateSlider();
        });
    });

    setInterval(goToNextSlide, 7000);
});
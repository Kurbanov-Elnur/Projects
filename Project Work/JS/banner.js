const images = [
    '../assets/Banner/male.jpg',
    '../assets/Banner/female.jpg',
    '../assets/Banner/male-female.jpg',
];

const bannerContents = document.querySelectorAll('.banner-content');

let currentIndex = 0;

function animateText() {
    bannerContents.forEach((content) => {
        content.classList.remove("visible");
    });

    setTimeout(() => {
        bannerContents.forEach((content, index) => {
            setTimeout(() => {
                content.classList.add("visible");
            }, index * 600);
        });
    }, 1000);
}

animateText()

function changeBackgroundImage() {
    const mainElement = document.querySelector('.banner');
    currentIndex = (currentIndex + 1) % images.length;
    mainElement.style.backgroundImage = `url(${images[currentIndex]})`;
}

window.addEventListener('scroll', function() {
    const header = document.querySelector('.header');
    if (window.scrollY > 100) {
        header.classList.add('scrolled');
    } else {
        header.classList.remove('scrolled');
    }
});

setInterval(changeBackgroundImage, 15000);
setInterval(animateText, 15000);
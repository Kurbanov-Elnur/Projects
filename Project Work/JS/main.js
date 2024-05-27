const images = [
    '../assets/male.jpg',
    '../assets/female.jpg',
];

let currentIndex = 0;

function changeBackgroundImage() {
    const mainElement = document.querySelector('.main');
    currentIndex = (currentIndex + 1) % images.length;
    mainElement.style.backgroundImage = `url(${images[currentIndex]})`;
}

setInterval(changeBackgroundImage, 10000);
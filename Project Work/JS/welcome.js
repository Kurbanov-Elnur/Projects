const radios = Array.from(document.querySelectorAll('input[name="r"]'));
const bars = Array.from(document.querySelectorAll('.navigation .bar'));

function switchSlide() {
    const checkedRadioIndex = Array.from(radios).findIndex(radio => radio.checked);
    let nextRadioIndex = checkedRadioIndex + 1;

    if (nextRadioIndex >= radios.length) {
        nextRadioIndex = 0;
    }

    radios[nextRadioIndex].checked = true;
    updateBars(radios[nextRadioIndex]);
}


function updateBars(radio) {
    const index = radios.indexOf(radio);
    bars.forEach(bar => bar.classList.remove('active'));
    bars[index].classList.add('active');
}

bars.forEach((bar, index) => {
    bar.addEventListener('click', () => {
        const prevActiveBar = bars.find(bar => bar.classList.contains('active'));
        if (prevActiveBar) {
            prevActiveBar.classList.remove('active');
        }
        bar.classList.add('active');

        radios[index].checked = true;
    });
});

setInterval(() => {
    switchSlide();
}, 5000);
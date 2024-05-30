document.addEventListener('DOMContentLoaded', () => {
    const counters = document.querySelectorAll('.stat-number');
    const speed = 150;

    const updateCount = (counter) => {
        const target = +counter.getAttribute('data-target');
        const count = +counter.innerText;
        const increment = target / speed;

        if (count < target) {
            counter.innerText = Math.ceil(count + increment);
            setTimeout(() => updateCount(counter), 10);
        } else {
            counter.innerText = target;
        }
    };

    const startAnimation = (entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const counter = entry.target.querySelector('.stat-number');
                if (counter) {
                    updateCount(counter);
                }
            }
        });
    };

    const observer = new IntersectionObserver(startAnimation, {
        threshold: 0.5
    });

    const statElements = document.querySelectorAll('.statistic');
    statElements.forEach(stat => {
        observer.observe(stat);
    });
});
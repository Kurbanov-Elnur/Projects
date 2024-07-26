import React from 'react';
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/css';
import 'swiper/css/effect-coverflow';
import '../Carousel/Carousel.css';
import { EffectCoverflow, Autoplay } from 'swiper/modules';

const images = [
  require('../../Assets/Products/chanel.png'),
  require('../../Assets/Products/macbook.png'),
  require('../../Assets/Products/man-mix.png'),
  require('../../Assets/Products/nike.png'),
  require('../../Assets/Products/watch.png'),
  require('../../Assets/Products/woman-mix.png'),
];

export default function Carousel() {
  return (
    <div className="container-surface" style={{ userSelect: 'none' }}>
      <Swiper
        effect={'coverflow'}
        grabCursor={true}
        centeredSlides={true}
        loop={true}
        slidesPerView={'auto'}
        coverflowEffect={{
          rotate: 0,
          stretch: 0,
          depth: 100,
          modifier: 1.5,
        }}
        autoplay={{
          delay: 5000,
          disableOnInteraction: false,
        }}
        modules={[EffectCoverflow, Autoplay]}
        className="swiper_container"
      >
        {images.map((image, index) => (
          <SwiperSlide key={index}>
            <img src={image} alt={`slide ${index + 1}`} />
          </SwiperSlide>
        ))}
      </Swiper>
    </div>
  );
}
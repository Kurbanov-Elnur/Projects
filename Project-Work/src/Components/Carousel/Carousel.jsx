import React from 'react';
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/css';
import 'swiper/css/effect-coverflow';
import '../Carousel/Carousel.css';

import { EffectCoverflow, Autoplay } from 'swiper/modules';

const images = [
  require('../../Assets/img_1.jpg'),
  require('../../Assets/img_2.jpg'),
  require('../../Assets/img_3.jpg'),
  require('../../Assets/img_4.jpg'),
  require('../../Assets/img_5.jpg'),
  require('../../Assets/img_6.jpg'),
  require('../../Assets/img_7.jpg')
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
import React, { useRef, useEffect } from 'react';
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/css';
import 'swiper/css/effect-coverflow';
import '../Carousel/Carousel.css';

import { EffectCoverflow, Autoplay, Pagination } from 'swiper/modules';

import slide_image_1 from '../../Assets/img_1.jpg';
import slide_image_2 from '../../Assets/img_2.jpg';
import slide_image_3 from '../../Assets/img_3.jpg';
import slide_image_4 from '../../Assets/img_4.jpg';
import slide_image_5 from '../../Assets/img_5.jpg';
import slide_image_6 from '../../Assets/img_6.jpg';
import slide_image_7 from '../../Assets/img_7.jpg';

const images = [slide_image_1, slide_image_2, slide_image_3, slide_image_4, slide_image_5, slide_image_6, slide_image_7];

export default function Carousel() {
  const swiperRef = useRef(null);

  useEffect(() => {
    if (swiperRef.current) {
      swiperRef.current.swiper.pagination.render();
    }
  }, []);

  return (
    <div className="container-surface">
      <Swiper
        ref={swiperRef}
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
        pagination={{
          clickable: true,
          el: '.swiper-pagination-custom',
        }}
        autoplay={{
          delay: 5000,
          disableOnInteraction: false,
        }}
        modules={[EffectCoverflow, Autoplay, Pagination]}
        className="swiper_container"
      >
        {images.map((image, index) => (
          <SwiperSlide key={index}>
            <img src={image} alt={`slide ${index + 1}`} />
          </SwiperSlide>
        ))}
      </Swiper>
      <div className="swiper-pagination-custom"></div>
    </div>
  );
}
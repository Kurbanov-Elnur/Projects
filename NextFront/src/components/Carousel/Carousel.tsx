'use client';

import React, { JSX } from 'react';
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/css';
import 'swiper/css/effect-coverflow';
import './Carousel.css'; 
import Image from 'next/image';

import { EffectCoverflow, Autoplay } from 'swiper/modules';

import chanel from '@/Assets/Products/chanel.png';
import macbook from '@/Assets/Products/macbook.png';
import manMix from '@/Assets/Products/man-mix.png';
import nike from '@/Assets/Products/nike.png';
import watch from '@/Assets/Products/watch.png';
import womanMix from '@/Assets/Products/woman-mix.png';

const images = [chanel, macbook, manMix, nike, watch, womanMix];

export default function Carousel(): JSX.Element {
  return (
    <div className="container-surface" style={{ userSelect: 'none' }}>
      <Swiper
        effect="coverflow"
        grabCursor
        centeredSlides
        loop
        slidesPerView="auto"
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
        {images.map((src, index) => (
          <SwiperSlide key={index}>
            <Image src={src} alt={`slide ${index + 1}`} width={300} height={300} />
          </SwiperSlide>
        ))}
      </Swiper>
    </div>
  );
}

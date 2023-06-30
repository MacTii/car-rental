import React from "react";
import Slider from "react-slick";

import "../../styles/testimonial.css";

import ava01 from "../../assets/all-images/ava-1.jpg";
import ava02 from "../../assets/all-images/ava-2.jpg";
import ava03 from "../../assets/all-images/ava-3.jpg";
import ava04 from "../../assets/all-images/ava-4.jpg";

const Testimonial = () => {
  const settings = {
    dots: true,
    infinite: true,
    autoplay: true,
    speed: 1000,
    swipeToSlide: true,
    autoplaySpeed: 2000,
    slidesToShow: 3,
    slidesToScroll: 1,
    responsive: [
      {
        breakpoint: 992,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 1,
          infinite: true,
          dots: true,
        },
      },
      {
        breakpoint: 576,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
        },
      },
    ],
  };
  return (
    <Slider {...settings}>
      <div className="tesimonial py-4 px-3">
        <p className="section__description">
          "I joined their Whole City Tour and had a fantastic time. The guide
          was knowledgeable and passionate about the city, and I learned so much
          during the tour. They took us to all the must-visit places and shared
          interesting stories along the way. It was a memorable experience, and
          I highly recommend it to anyone visiting the city."
        </p>
        <div className="mt-3 d-flex align-items-center gap-4">
          <img src={ava01} alt="" className="w-25 h-25 rounded-2" />

          <div>
            <h6 className="mb-0 mt-3">John Doe</h6>
            <p className="section__description">Customer</p>
          </div>
        </div>
      </div>

      <div className="tesimonial py-4 px-3">
        <p className="section__description">
          "The City Transfer service was fantastic! The driver was punctual,
          professional, and knowledgeable about the city. It made getting around
          so much easier, especially for someone like me who was visiting for
          the first time. I would definitely use their services again."
        </p>
        <div className="mt-3 d-flex align-items-center gap-4">
          <img src={ava02} alt="" className="w-25 h-25 rounded-2" />

          <div>
            <h6 className="mb-0 mt-3">Lisa David</h6>
            <p className="section__description">Customer</p>
          </div>
        </div>
      </div>

      <div className="tesimonial py-4 px-3">
        <p className="section__description">
          "I rented a car for a weekend trip, and it was a seamless experience
          from start to finish. The booking process was quick and easy, and the
          car was in excellent condition. Unlimited mileage was a huge plus, as
          it allowed us to explore different places without worrying about
          additional costs. Highly recommended!"
        </p>
        <div className="mt-3 d-flex align-items-center gap-4">
          <img src={ava03} alt="" className="w-25 h-25 rounded-2" />

          <div>
            <h6 className="mb-0 mt-3">Hilton King</h6>
            <p className="section__description">Customer</p>
          </div>
        </div>
      </div>

      <div className="tesimonial py-4 px-3">
        <p className="section__description">
          "I used their Airport Transfer service, and I was impressed. The
          driver was waiting for me at the arrivals area with a sign, and the
          ride to my hotel was comfortable and smooth. It took away the stress
          of finding transportation after a long flight. I will definitely use
          their services again in the future."
        </p>
        <div className="mt-3 d-flex align-items-center gap-4">
          <img src={ava04} alt="" className="w-25 h-25 rounded-2" />

          <div>
            <h6 className="mb-0 mt-3">Emily Madson</h6>
            <p className="section__description">Customer</p>
          </div>
        </div>
      </div>
    </Slider>
  );
};

export default Testimonial;

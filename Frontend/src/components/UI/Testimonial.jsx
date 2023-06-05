import React from "react";
import Slider from "react-slick";

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
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Sit magnam
          totam quod eveniet, modi earum. Dolorem, ullam voluptatum quod ducimus
          praesentium tenetur consequuntur quidem voluptate aut, ipsum non
          voluptatem fugit!
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
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Sit magnam
          totam quod eveniet, modi earum. Dolorem, ullam voluptatum quod ducimus
          praesentium tenetur consequuntur quidem voluptate aut, ipsum non
          voluptatem fugit!
        </p>
        <div className="mt-3 d-flex align-items-center gap-4">
          <img src={ava02} alt="" className="w-25 h-25 rounded-2" />

          <div>
            <h6 className="mb-0 mt-3">John Doe</h6>
            <p className="section__description">Customer</p>
          </div>
        </div>
      </div>

      <div className="tesimonial py-4 px-3">
        <p className="section__description">
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Sit magnam
          totam quod eveniet, modi earum. Dolorem, ullam voluptatum quod ducimus
          praesentium tenetur consequuntur quidem voluptate aut, ipsum non
          voluptatem fugit!
        </p>
        <div className="mt-3 d-flex align-items-center gap-4">
          <img src={ava03} alt="" className="w-25 h-25 rounded-2" />

          <div>
            <h6 className="mb-0 mt-3">John Doe</h6>
            <p className="section__description">Customer</p>
          </div>
        </div>
      </div>

      <div className="tesimonial py-4 px-3">
        <p className="section__description">
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Sit magnam
          totam quod eveniet, modi earum. Dolorem, ullam voluptatum quod ducimus
          praesentium tenetur consequuntur quidem voluptate aut, ipsum non
          voluptatem fugit!
        </p>
        <div className="mt-3 d-flex align-items-center gap-4">
          <img src={ava04} alt="" className="w-25 h-25 rounded-2" />

          <div>
            <h6 className="mb-0 mt-3">John Doe</h6>
            <p className="section__description">Customer</p>
          </div>
        </div>
      </div>
    </Slider>
  );
};

export default Testimonial;

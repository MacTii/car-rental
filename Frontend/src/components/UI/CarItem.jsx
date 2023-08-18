import React from "react";
import { Col } from "reactstrap";
import { Link } from "react-router-dom";

import "../../styles/car-item.css";

const CarItem = (props) => {
  const {
    id,
    make,
    model,
    engine,
    speed,
    pricePerDay,
    image,
  } = props.item;

  return (
    <Col lg="4" md="4" sm="6" className="mb-5">
      <div className="car__item">
        <div className="car__img">
          <img
            src={`data:image/png;base64,${image}`}
            alt={make}
            className="w-100"
          />
        </div>
        <div className="car__item-content mt-4">
          <h4 className="section__title text-center">{make}</h4>
          <h6 className="rent__price text-center mt-">
            ${pricePerDay}
            <span>/ Day</span>
          </h6>

          <div className="car__item-info d-flex align-items-center justify-content-between mt-3 mb-4">
            <span className="d-flex align-items-center gap-1">
              <i className="ri-car-line"></i> {model}
            </span>
            <span className="d-flex align-items-center gap-1">
              <i className="ri-settings-2-line"></i> {engine}
            </span>
            <span className="d-flex align-items-center gap-1">
              <i className="ri-timer-flash-line"></i> {speed} km/h
            </span>
          </div>

          <button className="w-50 car__item-btn car__btn-rent">
            <Link to={`/cars/${make}`} state={props.item}>
              Rent
            </Link>
          </button>

          <button className="w-50 car__item-btn car__btn-details">
            <Link to={`/cars/${make}`} state={props.item}>
              Details
            </Link>
          </button>
        </div>
      </div>
    </Col>
  );
};

export default CarItem;

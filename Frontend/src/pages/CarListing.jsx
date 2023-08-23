import React, { useState, useEffect } from "react";
import { Input, Container, Row, Col } from "reactstrap";
import { useLocation } from "react-router-dom";

import Helmet from "../components/Helmet/Helmet";
import CommonSection from "../components/UI/CommonSection";
import CarItem from "../components/UI/CarItem";

import { getCars } from "../services/carService";

import Slider from "rc-slider";
import "rc-slider/assets/index.css";
import "../styles/car-filter.css";

const CarListing = () => {
  const [cars, setCars] = useState([]);
  const [filters, setFilters] = useState({
    make: "",
    model: "",
    year: "",
    color: "",
    pricePerDay: [0, 1000],
  });

  const location = useLocation(); // Get object from CarItem Link
  const car = location.state;

  useEffect(() => {
    fetchGetCars();
    if (car) {
      setFilters((prevFilters) => ({
        ...prevFilters,
        make: car.make || "",
        model: car.model || "",
        year: car.year || "",
        color: car.color || "",
        pricePerDay: car.pricePerDay || [0, 1000],
      }));
    }
  }, [car]);

  const fetchGetCars = async () => {
    const result = await getCars();
    setCars(result);
  };

  const filteredCars = cars.filter((car) => {
    return (
      (filters.make === "" || car.make.includes(filters.make)) &&
      (filters.model === "" || car.model.includes(filters.model)) &&
      (filters.year === "" || car.year === parseInt(filters.year)) &&
      (filters.color === "" || car.color.includes(filters.color)) &&
      filters.pricePerDay[0] <= car.pricePerDay &&
      car.pricePerDay <= filters.pricePerDay[1]
    );
  });

  const handleFilterChange = (event) => {
    const { name, value } = event.target;
    setFilters((prevFilters) => ({
      ...prevFilters,
      [name]: value,
    }));
  };

  return (
    <Helmet title="Cars">
      <CommonSection title="Car Listing" />
      <section>
        <Container>
          <Row>
            <Col lg="3">
              <div className="car__filter">
                <h2>Car Filter</h2>
                <Input
                  type="text"
                  placeholder="Make"
                  name="make"
                  value={filters.make}
                  onChange={handleFilterChange}
                />
                <Input
                  type="text"
                  placeholder="Model"
                  name="model"
                  value={filters.model}
                  onChange={handleFilterChange}
                />
                <Input
                  type="text"
                  placeholder="Year"
                  name="year"
                  value={filters.year}
                  onChange={handleFilterChange}
                />
                <select
                  className="form-select"
                  name="color"
                  value={filters.color}
                  onChange={handleFilterChange}
                >
                  <option value="">Choose color</option>
                  <option value="Red">Red</option>
                  <option value="Blue">Blue</option>
                  <option value="Green">Green</option>
                </select>
                <Slider
                  min={0}
                  max={1000}
                  defaultValue={[0, 1000]}
                  onChange={(values) =>
                    setFilters((prevFilters) => ({
                      ...prevFilters,
                      pricePerDay: values,
                    }))
                  }
                  range
                  marks={{
                    0: "0$",
                    1000: "1000$",
                  }}
                />
              </div>
            </Col>
            <Col lg="9">
              <div className="d-flex align-items-center gap-3 mb-5">
                <span className="d-flex align-items-center gap-2">
                  <i className="ri-sort-asc"></i> Sort By
                </span>

                <select>
                  <option>Select</option>
                  <option value="low">Low to High</option>
                  <option value="high">High to Low</option>
                </select>
              </div>
              <Row>
                {filteredCars.map((car) => (
                  <CarItem item={car} key={car.id} />
                ))}
              </Row>
            </Col>
          </Row>
        </Container>
      </section>
    </Helmet>
  );
};

export default CarListing;

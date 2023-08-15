import React, { useState, useEffect } from "react";
import { Container, Row, Col } from "reactstrap";

import Helmet from "../components/Helmet/Helmet";
import CommonSection from "../components/UI/CommonSection";
import CarItem from "../components/UI/CarItem";
import carData from "../assets/data/carData";
import urls from "../config/config";

const baseURL = urls.development;

const CarListing = () => {
  const [cars, setCars] = useState([]);

  useEffect(() => {
    fetch(`${baseURL}/api/cars`, {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    })
      .then((response) => response.json())
      .then((result) => {
        if (result.data) {
          console.log(result.data); // show car list
          setCars(result.data);
        } else {
          console.error("List of cars is empty!");
        }
      })
      .catch((error) => {
        console.error("An error occurred:", error);
      });
  }, []);

  return (
    <Helmet title="Cars">
      <CommonSection title="Car Listing" />
      <section>
        <Container>
          <Row>
            <Col lg="12">
              <div className="d-flex align-items-center gap-3 mb-5">
                <span className="d-flex align-items-center gap-2">
                  <i class="ri-sort-asc"></i> Sort By
                </span>

                <select>
                  <option>Select</option>
                  <option value="low">Low to High</option>
                  <option value="high">High to Low</option>
                </select>
              </div>
            </Col>
            {cars.map((car) => (
              <CarItem item={car} key={car.id} />
            ))}
          </Row>
        </Container>
      </section>
    </Helmet>
  );
};

export default CarListing;

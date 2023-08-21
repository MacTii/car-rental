import React, { useEffect, useState } from "react";
import { Container, Row, Col } from "reactstrap";
import { useNavigate } from "react-router-dom";
import rentalHistoryData from "../assets/data/carData";
import "../styles/rental-history.css";
import { getUsernameFromToken } from "../services/tokenService";
import { getRentalByUsername } from "../services/rentalService";
import { getCarByID } from "../services/carService";

const RentalHistory = () => {
  const [token] = useState(localStorage.getItem("token"));
  const [rental, setRental] = useState([]);
  const [carRentals, setCarRentals] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    if (!token) {
      navigate("/home");
      return;
    }
    fetchGetRentals();
  }, []);

  const handleCarDetails = (carMake) => {
    navigate(`/cars/${carMake}`);
  };

  const fetchGetRentals = async () => {
    const username = getUsernameFromToken();

    const rentalResult = await getRentalByUsername(username);
    const carResults = await Promise.all(
      rentalResult.map(async (rentalItem) => {
        const carResult = await getCarByID(rentalItem.carID);
        return carResult;
      })
    );

    setRental(rentalResult);
    setCarRentals(carResults);
  };

  return (
    <Container className="rental-history">
      <h2>Rental History</h2>
      <Row className="header-row">
        <Col md="2">
          <strong>Image</strong>
        </Col>
        <Col md="2">
          <strong>Make</strong>
        </Col>
        <Col md="2">
          <strong>Model</strong>
        </Col>
        <Col md="2">
          <strong>Price Per Day</strong>
        </Col>
        <Col md="2">
          <strong>Rent Date</strong>
        </Col>
        <Col md="2">
          <strong>Return Date</strong>
        </Col>
      </Row>
      {rental.map((rentalItem, index) => {
        const carRental = carRentals.find((car) => car.id === rentalItem.carID);
        return (
          <Row key={index}>
            <Col md="2">
              <img
                src={`data:image/png;base64,${carRental.image}`}
                alt={carRental.make}
                className="car-image"
              />
            </Col>
            <Col
              md="2"
              onClick={() => handleCarDetails(carRental.make)}
              className="cursor-pointer"
            >
              {carRental.make}
            </Col>
            <Col md="2">{carRental.model}</Col>
            <Col md="2">${carRental.pricePerDay}</Col>
            <Col md="2">{rentalItem.rentDate}</Col>
          </Row>
        );
      })}
    </Container>
  );
};

export default RentalHistory;

import React from "react";
import { Container, Row, Col } from "reactstrap"; // Przyjmuję, że korzystasz z komponentów Bootstrap
import { useNavigate } from "react-router-dom";
import rentalHistoryData from "../assets/data/carData"; // Przykładowe dane historii wypożyczeń

const RentalHistory = () => {
  const navigate = useNavigate();

  const handleCarDetails = (carId) => {
    // Przekierowanie do strony szczegółów samochodu na podstawie jego ID
    navigate(`/car/${carId}`);
  };

  return (
    <Container>
      <h2>Rental History</h2>
      <Row>
        <Col md="3">Image</Col>
        <Col md="3">Make</Col>
        <Col md="3">Model</Col>
        <Col md="3">Price</Col>
      </Row>
      {rentalHistoryData.map((rental) => (
        <Row key={rental.id} className="mt-3">
          <Col md="3">
            <img
              src={rental.imgUrl}
              alt={rental.make}
              className="w-100"
              style={{ maxWidth: "100px" }}
            />
          </Col>
          <Col
            md="3"
            onClick={() => handleCarDetails(rental.id)}
            className="cursor-pointer"
          >
            {rental.make}
          </Col>
          <Col md="3">{rental.model}</Col>
          <Col md="3">${rental.pricePerDay}.00</Col>
        </Row>
      ))}
    </Container>
  );
};

export default RentalHistory;

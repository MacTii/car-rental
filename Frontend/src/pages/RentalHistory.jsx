import React, { useEffect, useState } from "react";
import { Container, Row, Col } from "reactstrap";
import { useNavigate } from "react-router-dom";
import rentalHistoryData from "../assets/data/carData";
import "../styles/rental-history.css"; // Importuj styl CSS
import { getUsernameFromToken } from "../services/tokenService";
import { getUserByUsername } from "../services/userService";
import { getRentalByUserID } from "../services/rentalService";

const RentalHistory = () => {
  const [token] = useState(localStorage.getItem("token"));
  const [rental, setRental] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    if (!token) {
      navigate("/home");
      return;
    }
    fetchGetRentals();
    console.log(rental);
  }, []);

  const handleCarDetails = (carMake) => {
    navigate(`/cars/${carMake}`);
  };

  const fetchGetRentals = async () => {
    const username = getUsernameFromToken();
    const user = await getUserByUsername(username); // Can remove it
    const result = await getRentalByUserID(user.id); // Replace it with getRentalByUsername(username);
    console.log(username);
    console.log(user);
    console.log(result);
    setRental(result);
  }

  return (
    <Container className="rental-history">
      <h2>Rental History</h2>
      <Row className="header-row">
        <Col md="3" className="header-col">
          Image
        </Col>
        <Col md="3" className="header-col">
          Make
        </Col>
        <Col md="3" className="header-col">
          Model
        </Col>
        <Col md="3" className="header-col">
          Price
        </Col>
      </Row>
      {rentalHistoryData.map((rental) => (
        <Row key={rental.id} className="rental-row">
          <Col md="3" className="image-col">
            <img src={rental.imgUrl} alt={rental.make} className="car-image" />
          </Col>
          <Col
            md="3"
            onClick={() => handleCarDetails(rental.make)}
            className="make-col cursor-pointer"
          >
            {rental.make}
          </Col>
          <Col md="3" className="model-col">
            {rental.model}
          </Col>
          <Col md="3" className="price-col">
            ${rental.price}.00
          </Col>
        </Row>
      ))}
    </Container>
  );
};

export default RentalHistory;

import React, { useEffect, useState } from "react";

import { Container, Row, Col } from "reactstrap";
import { useParams, useLocation, useNavigate } from "react-router-dom";

import BookingForm from "../components/UI/BookingForm";
import PaymentMethod from "../components/UI/PaymentMethod";
import Helmet from "../components/Helmet/Helmet";

import { getUserByUsername, getUsername } from "../services/userService";
import { addRental } from "../services/rentalService";

const CarDetails = () => {
  const [bookingData, setBookingData] = useState({
    name: null,
    surname: null,
    email: null,
    phoneNumber: null,
    address: null,
    comment: null,
  });
  const [selectedPayment, setSelectedPayment] = useState(
    "Direct Bank Transfer"
  );
  const [token, setToken] = useState(null);
  const { slug } = useParams();

  const location = useLocation(); // get object from CarItem Link
  const car = location.state;

  const navigate = useNavigate();

  useEffect(() => {
    window.scrollTo(0, 0);
    setToken(localStorage.getItem("token"));
  }, [slug]);

  const handleRental = async () => {
    if (!token) {
      navigate("/login");
      return;
    }

    // Get username
    const username = await getUsername();
    console.log(username);

    // Get user data
    const userData = await getUserByUsername(username);
    console.log(userData);

    // Set rental data
    const rental = {
      carID: car.id, // Car ID
      userID: userData.id, // User ID
      rentDate: new Date(), // Rent date
      returnDate: null, // Return date (optional)
      comment: bookingData.comment, // Comment (optional)
      paymentMethod: selectedPayment,
    };

    // Add rental to db
    const result = await addRental(rental);
    console.log(result);
  };

  return (
    <Helmet title={car.make}>
      <section>
        <Container>
          <Row>
            <Col lg="6">
              <img
                src={`data:image/png;base64,${car.image}`}
                alt={car.make}
                className="w-100"
              />
            </Col>
            <Col lg="6">
              <div className="car__info">
                <h2 className="section__title">{car.make}</h2>
                <div className="d-flex align-items-center gap-5">
                  <h6 className="rent__price fw-bold fs-4">
                    ${car.pricePerDay}.00 / Day
                  </h6>
                  <span className="d-flex align-items-center gap-2">
                    <span style={{ color: "#f9a826" }}>
                      <i className="ri-star-fill"></i>
                      <i className="ri-star-fill"></i>
                      <i className="ri-star-fill"></i>
                      <i className="ri-star-fill"></i>
                      <i className="ri-star-fill"></i>
                    </span>
                    ({car.ratings} ratings)
                  </span>
                </div>
                <p className="section__description">{car.description}</p>

                <div
                  className="d-flex align-items-center mt-3"
                  style={{ columnGap: "4rem" }}
                >
                  <span className="d-flex align-items-center gap-1 section__description">
                    <i
                      className="ri-roadster-line"
                      style={{ color: "#f9a826" }}
                    ></i>
                    {car.model}
                  </span>

                  <span className="d-flex align-items-center gap-1 section__description">
                    <i
                      className="ri-settings-2-line "
                      style={{ color: "#f9a826" }}
                    ></i>
                    {car.engine}
                  </span>

                  <span className="d-flex align-items-center gap-1 section__description">
                    <i
                      className="ri-timer-flash-line"
                      style={{ color: "#f9a826" }}
                    ></i>
                    {car.speed} km/h
                  </span>
                </div>

                <div
                  className="d-flex align-items-center mt-3"
                  style={{ columnGap: "2.8rem" }}
                >
                  <span className="d-flex align-items-center gap-1 section__description">
                    <i
                      className="ri-map-pin-line"
                      style={{ color: "#f9a826" }}
                    ></i>
                    {car.gps}
                  </span>

                  <span className="d-flex align-items-center gap-1 section__description">
                    <i
                      className="ri-wheelchair-line"
                      style={{ color: "#f9a826" }}
                    ></i>
                    {car.seatType}
                  </span>

                  <span className="d-flex align-items-center gap-1 section__description">
                    <i
                      className="ri-building-2-line"
                      style={{ color: "#f9a826" }}
                    ></i>
                    {car.make}
                  </span>
                </div>
              </div>
            </Col>

            <Col lg="7" className="mt-5">
              <div className="booking-info mt-5">
                <h5 className="mb-4 fw-bold">Booking Information</h5>
                <BookingForm
                  bookingData={bookingData}
                  setBookingData={setBookingData}
                />
              </div>
            </Col>

            <Col lg="5" className="mt-5">
              <div className="payment__info mt-5">
                <h5 className="mb-4 fw-bold">Payment Information</h5>
                <PaymentMethod
                  selectedPayment={selectedPayment}
                  setSelectedPayment={setSelectedPayment}
                />
                <div className="payment text-end mt-5">
                  <button onClick={handleRental}>Reserve Now</button>
                </div>
              </div>
            </Col>
          </Row>
        </Container>
      </section>
    </Helmet>
  );
};

export default CarDetails;

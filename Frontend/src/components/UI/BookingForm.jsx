import React from "react";
import "../../styles/booking-form.css";
import { Form, FormGroup } from "reactstrap";

const BookingForm = ({ bookingData, setBookingData }) => {
  const handleChange = (event) => {
    const { name, value } = event.target;
    setBookingData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const submitHandler = (event) => {
    event.preventDefault();
  };

  return (
    <Form onSubmit={submitHandler}>
      <FormGroup className="booking__form d-inline-block me-4 mb-4">
        <input
          type="text"
          placeholder="Name"
          name="name"
          value={bookingData.name}
          onChange={handleChange}
        />
      </FormGroup>
      <FormGroup className="booking__form d-inline-block ms-1 mb-4">
        <input
          type="text"
          placeholder="Surname"
          name="surname"
          value={bookingData.surname}
          onChange={handleChange}
        />
      </FormGroup>

      <FormGroup className="booking__form d-inline-block me-4 mb-4">
        <input
          type="email"
          placeholder="Email"
          name="email"
          value={bookingData.email}
          onChange={handleChange}
        />
      </FormGroup>
      <FormGroup className="booking__form d-inline-block ms-1 mb-4">
        <input
          type="number"
          placeholder="Phone Number"
          name="phoneNumber"
          value={bookingData.phoneNumber}
          onChange={handleChange}
        />
      </FormGroup>

      <FormGroup className="booking__form d-inline-block me-4 mb-4">
        <input
          type="text"
          placeholder="Address"
          name="address"
          value={bookingData.address}
          onChange={handleChange}
        />
      </FormGroup>

      <FormGroup className="booking__form d-inline-block ms-1 mb-4">
        <input type="date" placeholder="Return Date" />
      </FormGroup>

      <FormGroup>
        <textarea
          rows={5}
          type="textarea"
          className="textarea"
          placeholder="Comment"
          name="comment"
          value={bookingData.comment}
          onChange={handleChange}
        ></textarea>
      </FormGroup>
    </Form>
  );
};

export default BookingForm;

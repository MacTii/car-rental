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
          type="text"
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
        <input
          type="date"
          placeholder="Date Of Birth"
          name="dateOfBirth"
          value={bookingData.dateOfBirth}
          onChange={handleChange}
        />
      </FormGroup>
      <FormGroup className="booking__form d-inline-block me-4 mb-4">
        <select
          placeholder="Gender"
          name="gender"
          value={bookingData.gender}
          onChange={handleChange}
        >
          <option value="">Select gender</option>
          <option value="Male">Male</option>
          <option value="Female">Female</option>
          <option value="Other">Other</option>
        </select>
      </FormGroup>
      <FormGroup className="booking__form d-inline-block ms-1 mb-4">
        <input
          type="text"
          placeholder="Identification Number"
          name="identificationNumber"
          value={bookingData.identificationNumber}
          onChange={handleChange}
        />
      </FormGroup>
      <FormGroup className="booking__form d-inline-block me-4 mb-4">
        <input
          type="text"
          placeholder="Driving License Number"
          name="drivingLicenseNumber"
          value={bookingData.drivingLicenseNumber}
          onChange={handleChange}
        />
      </FormGroup>
      <FormGroup className="booking__form d-inline-block ms-1 mb-4">
        <input
          type="date"
          placeholder="Return Date"
          name="returnDate"
          value={bookingData.returnDate}
          onChange={handleChange}
        />
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

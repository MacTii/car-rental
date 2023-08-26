import React, { useState } from "react";
import "../../styles/find-car-form.css";
import { Form, FormGroup, Input } from "reactstrap";
import { Link } from "react-router-dom";

const FindCarForm = () => {
  const [searchCriteria, setSearchCriteria] = useState({
    make: "",
    model: "",
    year: "",
    pricePerDay: "",
    color: "",
  });

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setSearchCriteria((prevCriteria) => ({
      ...prevCriteria,
      [name]: value,
    }));
  };

  return (
    <Form className="form">
      <div className="d-flex align-items-center justify-content-between flex-wrap">
        <FormGroup className="form__group">
          <Input
            type="text"
            name="make"
            placeholder="Make"
            value={searchCriteria.make}
            onChange={handleInputChange}
          />
        </FormGroup>

        <FormGroup className="form__group">
          <Input
            type="text"
            name="model"
            placeholder="Model"
            value={searchCriteria.model}
            onChange={handleInputChange}
          />
        </FormGroup>

        <FormGroup className="form__group">
          <Input
            type="text"
            name="year"
            placeholder="Year"
            value={searchCriteria.year}
            onChange={handleInputChange}
          />
        </FormGroup>

        <FormGroup className="form__group">
          <Input
            type="text"
            name="pricePerDay"
            placeholder="Price Per Day"
            value={searchCriteria.pricePerDay}
            onChange={handleInputChange}
          />
        </FormGroup>

        <FormGroup className="select__group">
          <select
            className="form-select"
            name="color"
            value={searchCriteria.color}
            onChange={handleInputChange}
          >
            <option value="">Choose color</option>
            <option value="White">White</option>
            <option value="Black">Black</option>
          </select>
        </FormGroup>

        <FormGroup className="form__group">
          <Link to="/cars" className="btn find__car-btn" state={searchCriteria}>
            Find Car
          </Link>
        </FormGroup>
      </div>
    </Form>
  );
};

export default FindCarForm;

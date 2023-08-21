import React from "react";
import "../../styles/find-car-form.css";
import { Form, FormGroup } from "reactstrap";
import { Link } from "react-router-dom";

const FindCarForm = () => {
  return (
    <Form className="form">
      <div className="d-flex align-items-center justify-content-between flex-wrap">
        <FormGroup className="form__group">
          <input type="text" placeholder="Make" required />
        </FormGroup>

        <FormGroup className="form__group">
          <input type="text" placeholder="Model" required />
        </FormGroup>

        <FormGroup className="form__group">
          <input type="date" placeholder="Year" required />
        </FormGroup>

        <FormGroup className="form__group">
          <input type="text" placeholder="Price Per Day" required />
        </FormGroup>

        <FormGroup className="select__group">
          <select>
            <option value="White">White</option>
            <option value="Black">Black</option>
          </select>
        </FormGroup>

        <FormGroup className="form__group">
          <Link to='/cars' className="btn find__car-btn">Find Car</Link>
        </FormGroup>
      </div>
    </Form>
  );
};

export default FindCarForm;

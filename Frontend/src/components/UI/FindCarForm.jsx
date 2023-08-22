import React from "react";
import "../../styles/find-car-form.css";
import { Form, FormGroup, Input } from "reactstrap";
import { Link } from "react-router-dom";

const FindCarForm = () => {
  return (
    <Form className="form">
      <div className="d-flex align-items-center justify-content-between flex-wrap">
        <FormGroup className="form__group">
          <Input type="text" placeholder="Make" />
        </FormGroup>

        <FormGroup className="form__group">
          <Input type="text" placeholder="Model" />
        </FormGroup>

        <FormGroup className="form__group">
          <Input type="text" placeholder="Year" />
        </FormGroup>

        <FormGroup className="form__group">
          <Input type="text" placeholder="Price Per Day" />
        </FormGroup>

        <FormGroup className="select__group">
          <select className="form-select">
            <option value="">Choose color</option>
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

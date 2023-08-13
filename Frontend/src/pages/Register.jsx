import React, { useState, useEffect } from "react";
import Helmet from "../components/Helmet/Helmet";
import { Container, FormGroup, Row, Col, Form, Input } from "reactstrap";
import { Link, useNavigate } from "react-router-dom";

import "../styles/register.css";
import urls from "../config/config";

const baseURL = urls.development;

const Register = () => {
  const [username, setUsername] = useState("");
  const [firstPassword, setFirstPassword] = useState("");
  const [secondPassword, setSecondPassword] = useState("");
  const [name, setName] = useState("");
  const [surname, setSurname] = useState("");
  const [email, setEmail] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [address, setAddress] = useState("");
  const [dateOfBirth, setDateOfBirth] = useState("");
  const [gender, setGender] = useState("");
  const [identificationNumber, setIdentificationNumber] = useState("");
  const [drivingLicenseNumber, setDrivingLicenseNumber] = useState("");

  const navigate = useNavigate();

  useEffect(() => {
    // Check if the user already has a token in localStorage
    const token = localStorage.getItem("token");
    if (token) {
      // If token exists, redirect to the Home page
      navigate("/home");
    }
  }, [navigate]);

  const handleRegister = () => {
    const data = {
      username: username,
      firstPassword: firstPassword,
      secondPassword: secondPassword,
      name: name,
      surname: surname,
      email: email,
      phoneNumber: phoneNumber,
      address: address,
      dateOfBirth: dateOfBirth,
      gender: gender,
      identificationNumber: identificationNumber,
      drivingLicenseNumber: drivingLicenseNumber,
    };

    fetch(`${baseURL}/api/register`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    })
      .then((response) => response.json())
      .then((result) => {
        // Check if the response contains a token
        if (result.data) {
          // Successfully logged in, token received
          localStorage.setItem("token", result.data); // Save token in localStorage
          console.log("Registered successfully!");
          console.log("Token:", result.data);

          navigate('/home');
        } else {
          // Login failed, handle the error
          console.error("Invalid register credentials.");
        }
      })
      .catch((error) => {
        // Handle error
        console.error("An error occurred:", error);
      });
  };

  return (
    <Helmet title="Register">
      <section>
        <Container>
          <Row>
            <Col lg="6" className="m-auto text-center">
              <h3 className="fw-bold mb-4">Register</h3>
              <Form className="auth__form">
                <FormGroup>
                  <Input
                    type="text"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    placeholder="Enter your username"
                  />
                </FormGroup>
                <FormGroup>
                  <Input
                    type="password"
                    value={firstPassword}
                    onChange={(e) => setFirstPassword(e.target.value)}
                    placeholder="Enter your password"
                  />
                </FormGroup>
                <FormGroup>
                  <Input
                    type="password"
                    value={secondPassword}
                    onChange={(e) => setSecondPassword(e.target.value)}
                    placeholder="Confirm your password"
                  />
                </FormGroup>
                <FormGroup>
                  <Input
                    type="text"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    placeholder="Enter your name"
                  />
                </FormGroup>
                <FormGroup>
                  <Input
                    type="text"
                    value={surname}
                    onChange={(e) => setSurname(e.target.value)}
                    placeholder="Enter your surname"
                  />
                </FormGroup>
                <FormGroup>
                  <Input
                    type="text"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    placeholder="Enter your email"
                  />
                </FormGroup>
                <FormGroup>
                  <Input
                    type="text"
                    value={phoneNumber}
                    onChange={(e) => setPhoneNumber(e.target.value)}
                    placeholder="Enter your phone number"
                  />
                </FormGroup>
                <FormGroup>
                  <Input
                    type="text"
                    value={address}
                    onChange={(e) => setAddress(e.target.value)}
                    placeholder="Enter your address"
                  />
                </FormGroup>
                <FormGroup>
                  <Input
                    type="date"
                    value={dateOfBirth}
                    onChange={(e) => setDateOfBirth(e.target.value)}
                    placeholder="Enter your date of birth"
                  />
                </FormGroup>
                <FormGroup>
                  <select
                    id="gender"
                    value={gender}
                    onChange={(e) => setGender(e.target.value)}
                    className="form-select"
                  >
                    <option value="">Select gender</option> {/* Empty value */}
                    <option value="Male">Male</option>
                    <option value="Female">Female</option>
                    <option value="Other">Other</option>
                  </select>
                </FormGroup>
                <FormGroup>
                  <Input
                    type="text"
                    value={identificationNumber}
                    onChange={(e) => setIdentificationNumber(e.target.value)}
                    placeholder="Enter your identification number"
                  />
                </FormGroup>
                <FormGroup>
                  <Input
                    type="text"
                    value={drivingLicenseNumber}
                    onChange={(e) => setDrivingLicenseNumber(e.target.value)}
                    placeholder="Enter your driving license number"
                  />
                </FormGroup>

                <button
                  type="button"
                  className="register__btn btn"
                  onClick={handleRegister}
                >
                  Sign Up
                </button>

                <p>
                  Already have an account? <Link to="/login">Login</Link>
                </p>
              </Form>
            </Col>
          </Row>
        </Container>
      </section>
    </Helmet>
  );
};

export default Register;

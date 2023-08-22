import React, { useState, useEffect } from "react";
import Helmet from "../components/Helmet/Helmet";
import {
  Container,
  FormGroup,
  Row,
  Col,
  Form,
  Input,
  InputGroup,
  Button,
} from "reactstrap";
import { Link, useNavigate } from "react-router-dom";
import { FaEye, FaEyeSlash } from "react-icons/fa";
import { toast } from "react-toastify";

import "../styles/register.css";
import { useAuth } from "../context/AuthContext";
import { register } from "../services/authService";

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

  const [formError, setFormError] = useState(null);
  const [showFirstPassword, setShowFirstPassword] = useState(false);
  const [showSecondPassword, setShowSecondPassword] = useState(false);

  const navigate = useNavigate();

  const { isAuthenticated, setIsAuthenticated } = useAuth();

  useEffect(() => {
    if (isAuthenticated) {
      navigate("/home"); // If token exists, redirect to the Home page
    }
  }, [isAuthenticated]);

  const clearFormErrorWithDelay = () => {
    setTimeout(() => {
      setFormError(null);
    }, 3000);
  };

  const handleRegister = () => {
    if (
      !username ||
      !firstPassword ||
      !secondPassword ||
      !name ||
      !surname ||
      !email ||
      !phoneNumber ||
      !address ||
      !dateOfBirth ||
      !gender ||
      !identificationNumber ||
      !drivingLicenseNumber
    ) {
      setFormError("Please fill in all fields.");
      clearFormErrorWithDelay();
      return;
    }

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

    register(data)
      .then((token) => {
        console.log("Registered successfully!");

        localStorage.setItem("token", token);
        setIsAuthenticated(true);
        toast.success("Registration successful!");
      })
      .catch((error) => {
        console.error(error.message);

        setFormError(error.message);
        clearFormErrorWithDelay();
        toast.error("Failed to register. Please check your fields.");
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
                  <InputGroup>
                    <Input
                      type={showFirstPassword ? "text" : "password"}
                      value={firstPassword}
                      onChange={(e) => setFirstPassword(e.target.value)}
                      placeholder="Enter your password"
                    />
                    <Button
                      onClick={() => setShowFirstPassword(!showFirstPassword)}
                    >
                      {showFirstPassword ? <FaEyeSlash /> : <FaEye />}
                    </Button>
                  </InputGroup>
                </FormGroup>
                <FormGroup>
                  <InputGroup>
                    <Input
                      type={showSecondPassword ? "text" : "password"}
                      value={secondPassword}
                      onChange={(e) => setSecondPassword(e.target.value)}
                      placeholder="Confirm your password"
                    />
                    <Button
                      onClick={() => setShowSecondPassword(!showSecondPassword)}
                    >
                      {showSecondPassword ? <FaEyeSlash /> : <FaEye />}
                    </Button>
                  </InputGroup>
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
                {formError && <p className="error__message">{formError}</p>}
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

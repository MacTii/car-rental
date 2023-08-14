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

import "../styles/login.css";
import urls from "../config/config";
import { clear } from "@testing-library/user-event/dist/clear";

const baseURL = urls.development;

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const [formError, setFormError] = useState(null);
  const [showPassword, setShowPassword] = useState(false);

  const navigate = useNavigate();

  useEffect(() => {
    // Check if the user already has a token in localStorage
    const token = localStorage.getItem("token");
    if (token) {
      // If token exists, redirect to the Home page
      navigate("/home");
    }
  }, [navigate]);

  const clearFormErrorWithDelay = () => {
    setTimeout(() => {
      setFormError(null);
    }, 3000);
  };

  const handleLogin = () => {
    if (!username || !password) {
      setFormError("Please fill in all fields.");
      clearFormErrorWithDelay();
      return;
    }

    const data = {
      username: username,
      password: password,
    };

    fetch(`${baseURL}/api/login`, {
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
          console.log("Logged in successfully!");
          // console.log("Token:", result.data);

          navigate("/home");
        } else {
          // Login failed, handle the error
          console.error("Invalid login credentials.");
          setFormError("Invalid login credentials");
          clearFormErrorWithDelay();
          //toast.error("Invalid login credentials.")
        }
      })
      .catch((error) => {
        // Handle error
        console.error("An error occurred:", error);
        setFormError("Something went wrong!");
        clearFormErrorWithDelay();
      });
  };

  return (
    <Helmet title="Login">
      <section>
        <Container>
          <Row>
            <Col lg="6" className="m-auto text-center">
              <h3 className="fw-bold mb-4">Login</h3>
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
                      type={showPassword ? "text" : "password"}
                      value={password}
                      onChange={(e) => setPassword(e.target.value)}
                      placeholder="Enter your password"
                    />
                    <Button onClick={() => setShowPassword(!showPassword)}>
                      {showPassword ? <FaEyeSlash /> : <FaEye />}
                    </Button>
                  </InputGroup>
                </FormGroup>
                {formError && <p className="error__message">{formError}</p>}
                <button
                  type="button"
                  className="login__btn btn"
                  onClick={handleLogin}
                >
                  Log In
                </button>

                <p>
                  Don't have an account?{" "}
                  <Link to="/register">Create an account</Link>
                </p>
              </Form>
            </Col>
          </Row>
        </Container>
      </section>
    </Helmet>
  );
};

export default Login;

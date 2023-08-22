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

import "../styles/login.css";
import { useAuth } from "../context/AuthContext";
import { login } from "../services/authService";

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const [formError, setFormError] = useState(null);
  const [showPassword, setShowPassword] = useState(false);

  const navigate = useNavigate();

  const { isAuthenticated, setIsAuthenticated } = useAuth();

  useEffect(() => {
    if (isAuthenticated) {
      navigate("/home"); // If isAuthenticated, redirect to the Home page
    }
  }, [isAuthenticated]);

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

    login(data)
      .then((token) => {
        console.log("Logged in successfully!");

        localStorage.setItem("token", token);
        setIsAuthenticated(true);
        toast.success("Logged in successfully!");
      })
      .catch((error) => {
        console.error(error.message);

        setFormError("Something went wrong!");
        clearFormErrorWithDelay();
        toast.error("Failed to log in. Please check your credentials.");
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

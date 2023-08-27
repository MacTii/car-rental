import React, { useRef, useState, useEffect } from "react";

import { Container, Row, Col } from "reactstrap";
import { Link, NavLink, useNavigate } from "react-router-dom";

import "../../styles/header.css";
import { useAuth } from "../../context/AuthContext";
import { getUsernameFromToken } from "../../services/tokenService";

import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const navLinks = [
  {
    path: "/home",
    display: "Home",
  },
  {
    path: "/cars",
    display: "Cars",
  },
  {
    path: "/users",
    display: "Users",
  },
  {
    path: "/rentals",
    display: "Rentals",
  },
  {
    path: "/blogs",
    display: "Blogs",
  },
];

const AdminHeader = () => {
  const [username, setUsername] = useState(null);
  const [isUserMenuOpen, setIsUserMenuOpen] = useState(false);

  const menuRef = useRef(null);

  const toggleMenu = () => menuRef.current.classList.toggle("menu__active");
  const toggleUserMenu = () => setIsUserMenuOpen(!isUserMenuOpen);

  const { isAuthenticated, setIsAuthenticated } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (localStorage.getItem("token")) {
      const result = getUsernameFromToken();
      setUsername(result);
    }
  }, [isAuthenticated]);

  return (
    <header className="header">
      {/* === header top === */}
      <div className="admin__header__top">
        <Container>
          <Row>
            <Col lg="6" md="6" sm="6"></Col>
            {isAuthenticated ? (
              <Col lg="6" md="6" sm="6">
                <div
                  className="header__top__right d-flex align-items-center justify-content-end gap-3"
                  onClick={toggleUserMenu}
                >
                  {`Hi, ${username}`}
                  <i className="ri-user-line header__icon"></i>
                </div>
                {isUserMenuOpen && (
                  <div className="flex flex-col user__menu__list">
                    <ul className="flex flex-col gap-4">
                      <li
                        onClick={() => {
                          // clear data from local storage and session storage
                          localStorage.clear();
                          sessionStorage.clear();

                          toast.success("Logged out successfully!");
                          setIsAuthenticated(false);

                          navigate("/home");
                        }}
                      >
                        Logout
                      </li>
                    </ul>
                  </div>
                )}
              </Col>
            ) : (
              <Col lg="6" md="6" sm="6">
                <div className="header__top__right d-flex align-items-center justify-content-end gap-3">
                  <Link to="/login" className="d-flex align-items-center gap-1">
                    <i className="ri-login-circle-line"></i> Login
                  </Link>
                  <Link
                    to="/register"
                    className="d-flex align-items-center gap-1"
                  >
                    <i className="ri-user-line"></i> Register
                  </Link>
                </div>
              </Col>
            )}
          </Row>
        </Container>
      </div>

      {/* === header middle === */}
      <div className="header__middle">
        <Container>
          <Row>
            <Col
              lg="4"
              md="4"
              sm="4"
              className="d-flex justify-content-center align-items-center"
            >
              <div className="admin__logo text-center">
                <h1>
                  <Link to="/home" className="align-items-center gap-3">
                    <div>
                      <i className="ri-car-line"></i>
                    </div>
                    <span>
                      Rent Car <br /> Service
                    </span>
                  </Link>
                </h1>
              </div>
            </Col>
            <Col
              lg="4"
              md="4"
              sm="4"
              className="d-flex justify-content-center align-items-center"
            >
              <div className="admin__header__location text-center">
                <span>
                  <i className="ri-earth-line"></i>
                </span>
                <div className="admin__header__location-content">
                  <h4>Poland</h4>
                  <h6>Lodz City, Poland</h6>
                </div>
              </div>
            </Col>
            <Col
              lg="4"
              md="4"
              sm="4"
              className="d-flex justify-content-center align-items-center"
            >
              <div className="admin__header__location text-center">
                <span>
                  <i className="ri-time-line"></i>
                </span>
                <div className="admin__header__location-content">
                  <h4>Monday to Friday</h4>
                  <h6>9am - 6pm</h6>
                </div>
              </div>
            </Col>
          </Row>
        </Container>
      </div>

      {/* === main navigation === */}
      <div className="admin__main__navbar">
        <Container>
          <div className="navigation__wrapper d-flex align-items-center justify-content-between">
            <span className="mobile__menu">
              <i className="ri-menu-line" onClick={toggleMenu}></i>
            </span>
            <div className="navigation" ref={menuRef} onClick={toggleMenu}>
              <div className="menu">
                {navLinks.map((item, index) => (
                  <NavLink
                    to={item.path}
                    className={(navClass) =>
                      navClass.isActive ? "nav__active nav__item" : "nav__item"
                    }
                    key={index}
                  >
                    {item.display}
                  </NavLink>
                ))}
              </div>
            </div>
          </div>
        </Container>
      </div>

      {/* === toast container === */}
    </header>
  );
};

export default AdminHeader;

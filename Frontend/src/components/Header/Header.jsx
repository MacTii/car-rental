import React, { useRef, useState, useEffect } from "react";

import { Container, Row, Col } from "reactstrap";
import { Link, NavLink } from "react-router-dom";

import "../../styles/header.css";
import { useAuth } from "../../context/AuthContext";
import { getUsername } from "../../services/userService";

const navLinks = [
  {
    path: "/home",
    display: "Home",
  },
  {
    path: "/about",
    display: "About",
  },
  {
    path: "/cars",
    display: "Cars",
  },
  {
    path: "/blogs",
    display: "Blog",
  },
  {
    path: "/contact",
    display: "Contact",
  },
];

const Header = () => {
  const [username, setUsername] = useState(null);
  const [isUserMenuOpen, setIsUserMenuOpen] = useState(false);

  const menuRef = useRef(null);

  const toggleMenu = () => menuRef.current.classList.toggle("menu__active");
  const toggleUserMenu = () => setIsUserMenuOpen(!isUserMenuOpen);

  const { isAuthenticated } = useAuth();
  console.log("Authenticated: ", isAuthenticated);

  useEffect(() => {
    fetchUsername();
  }, [isAuthenticated, username]);

  const fetchUsername = async () => {
    const token = localStorage.getItem("token");
    console.log(token);
    if (token) {
      const result = await getUsername();
      setUsername(result);
    }
  };

  return (
    <header className="header">
      {/* === header top === */}
      <div className="header__top">
        <Container>
          <Row>
            <Col lg="6" md="6" sm="6">
              <div className="header__top__left">
                <span>Need Help?</span>
                <span className="header__top__help">
                  <i className="ri-phone-fill"></i> +48 200-300-400
                </span>
              </div>
            </Col>
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
                      <li>
                        <Link
                          to="/profile"
                          style={{ textDecoration: "none", color: "black" }}
                        >
                          Profile
                        </Link>
                      </li>
                      <li>
                        <Link
                          to="/rental-history"
                          style={{ textDecoration: "none", color: "black" }}
                        >
                          Rental History
                        </Link>
                      </li>
                      <li
                        onClick={() => {
                          // clear data from local storage and session storage
                          localStorage.clear();
                          sessionStorage.clear();

                          window.location.href = "/home"; // redirect the user to the logout page
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
            <Col lg="4" md="3" sm="4">
              <div className="logo">
                <h1>
                  <Link to="/home" className="d-flex align-items-center gap-3">
                    <i className="ri-car-line"></i>
                    <span>
                      Rent Car <br /> Service
                    </span>
                  </Link>
                </h1>
              </div>
            </Col>
            <Col lg="3" md="3" sm="4">
              <div className="header__location d-flex align-items-center gap-2">
                <span>
                  <i className="ri-earth-line"></i>
                </span>
                <div className="header__location-content">
                  <h4>Poland</h4>
                  <h6>Lodz City, Poland</h6>
                </div>
              </div>
            </Col>
            <Col lg="3" md="3" sm="4">
              <div className="header__location d-flex align-items-center gap-2">
                <span>
                  <i className="ri-time-line"></i>
                </span>
                <div className="header__location-content">
                  <h4>Monday to Friday</h4>
                  <h6>9am - 6pm</h6>
                </div>
              </div>
            </Col>
            <Col
              lg="2"
              md="3"
              sm="0"
              className="d-flex align-items-center justify-content-end"
            >
              <button className="header__btn btn">
                <Link to="/contact">
                  <i className="ri-phone-line"></i> Request a call
                </Link>
              </button>
            </Col>
          </Row>
        </Container>
      </div>

      {/* === main navigation === */}
      <div className="main__navbar">
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
            <div className="nav__right">
              <div className="search__box">
                <input type="text" placeholder="Search" />
                <span>
                  <i className="ri-search-line"></i>
                </span>
              </div>
            </div>
          </div>
        </Container>
      </div>
    </header>
  );
};

export default Header;

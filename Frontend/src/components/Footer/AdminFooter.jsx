import React from "react";

import { Container, Row, Col, ListGroup, ListGroupItem } from "reactstrap";
import { Link } from "react-router-dom";
import "../../styles/footer.css";

const quickLinks = [
  {
    path: "/users",
    display: "Users",
  },
  {
    path: "/cars",
    display: "Cars",
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

const AdminFooter = () => {
  const date = new Date();
  const year = date.getFullYear();
  return (
    <div className="admin__footer">
      <Container>
        <Row>
          <Col lg="4" md="4" sm="12">
            <div className="logo footer__logo d-flex flex-column align-items-center">
              <h1>
                <Link to="/home" className="d-flex align-items-center gap-3">
                  <i className="ri-car-line"></i>
                  <span>
                    Rent Car <br /> Service
                  </span>
                </Link>
              </h1>
            </div>
            <p className="footer__logo-content text-center">
              Choose the best car rental service for your needs. We offer a wide
              selection of vehicles, competitive prices, and exceptional
              customer service to ensure a smooth and enjoyable experience.
            </p>
          </Col>

          <Col lg="4" md="4" sm="6">
            <div className="mb-4 text-center">
              <h5 className="footer__link-title">Quick Links</h5>
              <ListGroup>
                {quickLinks.map((item, index) => (
                  <ListGroupItem key={index} className="p-0 mt-3 quick__link">
                    <Link to={item.path}>{item.display}</Link>
                  </ListGroupItem>
                ))}
              </ListGroup>
            </div>
          </Col>

          <Col lg="4" md="4" sm="6">
            <div className="mb-4 text-center">
              <h5 className="footer__link-title mb-4">Head Office</h5>
              <p className="office__info">Warszawska 54, Lodz, Poland</p>
              <p className="office__info">Phone: +48 200-300-400</p>
              <p className="office__info">Email: carrent@gmail.com</p>
              <p className="office__info">Office Time: 9am - 6pm</p>
            </div>
          </Col>
          <Col lg="12">
            <div className="admin__footer__bottom">
              <p className="admin__section__description d-flex align-items-center justify-content-center gap-1 pt-4">
                <i className="ri-copyright-line"></i> Copyright {year}, Developed by
                Mateusz Kapka. All rights reserved.
              </p>
            </div>
          </Col>
        </Row>
      </Container>
    </div>
  );
};

export default AdminFooter;

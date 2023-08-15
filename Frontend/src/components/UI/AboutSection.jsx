import React from "react";
import { Container, Row, Col } from "reactstrap";
import "../../styles/about-section.css";
import aboutImg from "../../assets/all-images/cars-img/bmw-offer.png";

const AboutSection = ({ aboutClass }) => {
  return (
    <section
      className="about__section"
      style={
        aboutClass === "aboutPage"
          ? { marginTop: "0px" }
          : { marginTop: "280px" }
      }
    >
      <Container>
        <Row>
          <Col lg="6" md="6">
            <div className="about__section-content">
              <h4 className="section__subtitle">About Us</h4>
              <h2 className="section__title">Welcome to car rent service</h2>
              <p className="section__description">
                At our car rental service, we strive to provide you with a
                seamless and enjoyable experience for all your transportation
                needs. Whether you're traveling for business or pleasure, our
                wide range of vehicles and exceptional customer service ensure
                that you have a reliable and comfortable journey.
              </p>
              <p className="section__description">
                Why choose our car rental service?
              </p>

              <div className="row">
                <div className="col text-center">
                  <div className="about__section-item d-flex align-items-center">
                    <p className="section__description d-flex align-items-center gap-2">
                      <i class="ri-checkbox-circle-line"></i> Diverse Fleet
                    </p>
                  </div>

                  <div className="about__section-item d-flex align-items-center">
                    <p className="section__description d-flex align-items-center gap-2">
                      <i class="ri-checkbox-circle-line"></i> Competitive Prices
                    </p>
                  </div>
                </div>

                <div className="col text-center">
                  <div className="about__section-item d-flex align-items-center">
                    <p className="section__description d-flex align-items-center gap-2">
                      <i class="ri-checkbox-circle-line"></i> Convenient
                      Locations
                    </p>
                  </div>

                  <div className="about__section-item d-flex align-items-center">
                    <p className="section__description d-flex align-items-center gap-2">
                      <i class="ri-checkbox-circle-line"></i> Excellent Customer
                      Service
                    </p>
                  </div>
                </div>
              </div>
            </div>
          </Col>
          <Col lg="6" md="6">
            <div className="about__img">
              <img src={aboutImg} alt="" className="w-100" />
            </div>
          </Col>
        </Row>
      </Container>
    </section>
  );
};

export default AboutSection;

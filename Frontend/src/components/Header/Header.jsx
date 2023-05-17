import React from 'react'

import { Container, Row,Col } from 'reactstrap'

const Header = () => {
  return <header className='header'>
    { /* === header top === */}
    <div className="header__top">
        <Container>
            <Row>
                <Col lg='6' md='6' sm='6'>
                    <div className="header__top__left">
                        <span>Need Help?</span>
                        <span className="header__top__help">
                            
                        </span>
                    </div>
                </Col>
                <Col lg='6' md='6' sm='6'>
                </Col>
            </Row>
        </Container>
    </div>
  </header>
}

export default Header
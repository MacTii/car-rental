import React from "react";

import masterCard from "../../assets/all-images/master-card.jpg";
import paypal from "../../assets/all-images/paypal.jpg";
import "../../styles/payment-method.css";

const PaymentMethod = ({ selectedPayment, setSelectedPayment }) => {
  const handlePaymentChange = (event) => {
    setSelectedPayment(event.target.value);
  };

  return (
    <>
      <div className="payment">
        <label htmlFor="" className="d-flex align-items-center gap-2">
          <input
            type="radio"
            value="Direct Bank Transfer"
            checked={selectedPayment === "Direct Bank Transfer"}
            onChange={handlePaymentChange}
          />
          Direct Bank Transfer
        </label>
      </div>

      <div className="payment mt-3">
        <label htmlFor="" className="d-flex align-items-center gap-2">
          <input
            type="radio"
            value="Cheque Payment"
            checked={selectedPayment === "Cheque Payment"}
            onChange={handlePaymentChange}
          />
          Cheque Payment
        </label>
      </div>

      <div className="payment mt-3 d-flex align-items-center justify-content-between">
        <label htmlFor="" className="d-flex align-items-center gap-2">
          <input
            type="radio"
            value="Master Card"
            checked={selectedPayment === "Master Card"}
            onChange={handlePaymentChange}
          />
          Master Card
        </label>

        <img src={masterCard} alt="" />
      </div>

      <div className="payment mt-3 d-flex align-items-center justify-content-between">
        <label htmlFor="" className="d-flex align-items-center gap-2">
          <input
            type="radio"
            value="Paypal"
            checked={selectedPayment === "Paypal"}
            onChange={handlePaymentChange}
          />
          Paypal
        </label>

        <img src={paypal} alt="" />
      </div>
    </>
  );
};

export default PaymentMethod;

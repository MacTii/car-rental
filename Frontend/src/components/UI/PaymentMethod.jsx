import React, { useState } from "react";

import masterCard from "../../assets/all-images/master-card.jpg";
import paypal from "../../assets/all-images/paypal.jpg";
import "../../styles/payment-method.css";

const PaymentMethod = () => {
  const [selectedPayment, setSelectedPayment] = useState("directBankTransfer");

  const handlePaymentChange = (event) => {
    setSelectedPayment(event.target.value);
  };

  return (
    <>
      <div className="payment">
        <label htmlFor="" className="d-flex align-items-center gap-2">
          <input
            type="radio"
            name="paymentMethod"
            value="directBankTransfer"
            checked={selectedPayment === "directBankTransfer"}
            onChange={handlePaymentChange}
          />
          Direct Bank Transfer
        </label>
      </div>

      <div className="payment mt-3">
        <label htmlFor="" className="d-flex align-items-center gap-2">
          <input
            type="radio"
            name="paymentMethod"
            value="chequePayment"
            checked={selectedPayment === "chequePayment"}
            onChange={handlePaymentChange}
          />
          Cheque Payment
        </label>
      </div>

      <div className="payment mt-3 d-flex align-items-center justify-content-between">
        <label htmlFor="" className="d-flex align-items-center gap-2">
          <input
            type="radio"
            name="paymentMethod"
            value="masterCard"
            checked={selectedPayment === "masterCard"}
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
            name="paymentMethod"
            value="paypal"
            checked={selectedPayment === "paypal"}
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

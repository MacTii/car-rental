import React, { Fragment, useEffect, useState } from "react";

import AdminHeader from "../Header/AdminHeader";
import AdminRouters from "../../routes/AdminRouters";
import AdminFooter from "../Footer/AdminFooter";
import { useAuth } from "../../context/AuthContext";
import { getRoleFromToken } from "../../services/tokenService";
import Header from "../Header/Header";
import Routers from "../../routes/Routers";
import Footer from "../Footer/Footer";
import { ToastContainer } from "react-toastify";

const Layout = () => {
  const [role, setRole] = useState("");
  const { isAuthenticated } = useAuth();

  useEffect(() => {
    fetchToken();
  }, [isAuthenticated]);

  const fetchToken = async () => {
    const token = localStorage.getItem("token");
    if (token) {
      const result = await getRoleFromToken();
      setRole(result);
    } else {
      setRole("User");
    }
  };

  return (
    <Fragment>
      {role === "Admin" ? (
        <Fragment>
          <AdminHeader />
          <div>
            <AdminRouters />
          </div>
          <AdminFooter />
          <ToastContainer />
        </Fragment>
      ) : (
        <Fragment>
          <Header />
          <div>
            <Routers />
          </div>
          <Footer />
          <ToastContainer />
        </Fragment>
      )}
    </Fragment>
  );
};

export default Layout;

import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Blogs from "../pages/admin/Blogs";
import NotFound from "../pages/NotFound";
import Login from "../pages/Login";
import Register from "../pages/Register";
import Rentals from "../pages/admin/Rentals";
import Cars from "../pages/admin/Cars";
import Users from "../pages/admin/Users";
import Home from "../pages/admin/Home";

const AdminRouters = () => {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/home" />} />
      <Route path="/home" element={<Home />} />
      <Route path="/cars" element={<Cars />} />
      <Route path="/users" element={<Users />} />
      <Route path="/rentals" element={<Rentals />} />
      <Route path="/blogs" element={<Blogs />} />
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
};

export default AdminRouters;

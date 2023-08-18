import React, { createContext, useContext, useState, useEffect } from "react";

const Context = createContext();

export const AuthContext = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {
    setIsAuthenticated(localStorage.getItem("token") !== null);
  }, []);

  return (
    <Context.Provider value={{ isAuthenticated, setIsAuthenticated }}>
      {children}
    </Context.Provider>
  );
};

export const useAuth = () => useContext(Context);

export default AuthContext;

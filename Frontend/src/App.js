import React from "react";
import Layout from "./components/Layout/Layout";
import { AuthContext } from "./context/AuthContext";

function App() {
  return (
    <AuthContext>
      <Layout />
    </AuthContext>
  );
}

export default App;

import jwt_decode from "jwt-decode";

// -- DECODE TOKEN OBJECT ---
export const decodeToken = () => {
  const token = localStorage.getItem("token");

  const decoded = jwt_decode(token);
  return decoded;
};

// --- GET USERNAME FROM DECODED TOKEN ---
export const getUsernameFromToken = () => {
  const decoded = decodeToken();
  const username =
    decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
  return username;
};

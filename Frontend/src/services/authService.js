import urls from "../config/config";
import { toast } from "react-toastify";

const baseURL = urls.development;

// --- GET USER (USING TOKEN) ---
export const getUsername = async () => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/username`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    });
    const result = await response.text();
    if (response.ok) {
      return result;
    } else {
      throw new Error(result.detail);
    }
  } catch (error) {
    toast.error(error.message);
    throw new Error(`An error occurred: ${error}`);
  }
};

// --- LOGIN SERVICE ---
export const login = async (data) => {
  try {
    const response = await fetch(`${baseURL}/api/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });
    const result = await response.json();

    if (response.ok) {
      return result.data;
    } else {
      throw new Error(result.detail);
    }
  } catch (error) {
    toast.error(error.message);
    throw new Error("An error occurred: " + error.message);
  }
};

// --- REGISTER SERVICE ---
export const register = async (data) => {
  try {
    const response = await fetch(`${baseURL}/api/register`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });
    const result = await response.json();

    if (response.ok) {
      return result.data;
    } else {
      throw new Error(result.detail);
    }
  } catch (error) {
    toast.error(error.message);
    throw new Error("An error occurred: " + error.message);
  }
};

// --- GENERATE PASSWORD CREDENTIALS ---
export const generatePasswordCredentials = async () => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(
      `${baseURL}/api/generate-password-credentials`,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      }
    );
    const result = await response.json();
    if (response.ok) {
      return result.data;
    } else {
      throw new Error(result.detail);
    }
  } catch (error) {
    toast.error(error.message);
    throw new Error(`An error occurred: ${error}`);
  }
};

// --- RESET PASSWORD CREDENTIALS ---
export const resetPasswordCredentials = async (userID) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/reset-password-credentials/${userID}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    });
    const result = await response.json();
    if (response.ok) {
      return result.response;
    } else {
      throw new Error(result.detail);
    }
  } catch (error) {
    toast.error(error.message);
    throw new Error(`An error occurred: ${error}`);
  }
};

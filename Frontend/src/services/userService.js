import urls from "../config/config";
import { toast } from "react-toastify";

const baseURL = urls.development;

// -- GET USER BY USERNAME ---
export const getUserByUsername = async (username) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/users/username/${username}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
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

// -- UPDATE USER BY ID ---
export const updateUser = async (userID, data) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/users/${userID}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(data),
    });

    const result = await response.json();

    if (response.ok) {
      return result.data; // Assuming result.data contains updated user information
    } else {
      throw new Error(result.detail);
    }
  } catch (error) {
    toast.error(error.message);
    throw new Error("An error occurred: " + error.message);
  }
};

// --- GET ALL USERS ---
export const getUsers = async () => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/users`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
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

// --- DELETE USER ---
export const deleteUser = async (userID) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/users/${userID}`, {
      method: "DELETE",
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
    throw new Error("An error occurred: " + error.message);
  }
};

// --- ADD CAR ---
export const addUser = async (data) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/users`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
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

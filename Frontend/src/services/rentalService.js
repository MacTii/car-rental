import urls from "../config/config";
import { toast } from "react-toastify";

const baseURL = urls.development;

// --- ADD RENTAL ---
export const addRental = async (data) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/rentals`, {
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

// --- GET RENTAL BY USERNAME ---
export const getRentalByUsername = async (username) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(
      `${baseURL}/api/rentals/username/${username}`,
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
    throw new Error("An error occurred: " + error.message);
  }
};

// --- GET ALL RENTALS ---
export const getRentals = async () => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/rentals`, {
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
      throw new Error("List of cars is empty!");
    }
  } catch (error) {
    toast.error(error.message);
    throw new Error("An error occurred: " + error.message);
  }
};

// --- UPDATE RENTAL ---
export const updateRental = async (rentalID, data) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/rentals/${rentalID}`, {
      method: "PUT",
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

// --- DELETE RENTAL ---
export const deleteRental = async (rentalID) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/rentals/${rentalID}`, {
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
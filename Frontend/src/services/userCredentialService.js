import urls from "../config/config";
import { toast } from "react-toastify";

const baseURL = urls.development;

// --- DELETE USER CREDENTIAL ---
export const deleteUserCredential = async (userCredentialsID) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/user-credentials/${userCredentialsID}`, {
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

// --- ADD USER CREDENTIAL ---
export const addUserCredential = async (data) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/user-credentials`, {
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
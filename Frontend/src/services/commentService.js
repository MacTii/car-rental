import urls from "../config/config";
import { toast } from "react-toastify";

const baseURL = urls.development;

// --- ADD COMMENT ---
export const addComment = async (data) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/comments`, {
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

// --- UPDATE COMMENT ---
export const updateComment = async (commentID, data) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/comments/${commentID}`, {
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

// --- GET COMMENT BY CARID ---
export const getCommentById = async (commentID) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/comments/${commentID}`, {
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

// --- DELETE CAR ---
export const deleteComment = async (commentID) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/comments/${commentID}`, {
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
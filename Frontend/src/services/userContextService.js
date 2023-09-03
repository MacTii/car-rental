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

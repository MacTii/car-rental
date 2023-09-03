import urls from "../config/config";
import { toast } from "react-toastify";

const baseURL = urls.development;

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
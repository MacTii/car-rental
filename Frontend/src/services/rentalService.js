import urls from "../config/config";

const baseURL = urls.development;
const token = localStorage.getItem("token");

export const addRental = async (data) => {
  try {
    const response = await fetch(`${baseURL}/api/rentals`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(data),
    });
    const result = await response.json();

    if (result.data) {
      return result.data;
    } else {
      throw new Error(result.detail);
    }
  } catch (error) {
    throw new Error("An error occurred: " + error.message);
  }
};

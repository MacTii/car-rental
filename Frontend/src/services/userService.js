import urls from "../config/config";

const baseURL = urls.development;
const token = localStorage.getItem("token");

// --- GET USER (USING TOKEN) ---
export const getUsername = async () => {
  try {
    const response = await fetch(`${baseURL}/api/username`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    });
    const result = await response.text();
    return result;
  } catch (error) {
    throw new Error(`An error occurred: ${error}`);
  }
};

// -- GET USER BY USERNAME ---
export const getUserByUsername = async (username) => {
  try {
    const response = await fetch(`${baseURL}/api/users/username/${username}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
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

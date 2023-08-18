import urls from "../config/config";

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
    return result;
  } catch (error) {
    throw new Error(`An error occurred: ${error}`);
  }
};

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

    if (result.data) {
      return result.data;
    } else {
      throw new Error(result.detail);
    }
  } catch (error) {
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

    if (result.data) {
      return result.data;
    } else {
      throw new Error(result.detail);
    }
  } catch (error) {
    throw new Error("An error occurred: " + error.message);
  }
};

import urls from "../config/config";

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
    throw new Error("An error occurred: " + error.message);
  }
};
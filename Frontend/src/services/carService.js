import urls from "../config/config";

const baseURL = urls.development;

export const getCars = async () => {
  try {
    const response = await fetch(`${baseURL}/api/cars`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const result = await response.json();

    if (result.data) {
      return result.data;
    } else {
      throw new Error("List of cars is empty!");
    }
  } catch (error) {
    throw new Error("An error occurred: " + error.message);
  }
};

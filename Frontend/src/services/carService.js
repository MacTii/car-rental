import urls from "../config/config";

const baseURL = urls.development;

// --- GET ALL CARS ---
export const getCars = async () => {
  try {
    const response = await fetch(`${baseURL}/api/cars`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const result = await response.json();

    if (response.ok) {
      return result.data;
    } else {
      throw new Error("List of cars is empty!");
    }
  } catch (error) {
    throw new Error("An error occurred: " + error.message);
  }
};

// --- GET CAR BY CARID ---
export const getCarByID = async (carID) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/cars/${carID}`, {
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
    throw new Error("An error occurred: " + error.message);
  }
};

// --- UPDATE CAR ---
export const updateCar = async (carID, data) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/cars/${carID}`, {
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
    throw new Error("An error occurred: " + error.message);
  }
};

// --- DELETE CAR ---
export const deleteCar = async (carID) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/cars/${carID}`, {
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

// --- ADD CAR ---
export const addCar = async (data) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/cars`, {
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
    throw new Error("An error occurred: " + error.message);
  }
};

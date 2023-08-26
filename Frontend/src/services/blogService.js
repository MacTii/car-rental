import urls from "../config/config";

const baseURL = urls.development;

// --- GET ALL BLOGS ---
export const getBlogs = async () => {
  try {
    const response = await fetch(`${baseURL}/api/blogs`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const result = await response.json();

    if (result.data) {
      return result.data;
    } else {
      throw new Error("List of blogs is empty!");
    }
  } catch (error) {
    throw new Error("An error occurred: " + error.message);
  }
};

// --- GET BLOG BY BLOG ID ---
export const getBlogByTitle = async (title) => {
  try {
    const token = localStorage.getItem("token");
    const response = await fetch(`${baseURL}/api/blogs/title/${title}`, {
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

import React, { useEffect, useState } from "react";
import "../styles/profile.css";
import { getUserByUsername, updateUser } from "../services/userService";
import { getUsernameFromToken } from "../services/tokenService";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const Profile = () => {
  const [user, setUser] = useState([]);
  const [token] = useState(localStorage.getItem("token"));

  const navigate = useNavigate();

  useEffect(() => {
    if (!token) {
      navigate("/home");
      return;
    }
    fetchUser();
  }, []);

  const fetchUser = async () => {
    const username = getUsernameFromToken();
    const result = await getUserByUsername(username);
    setUser(result);
  };

  const fetchUpdateUser = async () => {
    await updateUser(user.id, user);
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setUser((prevProfile) => ({
      ...prevProfile,
      [name]: value,
    }));
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    fetchUpdateUser();
    toast.success("Profile updated successfully!");
  };

  return (
    <div className="user__profile">
      <form onSubmit={handleSubmit} className="form__wrapper">
        <h2>User Profile</h2>
        <div className="form__grid">
          <div className="form__column">
            <label>
              Name:
              <input
                type="text"
                name="name"
                value={user.name}
                onChange={handleInputChange}
              />
            </label>
            <label>
              Phone Number:
              <input
                type="tel"
                name="phoneNumber"
                value={user.phoneNumber}
                onChange={handleInputChange}
              />
            </label>
            <label>
              Date of Birth:
              <input
                type="date"
                name="dateOfBirth"
                value={user.dateOfBirth}
                onChange={handleInputChange}
              />
            </label>
          </div>
          <div className="form__column">
            <label>
              Surname:
              <input
                type="text"
                name="surname"
                value={user.surname}
                onChange={handleInputChange}
              />
            </label>
            <label>
              Address:
              <input
                type="text"
                name="address"
                value={user.address}
                onChange={handleInputChange}
              />
            </label>
            <label>
              Driving License Number:
              <input
                type="text"
                name="drivingLicenseNumber"
                value={user.drivingLicenseNumber}
                onChange={handleInputChange}
              />
            </label>
          </div>
          <div className="form__column">
            <label>
              Email:
              <input
                type="email"
                name="email"
                value={user.email}
                onChange={handleInputChange}
              />
            </label>
            <label>
              Gender:
              <select
                name="gender"
                value={user.gender}
                onChange={handleInputChange}
              >
                <option value="Male">Male</option>
                <option value="Female">Female</option>
                <option value="Other">Other</option>
              </select>
            </label>
            <label>
              Identification Number:
              <input
                type="text"
                name="identificationNumber"
                value={user.identificationNumber}
                onChange={handleInputChange}
              />
            </label>
            <div className="submit__button">
              <button type="submit">Save Changes</button>
            </div>
          </div>
        </div>
      </form>
    </div>
  );
};

export default Profile;

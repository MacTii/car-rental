import React, { useState, useEffect } from "react";
import {
  Table,
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Form,
  FormGroup,
  Label,
  Input,
} from "reactstrap";
import {
  addUser,
  deleteUser,
  getUsers,
  updateUser,
} from "../../services/userService";
import "../../styles/admin/users.css";
import { toast } from "react-toastify";
import {
  addUserCredential,
  deleteUserCredential,
} from "../../services/userCredentialService";
import {
  generatePasswordCredentials,
  resetPasswordCredentials,
} from "../../services/passwordService";
import "../../styles/admin/users.css";

const Users = () => {
  const [users, setUsers] = useState([]);
  const [editUser, setEditUser] = useState({
    isActive: false,
  });
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [addModalOpen, setAddModalOpen] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");
  const [passwordCredentials, setPasswordCredentials] = useState();
  const [isGenerated, setIsGenerated] = useState(false);

  useEffect(() => {
    fetchGetUsers();
    console.log();
  }, []);

  const fetchGetUsers = async () => {
    const result = await getUsers(); // Get all users
    setUsers(result);
    console.log(result);
  };

  const clearEditUser = () => {
    setEditUser({});
  };

  const handleSearchChange = (e) => {
    setSearchTerm(e.target.value);
  };

  const handleResetPasswordCredentials = async (userId) => {
    await resetPasswordCredentials(userId);
    toast.success("Password reset successful");
  };

  const handleEditUser = (userId) => {
    const userToEdit = users.find((user) => user.id === userId);
    setEditUser(userToEdit);
    setEditModalOpen(true); // Open modal for user edit
  };

  const handleUpdateUser = async (e) => {
    e.preventDefault();
    await updateUser(editUser.id, editUser); // Update user
    setEditModalOpen(false); // Close modal for user edit
    fetchGetUsers(); // Refresh user list
    toast.success("User saved successfully");
  };

  const handleDeleteUser = async (user) => {
    await deleteUser(user.id);
    await deleteUserCredential(user.userCredentialsID);
    fetchGetUsers();
    toast.success("User deleted successfully");
  };

  const handleGeneratePasswordCredentials = async () => {
    const result = await generatePasswordCredentials();
    setPasswordCredentials(result);
    setIsGenerated(true);
    console.log(result);
  };

  const handleAddUser = async (e) => {
    e.preventDefault();

    const userCredentials = {
      username: editUser.userCredentials.username,
      passwordHash: passwordCredentials.passwordHash,
      passwordSalt: passwordCredentials.passwordSalt,
      refreshToken: null,
      tokenCreated: null,
      tokenExpires: null,
      isActive: editUser.userCredentials.isActive,
      userRole: editUser.userCredentials.userRole,
    };

    const user = {
      name: editUser.name,
      surname: editUser.surname,
      email: editUser.email,
      phoneNumber: editUser.phoneNumber,
      address: editUser.address,
      dateOfBirth: editUser.dateOfBirth,
      gender: editUser.gender,
      identificationNumber: editUser.identificationNumber,
      drivingLicenseNumber: editUser.drivingLicenseNumber,
      userCredentials: userCredentials,
    };

    console.log(editUser);
    console.log(user);

    await addUserCredential(userCredentials); // Dodanie danych użytkownika
    await addUser(user); // Dodanie użytkownika

    setEditUser({}); // Wyczyszczenie pól formularza

    setAddModalOpen(false);

    fetchGetUsers(); // Odświeżenie listy użytkowników
    toast.success("User added successfully");
  };

  return (
    <div className="users-container">
      <h2>Users</h2>
      <div className="search-container">
        <Button color="success" onClick={() => setAddModalOpen(true)}>
          Add User
        </Button>
        <div className="search-box">
          <Input
            type="text"
            placeholder="Search..."
            value={searchTerm}
            onChange={handleSearchChange}
          />
        </div>
      </div>
      <Table className="users-table">
        <thead>
          <tr>
            <th>Id</th>
            <th>Username</th>
            <th>Name</th>
            <th>Surname</th>
            <th>Email</th>
            <th>Phone Number</th>
            <th>Address</th>
            <th>Date Of Birth</th>
            <th>Gender</th>
            <th>Identification Number</th>
            <th>Driving License Number</th>
            <th>User Role</th>
            <th>Is Active</th>
            <th>Reset Password</th>
            <th>Edit</th>
            <th>Delete</th>
          </tr>
        </thead>
        <tbody>
          {users
            .filter(
              (user) =>
                user.userCredentials.username
                  .toLowerCase()
                  .includes(searchTerm.toLowerCase()) ||
                user.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
                user.surname.toLowerCase().includes(searchTerm.toLowerCase()) ||
                user.email?.toLowerCase().includes(searchTerm.toLowerCase())
            )
            .map((user) => (
              <tr key={user.id}>
                <td>{user.id}</td>
                <td>{user.userCredentials.username}</td>
                <td>{user.name}</td>
                <td>{user.surname}</td>
                <td>{user.email}</td>
                <td>{user.phoneNumber}</td>
                <td>{user.address}</td>
                <td>{user.dateOfBirth}</td>
                <td>{user.gender}</td>
                <td>{user.identificationNumber}</td>
                <td>{user.drivingLicenseNumber}</td>
                <td>{user.userCredentials.userRole}</td>
                <td>{user.userCredentials.isActive ? "Yes" : "No"}</td>
                <td>
                  <Button
                    className="reset-btn"
                    onClick={() => handleResetPasswordCredentials(user.id)}
                  >
                    Reset
                  </Button>
                </td>
                <td>
                  <Button
                    color="primary"
                    onClick={() => handleEditUser(user.id)}
                  >
                    Edit
                  </Button>
                </td>
                <td>
                  <Button color="danger" onClick={() => handleDeleteUser(user)}>
                    Delete
                  </Button>
                </td>
              </tr>
            ))}
        </tbody>
      </Table>

      <Modal
        isOpen={editModalOpen}
        toggle={() => {
          setEditModalOpen(!editModalOpen);
          clearEditUser();
        }}
        onClosed={() => clearEditUser()}
        className="edit-modal"
      >
        <ModalHeader toggle={() => setEditModalOpen(!editModalOpen)}>
          Edit User
        </ModalHeader>
        <ModalBody>
          <Form id="edit-user-form" onSubmit={handleUpdateUser}>
            <FormGroup>
              <Label for="username">Username</Label>
              <Input
                type="text"
                name="username"
                id="username"
                value={editUser?.userCredentials?.username || ""}
                onChange={(e) =>
                  setEditUser({
                    ...editUser,
                    userCredentials: {
                      ...editUser?.userCredentials,
                      username: e.target.value,
                    },
                  })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="name">Name</Label>
              <Input
                type="text"
                name="name"
                id="name"
                value={editUser?.name || ""}
                onChange={(e) =>
                  setEditUser({ ...editUser, name: e.target.value })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="surname">Surname</Label>
              <Input
                type="text"
                name="surname"
                id="surname"
                value={editUser?.surname || ""}
                onChange={(e) =>
                  setEditUser({ ...editUser, surname: e.target.value })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="email">Email</Label>
              <Input
                id="email"
                type="email"
                name="email"
                value={editUser?.email || ""}
                onChange={(e) =>
                  setEditUser({ ...editUser, email: e.target.value })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="phoneNumber">Phone Number</Label>
              <Input
                type="text"
                name="phoneNumber"
                id="phoneNumber"
                value={editUser?.phoneNumber || ""}
                onChange={(e) =>
                  setEditUser({ ...editUser, phoneNumber: e.target.value })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="address">Address</Label>
              <Input
                type="text"
                name="address"
                id="address"
                value={editUser?.address || ""}
                onChange={(e) =>
                  setEditUser({ ...editUser, address: e.target.value })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="dateOfBirth">Date Of Birth</Label>
              <Input
                type="date"
                name="dateOfBirth"
                id="dateOfBirth"
                value={editUser?.dateOfBirth || ""}
                onChange={(e) =>
                  setEditUser({ ...editUser, dateOfBirth: e.target.value })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="gender">Gender</Label>
              <select
                className="form-select"
                name="gender"
                id="gender"
                value={editUser?.gender || ""}
                onChange={(e) =>
                  setEditUser({
                    ...editUser,
                    gender: e.target.value,
                  })
                }
                required
              >
                <option value="Male">Male</option>
                <option value="Female">Female</option>
                <option value="Other">Other</option>
              </select>
            </FormGroup>
            <FormGroup>
              <Label for="identificationNumber">Identification Number</Label>
              <Input
                type="text"
                name="identificationNumber"
                id="identificationNumber"
                value={editUser?.identificationNumber || ""}
                onChange={(e) =>
                  setEditUser({
                    ...editUser,
                    identificationNumber: e.target.value,
                  })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="drivingLicenseNumber">Driving License Number</Label>
              <Input
                type="text"
                name="drivingLicenseNumber"
                id="drivingLicenseNumber"
                value={editUser?.drivingLicenseNumber || ""}
                onChange={(e) =>
                  setEditUser({
                    ...editUser,
                    drivingLicenseNumber: e.target.value,
                  })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="userRole">User Role</Label>
              <select
                className="form-select"
                name="userRole"
                id="userRole"
                value={editUser?.userCredentials?.userRole || ""}
                onChange={(e) =>
                  setEditUser({
                    ...editUser,
                    userCredentials: {
                      ...editUser?.userCredentials,
                      userRole: e.target.value,
                    },
                  })
                }
                required
              >
                <option value="User">User</option>
                <option value="Admin">Admin</option>
              </select>
            </FormGroup>
            <FormGroup>
              <Label for="isActive">Is Active</Label>
              <Input
                type="checkbox"
                name="isActive"
                id="isActive"
                checked={editUser?.userCredentials?.isActive || false}
                onChange={(e) =>
                  setEditUser({
                    ...editUser,
                    userCredentials: {
                      ...editUser?.userCredentials,
                      isActive: e.target.checked,
                    },
                  })
                }
              />
            </FormGroup>
          </Form>
        </ModalBody>
        <ModalFooter>
          <Button color="primary" form="edit-user-form" type="submit">
            Save
          </Button>
          <Button
            color="secondary"
            onClick={() => {
              setEditModalOpen(!editModalOpen);
              setEditUser({}); // Clear the form fields on cancel
            }}
          >
            Cancel
          </Button>
        </ModalFooter>
      </Modal>
      <Modal
        isOpen={addModalOpen}
        toggle={() => {
          setAddModalOpen(!addModalOpen);
          clearEditUser();
          setIsGenerated(false);
        }}
        onClosed={() => {
          clearEditUser();
          setIsGenerated(false);
        }}
        className="add-modal"
      >
        <ModalHeader toggle={() => setAddModalOpen(!addModalOpen)}>
          Add User
        </ModalHeader>
        <ModalBody>
          <Form id="add-user-form" onSubmit={handleAddUser}>
            <FormGroup>
              <Label for="username">Username</Label>
              <Input
                type="text"
                name="username"
                id="username"
                value={editUser?.userCredentials?.username || ""}
                onChange={(e) =>
                  setEditUser({
                    ...editUser,
                    userCredentials: {
                      ...editUser?.userCredentials,
                      username: e.target.value,
                    },
                  })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="password">Password</Label>
              <Input
                type="password"
                name="password"
                id="password"
                value={isGenerated ? "PASSWORD" : ""}
                required
                readOnly
              />
              <Button
                color="primary"
                disabled={isGenerated}
                onClick={handleGeneratePasswordCredentials}
              >
                Generate
              </Button>
            </FormGroup>
            <FormGroup>
              <Label for="name">Name</Label>
              <Input
                type="text"
                name="name"
                id="name"
                value={editUser?.name || ""}
                onChange={(e) =>
                  setEditUser({ ...editUser, name: e.target.value })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="surname">Surname</Label>
              <Input
                type="text"
                name="surname"
                id="surname"
                value={editUser?.surname || ""}
                onChange={(e) =>
                  setEditUser({ ...editUser, surname: e.target.value })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="email">Email</Label>
              <Input
                type="text"
                name="email"
                id="email"
                value={editUser?.email || ""}
                onChange={(e) =>
                  setEditUser({ ...editUser, email: e.target.value })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="phoneNumber">Phone Number</Label>
              <Input
                type="text"
                name="phoneNumber"
                id="phoneNumber"
                value={editUser?.phoneNumber || ""}
                onChange={(e) =>
                  setEditUser({ ...editUser, phoneNumber: e.target.value })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="address">Address</Label>
              <Input
                type="text"
                name="address"
                id="address"
                value={editUser?.address || ""}
                onChange={(e) =>
                  setEditUser({ ...editUser, address: e.target.value })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="dateOfBirth">Date Of Birth</Label>
              <Input
                type="date"
                name="dateOfBirth"
                id="dateOfBirth"
                value={editUser?.dateOfBirth || ""}
                onChange={(e) =>
                  setEditUser({ ...editUser, dateOfBirth: e.target.value })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="gender">Gender</Label>
              <select
                className="form-select"
                name="gender"
                id="gender"
                value={editUser?.gender || ""}
                onChange={(e) =>
                  setEditUser({
                    ...editUser,
                    gender: e.target.value,
                  })
                }
                required
              >
                <option selected="selected" disabled value="">
                  Select Gender...
                </option>
                <option value="Male">Male</option>
                <option value="Female">Female</option>
                <option value="Other">Other</option>
              </select>
            </FormGroup>
            <FormGroup>
              <Label for="identificationNumber">Identification Number</Label>
              <Input
                type="text"
                name="identificationNumber"
                id="identificationNumber"
                value={editUser?.identificationNumber || ""}
                onChange={(e) =>
                  setEditUser({
                    ...editUser,
                    identificationNumber: e.target.value,
                  })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="drivingLicenseNumber">Driving License Number</Label>
              <Input
                type="text"
                name="drivingLicenseNumber"
                id="drivingLicenseNumber"
                value={editUser?.drivingLicenseNumber || ""}
                onChange={(e) =>
                  setEditUser({
                    ...editUser,
                    drivingLicenseNumber: e.target.value,
                  })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="userRole">User Role</Label>
              <select
                className="form-select"
                name="userRole"
                id="userRole"
                value={editUser?.userCredentials?.userRole || ""}
                onChange={(e) =>
                  setEditUser({
                    ...editUser,
                    userCredentials: {
                      ...editUser?.userCredentials,
                      userRole: e.target.value,
                    },
                  })
                }
                required
              >
                <option selected="selected" disabled value="">
                  Select Role...
                </option>
                <option value="User">User</option>
                <option value="Admin">Admin</option>
              </select>
            </FormGroup>
            <FormGroup>
              <Label for="isActive">Is Active</Label>
              <Input
                type="checkbox"
                name="isActive"
                id="isActive"
                checked={editUser?.userCredentials?.isActive || false}
                onChange={(e) =>
                  setEditUser({
                    ...editUser,
                    userCredentials: {
                      ...editUser?.userCredentials,
                      isActive: e.target.checked,
                    },
                  })
                }
              />
            </FormGroup>
          </Form>
        </ModalBody>
        <ModalFooter>
          <Button form="add-user-form" type="submit" color="primary">
            Add
          </Button>
          <Button
            color="secondary"
            onClick={() => {
              setAddModalOpen(!addModalOpen);
              setEditUser({}); // Clear the form fields on cancel
              setIsGenerated(false);
            }}
          >
            Cancel
          </Button>
        </ModalFooter>
      </Modal>
    </div>
  );
};

export default Users;

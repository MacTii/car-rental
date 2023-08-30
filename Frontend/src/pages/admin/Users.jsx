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
import { deleteUser, getUsers, updateUser } from "../../services/userService";
import "../../styles/admin/users.css";
import { toast } from "react-toastify";
import { deleteUserCredential } from "../../services/userCredentialService";

const Users = () => {
  const [users, setUsers] = useState([]);
  const [editUser, setEditUser] = useState(null);
  const [editModalOpen, setEditModalOpen] = useState(false);

  useEffect(() => {
    fetchGetUsers();
  }, []);

  const fetchGetUsers = async () => {
    const result = await getUsers(); // Get all users
    setUsers(result);
    console.log(result);
  };

  const handleEditUser = (userId) => {
    const userToEdit = users.find((user) => user.id === userId);
    setEditUser(userToEdit);
    setEditModalOpen(true); // Open modal for user edit
  };

  const handleUpdateUser = async () => {
    await updateUser(editUser.id, editUser); // Update user
    setEditModalOpen(false); // Close modal for user edit
    fetchGetUsers(); // Refresh user list
    toast.success("User saved successfully");
  };

  const handleDeleteUser = async (user) => {
    await deleteUser(user.id);
    await deleteUserCredential(user.userCredentialsID);
    toast.success("User deleted successfully");
  };

  return (
    <div className="users-container">
      <h2>Users</h2>
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
            <th>Edit</th>
            <th>Delete</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
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
                  color="primary"
                  className="edit-btn"
                  onClick={() => handleEditUser(user.id)}
                >
                  Edit
                </Button>
              </td>
              <td>
                <Button
                  color="danger"
                  className="delete-btn"
                  onClick={() => handleDeleteUser(user)}
                >
                  Delete
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Modal
        isOpen={editModalOpen}
        toggle={() => setEditModalOpen(!editModalOpen)}
      >
        <ModalHeader toggle={() => setEditModalOpen(!editModalOpen)}>
          Edit User
        </ModalHeader>
        <ModalBody>
          <Form>
            <FormGroup>
              <Label for="username">Username</Label>
              <Input
                type="text"
                name="username"
                id="username"
                value={editUser?.userCredentials.username || ""}
                onChange={(e) =>
                  setEditUser({
                    ...editUser,
                    userCredentials: {
                      ...editUser.userCredentials,
                      username: e.target.value,
                    },
                  })
                }
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
              />
            </FormGroup>
            <FormGroup>
              <Label for="userRole">User Role</Label>
              <select
                className="form-select"
                name="userRole"
                id="userRole"
                value={editUser?.userCredentials.userRole || ""}
                onChange={(e) =>
                  setEditUser({
                    ...editUser,
                    userCredentials: {
                      ...editUser.userCredentials,
                      userRole: e.target.value,
                    },
                  })
                }
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
                checked={editUser?.userCredentials.isActive || false}
                onChange={(e) =>
                  setEditUser({
                    ...editUser,
                    userCredentials: {
                      ...editUser.userCredentials,
                      isActive: e.target.checked,
                    },
                  })
                }
              />
            </FormGroup>
          </Form>
        </ModalBody>
        <ModalFooter>
          <Button color="primary" onClick={handleUpdateUser}>
            Save
          </Button>{" "}
          <Button
            color="secondary"
            onClick={() => setEditModalOpen(!editModalOpen)}
          >
            Cancel
          </Button>
        </ModalFooter>
      </Modal>
    </div>
  );
};

export default Users;
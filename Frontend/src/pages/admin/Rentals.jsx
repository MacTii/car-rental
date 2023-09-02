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
import "../../styles/admin/rentals.css";
import { toast } from "react-toastify";
import {
  addRental,
  deleteRental,
  getRentals,
  updateRental,
} from "../../services/rentalService";
import { getUsers } from "../../services/userService";
import { getCars } from "../../services/carService";

const Rentals = () => {
  const [rentals, setRentals] = useState([]);
  const [cars, setCars] = useState([]);
  const [users, setUsers] = useState([]);
  const [editRental, setEditRental] = useState(null);
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [addModalOpen, setAddModalOpen] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");

  useEffect(() => {
    fetchGetRentals();
    fetchGetCars();
    fetchGetUsers();
  }, []);

  const fetchGetRentals = async () => {
    const result = await getRentals(); // Get all rental
    setRentals(result);
    console.log(result);
  };

  const clearEditRental = () => {
    setEditRental({});
  };

  const fetchGetUsers = async () => {
    const result = await getUsers(); // Get all rental
    setUsers(result);
    console.log(result);
  };

  const fetchGetCars = async () => {
    const result = await getCars(); // Get all rental
    setCars(result);
    console.log(result);
  };

  const getUserById = (userId) => {
    return users.find((user) => user.id === userId) || {};
  };

  const getCarById = (carId) => {
    return cars.find((car) => car.id === carId) || {};
  };

  const handleSearchChange = (e) => {
    setSearchTerm(e.target.value);
  };

  const handleEditRental = (rentalId) => {
    const rentalToEdit = rentals.find((rental) => rental.id === rentalId);
    setEditRental(rentalToEdit);
    setEditModalOpen(true); // Open modal for rental edit
  };

  const handleReturnRental = async (rentalId) => {
    const rentalToUpdate = rentals.find((rental) => rental.id === rentalId);

    // Convert the comment's date to local time before updating
    const localDate = new Date();
    localDate.setMinutes(
      localDate.getMinutes() - localDate.getTimezoneOffset()
    );

    const formattedReturnDate = localDate
      .toISOString()
      .slice(0, 16)
      .replace("T", " ");

    const updatedRental = {
      ...rentalToUpdate,
      returnDate: formattedReturnDate,
    };

    await updateRental(rentalId, updatedRental); // Update rental with new return date
    fetchGetRentals(); // Refresh rental list
    toast.success("Rental returned successfully");
  };

  const handleUpdateRental = async (e) => {
    e.preventDefault();

    // Ustal rentDate na bieżący czas
    const rentDate = new Date(editRental.rentDate);
    rentDate.setMinutes(rentDate.getMinutes() - rentDate.getTimezoneOffset());
    const formattedRentDate = rentDate
      .toISOString()
      .slice(0, 16)
      .replace("T", " ");

    // Ustal returnDate na bieżący czas, jeśli jest dostępny
    let formattedReturnDate = null;
    if (editRental.returnDate) {
      const localReturnDate = new Date(editRental.returnDate);
      localReturnDate.setMinutes(
        localReturnDate.getMinutes() - localReturnDate.getTimezoneOffset()
      );
      formattedReturnDate = localReturnDate
        .toISOString()
        .slice(0, 16)
        .replace("T", " ");
    }

    // Update the rentDate field in the editRental object
    editRental.rentDate = formattedRentDate;
    editRental.returnDate = formattedReturnDate;

    await updateRental(editRental.id, editRental); // Update rental
    setEditModalOpen(false); // Close modal for rental edit
    fetchGetRentals(); // Refresh rental list
    toast.success("Rental saved successfully");
  };

  const handleDeleteRental = async (rentalId) => {
    await deleteRental(rentalId);
    fetchGetRentals();
    toast.success("Rental deleted successfully");
  };

  const handleAddRental = async (e) => {
    e.preventDefault();
    console.log(editRental);
    await addRental(editRental); // Update car

    setEditRental({}); // Clear the form fields

    setAddModalOpen(false);

    fetchGetRentals(); // Refresh car list
    toast.success("Rental added successfully");
  };

  return (
    <div className="rentals-container">
      <h2>Rentals</h2>
      <div className="search-container">
        <Button color="success" onClick={() => setAddModalOpen(true)}>
          Add Rental
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
      <Table className="rentals-table">
        <thead>
          <tr>
            <th>Id</th>
            <th>Car</th>
            <th>User</th>
            <th>Rent Date</th>
            <th>Return Date</th>
            <th>Comment</th>
            <th>Payment Method</th>
            <th>Return</th>
            <th>Edit</th>
            <th>Delete</th>
          </tr>
        </thead>
        <tbody>
          {rentals
            .filter(
              (rental) =>
                getUserById(rental.userID)
                  .name.toLowerCase()
                  .includes(searchTerm.toLowerCase()) ||
                getUserById(rental.userID)
                  .surname.toLowerCase()
                  .includes(searchTerm.toLowerCase()) ||
                getCarById(rental.carID)
                  .make.toLowerCase()
                  .includes(searchTerm.toLowerCase()) ||
                getCarById(rental.carID)
                  .model.toLowerCase()
                  .includes(searchTerm.toLowerCase())
            )
            .map((rental) => {
              const user = getUserById(rental.userID);
              const car = getCarById(rental.carID);

              return (
                <tr key={rental.id}>
                  <td>{rental.id}</td>
                  <td>
                    {car.make} {car.model}
                  </td>
                  <td>
                    {user.name} {user.surname} ({user.email})
                  </td>
                  <td>{rental.rentDate}</td>
                  <td>{rental.returnDate}</td>
                  <td>{rental.comment}</td>
                  <td>{rental.paymentMethod}</td>
                  <td>
                    {!rental.returnDate ? (
                      <Button
                        color="info"
                        onClick={() => handleReturnRental(rental.id)}
                      >
                        Return
                      </Button>
                    ) : (
                      "Returned"
                    )}
                  </td>
                  <td>
                    <Button
                      color="primary"
                      onClick={() => handleEditRental(rental.id)}
                    >
                      Edit
                    </Button>
                  </td>
                  <td>
                    <Button
                      color="danger"
                      onClick={() => handleDeleteRental(rental.id)}
                    >
                      Delete
                    </Button>
                  </td>
                </tr>
              );
            })}
        </tbody>
      </Table>
      <Modal
        isOpen={editModalOpen}
        toggle={() => {
          setEditModalOpen(!editModalOpen);
          clearEditRental();
        }}
        onClosed={() => clearEditRental()}
        className="edit-modal"
      >
        <ModalHeader toggle={() => setEditModalOpen(!editModalOpen)}>
          Edit Rental
        </ModalHeader>
        <ModalBody>
          <Form id="edit-rental-form" onSubmit={handleUpdateRental}>
            <FormGroup>
              <Label for="carID">Car</Label>
              <select
                className="form-select"
                name="carID"
                id="carID"
                value={editRental?.carID || ""}
                onChange={(e) =>
                  setEditRental({
                    ...editRental,
                    carID: parseInt(e.target.value),
                  })
                }
                required
              >
                <option selected="selected" disabled value="">
                  Select Car...
                </option>
                {cars.map((car) => (
                  <option key={car.id} value={car.id}>
                    {car.make} - {car.model}
                  </option>
                ))}
              </select>
            </FormGroup>
            <FormGroup>
              <Label for="userID">User</Label>
              <select
                className="form-select"
                name="userID"
                id="userID"
                value={editRental?.userID || ""}
                onChange={(e) =>
                  setEditRental({
                    ...editRental,
                    userID: parseInt(e.target.value),
                  })
                }
                required
              >
                <option selected="selected" disabled value="">
                  Select User...
                </option>
                {users.map((user) => (
                  <option key={user.id} value={user.id}>
                    {user.name} - {user.surname} - {user.email}
                  </option>
                ))}
              </select>
            </FormGroup>
            <FormGroup>
              <Label for="rentDate">Rent Date</Label>
              <Input
                type="datetime-local"
                name="rentDate"
                id="rentDate"
                value={editRental?.rentDate || ""}
                onChange={(e) =>
                  setEditRental({ ...editRental, rentDate: e.target.value })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="returnDate">Return Date</Label>
              <Input
                type="datetime-local"
                name="returnDate"
                id="returnDate"
                value={editRental?.returnDate || ""}
                onChange={(e) =>
                  setEditRental({ ...editRental, returnDate: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="comment">Comment</Label>
              <textarea
                className="form-control"
                name="comment"
                id="comment"
                value={editRental?.comment || ""}
                onChange={(e) =>
                  setEditRental({ ...editRental, comment: e.target.value })
                }
                rows={3}
              />
            </FormGroup>
            <FormGroup>
              <Label for="paymentMethod">Payment Method</Label>
              <select
                className="form-select"
                name="paymentMethod"
                id="paymentMethod"
                value={editRental?.paymentMethod || ""}
                onChange={(e) =>
                  setEditRental({
                    ...editRental,
                    paymentMethod: e.target.value,
                  })
                }
                required
              >
                <option selected="selected" disabled value="">
                  Select Payment Method...
                </option>
                <option value="Direct Bank Transfer">
                  Direct Bank Transfer
                </option>
                <option value="Cheque Payment">Cheque Payment</option>
                <option value="Master Card">Master Card</option>
                <option value="Paypal">Paypal</option>
              </select>
            </FormGroup>
          </Form>
        </ModalBody>
        <ModalFooter>
          <Button form="edit-rental-form" type="submit" color="primary">
            Save
          </Button>
          <Button
            color="secondary"
            onClick={() => {
              setEditModalOpen(!editModalOpen);
              clearEditRental();
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
          clearEditRental();
        }}
        onClosed={() => clearEditRental()}
        className="add-modal"
      >
        <ModalHeader toggle={() => setAddModalOpen(!addModalOpen)}>
          Add Rental
        </ModalHeader>
        <ModalBody>
          <Form id="add-rental-form" onSubmit={handleAddRental}>
            <FormGroup>
              <Label for="carID">Car</Label>
              <select
                className="form-select"
                name="carID"
                id="carID"
                value={editRental?.carID || ""}
                onChange={(e) =>
                  setEditRental({
                    ...editRental,
                    carID: parseInt(e.target.value),
                  })
                }
                required
              >
                <option selected="selected" disabled value="">
                  Select Car...
                </option>
                {cars.map((car) => (
                  <option key={car.id} value={car.id}>
                    {car.make} - {car.model}
                  </option>
                ))}
              </select>
            </FormGroup>
            <FormGroup>
              <Label for="userID">User</Label>
              <select
                className="form-select"
                name="userID"
                id="userID"
                value={editRental?.userID || ""}
                onChange={(e) =>
                  setEditRental({
                    ...editRental,
                    userID: parseInt(e.target.value),
                  })
                }
                required
              >
                <option selected="selected" disabled value="">
                  Select User...
                </option>
                {users.map((user) => (
                  <option key={user.id} value={user.id}>
                    {user.name} - {user.surname} - {user.email}
                  </option>
                ))}
              </select>
            </FormGroup>
            <FormGroup>
              <Label for="rentDate">Rent Date</Label>
              <Input
                type="datetime-local"
                name="rentDate"
                id="rentDate"
                value={editRental?.rentDate || ""}
                onChange={(e) =>
                  setEditRental({ ...editRental, rentDate: e.target.value })
                }
                required
              />
            </FormGroup>
            <FormGroup>
              <Label for="returnDate">Return Date</Label>
              <Input
                type="datetime-local"
                name="returnDate"
                id="returnDate"
                value={editRental?.returnDate || ""}
                onChange={(e) =>
                  setEditRental({ ...editRental, returnDate: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="comment">Comment</Label>
              <textarea
                className="form-control"
                name="comment"
                id="comment"
                value={editRental?.comment || ""}
                onChange={(e) =>
                  setEditRental({ ...editRental, comment: e.target.value })
                }
                rows={3}
              />
            </FormGroup>
            <select
              className="form-select"
              name="paymentMethod"
              id="paymentMethod"
              value={editRental?.paymentMethod || ""}
              onChange={(e) =>
                setEditRental({
                  ...editRental,
                  userID: parseInt(e.target.value),
                })
              }
              required
            >
              <option selected="selected" disabled value="">
                Select Payment Method...
              </option>
              <option value="Direct Bank Transfer">Direct Bank Transfer</option>
              <option value="Cheque Payment">Cheque Payment</option>
              <option value="Master Card">Master Card</option>
              <option value="Paypal">Paypal</option>
            </select>
          </Form>
        </ModalBody>
        <ModalFooter>
          <Button form="add-rental-form" type="submit" color="primary">
            Add
          </Button>
          <Button
            color="secondary"
            onClick={() => {
              setAddModalOpen(!addModalOpen);
              clearEditRental();
            }}
          >
            Cancel
          </Button>
        </ModalFooter>
      </Modal>
    </div>
  );
};

export default Rentals;

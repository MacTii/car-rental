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
  deleteRental,
  getRentals,
  updateRental,
} from "../../services/rentalService";

const Rentals = () => {
  const [rentals, setRentals] = useState([]);
  const [editRental, setEditRental] = useState(null);
  const [editModalOpen, setEditModalOpen] = useState(false);

  useEffect(() => {
    fetchGetRentals();
  }, []);

  const fetchGetRentals = async () => {
    const result = await getRentals(); // Get all rental
    setRentals(result);
    console.log(result);
  };

  const handleEditRental = (rentalId) => {
    const rentalToEdit = rentals.find((rental) => rental.id === rentalId);
    setEditRental(rentalToEdit);
    setEditModalOpen(true); // Open modal for rental edit
  };

  const handleUpdateRental = async () => {
    await updateRental(editRental.id, editRental); // Update rental
    setEditModalOpen(false); // Close modal for rental edit
    fetchGetRentals(); // Refresh rental list
    toast.success("Rental saved successfully");
  };

  const handleDeleteRental = async (rentalId) => {
    await deleteRental(rentalId);
    toast.success("Rental deleted successfully");
  };

  return (
    <div className="rentals-container">
      <h2>Rentals</h2>
      <Table className="rentals-table">
        <thead>
          <tr>
            <th>Id</th>
            <th>Car Id</th>
            <th>User Id</th>
            <th>Rent Date</th>
            <th>Return Date</th>
            <th>Comment</th>
            <th>Payment Method</th>
            <th>Edit</th>
            <th>Delete</th>
          </tr>
        </thead>
        <tbody>
          {rentals.map((rental) => (
            <tr key={rental.id}>
              <td>{rental.id}</td>
              <td>{rental.carID}</td>
              <td>{rental.userID}</td>
              <td>{rental.rentDate}</td>
              <td>{rental.returnDate}</td>
              <td>{rental.comment}</td>
              <td>{rental.paymentMethod}</td>
              <td>
                <Button
                  color="primary"
                  className="edit-btn"
                  onClick={() => handleEditRental(rental.id)}
                >
                  Edit
                </Button>
              </td>
              <td>
                <Button
                  color="danger"
                  className="delete-btn"
                  onClick={() => handleDeleteRental(rental.id)}
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
          Edit Rental
        </ModalHeader>
        <ModalBody>
          <Form>
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
              <Input
                type="text"
                name="comment"
                id="comment"
                value={editRental?.comment || ""}
                onChange={(e) =>
                  setEditRental({ ...editRental, comment: e.target.value })
                }
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
              >
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
          <Button color="primary" onClick={handleUpdateRental}>
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

export default Rentals;

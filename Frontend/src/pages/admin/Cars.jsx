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
import { deleteCar, getCars, updateCar } from "../../services/carService";
import "../../styles/admin-cars.css";
import { toast } from "react-toastify";

const Cars = () => {
  const [cars, setCars] = useState([]);
  const [editCar, setEditCar] = useState(null);
  const [editModalOpen, setEditModalOpen] = useState(false);

  useEffect(() => {
    fetchGetCars();
  }, []);

  const fetchGetCars = async () => {
    const result = await getCars(); // Get all cars
    setCars(result);
    console.log(result);
  };

  const handleEditCar = (carId) => {
    const carToEdit = cars.find((car) => car.id === carId);
    setEditCar(carToEdit);
    setEditModalOpen(true); // Open modal for car edit
  };

  const handleUpdateCar = async () => {
    await updateCar(editCar.id, editCar); // Update car
    setEditModalOpen(false); // Close modal for car edit
    fetchGetCars(); // Refresh car list
    toast.success("Car saved successfully");
  };

  const handleDeleteCar = async (carId) => {
    await deleteCar(carId);
    toast.success("Car deleted successfully");
  };

  return (
    <div className="cars-container">
      <h2>Cars</h2>
      <Table className="cars-table">
        <thead>
          <tr>
            <th>Make</th>
            <th>Model</th>
            <th>Registration Number</th>
            <th>Color</th>
            <th>Is Available</th>
            <th>Price per Day</th>
            <th>Year</th>
            <th>Engine</th>
            <th>Speed</th>
            <th>Image</th>
            <th>Description</th>
            <th>GPS</th>
            <th>Ratings</th>
            <th>Seat Type</th>
            <th>Edit</th>
            <th>Delete</th>
          </tr>
        </thead>
        <tbody>
          {cars.map((car) => (
            <tr key={car.id}>
              <td>{car.make}</td>
              <td>{car.model}</td>
              <td>{car.registrationNumber}</td>
              <td>{car.color}</td>
              <td>{car.isAvailable ? "Yes" : "No"}</td>
              <td>{car.pricePerDay}</td>
              <td>{car.year}</td>
              <td>{car.engine}</td>
              <td>{car.speed}</td>
              <td>
                <img src={car.image} alt={car.make} className="w-100" />
              </td>
              <td>{car.description}</td>
              <td>{car.gps}</td>
              <td>{car.ratings}</td>
              <td>{car.seatType}</td>
              <td>
                <Button
                  color="primary"
                  className="edit-btn"
                  onClick={() => handleEditCar(car.id)}
                >
                  Edit
                </Button>
              </td>
              <td>
                <Button
                  color="danger"
                  className="delete-btn"
                  onClick={() => handleDeleteCar(car.id)}
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
          Edit Car
        </ModalHeader>
        <ModalBody>
          <Form>
            <FormGroup>
              <Label for="make">Make</Label>
              <Input
                type="text"
                name="make"
                id="make"
                value={editCar?.make || ""}
                onChange={(e) =>
                  setEditCar({ ...editCar, make: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="model">Model</Label>
              <Input
                type="text"
                name="model"
                id="model"
                value={editCar?.model || ""}
                onChange={(e) =>
                  setEditCar({ ...editCar, model: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="registrationNumber">Registration Number</Label>
              <Input
                type="text"
                name="registrationNumber"
                id="registrationNumber"
                value={editCar?.registrationNumber || ""}
                onChange={(e) =>
                  setEditCar({ ...editCar, registrationNumber: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="color">Color</Label>
              <Input
                type="text"
                name="color"
                id="color"
                value={editCar?.color || ""}
                onChange={(e) =>
                  setEditCar({ ...editCar, color: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="isAvailable">Is Available</Label>
              <Input
                type="checkbox"
                name="isAvailable"
                id="isAvailable"
                checked={editCar?.isAvailable || false}
                onChange={(e) =>
                  setEditCar({ ...editCar, isAvailable: e.target.checked })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="pricePerDay">Price per Day</Label>
              <Input
                type="number"
                name="pricePerDay"
                id="pricePerDay"
                step="0.01"
                value={editCar?.pricePerDay || ""}
                onChange={(e) =>
                  setEditCar({
                    ...editCar,
                    pricePerDay: parseFloat(e.target.value),
                  })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="year">Year</Label>
              <Input
                type="number"
                name="year"
                id="year"
                value={editCar?.year || ""}
                onChange={(e) =>
                  setEditCar({ ...editCar, year: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="engine">Engine</Label>
              <Input
                type="text"
                name="engine"
                id="engine"
                value={editCar?.engine || ""}
                onChange={(e) =>
                  setEditCar({ ...editCar, engine: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="speed">Speed</Label>
              <Input
                type="number"
                name="speed"
                id="speed"
                value={editCar?.speed || ""}
                onChange={(e) =>
                  setEditCar({ ...editCar, speed: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="image">Image</Label>
              <img src={editCar?.image} alt={editCar?.make} className="w-100" />
              <Input
                type="file"
                accept="image/png" // accept only png files
                name="image"
                id="image"
                onChange={(e) => {
                  const file = e.target.files[0];
                  if (file) {
                    const reader = new FileReader();
                    reader.onload = (event) => {
                      const base64Image = event.target.result;
                      setEditCar({ ...editCar, image: base64Image });
                    };
                    reader.readAsDataURL(file);
                  }
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="description">Description</Label>
              <Input
                type="text"
                name="description"
                id="description"
                value={editCar?.description || ""}
                onChange={(e) =>
                  setEditCar({ ...editCar, description: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="gps">GPS</Label>
              <Input
                type="text"
                name="gps"
                id="gps"
                value={editCar?.gps || ""}
                onChange={(e) =>
                  setEditCar({ ...editCar, gps: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="ratings">Ratings</Label>
              <Input
                type="number"
                name="ratings"
                id="ratings"
                value={editCar?.ratings || ""}
                onChange={(e) =>
                  setEditCar({ ...editCar, ratings: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="seatType">Seat Type</Label>
              <Input
                type="text"
                name="seatType"
                id="seatType"
                value={editCar?.seatType || ""}
                onChange={(e) =>
                  setEditCar({ ...editCar, seatType: e.target.value })
                }
              />
            </FormGroup>
          </Form>
        </ModalBody>
        <ModalFooter>
          <Button color="primary" onClick={handleUpdateCar}>
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

export default Cars;

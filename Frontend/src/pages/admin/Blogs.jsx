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
import "../../styles/admin/blogs.css";
import { toast } from "react-toastify";
import {
  addBlog,
  deleteBlog,
  getBlogs,
  updateBlog,
} from "../../services/blogService";

const Blogs = () => {
  const [blogs, setBlogs] = useState([]);
  const [editBlog, setEditBlog] = useState(null);
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [addModalOpen, setAddModalOpen] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");

  useEffect(() => {
    fetchGetBlogs();
  }, []);

  const fetchGetBlogs = async () => {
    const result = await getBlogs(); // Get all blogs
    setBlogs(result);
    console.log(result);
  };

  const handleSearchChange = (e) => {
    setSearchTerm(e.target.value);
  };

  const handleEditBlog = (blogId) => {
    const blogToEdit = blogs.find((blog) => blog.id === blogId);
    setEditBlog(blogToEdit);
    setEditModalOpen(true); // Open modal for blog edit
  };

  const handleAddCar = async () => {
    await addBlog(editBlog); // Update blog

    setEditBlog({}); // Clear the form fields

    setAddModalOpen(false);

    fetchGetBlogs(); // Refresh car list
    toast.success("Blog added successfully");
  };

  const handleUpdateUser = async () => {
    await updateBlog(editBlog.id, editBlog); // Update blog
    setEditModalOpen(false); // Close modal for blog edit
    fetchGetBlogs(); // Refresh blog list
    toast.success("Blog saved successfully");
  };

  const handleDeleteBlog = async (blogId) => {
    await deleteBlog(blogId);
    toast.success("Blog deleted successfully");
  };

  return (
    <div className="blogs-container">
      <h2>Blogs</h2>
      <div className="search-container">
        <Button color="success" onClick={() => setAddModalOpen(true)}>
          Add Blog
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
      <Table className="blogs-table">
        <thead>
          <tr>
            <th>Id</th>
            <th>Title</th>
            <th>Author Name</th>
            <th>Author Surname</th>
            <th>Description</th>
            <th>Date</th>
            <th>Image</th>
            <th>Detailed Description</th>
            <th>Edit</th>
            <th>Delete</th>
          </tr>
        </thead>
        <tbody>
          {blogs
            .filter(
              (blog) =>
                blog.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
                blog.authorName
                  .toLowerCase()
                  .includes(searchTerm.toLowerCase()) ||
                blog.authorSurname
                  .toLowerCase()
                  .includes(searchTerm.toLowerCase())
            )
            .map((blog) => (
              <tr key={blog.id}>
                <td>{blog.id}</td>
                <td>{blog.title}</td>
                <td>{blog.authorName}</td>
                <td>{blog.authorSurname}</td>
                <td>{blog.description}</td>
                <td>{blog.date}</td>
                <td>
                  <img src={blog.image} alt="" className="w-100" />
                </td>
                <td>{blog.detailedDescription}</td>
                <td>
                  <Button
                    color="primary"
                    className="edit-btn"
                    onClick={() => handleEditBlog(blog.id)}
                  >
                    Edit
                  </Button>
                </td>
                <td>
                  <Button
                    color="danger"
                    className="delete-btn"
                    onClick={() => handleDeleteBlog(blog.id)}
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
          Edit Blog
        </ModalHeader>
        <ModalBody>
          <Form>
            <FormGroup>
              <Label for="title">Title</Label>
              <Input
                type="text"
                name="title"
                id="title"
                value={editBlog?.title || ""}
                onChange={(e) =>
                  setEditBlog({ ...editBlog, title: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="authorName">Author Name</Label>
              <Input
                type="text"
                name="authorName"
                id="authorName"
                value={editBlog?.authorName || ""}
                onChange={(e) =>
                  setEditBlog({ ...editBlog, authorName: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="authorSurname">Author Surname</Label>
              <Input
                type="text"
                name="authorSurname"
                id="authorSurname"
                value={editBlog?.authorSurname || ""}
                onChange={(e) =>
                  setEditBlog({ ...editBlog, authorSurname: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="description">Description</Label>
              <textarea
                className="form-control"
                name="description"
                id="description"
                value={editBlog?.description || ""}
                onChange={(e) =>
                  setEditBlog({ ...editBlog, description: e.target.value })
                }
                rows={4}
              />
            </FormGroup>
            <FormGroup>
              <Label for="date">Date</Label>
              <Input
                type="datetime-local"
                name="date"
                id="date"
                value={editBlog?.date || ""}
                onChange={(e) =>
                  setEditBlog({ ...editBlog, date: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="image">Image</Label>
              <img src={editBlog?.image} alt="" className="w-100" />
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
                      setEditBlog({ ...editBlog, image: base64Image });
                    };
                    reader.readAsDataURL(file);
                  }
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="detailedDescription">Detailed Description</Label>
              <textarea
                className="form-control"
                name="detailedDescription"
                id="detailedDescription"
                value={editBlog?.detailedDescription || ""}
                onChange={(e) =>
                  setEditBlog({
                    ...editBlog,
                    detailedDescription: e.target.value,
                  })
                }
                rows={5} // How many rows
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
      <Modal
        isOpen={addModalOpen}
        toggle={() => setAddModalOpen(!addModalOpen)}
      >
        <ModalHeader toggle={() => setAddModalOpen(!addModalOpen)}>
          Add Blog
        </ModalHeader>
        <ModalBody>
          <Form>
            <FormGroup>
              <Label for="title">Title</Label>
              <Input
                type="text"
                name="title"
                id="title"
                value={editBlog?.title || ""}
                onChange={(e) =>
                  setEditBlog({ ...editBlog, title: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="authorName">Author Name</Label>
              <Input
                type="text"
                name="authorName"
                id="authorName"
                value={editBlog?.authorName || ""}
                onChange={(e) =>
                  setEditBlog({ ...editBlog, authorName: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="authorSurname">Author Surname</Label>
              <Input
                type="text"
                name="authorSurname"
                id="authorSurname"
                value={editBlog?.authorSurname || ""}
                onChange={(e) =>
                  setEditBlog({ ...editBlog, authorSurname: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="description">Description</Label>
              <textarea
                className="form-control"
                name="description"
                id="description"
                value={editBlog?.description || ""}
                onChange={(e) =>
                  setEditBlog({ ...editBlog, description: e.target.value })
                }
                rows={4}
              />
            </FormGroup>
            <FormGroup>
              <Label for="date">Date</Label>
              <Input
                type="datetime-local"
                name="date"
                id="date"
                value={editBlog?.date || ""}
                onChange={(e) =>
                  setEditBlog({ ...editBlog, date: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="image">Image</Label>
              <img src={editBlog?.image} alt="" className="w-100" />
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
                      setEditBlog({ ...editBlog, image: base64Image });
                    };
                    reader.readAsDataURL(file);
                  }
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="detailedDescription">Detailed Description</Label>
              <textarea
                className="form-control"
                name="detailedDescription"
                id="detailedDescription"
                value={editBlog?.detailedDescription || ""}
                onChange={(e) =>
                  setEditBlog({
                    ...editBlog,
                    detailedDescription: e.target.value,
                  })
                }
                rows={5} // How many rows
              />
            </FormGroup>
          </Form>
        </ModalBody>
        <ModalFooter>
          <Button color="primary" onClick={handleAddCar}>
            Add
          </Button>{" "}
          <Button
            color="secondary"
            onClick={() => {
              setAddModalOpen(!addModalOpen);
              setEditBlog({}); // Clear the form fields on cancel
            }}
          >
            Cancel
          </Button>
        </ModalFooter>
      </Modal>
    </div>
  );
};

export default Blogs;

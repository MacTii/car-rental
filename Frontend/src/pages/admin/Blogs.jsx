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
import { deleteComment, updateComment } from "../../services/commentService";
import CommentList from "../../components/UI/CommentList";

const Blogs = () => {
  const [blogs, setBlogs] = useState([]);
  const [editBlog, setEditBlog] = useState({
    comments: [],
  });
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [addModalOpen, setAddModalOpen] = useState(false);
  const [editComment, setEditComment] = useState({}); // New state for editing comments
  const [editCommentModalOpen, setEditCommentModalOpen] = useState(false); // Modal state for editing comments
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

  const handleEditComment = (commentId, blogId) => {
    const blogToEdit = blogs.find((blog) => blog.id === blogId);
    const commentToEdit = blogToEdit.comments.find(
      (comment) => comment.id === commentId
    );

    // Tutaj ustawiamy stan komentarza do edycji
    setEditComment({
      id: commentId,
      blogId: blogId,
      name: commentToEdit.name,
      surname: commentToEdit.surname,
      email: commentToEdit.email,
      date: commentToEdit.date,
      description: commentToEdit.description,
    });

    // Tutaj otwieramy modal do edycji komentarza
    setEditCommentModalOpen(true);
  };

  const handleAddCar = async () => {
    await addBlog(editBlog); // Update blog

    setEditBlog({}); // Clear the form fields

    setAddModalOpen(false);

    fetchGetBlogs(); // Refresh car list
    toast.success("Blog added successfully");
  };

  const handleEditCommentSubmit = async () => {
    // Convert the comment's date to local time before updating
    const localDate = new Date(editComment.date);
    localDate.setMinutes(
      localDate.getMinutes() - localDate.getTimezoneOffset()
    );

    // Format date before updating the comment
    const formattedDate = localDate
      .toISOString()
      .slice(0, 16)
      .replace("T", " ");

    // Update the comment with the local date
    const updatedComment = { ...editComment, date: formattedDate };
    await updateComment(editComment.id, updatedComment);

    setEditCommentModalOpen(false);
    fetchGetBlogs();
    setEditComment({});
    toast.success("Comment updated successfully");
  };

  const handleUpdateBlog = async () => {
    await updateBlog(editBlog.id, editBlog); // Update blog
    setEditModalOpen(false); // Close modal for blog edit
    fetchGetBlogs(); // Refresh blog list
    toast.success("Blog saved successfully");
  };

  const handleDeleteBlog = async (blogId) => {
    await deleteBlog(blogId);
    fetchGetBlogs();
    toast.success("Blog deleted successfully");
  };

  const handleDeleteComment = async (commentId) => {
    await deleteComment(commentId);
    fetchGetBlogs();
    toast.success("Comment deleted successfully");
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
              <React.Fragment key={blog.id}>
                <tr>
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
                {blog.comments.length > 0 && (
                  <tr>
                    <td colSpan="10">
                      <CommentList
                        comments={blog.comments}
                        handleEditComment={handleEditComment}
                        handleDeleteComment={handleDeleteComment}
                      />
                    </td>
                  </tr>
                )}
              </React.Fragment>
            ))}
        </tbody>
      </Table>

      <Modal
        isOpen={editModalOpen}
        toggle={() => setEditModalOpen(!editModalOpen)}
        className="edit-modal"
      >
        <ModalHeader toggle={() => setEditModalOpen(!editModalOpen)}>
          Edit Blog
        </ModalHeader>
        <ModalBody>
          <Form id="edit-blog-form" onSubmit={handleUpdateBlog}>
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
                required
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
                required
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
                required
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
                required
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
                required
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
                required
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
                required
              />
            </FormGroup>
          </Form>
        </ModalBody>
        <ModalFooter>
          <Button id="edit-blog-form" color="primary" type="submit">
            Save
          </Button>
          <Button
            color="secondary"
            onClick={() => {
              setEditModalOpen(!editModalOpen);
              setEditBlog({}); // Clear the form fields on cancel
            }}
          >
            Cancel
          </Button>
        </ModalFooter>
      </Modal>
      <Modal
        isOpen={addModalOpen}
        toggle={() => setAddModalOpen(!addModalOpen)}
        className="add-modal"
      >
        <ModalHeader toggle={() => setAddModalOpen(!addModalOpen)}>
          Add Blog
        </ModalHeader>
        <ModalBody>
          <Form id="add-blog-form" onSubmit={handleAddCar}>
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
                required
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
                required
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
                required
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
                required
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
                required
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
                required
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
                required
              />
            </FormGroup>
          </Form>
        </ModalBody>
        <ModalFooter>
          <Button form="add-blog-form" color="primary" type="submit">
            Add
          </Button>
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
      <Modal
        isOpen={editCommentModalOpen}
        toggle={() => setEditCommentModalOpen(!editCommentModalOpen)}
        className="edit-modal"
      >
        <ModalHeader
          toggle={() => setEditCommentModalOpen(!editCommentModalOpen)}
        >
          Edit Comment
        </ModalHeader>
        <ModalBody>
          <Form>
            <FormGroup>
              <Label for="name">Name</Label>
              <Input
                type="text"
                name="name"
                id="name"
                value={editComment?.name || ""}
                onChange={(e) =>
                  setEditComment({ ...editComment, name: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="surname">Surname</Label>
              <Input
                type="text"
                name="surname"
                id="surname"
                value={editComment?.surname || ""}
                onChange={(e) =>
                  setEditComment({ ...editComment, surname: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="email">Email</Label>
              <Input
                type="text"
                name="email"
                id="email"
                value={editComment?.email || ""}
                onChange={(e) =>
                  setEditComment({ ...editComment, email: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="date">Date</Label>
              <Input
                type="datetime-local"
                name="date"
                id="date"
                value={editComment?.date || ""}
                onChange={(e) =>
                  setEditComment({ ...editComment, date: e.target.value })
                }
              />
            </FormGroup>
            <FormGroup>
              <Label for="description">Description</Label>
              <textarea
                className="form-control"
                name="description"
                id="description"
                value={editComment?.description || ""}
                onChange={(e) =>
                  setEditComment({
                    ...editComment,
                    description: e.target.value,
                  })
                }
                rows={4}
              />
            </FormGroup>
          </Form>
        </ModalBody>
        <ModalFooter>
          <Button color="primary" onClick={handleEditCommentSubmit}>
            Save
          </Button>
          <Button
            color="secondary"
            onClick={() => setEditCommentModalOpen(!editCommentModalOpen)}
          >
            Cancel
          </Button>
        </ModalFooter>
      </Modal>
    </div>
  );
};

export default Blogs;

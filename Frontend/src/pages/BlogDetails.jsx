import React, { useEffect, useState } from "react";
import { Container, Row, Col, Form, FormGroup, Input } from "reactstrap";

import { useParams } from "react-router-dom";
import Helmet from "../components/Helmet/Helmet";
import { Link } from "react-router-dom";

import userImg from "../assets/all-images/user-img.png";

import "../styles/blog-details.css";
import { addComment } from "../services/commentService.js";
import { getBlogByTitle } from "../services/blogService.js";
import { getBlogs } from "../services/blogService";
import { getUserByUsername } from "../services/userService";
import { getUsernameFromToken } from "../services/tokenService";
import { toast } from "react-toastify";

const BlogDetails = () => {
  const { slug } = useParams();
  const [blog, setBlog] = useState();
  const [blogs, setBlogs] = useState([]);
  const [token] = useState(localStorage.getItem("token"));
  const [user, setUser] = useState();

  useEffect(() => {
    window.scrollTo(0, 0);
    fetchGetBlog();
    fetchGetBlogs();
    if (token) {
      fetchGetUser();
    }
  }, [slug]);

  const fetchGetBlog = async () => {
    const result = await getBlogByTitle(slug);
    setBlog(result);
  };

  const fetchGetBlogs = async () => {
    const result = await getBlogs();
    setBlogs(result);
  };

  const fetchGetUser = async () => {
    const username = getUsernameFromToken(); // Decode token and get username
    const user = await getUserByUsername(username); // Get user data by username
    setUser(user);
  };

  if (!blog) {
    return null; // HOW TO FIX THAT
  }

  const formattedDate = new Date(blog.date);
  const formattedTimeString = formattedDate.toLocaleTimeString();
  const formattedDateString = formattedDate.toDateString();

  const handleCommentSubmit = async (event) => {
    event.preventDefault();

    const name = event.target.elements.name.value;
    const surname = event.target.elements.surname.value;
    const email = event.target.elements.email.value;
    const comment = event.target.elements.comment.value;

    // Convert the comment's date to local time before updating
    const localDate = new Date();
    localDate.setMinutes(
      localDate.getMinutes() - localDate.getTimezoneOffset()
    );

    // Format date before updating the comment
    const formattedDate = localDate
      .toISOString()
      .slice(0, 16)
      .replace("T", " ");

    const newComment = {
      blogID: blog.id,
      name: name,
      surname: surname,
      email: email,
      date: formattedDate,
      description: comment,
    };

    console.log(newComment);

    await addComment(newComment); // add comment to db

    // Clear fields in form
    event.target.elements.name.value = "";
    event.target.elements.surname.value = "";
    event.target.elements.email.value = "";
    event.target.elements.comment.value = "";

    fetchGetBlog(); // refresh comments in blog
    toast.success("Comment posted successfully!");
  };

  return (
    <Helmet title={blog.title}>
      <section>
        <Container>
          <Row>
            <Col lg="8" md="8">
              <div className="blog__details">
                <img src={blog.image} alt="" className="w-100" />
                <h2 className="section__title mt-4">{blog.title}</h2>
                <div className="blog__publisher d-flex align-items-center gap-4 mb-4">
                  <span className="blog__author">
                    <i className="ri-user-line"></i> {blog.authorName}{" "}
                    {blog.authorSurname}
                  </span>

                  <span className="d-flex align-items-center gap-1 section__description">
                    <i className="ri-calendar-line"></i> {formattedDateString}
                  </span>

                  <span className="d-flex align-items-center gap-1 section__description">
                    <i className="ri-time-line"></i> {formattedTimeString}
                  </span>
                </div>

                <p className="section__description">
                  {blog.detailedDescription}
                </p>
                <p className="section__description">{blog.description}</p>
              </div>

              <div className="comment__list mt-5">
                <h4 className="mb-5">{blog.comments.length} Comments</h4>
                {blog.comments.map((comment) => (
                  <div
                    className="single__comment d-flex gap-3"
                    key={comment.id}
                  >
                    <img src={userImg} alt="" />
                    <div className="comment__content">
                      <h6 className="fw-bold">
                        {comment.name} {comment.surname}
                      </h6>
                      <p className="section__description mb-0">
                        {comment.date}
                      </p>
                      <p className="section__description">
                        {comment.description}
                      </p>
                    </div>
                  </div>
                ))}

                {/* === comment form === */}
                <div className="leave__comment-form mt-5">
                  <h4>Leave a Comment</h4>
                  {/* <p className="section__description">
                    You must sign-in to comment a post
                  </p> */}

                  <Form onSubmit={handleCommentSubmit}>
                    <FormGroup className="d-flex gap-3">
                      <Input
                        type="text"
                        name="name"
                        placeholder="Name"
                        value={user?.name}
                        disabled={user !== undefined}
                        required={!user}
                      />
                      <Input
                        type="text"
                        name="surname"
                        placeholder="Surname"
                        value={user?.surname}
                        disabled={user !== undefined}
                        required={!user}
                      />
                      <Input
                        type="email"
                        name="email"
                        placeholder="Email"
                        value={user?.email}
                        disabled={user !== undefined}
                        required={!user}
                      />
                    </FormGroup>

                    <FormGroup>
                      <textarea
                        rows="5"
                        className="form-control w-100 py-2 px-3"
                        placeholder="Comment ..."
                        name="comment"
                        required
                      ></textarea>
                    </FormGroup>

                    <button className="btn comment__btn mt-3" type="submit">
                      Post a Comment
                    </button>
                  </Form>
                </div>
              </div>
            </Col>

            <Col lg="4" md="4">
              <div className="recent__post mb-4">
                <h5 className="fw-bold">Recent Posts</h5>
              </div>
              {blogs.map((item) => (
                <div className="recent__blog-post mb-4" key={item.id}>
                  <div className="recent__blog-item d-flex gap-3">
                    <img src={item.image} alt="" className="w-25 rounded-2" />
                    <h6>
                      <Link to={`/blogs/${item.title}`}>{item.title}</Link>
                    </h6>
                  </div>
                </div>
              ))}
            </Col>
          </Row>
        </Container>
      </section>
    </Helmet>
  );
};

export default BlogDetails;

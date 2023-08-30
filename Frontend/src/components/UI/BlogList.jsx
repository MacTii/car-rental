import React, { useEffect, useState } from "react";
import { Col } from "reactstrap";
import "../../styles/blog-item.css";
import { Link } from "react-router-dom";
import { getBlogs } from "../../services/blogService";

const BlogList = () => {
  const [blogs, setBlogs] = useState([]);

  useEffect(() => {
    fetchGetBlogs();
  }, []);

  const fetchGetBlogs = async () => {
    const result = await getBlogs();
    setBlogs(result);
  };

  return (
    <>
      {blogs.map((item) => (
        <BlogItem item={item} key={item.id} />
      ))}
    </>
  );
};

const BlogItem = ({ item }) => {
  const { title, authorName, authorSurname, date, description, image } = item;

  const formattedDate = new Date(date);
  const formattedTimeString = formattedDate.toLocaleTimeString();
  const formattedDateString = formattedDate.toDateString();

  return (
    <Col lg="4" md="6" sm="6" className="mb-5">
      <div className="blog__item">
        <img src={image} alt="" className="w-100" />
        <div className="blog__info p-3">
          <Link to={`/blogs/${title}`} className="blog__title">
            {title}
          </Link>
          <p className="section__description mt-3">
            {description.length > 250
              ? description.substr(0, 250)
              : description}
          </p>

          <Link to={`/blogs/${title}`} className="read__more">
            Read More
          </Link>

          <div className="blog__time pt-3 mt-3 d-flex align-items-center justify-content-between">
            <span className="blog__author">
              <i className="ri-user-line"></i> {authorName} {authorSurname}
            </span>

            <div className="d-flex align-items-center gap-3">
              <span className="d-flex align-items-center gap-1 section__description">
                <i className="ri-calendar-line"></i> {formattedDateString}
              </span>

              <span className="d-flex align-items-center gap-1 section__description">
                <i className="ri-time-line"></i> {formattedTimeString}
              </span>
            </div>
          </div>
        </div>
      </div>
    </Col>
  );
};

export default BlogList;

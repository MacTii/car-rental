import React from "react";
import { Button } from "reactstrap";
import "../../styles/admin/comment.css"; // Importuj plik CSS dla komponentu Comment

const Comment = ({ comment, handleEditComment, handleDeleteComment }) => {
  return (
    <div className="comment-item">
      <table className="comment-table">
        <tbody>
          <tr>
            <td className="comment-label">
              <strong>Name:</strong>
            </td>
            <td className="comment-content">
              {comment.name} {comment.surname}
            </td>
          </tr>
          <tr>
            <td className="comment-label">
              <strong>Email:</strong>
            </td>
            <td className="comment-content">{comment.email}</td>
          </tr>
          <tr>
            <td className="comment-label">
              <strong>Date:</strong>
            </td>
            <td className="comment-content">{comment.date}</td>
          </tr>
          <tr>
            <td className="comment-label">
              <strong>Description:</strong>
            </td>
            <td className="comment-content">{comment.description}</td>
          </tr>
          <tr>
            <td colSpan="2" className="comment-actions">
              <Button
                color="danger"
                size="sm"
                className="comment-action-btn"
                onClick={() => {
                  handleDeleteComment(comment.id, comment.blogID);
                }}
              >
                Delete
              </Button>
              <Button
                color="primary"
                size="sm"
                className="comment-action-btn"
                onClick={() => {
                  handleEditComment(comment.id, comment.blogID);
                }}
              >
                Edit
              </Button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default Comment;

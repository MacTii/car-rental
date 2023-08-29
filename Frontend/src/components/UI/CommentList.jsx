import React, { useState } from "react";
import Comment from "./Comment";
import "../../styles/admin/comment-list.css"; // Zaimportuj plik CSS dla CommentList

const CommentList = ({ comments, handleEditComment, handleDeleteComment }) => {
  const [isExpanded, setIsExpanded] = useState(false);

  return (
    <div className="comment-list">
      <div
        className="comment-toggle"
        onClick={() => setIsExpanded(!isExpanded)}
      >
        {isExpanded ? "▼" : "►"} Comments
      </div>
      {isExpanded && (
        <ul>
          {comments.map((comment, index) => (
            <Comment
              key={index}
              comment={comment}
              handleEditComment={handleEditComment}
              handleDeleteComment={handleDeleteComment}
            />
          ))}
        </ul>
      )}
    </div>
  );
};

export default CommentList;

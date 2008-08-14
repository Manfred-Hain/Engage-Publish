using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Engage.Dnn.Publish.Data;
using Engage.Dnn.Publish.Util;
using System.Xml.Serialization;

namespace Engage.Dnn.Publish
{
    /// <summary>
    /// A comment on an item, possibly tied to a <see cref="Rating"/>.
    /// </summary>
    [XmlRootAttribute(ElementName = "Comment", IsNullable = false)]
    public class Comment : UserFeedback.Comment
    {
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="itemVersionId">The item version id.</param>
        /// <param name="itemVersionId">The item version id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="commentText">The comment text.</param>
        /// <param name="approvalStatusId">The approval status id.</param>
        /// <param name="createdDate">The created date.</param>
        /// <param name="lastUpdated">The last updated.</param>
        /// <param name="ratingId">This is not currently implemented. The id of the <see cref="Rating"/> associated with this <see cref="Comment"/>.</param>
        /// <param name="firstName">First name of the commenter.  Will be truncated if longer than <see cref="UserFeedback.Comment.NameSizeLimit"/>.</param>
        /// <param name="lastName">Last name of the commenter.  Will be truncated if longer than <see cref="UserFeedback.Comment.NameSizeLimit"/>.</param>
        /// <param name="emailAddress">Email address of the commenter.  Will be truncated if longer than <see cref="UserFeedback.Comment.EmailAddressSizeLimit"/>.</param>
        private Comment(int commentId, int itemVersionId, int? userId, string commentText, int approvalStatusId, DateTime createdDate, DateTime lastUpdated, int? ratingId, string firstName, string lastName, string emailAddress, string url)
            : base(commentId, itemVersionId, userId, commentText, approvalStatusId, createdDate, lastUpdated, ratingId, firstName,  lastName, emailAddress, url)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="itemVersionId">The item version id.</param>
        /// <param name="userId">The user id, or <c>null</c> is the user is unauthenticated.</param>
        /// <param name="commentText">The comment itself.</param>
        /// <param name="approvalStatusId">The approval status id.</param>
        /// <param name="ratingId">This is not currently implemented. The rating id, or <c>null</c> if the <see cref="Comment"/> is not associated with a <see cref="Rating"/>. This is not currently implemented.</param>
        /// <param name="firstName">First name of the commenter.  Will be truncated if longer than <see cref="UserFeedback.Comment.NameSizeLimit"/>.</param>
        /// <param name="lastName">Last name of the commenter.  Will be truncated if longer than <see cref="UserFeedback.Comment.NameSizeLimit"/>.</param>
        /// <param name="emailAddress">Email address of the commenter.  Will be truncated if longer than <see cref="UserFeedback.Comment.EmailAddressSizeLimit"/>.</param>
        public Comment(int itemVersionId, int? userId, string commentText, int approvalStatusId, int? ratingId, string firstName, string lastName, string emailAddress, string url)
            : base(itemVersionId, userId, commentText, approvalStatusId, ratingId, firstName, lastName, emailAddress, url)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        public Comment()
            : base()
        {
        }
        
        #endregion

        #region Static Data Methods
        /// <summary>
        /// Gets all comments for an <see cref="Item"/> of a particular <see cref="ApprovalStatus"/>.
        /// </summary>
        /// <param name="itemId">The item id.</param>
        /// <param name="approvalStatusId">The approval status id.</param>
        /// <returns>A <see cref="List{Comment}"/> filled with all Comments of the specified <see cref="ApprovalStatus"/> for the specified <see cref="Item"/></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Not intended to be a reusable library.")]
        public static List<UserFeedback.Comment> GetCommentsByItemId(int itemId, int approvalStatusId)
        {
            IDataReader dr = DataProvider.Instance().GetComments(itemId, approvalStatusId);
            List<UserFeedback.Comment> comments = new List<UserFeedback.Comment>();

            while (dr.Read())
            {
                comments.Add(FillComment(dr));
            }

            return comments;
        }

        /// <summary>
        /// Fills a <see cref="Comment"/> from an <see cref="IDataReader"/>.
        /// </summary>
        /// <param name="dr">An <see cref="IDataReader"/> filled with information needed to fill a <see cref="Comment"/>.</param>
        /// <returns>The instantiated <see cref="Comment"/>.</returns>
        private static UserFeedback.Comment FillComment(IDataRecord dr)
        {
            int? userId = null;
            int? ratingId = null;
            string firstName = null;
            string lastName = null;
            string emailAddress = null;
            string url = null;

            if (!dr.IsDBNull(dr.GetOrdinal("userId")))
            {
                userId = (int)dr["userId"];
            }
            if (!dr.IsDBNull(dr.GetOrdinal("ratingId")))
            {
                ratingId = (int)dr["ratingId"];
            }
            if (!dr.IsDBNull(dr.GetOrdinal("firstName")))
            {
                firstName = dr["firstName"].ToString();
            }
            if (!dr.IsDBNull(dr.GetOrdinal("lastName")))
            {
                lastName = dr["lastName"].ToString();
            }
            if (!dr.IsDBNull(dr.GetOrdinal("emailAddress")))
            {
                emailAddress = dr["emailAddress"].ToString();
            }

            if (!dr.IsDBNull(dr.GetOrdinal("url")))
            {
                url = dr["url"].ToString();
            }

            return new Comment((int)dr["commentId"], (int)dr["itemVersionId"], userId, (string)dr["commentText"], (int)dr["approvalStatusId"], Convert.ToDateTime(dr["createdDate"], CultureInfo.InvariantCulture), Convert.ToDateTime(dr["lastUpdated"], CultureInfo.InvariantCulture), ratingId, firstName, lastName, emailAddress, url);
        }
	    #endregion

        #region Instance Methods
        /// <summary>
        /// Deletes this instance.
        /// </summary>
		public void Delete()
        {
            Delete(DataProvider.ModuleQualifier);
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
           Save(DataProvider.ModuleQualifier);
        }
	    #endregion
    }
}
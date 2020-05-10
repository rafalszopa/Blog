using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Persistence
{
    public static class DatabaseSchema
    {
        public class WithId
        {
            public const string Id = "id";
        }

        public class Users : WithId
        {
            public const string TableName = "users";

            public const string FirstName = "first_name";

            public const string LastName = "last_name";

            public const string Bio = "bio";

            public const string Email = "email";

            public const string Phone = "phone";

            public const string PasswordHash = "password_hash";

            public const string Locked = "locked";
        }

        public class Roles : WithId
        {
            public const string TableName = "roles";

            public const string Name = "name";
        }

        public class Posts : WithId
        {
            public const string TableName = "posts";

            public const string Title = "title";

            public const string Description = "description";

            public const string Content = "content";

            public const string StatusId = "status_id";

            public const string AuthorId = "author_id";
        }


        public class PostStatus : WithId
        {
            public const string TableName = "post_status";

            public const string Status = "status";
        }

        public static class UserRoles
        {
            public const string TableName = "user_roles";

            public const string UserId = "user_id";

            public const string RoleId = "role_id";
        }
    }
}

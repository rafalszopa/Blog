using FluentMigrator;

namespace Blog.Persistence.Migrations
{
    [Migration(200)]
    public class InitialDataSeed : Migration
    {
        #region Public methods

        public override void Down()
        {
            Delete.Table("Users");
        }

        public override void Up()
        {
            InsertAdminUser();
            InsertPostStatus();
            InsertRoles();
            InsertUserRole();
        }

        #endregion Public methods
        #region Private methods

        private void InsertAdminUser()
        {
            Insert
                .IntoTable(DatabaseSchema.Users.TableName)
                .Row(new
                {
                    first_name = "John",
                    last_name = "Smith",
                    bio = "John is very experienced .NET developer.",
                    email = "contact@johnsmith.com",
                    phone = "+48 512 000 000",
                    password_hash = "AQAAABAnAAAQAAAAvYTt1XQGI++MGcxR48RCCyzL1HowYZDamc7BdeO/NgnAZdE4tXr26HZMoMcXGxKI", // Admin_123
                    locked = false
                });
        }

        private void InsertPostStatus()
        {
            Insert
                .IntoTable(DatabaseSchema.PostStatus.TableName)
                .Row(new { Status = "Archived" })
                .Row(new { Status = "Draft" })
                .Row(new { Status = "Published" })
                .Row(new { Status = "Scheduled" });
        }

        private void InsertRoles()
        {
            Insert
                .IntoTable(DatabaseSchema.Roles.TableName)
                .Row(new { name = "Administrator" })
                .Row(new { name = "Author" });
        }

        private void InsertUserRole()
        {
            Insert
                .IntoTable(DatabaseSchema.UserRoles.TableName)
                .Row(new { user_id = 1, role_id = 1 })
                .Row(new { user_id = 1, role_id = 2 });
        }

        #endregion Private methods
    }
}

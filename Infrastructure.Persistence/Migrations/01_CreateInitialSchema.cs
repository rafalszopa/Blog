using Blog.Persistence.Extensions;
using FluentMigrator;
using System;

namespace Blog.Persistence.Migrations
{
    [Migration(100)]
    public class CreateInitialSchema : Migration
    {
        #region Public methods

        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            CreateUserTable();
            CreateRoleTable();
            CreateUserRoleTable();
            CreatePostStatusTable();
            CreatePostTable();

            CreateForeignKeys();

            CreateConstraints();
        }

        #endregion Public methods
        #region Private methods

        private void CreatePostTable()
        {
            Create
                .Table(DatabaseSchema.Posts.TableName)
                .WithIdColumn()
                .WithColumn(DatabaseSchema.Posts.Title).AsString(255).Nullable()
                .WithColumn(DatabaseSchema.Posts.Description).AsString(500).Nullable()
                .WithColumn(DatabaseSchema.Posts.Content).AsString().Nullable()
                .WithColumn(DatabaseSchema.Posts.StatusId).AsInt64().NotNullable()
                .WithColumn(DatabaseSchema.Posts.AuthorId).AsInt64().NotNullable();
        }

        private void CreatePostStatusTable()
        {
            Create
                .Table(DatabaseSchema.PostStatus.TableName)
                .WithIdColumn()
                .WithColumn(DatabaseSchema.PostStatus.Status).AsString(50).NotNullable();
        }

        private void CreateRoleTable()
        {
            Create
                .Table(DatabaseSchema.Roles.TableName)
                .WithIdColumn()
                .WithColumn(DatabaseSchema.Roles.Name).AsString(50).NotNullable();
        }

        private void CreateUserTable()
        {
            Create
                .Table(DatabaseSchema.Users.TableName)
                .WithIdColumn()
                .WithColumn(DatabaseSchema.Users.FirstName).AsString(50).NotNullable()
                .WithColumn(DatabaseSchema.Users.LastName).AsString(50).NotNullable()
                .WithColumn(DatabaseSchema.Users.Bio).AsString(255).Nullable()
                .WithColumn(DatabaseSchema.Users.Email).AsString(50).NotNullable()
                .WithColumn(DatabaseSchema.Users.Phone).AsString(20).Nullable()
                .WithColumn(DatabaseSchema.Users.PasswordHash).AsString(100).NotNullable()
                .WithColumn(DatabaseSchema.Users.Locked).AsBoolean().NotNullable().WithDefaultValue(false);
        }

        private void CreateUserRoleTable()
        {
            Create
                .Table(DatabaseSchema.UserRoles.TableName)
                .WithColumn(DatabaseSchema.UserRoles.UserId).AsInt64().NotNullable()
                .WithColumn(DatabaseSchema.UserRoles.RoleId).AsInt64().NotNullable();
        }

        private void CreateForeignKeys()
        {
            Create.ForeignKey()
                .FromTable(DatabaseSchema.Posts.TableName).ForeignColumn(DatabaseSchema.Posts.StatusId)
                .ToTable(DatabaseSchema.PostStatus.TableName).PrimaryColumn(DatabaseSchema.PostStatus.Id);

            Create.ForeignKey()
                .FromTable(DatabaseSchema.Posts.TableName).ForeignColumn(DatabaseSchema.Posts.AuthorId)
                .ToTable(DatabaseSchema.Users.TableName).PrimaryColumn(DatabaseSchema.Users.Id);

            Create.ForeignKey()
                .FromTable(DatabaseSchema.UserRoles.TableName).ForeignColumn(DatabaseSchema.UserRoles.UserId)
                .ToTable(DatabaseSchema.Users.TableName).PrimaryColumn(DatabaseSchema.Users.Id);

            Create.ForeignKey()
                .FromTable(DatabaseSchema.UserRoles.TableName).ForeignColumn(DatabaseSchema.UserRoles.RoleId)
                .ToTable(DatabaseSchema.Roles.TableName).PrimaryColumn(DatabaseSchema.Roles.Id);
        }

        private void CreateConstraints()
        {
            Create.UniqueConstraint()
                .OnTable(DatabaseSchema.Users.TableName)
                .Column(DatabaseSchema.Users.Email);

            Create.UniqueConstraint()
                .OnTable(DatabaseSchema.PostStatus.TableName)
                .Column(DatabaseSchema.PostStatus.Status);

            Create.UniqueConstraint()
                .OnTable(DatabaseSchema.UserRoles.TableName)
                .Columns(DatabaseSchema.UserRoles.UserId, DatabaseSchema.UserRoles.RoleId);
        }

        #endregion Private methods
    }
}

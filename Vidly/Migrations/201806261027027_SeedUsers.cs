namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4891ad74-4fab-46ac-a27f-a27ce32478ac', N'admin@vidly.com', 0, N'AGUnNM8lpXgFVkas938cqVQ9g4fFe8Z27QrMN1uoS9+Vo2Ph83V3dMXmdb/9xGyFyw==', N'844df211-a32f-4fc6-b55d-422d2b03b7dc', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'95afd596-5a16-4ff5-b3ef-2a9677e289b4', N'guest@vidly.com', 0, N'AHpyZehlu5dV77uslIsp9XmYA176ilVewznzEEVtCQECYkTE8LU4cQcXsrpz/kZlmg==', N'19807ecd-3326-4822-a0cb-63db7d75e7f6', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'19cbd5a1-2187-4c16-bf1a-31161e520007', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4891ad74-4fab-46ac-a27f-a27ce32478ac', N'19cbd5a1-2187-4c16-bf1a-31161e520007')

");
        }

        public override void Down()
        {
        }
    }
}

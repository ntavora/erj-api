
using FluentMigrator;
using System;
using System.Globalization;
namespace erj_api.Migrations
{
    [Migration(202272400001)]
    public class InitialScript_202272400001 : Migration
    {
        public override void Down()
        {
            
            Delete.Table("Users");
            Delete.Table("Profile");
        }

        public override void Up()
        {

            Create.Table("Users")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("UserName").AsString(50).NotNullable()
                .WithColumn("Password").AsString(20).NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable()
                .WithColumn("CreatedDate").AsDate().WithDefaultValue(DateTime.Now)
                .WithColumn("ModifiedDate").AsDate().WithDefaultValue(DateTime.Now);

            Create.Table("Profile")
               .WithColumn("ProfileId").AsGuid().NotNullable().PrimaryKey()
               .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("FK_Profile_Users_UserId", "Users", "Id")
               .WithColumn("Orden").AsInt32().NotNullable()
               .WithColumn("FirstName").AsString(50).NotNullable()
               .WithColumn("LastName").AsString(50).NotNullable()
               .WithColumn("Dni").AsString(20).NotNullable()
               .WithColumn("Address").AsString(100).NotNullable()
               .WithColumn("Phone").AsString(50).NotNullable()
               .WithColumn("BirthdayDate").AsDate().NotNullable()
               .WithColumn("MaritalStatus").AsString(1).NotNullable()
               .WithColumn("ChurchName").AsString(50).NotNullable()
               .WithColumn("BelieverTime").AsString(50).NotNullable()
               .WithColumn("LetterOfRecommendation").AsBoolean().WithDefaultValue(false)
               .WithColumn("BriefTestimony").AsString(500).NotNullable()
               .WithColumn("IsActiveMember").AsBoolean().WithDefaultValue(true)
               .WithColumn("IsBaptized").AsBoolean().WithDefaultValue(true)
               .WithColumn("CreatedDate").AsDate().Nullable()
               .WithColumn("ModifiedDate").AsDate().Nullable();

        }
    }
}

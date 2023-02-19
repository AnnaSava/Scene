﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SavaDev.Users.Data;

#nullable disable

namespace Scene.Migrations.PostgreSql.Users
{
    [DbContext(typeof(UsersContext))]
    [Migration("20230219075127_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SavaDev.Base.User.Data.Entities.AuthToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AuthJti")
                        .HasColumnType("text")
                        .HasColumnName("auth_jti");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_updated");

                    b.Property<string>("RefreshJti")
                        .HasColumnType("text")
                        .HasColumnName("refresh_jti");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_app_auth_tokens");

                    b.HasIndex("AuthJti")
                        .IsUnique()
                        .HasDatabaseName("ix_app_auth_tokens_auth_jti");

                    b.HasIndex("RefreshJti")
                        .IsUnique()
                        .HasDatabaseName("ix_app_auth_tokens_refresh_jti");

                    b.ToTable("app_auth_tokens", (string)null);
                });

            modelBuilder.Entity("SavaDev.Base.User.Data.Entities.Lockout", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("LockDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lock_date");

                    b.Property<long>("LockedByUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("locked_by_user_id");

                    b.Property<DateTime?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("Reason")
                        .HasColumnType("text")
                        .HasColumnName("reason");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_app_lockouts");

                    b.ToTable("app_lockouts", (string)null);
                });

            modelBuilder.Entity("SavaDev.Base.User.Data.Entities.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_app_role_claims");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_app_role_claims_role_id");

                    b.ToTable("app_role_claims", (string)null);
                });

            modelBuilder.Entity("SavaDev.Base.User.Data.Entities.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_app_user_claims");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_app_user_claims_user_id");

                    b.ToTable("app_user_claims", (string)null);
                });

            modelBuilder.Entity("SavaDev.Base.User.Data.Entities.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.HasKey("LoginProvider", "ProviderKey", "UserId")
                        .HasName("pk_app_user_logins");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_app_user_logins_user_id");

                    b.ToTable("app_user_logins", (string)null);
                });

            modelBuilder.Entity("SavaDev.Base.User.Data.Entities.UserRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_app_user_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_app_user_roles_role_id");

                    b.ToTable("app_user_roles", (string)null);
                });

            modelBuilder.Entity("SavaDev.Base.User.Data.Entities.UserToken", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("LoginProvider", "UserId")
                        .HasName("pk_app_user_tokens");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_app_user_tokens_user_id");

                    b.ToTable("app_user_tokens", (string)null);
                });

            modelBuilder.Entity("SavaDev.System.Data.Entities.Parts.PermissionCulture", b =>
                {
                    b.Property<string>("PermissionName")
                        .HasColumnType("text")
                        .HasColumnName("permission_name");

                    b.Property<string>("Culture")
                        .HasColumnType("text")
                        .HasColumnName("culture");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("PermissionName", "Culture")
                        .HasName("pk_permission_culture");

                    b.ToTable("permission_culture", (string)null);
                });

            modelBuilder.Entity("SavaDev.System.Data.Entities.Permission", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Name")
                        .HasName("pk_permission");

                    b.ToTable("permission", (string)null);
                });

            modelBuilder.Entity("SavaDev.Users.Data.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id")
                        .HasName("pk_app_roles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("app_roles", (string)null);
                });

            modelBuilder.Entity("SavaDev.Users.Data.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("display_name");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("middle_name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<DateTime>("RegDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("reg_date");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_app_users");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("app_users", (string)null);
                });

            modelBuilder.Entity("SavaDev.Base.User.Data.Entities.RoleClaim", b =>
                {
                    b.HasOne("SavaDev.Users.Data.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_app_role_claims_app_roles_role_id");
                });

            modelBuilder.Entity("SavaDev.Base.User.Data.Entities.UserClaim", b =>
                {
                    b.HasOne("SavaDev.Users.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_app_user_claims_app_users_user_id");
                });

            modelBuilder.Entity("SavaDev.Base.User.Data.Entities.UserLogin", b =>
                {
                    b.HasOne("SavaDev.Users.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_app_user_logins_app_users_user_id");
                });

            modelBuilder.Entity("SavaDev.Base.User.Data.Entities.UserRole", b =>
                {
                    b.HasOne("SavaDev.Users.Data.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_app_user_roles_app_roles_role_id");

                    b.HasOne("SavaDev.Users.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_app_user_roles_app_users_user_id");
                });

            modelBuilder.Entity("SavaDev.Base.User.Data.Entities.UserToken", b =>
                {
                    b.HasOne("SavaDev.Users.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_app_user_tokens_app_users_user_id");
                });

            modelBuilder.Entity("SavaDev.System.Data.Entities.Parts.PermissionCulture", b =>
                {
                    b.HasOne("SavaDev.System.Data.Entities.Permission", "Permission")
                        .WithMany("Cultures")
                        .HasForeignKey("PermissionName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_permission_culture_permission_permission_name");

                    b.Navigation("Permission");
                });

            modelBuilder.Entity("SavaDev.System.Data.Entities.Permission", b =>
                {
                    b.Navigation("Cultures");
                });
#pragma warning restore 612, 618
        }
    }
}
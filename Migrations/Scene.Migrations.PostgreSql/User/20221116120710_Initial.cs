using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Scene.Migrations.PostgreSql.User
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ap_auth_tokens",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    auth_jti = table.Column<string>(type: "text", nullable: true),
                    refresh_jti = table.Column<string>(type: "text", nullable: true),
                    date_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ap_auth_tokens", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ap_legal_documents",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    perm_name = table.Column<string>(type: "text", nullable: true),
                    culture = table.Column<string>(type: "text", nullable: true),
                    title = table.Column<string>(type: "text", nullable: true),
                    info = table.Column<string>(type: "text", nullable: true),
                    text = table.Column<string>(type: "text", nullable: true),
                    comment = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    last_updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ap_legal_documents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ap_lockouts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    locked_by_user_id = table.Column<long>(type: "bigint", nullable: false),
                    lock_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    reason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ap_lockouts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ap_permissions",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ap_permissions", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "ap_reserved_names",
                columns: table => new
                {
                    text = table.Column<string>(type: "text", nullable: false),
                    include_plural = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ap_reserved_names", x => x.text);
                });

            migrationBuilder.CreateTable(
                name: "ap_roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    last_updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ap_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ap_users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false),
                    reg_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ap_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ap_permission_cultures",
                columns: table => new
                {
                    permission_name = table.Column<string>(type: "text", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ap_permission_cultures", x => new { x.permission_name, x.culture });
                    table.ForeignKey(
                        name: "fk_ap_permission_cultures_ap_permissions_permission_name",
                        column: x => x.permission_name,
                        principalTable: "ap_permissions",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ap_role_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ap_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_ap_role_claims_ap_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "ap_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ap_user_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ap_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_ap_user_claims_ap_users_user_id",
                        column: x => x.user_id,
                        principalTable: "ap_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ap_user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ap_user_logins", x => new { x.login_provider, x.provider_key, x.user_id });
                    table.ForeignKey(
                        name: "fk_ap_user_logins_ap_users_user_id",
                        column: x => x.user_id,
                        principalTable: "ap_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ap_user_roles",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ap_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_ap_user_roles_ap_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "ap_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ap_user_roles_ap_users_user_id",
                        column: x => x.user_id,
                        principalTable: "ap_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ap_user_tokens",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ap_user_tokens", x => new { x.login_provider, x.user_id });
                    table.ForeignKey(
                        name: "fk_ap_user_tokens_ap_users_user_id",
                        column: x => x.user_id,
                        principalTable: "ap_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_ap_auth_tokens_auth_jti",
                table: "ap_auth_tokens",
                column: "auth_jti",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ap_auth_tokens_refresh_jti",
                table: "ap_auth_tokens",
                column: "refresh_jti",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ap_role_claims_role_id",
                table: "ap_role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "ap_roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ap_user_claims_user_id",
                table: "ap_user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_ap_user_logins_user_id",
                table: "ap_user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_ap_user_roles_role_id",
                table: "ap_user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_ap_user_tokens_user_id",
                table: "ap_user_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "ap_users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "ap_users",
                column: "normalized_user_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ap_auth_tokens");

            migrationBuilder.DropTable(
                name: "ap_legal_documents");

            migrationBuilder.DropTable(
                name: "ap_lockouts");

            migrationBuilder.DropTable(
                name: "ap_permission_cultures");

            migrationBuilder.DropTable(
                name: "ap_reserved_names");

            migrationBuilder.DropTable(
                name: "ap_role_claims");

            migrationBuilder.DropTable(
                name: "ap_user_claims");

            migrationBuilder.DropTable(
                name: "ap_user_logins");

            migrationBuilder.DropTable(
                name: "ap_user_roles");

            migrationBuilder.DropTable(
                name: "ap_user_tokens");

            migrationBuilder.DropTable(
                name: "ap_permissions");

            migrationBuilder.DropTable(
                name: "ap_roles");

            migrationBuilder.DropTable(
                name: "ap_users");
        }
    }
}

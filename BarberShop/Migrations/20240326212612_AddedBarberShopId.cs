using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BarberShop.Migrations
{
    /// <inheritdoc />
    public partial class AddedBarberShopId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
   
            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "Testimonials",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "TeamSections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "StatsSections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "StatItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "NewsletterSubscribers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "Navbars",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "NavbarActions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "HeroSections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "GalleryItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "FormFields",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "FooterSections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "FAQs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "CtaSections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "ContactInquiries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "ContactFormSections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "BlogPosts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "BenefitSections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "BarberServices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "Barbers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BarberShopId",
                table: "AboutSections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
   

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "Testimonials");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "TeamSections");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "StatsSections");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "StatItems");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "NewsletterSubscribers");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "Navbars");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "NavbarActions");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "HeroSections");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "GalleryItems");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "FormFields");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "FooterSections");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "FAQs");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "CtaSections");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "ContactInquiries");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "ContactFormSections");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "BenefitSections");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "BarberServices");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "Barbers");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "BarberShopId",
                table: "AboutSections");

     
        }
    }
}

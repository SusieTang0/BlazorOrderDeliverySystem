﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderDeliverySystem.Share.Data;

#nullable disable

namespace OrderDeliverySystem.Share.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241003215634_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.AddressModel", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Postcode")
                        .HasColumnType("TEXT");

                    b.Property<string>("Province")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.Property<string>("Unit")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AddressId");

                    b.HasIndex("UserId", "Type")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CartId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CartId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("CartItemId");

                    b.HasIndex("CartId");

                    b.HasIndex("ItemId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CustomerId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.DeliveryTask", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("AssignedTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CompletedTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("WorkerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TaskId");

                    b.HasIndex("OrderId");

                    b.HasIndex("WorkerId");

                    b.ToTable("DeliveryTasks", t =>
                        {
                            t.HasCheckConstraint("CK_DeliveryTask_Status", "Status IN ('Assigned', 'Completed', 'Failed')");
                        });
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.DeliveryWorker", b =>
                {
                    b.Property<int>("WorkerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("CommissionRate")
                        .HasPrecision(5, 2)
                        .HasColumnType("TEXT");

                    b.Property<int>("IsAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastTaskAssigned")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("WorkerId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("DeliveryWorkers");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IsAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemPic")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ItemPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("TEXT");

                    b.Property<int>("MerchantId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ItemId");

                    b.HasIndex("MerchantId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Merchant", b =>
                {
                    b.Property<int>("MerchantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BusinessName")
                        .HasColumnType("TEXT");

                    b.Property<string>("MerchantDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("MerchantPic")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PreparingTime")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MerchantId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Merchants");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DeliveryWorkerWorkerId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DropoffAddressId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MerchantId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PickupAddressId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(10, 2)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("WorkerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DeliveryWorkerWorkerId");

                    b.HasIndex("DropoffAddressId");

                    b.HasIndex("MerchantId");

                    b.HasIndex("PickupAddressId");

                    b.ToTable("Orders", t =>
                        {
                            t.HasCheckConstraint("CK_Order_Status", "Status IN ('Pending', 'Approved', 'In Delivery', 'Delivered', 'Cancelled')");
                        });
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("PriceAtOrder")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("OrderItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Reply")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ReplyCreatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("ReviewId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Reviews", t =>
                        {
                            t.HasCheckConstraint("CK_Reviews_Rating", "Rating BETWEEN 1 AND 5");
                        });
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1L)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<int>("IsActived")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1);

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.WorkerLocation", b =>
                {
                    b.Property<int>("WorkerLocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Latitude")
                        .HasColumnType("decimal(11, 8)");

                    b.Property<decimal?>("Longitude")
                        .HasColumnType("decimal(11, 8)");

                    b.Property<int>("WorkerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("WorkerLocationId");

                    b.HasIndex("WorkerId")
                        .IsUnique();

                    b.ToTable("WorkerLocations");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.AddressModel", b =>
                {
                    b.HasOne("OrderDeliverySystem.Share.Data.Models.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Cart", b =>
                {
                    b.HasOne("OrderDeliverySystem.Share.Data.Models.Customer", "Customer")
                        .WithMany("Carts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.CartItem", b =>
                {
                    b.HasOne("OrderDeliverySystem.Share.Data.Models.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderDeliverySystem.Share.Data.Models.Item", "Item")
                        .WithMany("CartItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Customer", b =>
                {
                    b.HasOne("OrderDeliverySystem.Share.Data.Models.User", "User")
                        .WithOne("Customer")
                        .HasForeignKey("OrderDeliverySystem.Share.Data.Models.Customer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.DeliveryTask", b =>
                {
                    b.HasOne("OrderDeliverySystem.Share.Data.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderDeliverySystem.Share.Data.Models.DeliveryWorker", "DeliveryWorker")
                        .WithMany("DeliveryTasks")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliveryWorker");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.DeliveryWorker", b =>
                {
                    b.HasOne("OrderDeliverySystem.Share.Data.Models.User", "User")
                        .WithOne("DeliveryWorker")
                        .HasForeignKey("OrderDeliverySystem.Share.Data.Models.DeliveryWorker", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Item", b =>
                {
                    b.HasOne("OrderDeliverySystem.Share.Data.Models.Merchant", "Merchant")
                        .WithMany("Items")
                        .HasForeignKey("MerchantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Merchant");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Merchant", b =>
                {
                    b.HasOne("OrderDeliverySystem.Share.Data.Models.User", "User")
                        .WithOne("Merchant")
                        .HasForeignKey("OrderDeliverySystem.Share.Data.Models.Merchant", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Order", b =>
                {
                    b.HasOne("OrderDeliverySystem.Share.Data.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OrderDeliverySystem.Share.Data.Models.DeliveryWorker", "DeliveryWorker")
                        .WithMany()
                        .HasForeignKey("DeliveryWorkerWorkerId");

                    b.HasOne("OrderDeliverySystem.Share.Data.Models.AddressModel", "DropoffAddress")
                        .WithMany()
                        .HasForeignKey("DropoffAddressId");

                    b.HasOne("OrderDeliverySystem.Share.Data.Models.Merchant", "Merchant")
                        .WithMany("Orders")
                        .HasForeignKey("MerchantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OrderDeliverySystem.Share.Data.Models.AddressModel", "PickupAddress")
                        .WithMany()
                        .HasForeignKey("PickupAddressId");

                    b.Navigation("Customer");

                    b.Navigation("DeliveryWorker");

                    b.Navigation("DropoffAddress");

                    b.Navigation("Merchant");

                    b.Navigation("PickupAddress");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.OrderItem", b =>
                {
                    b.HasOne("OrderDeliverySystem.Share.Data.Models.Item", "Item")
                        .WithMany("OrderItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OrderDeliverySystem.Share.Data.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Review", b =>
                {
                    b.HasOne("OrderDeliverySystem.Share.Data.Models.Customer", "Customer")
                        .WithMany("Reviews")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OrderDeliverySystem.Share.Data.Models.Order", "Order")
                        .WithOne("Reviews")
                        .HasForeignKey("OrderDeliverySystem.Share.Data.Models.Review", "OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.WorkerLocation", b =>
                {
                    b.HasOne("OrderDeliverySystem.Share.Data.Models.DeliveryWorker", "DeliveryWorker")
                        .WithOne("WorkerLocation")
                        .HasForeignKey("OrderDeliverySystem.Share.Data.Models.WorkerLocation", "WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliveryWorker");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Customer", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Orders");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.DeliveryWorker", b =>
                {
                    b.Navigation("DeliveryTasks");

                    b.Navigation("WorkerLocation");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Item", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Merchant", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.Order", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("OrderDeliverySystem.Share.Data.Models.User", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Customer");

                    b.Navigation("DeliveryWorker");

                    b.Navigation("Merchant");
                });
#pragma warning restore 612, 618
        }
    }
}
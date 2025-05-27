using System;
using System.Collections.Generic;
using BeYou.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BeYou.Infraestructure.Data;

public partial class BeYouContext(DbContextOptions<BeYouContext> options) : DbContext  (options)
{

    const string CREATEDNAME = "Created";
    const string UPDATEDNAME = "Updated";

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<BranchHoliday> BranchHolidays { get; set; }

    public virtual DbSet<BranchSchedule> BranchSchedules { get; set; }

    public virtual DbSet<BranchScheduleBlock> BranchScheduleBlocks { get; set; }

    public virtual DbSet<Canton> Cantons { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Holiday> Holidays { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<InventoryProduct> InventoryProducts { get; set; }

    public virtual DbSet<InventoryProductTransaction> InventoryProductTransactions { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<InvoiceDetailProduct> InvoiceDetailProducts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderDetailProduct> OrderDetailProducts { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<ReservationDetail> ReservationDetails { get; set; }

    public virtual DbSet<ReservationQuestion> ReservationQuestions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<StatusOrder> StatusOrders { get; set; }

    public virtual DbSet<Tax> Taxes { get; set; }

    public virtual DbSet<TokenMaster> TokenMasters { get; set; }

    public virtual DbSet<TypeService> TypeServices { get; set; }

    public virtual DbSet<UnitMeasure> UnitMeasures { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBranch> UserBranches { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.ToTable("Branch");

            entity.HasIndex(e => e.DistrictId, "IX_Branch_DistrictId");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(80);
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);

            entity.HasOne(d => d.District).WithMany(p => p.Branches)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Branch_District");
        });

        modelBuilder.Entity<BranchHoliday>(entity =>
        {
            entity.ToTable("BranchHoliday");

            entity.HasIndex(e => e.BranchId, "IX_BranchHoliday_BranchId");

            entity.HasIndex(e => e.HolidayId, "IX_BranchHoliday_HolidayId");

            entity.HasOne(d => d.Branch).WithMany(p => p.BranchHolidays)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchHoliday_Sucursal");

            entity.HasOne(d => d.Holiday).WithMany(p => p.BranchHolidays)
                .HasForeignKey(d => d.HolidayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchHoliday_Holiday");
        });

        modelBuilder.Entity<BranchSchedule>(entity =>
        {
            entity.ToTable("BranchSchedule");

            entity.HasIndex(e => e.BranchId, "IX_BranchSchedule_BranchId");

            entity.HasIndex(e => e.ScheduleId, "IX_BranchSchedule_ScheduleId");

            entity.HasOne(d => d.Branch).WithMany(p => p.BranchSchedules)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchSchedule_Branch");

            entity.HasOne(d => d.Schedule).WithMany(p => p.BranchSchedules)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchSchedule_Schedule");
        });

        modelBuilder.Entity<BranchScheduleBlock>(entity =>
        {
            entity.ToTable("BranchScheduleBlock");

            entity.HasIndex(e => e.BranchScheduleId, "IX_BranchScheduleBlock_BranchSchedule");

            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.BranchSchedule).WithMany(p => p.BranchScheduleBlocks)
                .HasForeignKey(d => d.BranchScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchScheduleBlock_BranchSchedule");
        });

        modelBuilder.Entity<Canton>(entity =>
        {
            entity.ToTable("Canton");

            entity.HasIndex(e => e.ProvinceId, "IX_Canton_ProvinceId");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Province).WithMany(p => p.Cantons)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Canton_Province");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("Contact");

            entity.HasIndex(e => e.VendorId, "IX_Contact_VendorId");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(80);
            entity.Property(e => e.LastName).HasMaxLength(80);
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);

            entity.HasOne(d => d.Vendor).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_Vendor");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.HasIndex(e => e.DistrictId, "IX_Customer_DistrictId");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(80);
            entity.Property(e => e.LastName).HasMaxLength(80);
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);

            entity.HasOne(d => d.District).WithMany(p => p.Customers)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_District");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.ToTable("District");

            entity.HasIndex(e => e.CantonId, "IX_District_CantonId");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Canton).WithMany(p => p.Districts)
                .HasForeignKey(d => d.CantonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_District_Canton");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("Gender");

            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<Holiday>(entity =>
        {
            entity.ToTable("Holiday");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.Month).HasMaxLength(25);
            entity.Property(e => e.Name).HasMaxLength(80);
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToTable("Inventory");

            entity.HasIndex(e => e.BranchId, "IX_Inventory_BranchId");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);

            entity.HasOne(d => d.Branch).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_Branch");
        });

        modelBuilder.Entity<InventoryProduct>(entity =>
        {
            entity.ToTable("InventoryProduct");

            entity.HasIndex(e => e.IdProducto, "IX_InventoryProduct_IdProducto");

            entity.HasIndex(e => e.InventoryId, "IX_InventoryProduct_InventoryId");

            entity.HasIndex(e => e.ProductId, "IX_InventoryProduct_ProductId");

            entity.Property(e => e.Assignable).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.Maximum).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Mininum).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.InventoryProducts)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryProduct_Product");

            entity.HasOne(d => d.Inventory).WithMany(p => p.InventoryProducts)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryProduct_Inventory");
        });

        modelBuilder.Entity<InventoryProductTransaction>(entity =>
        {
            entity.ToTable("InventoryProductTransaction");

            entity.HasIndex(e => e.InventoryProductId, "IX_InventoryProductTransaction_InventoryProductId");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);

            entity.HasOne(d => d.InventoryProduct).WithMany(p => p.InventoryProductTransactions)
                .HasForeignKey(d => d.InventoryProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryProductTransaction_InventoryProduct");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("Invoice");

            entity.HasIndex(e => e.BranchId, "IX_Invoice_BranchId");

            entity.HasIndex(e => e.CustomerId, "IX_Invoice_CustomerId");

            entity.HasIndex(e => e.OrderId, "IX_Invoice_OrderId");

            entity.HasIndex(e => e.PaymentTypeId, "IX_Invoice_PaymentTypeId");

            entity.HasIndex(e => e.TaxId, "IX_Invoice_TaxId");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.CustomerName).HasMaxLength(160);
            entity.Property(e => e.SubTotal).HasColumnType("money");
            entity.Property(e => e.Tax).HasColumnType("money");
            entity.Property(e => e.TaxRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Total).HasColumnType("money");
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);

            entity.HasOne(d => d.Branch).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Branch");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Customer");

            entity.HasOne(d => d.Order).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Invoice_Order");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_PaymentType");

            entity.HasOne(d => d.TaxNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.TaxId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Tax");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.ToTable("InvoiceDetail");

            entity.HasIndex(e => e.InvoiceId, "IX_InvoiceDetail_InvoiceId");

            entity.HasIndex(e => e.ProductId, "IX_InvoiceDetail_ProductId");

            entity.HasIndex(e => e.ServiceId, "IX_InvoiceDetail_ServiceId");

            entity.Property(e => e.SubTotal).HasColumnType("money");
            entity.Property(e => e.Tax).HasColumnType("money");
            entity.Property(e => e.Total).HasColumnType("money");
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetail_Invoice");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_InvoiceDetail_Product");

            entity.HasOne(d => d.Service).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_InvoiceDetail_Service");
        });

        modelBuilder.Entity<InvoiceDetailProduct>(entity =>
        {
            entity.ToTable("InvoiceDetailProduct");

            entity.HasIndex(e => e.InvoiceDetailId, "IX_InvoiceDetailProduct_InvoiceDetailId");

            entity.HasIndex(e => e.ProductId, "IX_InvoiceDetailProduct_ProductId");

            entity.Property(e => e.Quantity).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.InvoiceDetail).WithMany(p => p.InvoiceDetailProducts)
                .HasForeignKey(d => d.InvoiceDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetailProduct_InvoiceDetail");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetailProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetailProduct_Product");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.HasIndex(e => e.BranchId, "IX_Order_BranchId");

            entity.HasIndex(e => e.CustomerId, "IX_Order_CustomerId");

            entity.HasIndex(e => e.PaymentTypeId, "IX_Order_PaymentTypeId");

            entity.HasIndex(e => e.ReservationId, "IX_Order_ReservationId");

            entity.HasIndex(e => e.StatusOrderId, "IX_Order_StatusOrderId");

            entity.HasIndex(e => e.TaxId, "IX_Order_TaxId");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.CustomerName).HasMaxLength(160);
            entity.Property(e => e.SubTotal).HasColumnType("money");
            entity.Property(e => e.Tax).HasColumnType("money");
            entity.Property(e => e.TaxRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Total).HasColumnType("money");
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);

            entity.HasOne(d => d.Branch).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Branch");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Customer");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_PaymentType");

            entity.HasOne(d => d.Reservation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Reservation");

            entity.HasOne(d => d.StatusOrder).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_StatusOrder");

            entity.HasOne(d => d.TaxNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TaxId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Tax");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("OrderDetail");

            entity.HasIndex(e => e.OrderId, "IX_OrderDetail_OrderId");

            entity.HasIndex(e => e.ProductId, "IX_OrderDetail_ProductId");

            entity.HasIndex(e => e.ServiceId, "IX_OrderDetail_ServiceId");

            entity.Property(e => e.SubTotal).HasColumnType("money");
            entity.Property(e => e.Tax).HasColumnType("money");
            entity.Property(e => e.Total).HasColumnType("money");
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrderDetail_Product");

            entity.HasOne(d => d.Service).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_OrderDetail_Service");
        });

        modelBuilder.Entity<OrderDetailProduct>(entity =>
        {
            entity.ToTable("OrderDetailProduct");

            entity.HasIndex(e => e.OrderDetailId, "IX_OrderDetailProduct_OrderDetailId");

            entity.HasIndex(e => e.ProductId, "IX_OrderDetailProduct_ProductId");

            entity.Property(e => e.Quantity).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.OrderDetail).WithMany(p => p.OrderDetailProducts)
                .HasForeignKey(d => d.OrderDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetailProduct_OrderDetail");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetailProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetailProduct_Product");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.ToTable("PaymentType");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.ReferenceNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.CategoryId, "IX_Producto_CategoryId");

            entity.HasIndex(e => e.UnitMeasureId, "IX_Producto_UnitMeasureId");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Brand).HasMaxLength(50);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(70);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .HasColumnName("SKU");
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category");

            entity.HasOne(d => d.UnitMeasure).WithMany(p => p.Products)
                .HasForeignKey(d => d.UnitMeasureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_UnitMeasure");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.ToTable("Province");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("Reservation");

            entity.HasIndex(e => e.BranchId, "IX_Reservation_BranchId");

            entity.HasIndex(e => e.CustomerId, "IX_Reservation_CustomerId");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.CustomerName).HasMaxLength(80);
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("P")
                .IsFixedLength();
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);

            entity.HasOne(d => d.Branch).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_Branch");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_Customer");
        });

        modelBuilder.Entity<ReservationDetail>(entity =>
        {
            entity.ToTable("ReservationDetail");

            entity.HasIndex(e => e.ProductId, "IX_ReservationDetail_ProductId");

            entity.HasIndex(e => e.ReservationId, "IX_ReservationDetail_ReservationId");

            entity.HasIndex(e => e.ServiceId, "IX_ReservationDetail_ServiceId");

            entity.HasOne(d => d.Product).WithMany(p => p.ReservationDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ReservationDetail_Product");

            entity.HasOne(d => d.Reservation).WithMany(p => p.ReservationDetails)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReservationDetail_Reservation");

            entity.HasOne(d => d.Service).WithMany(p => p.ReservationDetails)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_ReservationDetail_Service");
        });

        modelBuilder.Entity<ReservationQuestion>(entity =>
        {
            entity.ToTable("ReservationQuestion");

            entity.HasIndex(e => e.ReservationId, "IX_ReservationQuestion_ReservationId");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Answer).HasMaxLength(250);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.Question).HasMaxLength(250);
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);

            entity.HasOne(d => d.Reservation).WithMany(p => p.ReservationQuestions)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReservationQuestion_Reservation");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedule");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");

            entity.HasIndex(e => e.TypeServiceId, "IX_Service_TypeServiceId");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Observation).HasMaxLength(250);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);

            entity.HasOne(d => d.TypeService).WithMany(p => p.Services)
                .HasForeignKey(d => d.TypeServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_TypeService");
        });

        modelBuilder.Entity<StatusOrder>(entity =>
        {
            entity.ToTable("StatusOrder");

            entity.Property(e => e.Description).HasMaxLength(25);
        });

        modelBuilder.Entity<Tax>(entity =>
        {
            entity.ToTable("Tax");

            entity.Property(e => e.Created)
                .HasDefaultValue(new DateTime(2025, 5, 6, 21, 11, 26, 987, DateTimeKind.Unspecified))
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(70)
                .HasDefaultValue("");
            entity.Property(e => e.Name).HasMaxLength(40);
            entity.Property(e => e.Rate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);
        });

        modelBuilder.Entity<TokenMaster>(entity =>
        {
            entity.ToTable("TokenMaster");

            entity.HasIndex(e => e.UserId, "IX_TokenMaster_UserId");

            entity.Property(e => e.Created)
                .HasDefaultValue(new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(70)
                .HasDefaultValue("");
            entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            entity.Property(e => e.JwtId).HasMaxLength(250);
            entity.Property(e => e.Token).HasMaxLength(250);
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);

            entity.HasOne(d => d.User).WithMany(p => p.TokenMasters)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TokenMaster_User");
        });

        modelBuilder.Entity<TypeService>(entity =>
        {
            entity.ToTable("TypeService");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UnitMeasure>(entity =>
        {
            entity.ToTable("UnitMeasure");

            entity.Property(e => e.Created)
                .HasDefaultValue(new DateTime(2025, 5, 6, 21, 11, 27, 299, DateTimeKind.Unspecified))
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(70)
                .HasDefaultValue("");
            entity.Property(e => e.Name).HasMaxLength(25);
            entity.Property(e => e.Symbol)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.DistrictId, "IX_User_DistrictId");

            entity.HasIndex(e => e.GenderId, "IX_User_GenderId");

            entity.HasIndex(e => e.RoleId, "IX_User_RoleId");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.CardId).HasMaxLength(50);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(80);
            entity.Property(e => e.LastName).HasMaxLength(80);
            entity.Property(e => e.Password)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePictureUrl).HasMaxLength(200);
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);

            entity.HasOne(d => d.District).WithMany(p => p.Users)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_District");

            entity.HasOne(d => d.Gender).WithMany(p => p.Users)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Gender");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<UserBranch>(entity =>
        {
            entity.ToTable("UserBranch");

            entity.HasIndex(e => e.BranchId, "IX_UserBranch_BranchId");

            entity.HasIndex(e => e.UserId, "IX_UserBranch_UserId");

            entity.HasOne(d => d.Branch).WithMany(p => p.UserBranches)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserBranch_Branch");

            entity.HasOne(d => d.User).WithMany(p => p.UserBranches)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserBranch_User");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.ToTable("Vendor");

            entity.HasIndex(e => e.DistrictId, "IX_Vendor_DistrictId");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(70);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FiscalNumber).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(80);
            entity.Property(e => e.SocialReason).HasMaxLength(150);
            entity.Property(e => e.Updated).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(70);

            entity.HasOne(d => d.District).WithMany(p => p.Vendors)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vendor_District");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
    {
        OnBeforeSaving();

        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        DefaultProperties();
    }

    private void DefaultProperties()
    {
        string createdByName = "CreatedBy";
        string updatedByName = "UpdatedBy";

        DateTime created = DateTime.Now;
        DateTime updated = DateTime.Now;

        foreach (var entry in ChangeTracker.Entries())
        {
            string createdBy = string.Empty;
            string updatedBy = null!;
            if (entry.Entity.GetType().GetProperty(createdByName) != null) createdBy = entry.Property(createdByName).CurrentValue!.ToString()!;
            if (entry.Entity.GetType().GetProperty(updatedByName) != null)
            {
                var modification = entry.Property(updatedByName).CurrentValue;
                if (modification != null) updatedBy = modification.ToString()!;
            }

            if (entry.State == EntityState.Added)
            {
                GenerateAdded(entry, createdByName, createdBy, updatedByName, created);
            }
            else
            {
                GenerateModified(entry, createdByName, updatedByName, updatedBy, updated);
            }
        }
    }

    private static void GenerateAdded(EntityEntry entry, string createdByName, string createdBy, string updatedByName, DateTime created)
    {
        string activeName = "Active";

        if (entry.Entity.GetType().GetProperty(CREATEDNAME) != null && entry.Property(CREATEDNAME).CurrentValue != null) entry.Property(CREATEDNAME).CurrentValue = created;
        if (entry.Entity.GetType().GetProperty(activeName) != null && !(bool)entry.Property(activeName).CurrentValue!) entry.Property(activeName).CurrentValue = true;

        if (entry.Entity.GetType().GetProperty(createdByName) != null && entry.Property(updatedByName).CurrentValue != null)
        {
            entry.Property(createdByName).CurrentValue = entry.Property(updatedByName).CurrentValue;
            entry.Property(updatedByName).CurrentValue = null;
        }

        if (entry.Entity.GetType().GetProperty(createdByName) != null) entry.Property(createdByName).CurrentValue = createdBy;
        if (entry.Entity.GetType().GetProperty(UPDATEDNAME) != null) entry.Property(UPDATEDNAME).IsModified = false;
        if (entry.Entity.GetType().GetProperty(updatedByName) != null) entry.Property(updatedByName).IsModified = false;
    }

    private static void GenerateModified(EntityEntry entry, string createdByName, string updatedByName, string updatedBy, DateTime updated)
    {
        if (entry.State == EntityState.Modified)
        {
            if (entry.Entity.GetType().GetProperty(UPDATEDNAME) != null) entry.Property(UPDATEDNAME).CurrentValue = updated;

            if (entry.Entity.GetType().GetProperty(updatedByName) != null) entry.Property(updatedByName).CurrentValue = updatedBy;
            if (entry.Entity.GetType().GetProperty(CREATEDNAME) != null) entry.Property(CREATEDNAME).IsModified = false;
            if (entry.Entity.GetType().GetProperty(createdByName) != null) entry.Property(createdByName).IsModified = false;
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HowrashokShop.Models;

public partial class HowrashokShopContext : DbContext
{
    public HowrashokShopContext()
    {
    }

    public HowrashokShopContext(DbContextOptions<HowrashokShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdminPassword> AdminPasswords { get; set; }

    public virtual DbSet<Busket> Buskets { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientsPassword> ClientsPasswords { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Cost> Costs { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<TablePart> TableParts { get; set; }

    public virtual DbSet<Theme> Themes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(File.ReadAllText("connectionString.txt"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Admin__3214EC2730FEF03D");

            entity.ToTable("Admin");

            entity.HasIndex(e => e.Login, "UQ__Admin__5E55825BACE187DE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(200);
        });

        modelBuilder.Entity<AdminPassword>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AdminPas__3214EC2754599F96");

            entity.ToTable("AdminPassword");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Password).HasMaxLength(3500);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.AdminPassword)
                .HasForeignKey<AdminPassword>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AdminPasswor__ID__76619304");
        });

        modelBuilder.Entity<Busket>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.ClientId, e.ProductId }).HasName("PK__Busket__80C7014197538F0F");

            entity.ToTable("Busket");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Client).WithMany(p => p.Buskets)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Busket__ClientID__282DF8C2");

            entity.HasOne(d => d.Product).WithMany(p => p.Buskets)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Busket__ProductI__245D67DE");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC27F8516FE7");

            entity.ToTable("Category");

            entity.HasIndex(e => e.Name, "UQ__Category__737584F6E77B6B39").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Chat__3214EC070178FC5D");

            entity.ToTable("Chat");

            entity.HasOne(d => d.Admin).WithMany(p => p.Chats)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Chat__AdminId__7755B73D");

            entity.HasOne(d => d.Client).WithMany(p => p.Chats)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Chat__ClientId__756D6ECB");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Client__3214EC272CE7D5AD");

            entity.ToTable("Client");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(3500);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<ClientsPassword>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__ClientsP__E67E1A04E7A53587");

            entity.ToTable("ClientsPassword");

            entity.Property(e => e.ClientId)
                .ValueGeneratedNever()
                .HasColumnName("ClientID");
            entity.Property(e => e.Password).HasMaxLength(3500);

            entity.HasOne(d => d.Client).WithOne(p => p.ClientsPassword)
                .HasForeignKey<ClientsPassword>(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClientsPa__Clien__2645B050");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comments__3214EC2757AC3EB7");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.Comment1)
                .HasMaxLength(1000)
                .HasColumnName("Comment");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Client).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__Client__3D2915A8");

            entity.HasOne(d => d.Product).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__Produc__3C34F16F");
        });

        modelBuilder.Entity<Cost>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.DateOfSetting }).HasName("PK__Cost__33043B9B3491B059");

            entity.ToTable("Cost");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.DateOfSetting).HasColumnType("datetime");
            entity.Property(e => e.Size).HasColumnType("money");

            entity.HasOne(d => d.Product).WithMany(p => p.Costs)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cost__ProductID__4CA06362");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.DateOfSetting, e.ProductId }).HasName("PK__Discount__59A81D97DAF5985B");

            entity.ToTable("Discount");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.DateOfSetting).HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Discount__Produc__5070F446");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Material__3214EC073C32EFF4");

            entity.ToTable("Material");

            entity.HasIndex(e => e.Name, "UQ__Material__737584F693C01360").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Message__3214EC07DA591184");

            entity.ToTable("Message");

            entity.Property(e => e.MesText).HasMaxLength(2000);

            entity.HasOne(d => d.Chat).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Message__ChatId__7849DB76");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.DateOrder }).HasName("PK__Order__F7251F5F585EBDD7");

            entity.ToTable("Order");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.DateOrder).HasColumnType("datetime");
            entity.Property(e => e.Address).HasMaxLength(1000);
            entity.Property(e => e.ClientId).HasColumnName("ClientID");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__ClientID__2739D489");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Photo__3214EC27332A9D2D");

            entity.ToTable("Photo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Photopath).HasMaxLength(200);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.Photos)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Photo__ProductID__02FC7413");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC27F8A7158E");

            entity.ToTable("Product");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Description).HasMaxLength(3000);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.ThemeId).HasColumnName("ThemeID");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Categor__5165187F");

            entity.HasOne(d => d.Theme).WithMany(p => p.Products)
                .HasForeignKey(d => d.ThemeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__ThemeID__5629CD9C");

            entity.HasMany(d => d.Materials).WithMany(p => p.Ids)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductMaterial",
                    r => r.HasOne<Material>().WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductMa__Mater__6AEFE058"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductMater__ID__671F4F74"),
                    j =>
                    {
                        j.HasKey("Id", "MaterialId").HasName("PK__ProductM__5E448D28EEF7D477");
                        j.ToTable("ProductMaterials");
                        j.IndexerProperty<int>("Id").HasColumnName("ID");
                    });
        });

        modelBuilder.Entity<TablePart>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.OrderId, e.DateOrder, e.ProductId }).HasName("PK__TablePar__3CB5B666EA1C18F3");

            entity.ToTable("TablePart");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.DateOrder).HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.TableParts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TablePart__Produ__236943A5");

            entity.HasOne(d => d.Order).WithMany(p => p.TableParts)
                .HasForeignKey(d => new { d.OrderId, d.DateOrder })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TablePart__2A164134");
        });

        modelBuilder.Entity<Theme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Theme__3214EC279798A534");

            entity.ToTable("Theme");

            entity.HasIndex(e => e.Name, "UQ__Theme__737584F6095DD7B4").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

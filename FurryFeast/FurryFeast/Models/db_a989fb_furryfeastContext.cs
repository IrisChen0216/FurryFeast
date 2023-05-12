using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FurryFeast.Models
{
    public partial class db_a989fb_furryfeastContext : DbContext
    {
        public db_a989fb_furryfeastContext()
        {
        }

        public db_a989fb_furryfeastContext(DbContextOptions<db_a989fb_furryfeastContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<Authority> Authorities { get; set; } = null!;
        public virtual DbSet<ClassReservetion> ClassReservetions { get; set; } = null!;
        public virtual DbSet<Conpon> Conpons { get; set; } = null!;
        public virtual DbSet<ContactU> ContactUs { get; set; } = null!;
        public virtual DbSet<GameOutcome> GameOutcomes { get; set; } = null!;
        public virtual DbSet<GameQue> GameQues { get; set; } = null!;
        public virtual DbSet<GameQuesChoice> GameQuesChoices { get; set; } = null!;
        public virtual DbSet<LostAnimal> LostAnimals { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<MsgBoard> MsgBoards { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<PetClass> PetClasses { get; set; } = null!;
        public virtual DbSet<PetClassPic> PetClassPics { get; set; } = null!;
        public virtual DbSet<PetClassType> PetClassTypes { get; set; } = null!;
        public virtual DbSet<PetFriendlyMap> PetFriendlyMaps { get; set; } = null!;
        public virtual DbSet<PetType> PetTypes { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductPic> ProductPics { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
        public virtual DbSet<Recipe> Recipes { get; set; } = null!;
        public virtual DbSet<RecipesPhoto> RecipesPhotos { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<StockArticle> StockArticles { get; set; } = null!;
        public virtual DbSet<StockGroup> StockGroups { get; set; } = null!;
        public virtual DbSet<StockImage> StockImages { get; set; } = null!;
        public virtual DbSet<StockMeasureUnit> StockMeasureUnits { get; set; } = null!;
        public virtual DbSet<StockSupplier> StockSuppliers { get; set; } = null!;
        public virtual DbSet<StockSuppliersGroup> StockSuppliersGroups { get; set; } = null!;
        public virtual DbSet<StockWarehouse> StockWarehouses { get; set; } = null!;
        public virtual DbSet<StockWarehouseGroup> StockWarehouseGroups { get; set; } = null!;
        public virtual DbSet<Teachere> Teacheres { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

				IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
				optionsBuilder.UseSqlServer(configuration.GetConnectionString("FurryFeast"));
			}

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId).HasColumnName("Admin_ID");

                entity.Property(e => e.AdminAccount)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Admin_Account");

                entity.Property(e => e.AdminName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Admin_Name");

                entity.Property(e => e.AdminPassword)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Admin_Password");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Article");

                entity.Property(e => e.AdminId).HasColumnName("Admin_ID");

                entity.Property(e => e.AdminName)
                    .HasMaxLength(10)
                    .HasColumnName("Admin_Name");

                entity.Property(e => e.ArticleId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Article_ID");

                entity.Property(e => e.ArticleText)
                    .HasMaxLength(100)
                    .HasColumnName("Article_Text");

                entity.Property(e => e.ArticleTitle)
                    .HasMaxLength(10)
                    .HasColumnName("Article_Title");

                entity.Property(e => e.ArticleType)
                    .HasMaxLength(10)
                    .HasColumnName("Article_Type");

                entity.HasOne(d => d.Admin)
                    .WithMany()
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Article_Admin1");
            });

            modelBuilder.Entity<Authority>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Authority");

                entity.Property(e => e.AdminId).HasColumnName("Admin_ID");

                entity.Property(e => e.AuthorityId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Authority_ID");

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.HasOne(d => d.Admin)
                    .WithMany()
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Authority_Admin");

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Authority_Role");
            });

            modelBuilder.Entity<ClassReservetion>(entity =>
            {
                entity.ToTable("ClassReservetion");

                entity.Property(e => e.ClassReservetionId).HasColumnName("ClassReservetion_ID");

                entity.Property(e => e.ClassReservetionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ClassReservetion_Date");

                entity.Property(e => e.ClassReservetionState)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ClassReservetion_State")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MemberId).HasColumnName("Member_ID");

                entity.Property(e => e.PetClassId).HasColumnName("PetClass_ID");

                entity.HasOne(d => d.PetClass)
                    .WithMany(p => p.ClassReservetions)
                    .HasForeignKey(d => d.PetClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassReservetion_PetClass");
            });

            modelBuilder.Entity<Conpon>(entity =>
            {
                entity.ToTable("Conpon");

                entity.Property(e => e.ConponId).HasColumnName("Conpon_ID");

                entity.Property(e => e.ConponContent)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Conpon_Content");

                entity.Property(e => e.ConponDiscount).HasColumnName("Conpon_Discount");

                entity.Property(e => e.ConponEndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Conpon_EndTime");

                entity.Property(e => e.ConponName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Conpon_Name");

                entity.Property(e => e.ConponStartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Conpon_StartTime");
            });

            modelBuilder.Entity<ContactU>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.GuestContext)
                    .HasMaxLength(200)
                    .HasColumnName("guest_context");

                entity.Property(e => e.GuestEmail)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("guest_Email");

                entity.Property(e => e.GuestId).HasColumnName("guest_ID");

                entity.Property(e => e.GuestName)
                    .HasMaxLength(5)
                    .HasColumnName("guest_Name");

                entity.Property(e => e.GuestPhone).HasColumnName("guest_phone");

                entity.Property(e => e.GuestSubject)
                    .HasMaxLength(20)
                    .HasColumnName("guest_subject");
            });

            modelBuilder.Entity<GameOutcome>(entity =>
            {
                entity.HasKey(e => e.AnswerId);

                entity.ToTable("Game_Outcomes");

                entity.Property(e => e.AnswerId).HasColumnName("Answer_ID");

                entity.Property(e => e.AnswerResult).HasColumnName("Answer_Result");

                entity.Property(e => e.ChoicesId).HasColumnName("Choices_ID");

                entity.HasOne(d => d.Choices)
                    .WithMany(p => p.GameOutcomes)
                    .HasForeignKey(d => d.ChoicesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Game_Outcomes_Game_QuesChoices");
            });

            modelBuilder.Entity<GameQue>(entity =>
            {
                entity.HasKey(e => e.QuesId);

                entity.ToTable("Game_Ques");

                entity.Property(e => e.QuesId).HasColumnName("Ques_ID");

                entity.Property(e => e.PetTypesId).HasColumnName("PetTypes_ID");

                entity.Property(e => e.QuesContent).HasColumnName("Ques_Content");

                entity.HasOne(d => d.PetTypes)
                    .WithMany(p => p.GameQues)
                    .HasForeignKey(d => d.PetTypesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Game_Ques_PetTypes");
            });

            modelBuilder.Entity<GameQuesChoice>(entity =>
            {
                entity.HasKey(e => e.ChoicesId);

                entity.ToTable("Game_QuesChoices");

                entity.Property(e => e.ChoicesId).HasColumnName("Choices_ID");

                entity.Property(e => e.ChoicesContent).HasColumnName("Choices_Content");

                entity.Property(e => e.QuesId).HasColumnName("Ques_ID");

                entity.HasOne(d => d.Ques)
                    .WithMany(p => p.GameQuesChoices)
                    .HasForeignKey(d => d.QuesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Game_QuesChoices_Game_Ques");
            });

            modelBuilder.Entity<LostAnimal>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Lost_Animal");

                entity.Property(e => e.AnimalAge)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Animal_age");

                entity.Property(e => e.AnimalBreed)
                    .HasMaxLength(10)
                    .HasColumnName("Animal_breed");

                entity.Property(e => e.AnimalFeature)
                    .HasMaxLength(20)
                    .HasColumnName("Animal_Feature");

                entity.Property(e => e.AnimalName)
                    .HasMaxLength(10)
                    .HasColumnName("Animal_Name");

                entity.Property(e => e.AnimalPattern)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Animal_Pattern");

                entity.Property(e => e.AnimalSex)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("Animal_Sex");

                entity.Property(e => e.AnimalType)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Animal_Type");

                entity.Property(e => e.LostAnimalId).HasColumnName("LostAnimal_ID");

                entity.Property(e => e.LostDay).HasColumnType("datetime");

                entity.Property(e => e.LostPlace).HasMaxLength(50);

                entity.Property(e => e.MicrochipId).HasColumnName("microchip_ID");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.MemberId).HasColumnName("Member_ID");

                entity.Property(e => e.ConponId).HasColumnName("Conpon_ID");

                entity.Property(e => e.MemberAccount)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Member_Account");

                entity.Property(e => e.MemberAdress)
                    .HasMaxLength(50)
                    .HasColumnName("Member_Adress");

                entity.Property(e => e.MemberBirthday)
                    .HasColumnType("date")
                    .HasColumnName("Member_Birthday");

                entity.Property(e => e.MemberEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Member_Email");

                entity.Property(e => e.MemberGender).HasColumnName("Member_Gender");

                entity.Property(e => e.MemberName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Member_Name");

                entity.Property(e => e.MemberPassord)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Member_Passord");

                entity.Property(e => e.MemberPhone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Member_Phone");

                entity.HasOne(d => d.Conpon)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.ConponId)
                    .HasConstraintName("FK_Members_Conpon");
            });

            modelBuilder.Entity<MsgBoard>(entity =>
            {
                entity.HasKey(e => e.MsgId);

                entity.ToTable("MsgBoard");

                entity.Property(e => e.MsgId).HasColumnName("Msg_ID");

                entity.Property(e => e.MsgContent).HasColumnName("Msg_Content");

                entity.Property(e => e.MsgDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Msg_DateTime");

                entity.Property(e => e.MsgRecipesId).HasColumnName("Msg_RecipesID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.MsgRecipes)
                    .WithMany(p => p.MsgBoards)
                    .HasForeignKey(d => d.MsgRecipesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MsgBoard_Recipes");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                entity.Property(e => e.MemberId).HasColumnName("Member_ID");

                entity.Property(e => e.OrderCreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Order_CreateDate");

                entity.Property(e => e.OrderRecipientAdress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Order_RecipientAdress");

                entity.Property(e => e.OrderRecipientName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Order_RecipientName");

                entity.Property(e => e.OrderRecipientPhone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Order_RecipientPhone");

                entity.Property(e => e.OrderShipDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Order_ShipDate");

                entity.Property(e => e.OrderStatus).HasColumnName("Order_Status");

                entity.Property(e => e.OrderTotalPrice).HasColumnName("Order_TotalPrice");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Members_Orders");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.OrderDetailId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderDetail_ID");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Order_ID");

                entity.Property(e => e.OrderPrice).HasColumnName("Order_Price");

                entity.Property(e => e.OrderQuantity).HasColumnName("Order_Quantity");

                entity.Property(e => e.ProductId).HasColumnName("Product_ID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_OrderDetails1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Products");
            });

            modelBuilder.Entity<PetClass>(entity =>
            {
                entity.ToTable("PetClass");

                entity.Property(e => e.PetClassId).HasColumnName("PetClass_ID");

                entity.Property(e => e.PetClassDate)
                    .HasColumnType("datetime")
                    .HasColumnName("PetClass_Date");

                entity.Property(e => e.PetClassInformation)
                    .HasMaxLength(300)
                    .HasColumnName("PetClass_Information");

                entity.Property(e => e.PetClassName)
                    .HasMaxLength(20)
                    .HasColumnName("PetClass_Name");

                entity.Property(e => e.PetClassPrice).HasColumnName("PetClass_Price");

                entity.Property(e => e.PetClassTypeId).HasColumnName("PetClassType_ID");

                entity.Property(e => e.PetTypesId).HasColumnName("PetTypes_ID");

                entity.Property(e => e.TeacherId).HasColumnName("Teacher_ID");

                entity.HasOne(d => d.PetClassType)
                    .WithMany(p => p.PetClasses)
                    .HasForeignKey(d => d.PetClassTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PetClass_PetClassTypes");

                entity.HasOne(d => d.PetTypes)
                    .WithMany(p => p.PetClasses)
                    .HasForeignKey(d => d.PetTypesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PetClass_PetTypes");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.PetClasses)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PetClass_Teacheres");
            });

            modelBuilder.Entity<PetClassPic>(entity =>
            {
                entity.ToTable("PetClassPic");

                entity.Property(e => e.PetClassPicId).HasColumnName("PetClassPic_ID");

                entity.Property(e => e.PetClassId).HasColumnName("PetClass_ID");

                entity.Property(e => e.PetClassPicImage).HasColumnName("PetClassPic_Image");

                entity.HasOne(d => d.PetClass)
                    .WithMany(p => p.PetClassPics)
                    .HasForeignKey(d => d.PetClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PetClassPic_PetClass");
            });

            modelBuilder.Entity<PetClassType>(entity =>
            {
                entity.Property(e => e.PetClassTypeId).HasColumnName("PetClassType_ID");

                entity.Property(e => e.PetClassTypeName)
                    .HasMaxLength(10)
                    .HasColumnName("PetClassType_Name")
                    .IsFixedLength();
            });

            modelBuilder.Entity<PetFriendlyMap>(entity =>
            {
                entity.HasKey(e => e.MapPlaceId);

                entity.ToTable("Pet_Friendly_Map");

                entity.Property(e => e.MapPlaceId).HasColumnName("MapPlace_ID");

                entity.Property(e => e.MapPlaceAddress).HasColumnName("MapPlace_Address");

                entity.Property(e => e.MapPlaceLat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MapPlace_Lat");

                entity.Property(e => e.MapPlaceLng)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MapPlace_Lng");

                entity.Property(e => e.MapPlaceName).HasColumnName("MapPlace_Name");

                entity.Property(e => e.MapPlaceNotes).HasColumnName("MapPlace_Notes");

                entity.Property(e => e.MapPlacePhone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MapPlace_Phone");

                entity.Property(e => e.MapPlaceType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MapPlace_Type");
            });

            modelBuilder.Entity<PetType>(entity =>
            {
                entity.HasKey(e => e.PetTypesId);

                entity.Property(e => e.PetTypesId).HasColumnName("PetTypes_ID");

                entity.Property(e => e.PetTypes).HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("Product_ID");

                entity.Property(e => e.ArticlesId).HasColumnName("Articles_ID");

                entity.Property(e => e.ProductAmount).HasColumnName("Product_Amount");

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(1000)
                    .HasColumnName("Product_Description");

                entity.Property(e => e.ProductLaunchedTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Product_LaunchedTime");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(20)
                    .HasColumnName("Product_Name");

                entity.Property(e => e.ProductPicId).HasColumnName("ProductPic_ID");

                entity.Property(e => e.ProductPrice).HasColumnName("Product_Price");

                entity.Property(e => e.ProductSoldTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Product_SoldTime");

                entity.Property(e => e.ProductState).HasColumnName("Product_State");

                entity.Property(e => e.ProductTypeId).HasColumnName("ProductType_ID");

                entity.HasOne(d => d.Articles)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ArticlesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Stock_articles");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_ProductType");
            });

            modelBuilder.Entity<ProductPic>(entity =>
            {
                entity.ToTable("ProductPic");

                entity.Property(e => e.ProductPicId).HasColumnName("ProductPic_ID");

                entity.Property(e => e.ProductId).HasColumnName("Product_ID");

                entity.Property(e => e.ProductPicImage).HasColumnName("ProductPic_Image");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPics)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductPic_Products");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductType");

                entity.Property(e => e.ProductTypeId).HasColumnName("ProductType_ID");

                entity.Property(e => e.PetTypesId).HasColumnName("PetTypes_ID");

                entity.Property(e => e.ProductTypeName)
                    .HasMaxLength(20)
                    .HasColumnName("ProductType_Name");

                entity.HasOne(d => d.PetTypes)
                    .WithMany(p => p.ProductTypes)
                    .HasForeignKey(d => d.PetTypesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductType_PetTypes");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasKey(e => e.RecipesId);

                entity.Property(e => e.RecipesId).HasColumnName("Recipes_ID");

                entity.Property(e => e.PetTypesId).HasColumnName("PetTypes_ID");

                entity.Property(e => e.PhotoId).HasColumnName("Photo_ID");

                entity.Property(e => e.RecipesData).HasColumnName("Recipes_Data");

                entity.Property(e => e.RecipesDescription).HasColumnName("Recipes_Description");

                entity.Property(e => e.RecipesMethod).HasColumnName("Recipes_Method");

                entity.Property(e => e.RecipesName)
                    .HasMaxLength(50)
                    .HasColumnName("Recipes_Name");

                entity.Property(e => e.RecipesNotes).HasColumnName("Recipes_Notes");

                entity.HasOne(d => d.PetTypes)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.PetTypesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipes_PetTypes");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipes_Recipes_Photos");
            });

            modelBuilder.Entity<RecipesPhoto>(entity =>
            {
                entity.HasKey(e => e.PhotoId);

                entity.ToTable("Recipes_Photos");

                entity.Property(e => e.PhotoId).HasColumnName("Photo_ID");

                entity.Property(e => e.RecipesPhotos).HasColumnName("Recipes_Photos");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Role_Name");
            });

            modelBuilder.Entity<StockArticle>(entity =>
            {
                entity.HasKey(e => e.ArticlesId);

                entity.ToTable("Stock_articles");

                entity.HasIndex(e => e.AarticlesCode, "unq_Stock_articles")
                    .IsUnique();

                entity.Property(e => e.ArticlesId).HasColumnName("Articles_ID");

                entity.Property(e => e.AarticlesCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Aarticles_code");

                entity.Property(e => e.ArticlesDescription)
                    .HasMaxLength(40)
                    .HasColumnName("Articles_description");

                entity.Property(e => e.ArticlesIsValid)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("Articles_is_valid");

                entity.Property(e => e.ArticlesNotes)
                    .HasMaxLength(40)
                    .HasColumnName("Articles_notes");

                entity.Property(e => e.ArticlesPrice)
                    .HasColumnType("numeric(15, 5)")
                    .HasColumnName("Articles_price");

                entity.Property(e => e.ArticlesQuantity)
                    .HasColumnType("numeric(15, 5)")
                    .HasColumnName("Articles_quantity");

                entity.Property(e => e.GroupId).HasColumnName("Group_id");

                entity.Property(e => e.ImagesId).HasColumnName("Images_id");

                entity.Property(e => e.MeasureUnitId).HasColumnName("Measure_unit_id");

                entity.Property(e => e.SuppliersId).HasColumnName("Suppliers_id");

                entity.Property(e => e.WarehousesId).HasColumnName("Warehouses_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.StockArticles)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Stock_articles_Stock_groups");

                entity.HasOne(d => d.Images)
                    .WithMany(p => p.StockArticles)
                    .HasForeignKey(d => d.ImagesId)
                    .HasConstraintName("FK_Stock_articles_Stock_images");

                entity.HasOne(d => d.MeasureUnit)
                    .WithMany(p => p.StockArticles)
                    .HasForeignKey(d => d.MeasureUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stock_articles_Stock_measure_units");

                entity.HasOne(d => d.Suppliers)
                    .WithMany(p => p.StockArticles)
                    .HasForeignKey(d => d.SuppliersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stock_articles_Stock_suppliers");

                entity.HasOne(d => d.Warehouses)
                    .WithMany(p => p.StockArticles)
                    .HasForeignKey(d => d.WarehousesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stock_articles_Stock_warehouses");
            });

            modelBuilder.Entity<StockGroup>(entity =>
            {
                entity.HasKey(e => e.GroupsId);

                entity.ToTable("Stock_groups");

                entity.HasIndex(e => e.GroupsCode, "unq_Stock_groups")
                    .IsUnique();

                entity.Property(e => e.GroupsId).HasColumnName("Groups_ID");

                entity.Property(e => e.GgroupsDescription)
                    .HasMaxLength(40)
                    .HasColumnName("Ggroups_description");

                entity.Property(e => e.GroupsCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Groups_code");

                entity.Property(e => e.GroupsNotes)
                    .HasMaxLength(40)
                    .HasColumnName("Groups_notes");
            });

            modelBuilder.Entity<StockImage>(entity =>
            {
                entity.HasKey(e => e.ImagesId);

                entity.ToTable("Stock_images");

                entity.HasIndex(e => e.ImagesCode, "unq_Stock_images")
                    .IsUnique();

                entity.Property(e => e.ImagesId).HasColumnName("Images_ID");

                entity.Property(e => e.ImagesBitmapFile).HasColumnName("Images_bitmap_file");

                entity.Property(e => e.ImagesCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Images_code");

                entity.Property(e => e.ImagesDescription)
                    .HasMaxLength(40)
                    .HasColumnName("Images_description");

                entity.Property(e => e.ImagesFileCrc)
                    .HasColumnName("Images_file_crc")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<StockMeasureUnit>(entity =>
            {
                entity.HasKey(e => e.MeasureUnitsId);

                entity.ToTable("Stock_measure_units");

                entity.HasIndex(e => e.MeasureUnitsCode, "unq_Stock_measure_units")
                    .IsUnique();

                entity.Property(e => e.MeasureUnitsId).HasColumnName("Measure_units_ID");

                entity.Property(e => e.MeasureUnitsCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Measure_units_code");

                entity.Property(e => e.MeasureUnitsDescription)
                    .HasMaxLength(40)
                    .HasColumnName("Measure_units_description");
            });

            modelBuilder.Entity<StockSupplier>(entity =>
            {
                entity.HasKey(e => e.SuppliersId);

                entity.ToTable("Stock_suppliers");

                entity.HasIndex(e => e.SuppliersCode, "unq_Stock_suppliers")
                    .IsUnique();

                entity.Property(e => e.SuppliersId).HasColumnName("Suppliers_ID");

                entity.Property(e => e.SupplierGroupId).HasColumnName("Supplier_group_id");

                entity.Property(e => e.SuppliersCity)
                    .HasMaxLength(10)
                    .HasColumnName("Suppliers_city");

                entity.Property(e => e.SuppliersCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Suppliers_code");

                entity.Property(e => e.SuppliersCountry)
                    .HasMaxLength(10)
                    .HasColumnName("Suppliers_country");

                entity.Property(e => e.SuppliersDescription)
                    .HasMaxLength(40)
                    .HasColumnName("Suppliers_description");

                entity.Property(e => e.SuppliersEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Suppliers_email");

                entity.Property(e => e.SuppliersFax)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Suppliers_fax");

                entity.Property(e => e.SuppliersNation)
                    .HasMaxLength(10)
                    .HasColumnName("Suppliers_nation");

                entity.Property(e => e.SuppliersPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Suppliers_phone");

                entity.Property(e => e.SuppliersStreet)
                    .HasMaxLength(40)
                    .HasColumnName("Suppliers_street");

                entity.Property(e => e.SuppliersUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Suppliers_url");

                entity.Property(e => e.SuppliersZipCode)
                    .HasMaxLength(10)
                    .HasColumnName("Suppliers_zip_code");

                entity.HasOne(d => d.SupplierGroup)
                    .WithMany(p => p.StockSuppliers)
                    .HasForeignKey(d => d.SupplierGroupId)
                    .HasConstraintName("FK_Stock_suppliers_Stock_suppliers_groups");
            });

            modelBuilder.Entity<StockSuppliersGroup>(entity =>
            {
                entity.HasKey(e => e.SuppliersGroupsId);

                entity.ToTable("Stock_suppliers_groups");

                entity.HasIndex(e => e.SuppliersGroupsCode, "unq_Stock_suppliers_groups")
                    .IsUnique();

                entity.Property(e => e.SuppliersGroupsId).HasColumnName("Suppliers_groups_ID");

                entity.Property(e => e.SuppliersGroupsCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Suppliers_groups_code");

                entity.Property(e => e.SuppliersGroupsDescription)
                    .HasMaxLength(40)
                    .HasColumnName("Suppliers_groups_description");
            });

            modelBuilder.Entity<StockWarehouse>(entity =>
            {
                entity.HasKey(e => e.WarehousesId);

                entity.ToTable("Stock_warehouses");

                entity.HasIndex(e => e.WarehousesCode, "unq_Stock_warehouses")
                    .IsUnique();

                entity.Property(e => e.WarehousesId).HasColumnName("Warehouses_ID");

                entity.Property(e => e.WarehouseGroupId).HasColumnName("Warehouse_group_id");

                entity.Property(e => e.WarehousesCity)
                    .HasMaxLength(10)
                    .HasColumnName("Warehouses_city");

                entity.Property(e => e.WarehousesCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Warehouses_code");

                entity.Property(e => e.WarehousesCountry)
                    .HasMaxLength(10)
                    .HasColumnName("Warehouses_country");

                entity.Property(e => e.WarehousesDescription)
                    .HasMaxLength(40)
                    .HasColumnName("Warehouses_description");

                entity.Property(e => e.WarehousesNation)
                    .HasMaxLength(10)
                    .HasColumnName("Warehouses_nation");

                entity.Property(e => e.WarehousesStreet)
                    .HasMaxLength(40)
                    .HasColumnName("Warehouses_street");

                entity.Property(e => e.WarehousesZipCode)
                    .HasMaxLength(10)
                    .HasColumnName("Warehouses_zip_code");

                entity.HasOne(d => d.WarehouseGroup)
                    .WithMany(p => p.StockWarehouses)
                    .HasForeignKey(d => d.WarehouseGroupId)
                    .HasConstraintName("FK_Stock_warehouses_Stock_warehouse_groups");
            });

            modelBuilder.Entity<StockWarehouseGroup>(entity =>
            {
                entity.HasKey(e => e.WarehouseGroupsId);

                entity.ToTable("Stock_warehouse_groups");

                entity.HasIndex(e => e.WarehouseGroupsCode, "unq_Stock_warehouse_groups")
                    .IsUnique();

                entity.Property(e => e.WarehouseGroupsId).HasColumnName("Warehouse_groups_ID");

                entity.Property(e => e.WarehouseGroupsCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Warehouse_groups_code");

                entity.Property(e => e.WarehouseGroupsDescription)
                    .HasMaxLength(40)
                    .HasColumnName("Warehouse_groups_description");
            });

            modelBuilder.Entity<Teachere>(entity =>
            {
                entity.HasKey(e => e.TeacherId);

                entity.Property(e => e.TeacherId).HasColumnName("Teacher_ID");

                entity.Property(e => e.TeacherName)
                    .HasMaxLength(10)
                    .HasColumnName("Teacher_Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

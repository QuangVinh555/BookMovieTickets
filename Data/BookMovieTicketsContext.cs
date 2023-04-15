using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BookMovieTickets.Data
{
    public partial class BookMovieTicketsContext : DbContext
    {
        public BookMovieTicketsContext()
        {
        }

        public BookMovieTicketsContext(DbContextOptions<BookMovieTicketsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<BookTicket> BookTickets { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Chair> Chairs { get; set; }
        public virtual DbSet<ChairType> ChairTypes { get; set; }
        public virtual DbSet<CinemaName> CinemaNames { get; set; }
        public virtual DbSet<CinemaRoom> CinemaRooms { get; set; }
        public virtual DbSet<CinemaType> CinemaTypes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LoginType> LoginTypes { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<MovieActor> MovieActors { get; set; }
        public virtual DbSet<MovieCategory> MovieCategories { get; set; }
        public virtual DbSet<MovieProducer> MovieProducers { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Raiting> Raitings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ShowTime> ShowTimes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRank> UserRanks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.ToTable("Actor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.ToTable("Banner");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ContainerSlide)
                    .HasMaxLength(255)
                    .HasColumnName("container_slide");

                entity.Property(e => e.MainSlide)
                    .HasMaxLength(255)
                    .HasColumnName("main_slide");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Banners)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__Banner__movie_id__797309D9");
            });

            modelBuilder.Entity<BookTicket>(entity =>
            {
                entity.ToTable("Book_Ticket");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChairId).HasColumnName("chair_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.RewardPoints).HasColumnName("reward_points");

                entity.Property(e => e.ShowTimeId).HasColumnName("show_time_id");

                entity.Property(e => e.TicketCount).HasColumnName("ticket_count");

                entity.Property(e => e.TicketPrice).HasColumnName("ticket_price");

                entity.Property(e => e.TotalPrice).HasColumnName("total_price");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Chair)
                    .WithMany(p => p.BookTickets)
                    .HasForeignKey(d => d.ChairId)
                    .HasConstraintName("FK__Book_Tick__chair__09A971A2");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.BookTickets)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__Book_Tick__movie__01142BA1");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.BookTickets)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK__Book_Tick__payme__0B91BA14");

                entity.HasOne(d => d.ShowTime)
                    .WithMany(p => p.BookTickets)
                    .HasForeignKey(d => d.ShowTimeId)
                    .HasConstraintName("FK__Book_Tick__show___0A9D95DB");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BookTickets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Book_Tick__user___00200768");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Chair>(entity =>
            {
                entity.ToTable("Chair");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChairTypeId).HasColumnName("chair_type_id");

                entity.Property(e => e.CinemaRoomId).HasColumnName("cinema_room_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ChairType)
                    .WithMany(p => p.Chairs)
                    .HasForeignKey(d => d.ChairTypeId)
                    .HasConstraintName("FK__Chair__chair_typ__07C12930");

                entity.HasOne(d => d.CinemaRoom)
                    .WithMany(p => p.Chairs)
                    .HasForeignKey(d => d.CinemaRoomId)
                    .HasConstraintName("FK__Chair__cinema_ro__08B54D69");
            });

            modelBuilder.Entity<ChairType>(entity =>
            {
                entity.ToTable("Chair_Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CinemaName>(entity =>
            {
                entity.ToTable("Cinema_Name");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CinemaTypeId).HasColumnName("cinema_type_id");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LocationDetail)
                    .HasMaxLength(250)
                    .HasColumnName("location_detail");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");

                entity.HasOne(d => d.CinemaType)
                    .WithMany(p => p.CinemaNames)
                    .HasForeignKey(d => d.CinemaTypeId)
                    .HasConstraintName("FK__Cinema_Na__cinem__02FC7413");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.CinemaNames)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__Cinema_Na__locat__02084FDA");
            });

            modelBuilder.Entity<CinemaRoom>(entity =>
            {
                entity.ToTable("Cinema_Room");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CinemaNameId).HasColumnName("cinema_name_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");

                entity.HasOne(d => d.CinemaName)
                    .WithMany(p => p.CinemaRooms)
                    .HasForeignKey(d => d.CinemaNameId)
                    .HasConstraintName("FK__Cinema_Ro__cinem__04E4BC85");
            });

            modelBuilder.Entity<CinemaType>(entity =>
            {
                entity.ToTable("Cinema_Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Logo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("logo");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasMaxLength(1)
                    .HasColumnName("content");

                entity.Property(e => e.CountLikeComment).HasColumnName("count_like_comment");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__Comments__movie___0D7A0286");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comments__user_i__0C85DE4D");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Province)
                    .HasMaxLength(200)
                    .HasColumnName("province");
            });

            modelBuilder.Entity<LoginType>(entity =>
            {
                entity.ToTable("Login_Type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Author)
                    .HasMaxLength(100)
                    .HasColumnName("author");

                entity.Property(e => e.Content)
                    .HasMaxLength(1)
                    .HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Description)
                    .HasMaxLength(1)
                    .HasColumnName("description");

                entity.Property(e => e.MovieDuration)
                    .HasColumnType("datetime")
                    .HasColumnName("movie_duration");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");

                entity.Property(e => e.Nation)
                    .HasMaxLength(100)
                    .HasColumnName("nation");

                entity.Property(e => e.PremiereDate)
                    .HasColumnType("datetime")
                    .HasColumnName("premiere_date");

                entity.Property(e => e.PremiereYear)
                    .HasColumnType("datetime")
                    .HasColumnName("premiere_year");

                entity.Property(e => e.Stamp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("stamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Movie__user_id__7F2BE32F");
            });

            modelBuilder.Entity<MovieActor>(entity =>
            {
                entity.ToTable("Movie_Actor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActorId).HasColumnName("actor_id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.Role)
                    .HasMaxLength(200)
                    .HasColumnName("role");

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.MovieActors)
                    .HasForeignKey(d => d.ActorId)
                    .HasConstraintName("FK__Movie_Act__actor__7C4F7684");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieActors)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__Movie_Act__movie__7B5B524B");
            });

            modelBuilder.Entity<MovieCategory>(entity =>
            {
                entity.ToTable("Movie_Category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.MovieCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Movie_Cat__categ__787EE5A0");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieCategories)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__Movie_Cat__movie__778AC167");
            });

            modelBuilder.Entity<MovieProducer>(entity =>
            {
                entity.ToTable("Movie_Producer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.ProducerId).HasColumnName("producer_id");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieProducers)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__Movie_Pro__movie__7E37BEF6");

                entity.HasOne(d => d.Producer)
                    .WithMany(p => p.MovieProducers)
                    .HasForeignKey(d => d.ProducerId)
                    .HasConstraintName("FK__Movie_Pro__produ__7D439ABD");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasMaxLength(1)
                    .HasColumnName("content");

                entity.Property(e => e.CoverImage)
                    .HasMaxLength(255)
                    .HasColumnName("cover_image");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .HasMaxLength(1)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(250)
                    .HasColumnName("payment_type");
            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.ToTable("Producer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Raiting>(entity =>
            {
                entity.ToTable("Raiting");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountDislikeMovie).HasColumnName("count_dislike_movie");

                entity.Property(e => e.CountLikeMovie).HasColumnName("count_like_movie");

                entity.Property(e => e.CountStarsMovie).HasColumnName("count_stars_movie");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Raitings)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__Raiting__movie_i__7A672E12");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Raitings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Raiting__user_id__03F0984C");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ShowTime>(entity =>
            {
                entity.ToTable("Show_Time");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CinemaRoomId).HasColumnName("cinema_room_id");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.NumTicket).HasColumnName("num_ticket");

                entity.Property(e => e.Role)
                    .HasMaxLength(1)
                    .HasColumnName("role");

                entity.Property(e => e.ShowDate)
                    .HasColumnType("datetime")
                    .HasColumnName("show_date");

                entity.Property(e => e.ShowTime1)
                    .HasColumnType("datetime")
                    .HasColumnName("show_time");

                entity.Property(e => e.TicketPrice).HasColumnName("ticket_price");

                entity.HasOne(d => d.CinemaRoom)
                    .WithMany(p => p.ShowTimes)
                    .HasForeignKey(d => d.CinemaRoomId)
                    .HasConstraintName("FK__Show_Time__cinem__05D8E0BE");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.ShowTimes)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK__Show_Time__movie__06CD04F7");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccumulatedPoints)
                    .HasColumnName("accumulated_points")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .HasColumnName("address");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("avatar");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.LoginTypeId)
                    .HasColumnName("login_type_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.RewardPoints)
                    .HasColumnName("reward_points")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserRankId)
                    .HasColumnName("user_rank_id")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.LoginType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LoginTypeId)
                    .HasConstraintName("FK__User__login_type__76969D2E");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__User__role_id__75A278F5");

                entity.HasOne(d => d.UserRank)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRankId)
                    .HasConstraintName("FK__User__user_rank___0E6E26BF");
            });

            modelBuilder.Entity<UserRank>(entity =>
            {
                entity.ToTable("User_Rank");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Benchmark).HasColumnName("benchmark");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

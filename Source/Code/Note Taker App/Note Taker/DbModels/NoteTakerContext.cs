using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Note_Taker.DbModels;

public partial class NoteTakerContext : DbContext
{
    public NoteTakerContext()
    {
    }

    public NoteTakerContext(DbContextOptions<NoteTakerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Process> Processes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=NoteTaker;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notes__3214EC07AB311091");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.ProcessNavigation).WithMany(p => p.Notes)
                .HasForeignKey(d => d.Process)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notes_Processes");
        });

        modelBuilder.Entity<Process>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Processe__3214EC07DD6DE052");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

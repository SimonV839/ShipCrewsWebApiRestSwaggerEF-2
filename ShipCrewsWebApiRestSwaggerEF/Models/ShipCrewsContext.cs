﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SimonV839.ShipCrewsWebApiRestSwaggerEF.Models;

public partial class ShipCrewsContext : DbContext
{
    public ShipCrewsContext()
    {
    }

    public ShipCrewsContext(DbContextOptions<ShipCrewsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Crew> Crews { get; set; }

    public virtual DbSet<CrewAssignment> CrewAssignments { get; set; }

    public virtual DbSet<NeverAssignedPerson> NeverAssignedPeople { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=(local)\\SQLEXPRESS;database=ShipCrews;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Crew>(entity =>
        {
            entity.HasKey(e => e.CrewId).HasName("PK__Crews__89BCFC2926394403");

            entity.HasIndex(e => e.Name, "UQ__Crews__737584F663B715FD").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CrewAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CrewAssi__3214EC076DFAEC58");

            entity.HasIndex(e => new { e.CrewId, e.PersonId }, "CK_CrewAssignments_Unique").IsUnique();

            entity.HasOne(d => d.Crew).WithMany(p => p.CrewAssignments)
                .HasForeignKey(d => d.CrewId)
                .HasConstraintName("FK__CrewAssig__CrewI__403A8C7D");

            entity.HasOne(d => d.Person).WithMany(p => p.CrewAssignments)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__CrewAssig__Perso__412EB0B6");
        });

        modelBuilder.Entity<NeverAssignedPerson>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("NeverAssignedPeople");

            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__People__AA2FFBE52555D25E");

            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.People)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__People__RoleId__3C69FB99");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A242202A3");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

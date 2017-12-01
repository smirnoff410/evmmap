using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Maps.Entities;

namespace Maps.Models {
  public class MapsDB : DbContext {
    public DbSet<Lea> Leas { get; set; }
    public DbSet<Point> Points { get; set; }
    public DbSet<LeaPoint> LeaPoints { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UsersLea> UsersLeas { get; set; }
  }
}
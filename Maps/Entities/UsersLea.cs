using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maps.Entities {
  public class UsersLea {
    [Key] public int id { get; set; }
    public int id_lea { get; set; }
    public int id_user { get; set; }
  }
}
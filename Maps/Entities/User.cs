using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maps.Entities {
  public class User {
    [Key] public int id_user { get; set; }
  }
}
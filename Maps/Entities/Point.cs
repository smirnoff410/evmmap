using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maps.Entities {
  public class Point {
    [Key] public int id_point { get; set; }
    public double latitude { get; set; }  //широта
    public double longitude { get; set; } //долгота
  }
}
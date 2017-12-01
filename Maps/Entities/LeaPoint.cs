using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maps.Entities {
  public class LeaPoint {
    [Key] public int id { get; set; }
    public int id_lea { get; set; }    //id поля
    public int id_point { get; set; }  //id точки
  }
}
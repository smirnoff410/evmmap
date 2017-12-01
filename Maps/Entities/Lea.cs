using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maps.Entities {
  public class Lea {
    [Key] public int id_lea { get; set; }
    public string color { get; set; }  //цвет поля
    public double area { get; set; }   //площадь в га
    public string name { get; set; }   //название поля
    public int center { get; set; }    //id центра в таблице Points
  }
}
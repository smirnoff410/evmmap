using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maps.Models {
  public struct Coords {
    public double lat;
    public double lon;
  }
  public struct Wind {
    public double speed;
    public double deg;
  }
  public class City {
    public Coords coord;
    public double temp;
    public double temp_min;
    public double temp_max;
    public double pressure;
    public double humidity;
    public dynamic rain;
    public dynamic snow;
    public double clouds;
    public string type;
    public string description;
    public Wind wind;
  }
}
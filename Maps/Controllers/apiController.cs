using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using Maps.Models;
using Maps.Entities;

namespace Maps.Controllers {
  public class apiController : Controller {
    // GET: Api
    MapsDB db = new MapsDB();
    public string Index() {
      return "Invalid method Api";
    }

    public JsonResult getweather(string pol) {
      string[] paramString = pol.Split(',');
      double[] param = new double[paramString.Length];
      for (int i = 0; i < paramString.Length; ++i) {
        param[i] = Double.Parse(paramString[i]);
      }
      string zoom = "";
      if (param.Length % 2 == 1) {
        zoom = "," + param.Last();
        param = param.Take(param.Length - 1).ToArray();
      }

      double[] coords = new double[4];
      coords[0] = coords[2] = pol[0]; //longitude
      coords[1] = coords[3] = pol[1]; //latitude
      for (int i = 0; i < param.Length; i += 2) {
        coords[0] = Math.Min(coords[0], param[i]);
        coords[1] = Math.Min(coords[1], param[i + 1]);
        coords[2] = Math.Max(coords[2], param[i]);
        coords[3] = Math.Max(coords[3], param[i + 1]);
      }

      string url = "http://api.openweathermap.org/data/2.5/box/city?bbox=" + 
        String.Format("{0},{1},{2},{3}", coords[0], coords[1], coords[2], coords[3]) + zoom + 
        "&APPID=e20295bfea533879460864f0fa3bf643&units=metric";

      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
      HttpWebResponse response = (HttpWebResponse)request.GetResponse();

      string res = "";

      using (StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) {
        res = stream.ReadToEnd();
      }

      dynamic json = JsonConvert.DeserializeObject(res);
      Weather jsonObj = new Weather();
      jsonObj.list = new City[json.cnt];
      for(int i = 0; i < json.list.Count; ++i) {
        jsonObj.list[i] = new City();
        jsonObj.list[i].clouds = json.list[i].clouds.today;
        jsonObj.list[i].coord = new Coords();
        jsonObj.list[i].coord.lat = json.list[i].coord.Lat;
        jsonObj.list[i].coord.lon = json.list[i].coord.Lon;
        jsonObj.list[i].description = json.list[i].weather[0].description;
        jsonObj.list[i].humidity = json.list[i].main.humidity;
        jsonObj.list[i].pressure = json.list[i].main.pressure;
        jsonObj.list[i].rain = json.list[i].rain == null ? null : json.list[i].rain;
        jsonObj.list[i].snow = json.list[i].snow == null ? null : json.list[i].snow;
        jsonObj.list[i].temp = json.list[i].main.temp;
        jsonObj.list[i].temp_max = json.list[i].main.temp_max;
        jsonObj.list[i].temp_min = json.list[i].main.temp_min;
        jsonObj.list[i].type = json.list[i].weather[0].main;
        jsonObj.list[i].wind = new Wind();
        jsonObj.list[i].wind.speed = json.list[i].wind.speed;
        jsonObj.list[i].wind.deg = json.list[i].wind.deg;
      }
      return Json(jsonObj, JsonRequestBehavior.AllowGet);
    }

    private bool checkPoint(Point p, string[] center) {
      return p.latitude == Double.Parse(center[0]) &&
        p.longitude == Double.Parse(center[1]);
    }

    private int addPoint(string[] center) {
      Point centerPoint = db.Points.FirstOrDefault(p => checkPoint(p, center));
      if (centerPoint == null) {
        centerPoint = new Point {
          latitude = Double.Parse(center[0]),
          longitude = Double.Parse(center[1])
        };

        db.Points.Add(centerPoint);
        db.SaveChanges();
        centerPoint.id_point = db.Points.Max(c => c.id_point);
      }
      return centerPoint.id_point;
    }

    public void addLea(string color, double area, string coords, string name, string center) {
      string[] centerString = center.Split(',');
      string[] coordsString = coords.Split(',');

      int id_point = this.addPoint(centerString);

      Lea lea = db.Leas.FirstOrDefault(l => l.area == area && l.center == id_point &&
       l.color == color && l.name == name);

      if (lea == null) {
        lea = new Lea {
          name = name,
          area = area,
          center = id_point,
          color = color
        };
        db.Leas.Add(lea);
        db.SaveChanges();
        lea.id_lea = db.Leas.Max(l => l.id_lea);
      }

      for (int i = 0; i < coordsString.Length; i += 2) {
        id_point = addPoint(new string[] { coordsString[i], coordsString[i + 1]});
        db.LeaPoints.Add(new LeaPoint {
          id_lea = lea.id_lea,
          id_point = id_point
        });
      }
      db.SaveChanges();
    }
  }
}
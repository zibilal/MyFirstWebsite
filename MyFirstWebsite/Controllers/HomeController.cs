﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using MyFirstWebsite.DAL;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.Controllers
{
    public class HomeController : Controller
    {
        // POST 
        public ActionResult Index(List list)
        {
            if (ModelState.IsValid)
            {
                string timeToday = DateTime.Now.ToString("h:mm:ss tt");
                string dateToday = DateTime.Now.ToString("M/dd/yyyy");
                using (var db = new MainDbContext())
                {
                    Claim sessionEmail = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Email);
                    string userEmail = sessionEmail.Value;
                    var userIdQuery = db.Users.Where(u => u.Email == userEmail).Select(u => u.Id);
                    var userId = userIdQuery.ToList();

                    var dbList = db.List.Create();
                    dbList.Details = list.Details;
                    dbList.Date_Posted = dateToday;
                    dbList.Time_Posted = timeToday;
                    dbList.User_Id = userId[0];
                    if (list.Check_Public)
                    {
                        dbList.Public = "YES";
                    }
                    else
                    {
                        dbList.Public = "NO";
                    }
                    db.List.Add(dbList);
                    db.SaveChanges();
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect format has been placed");
            }
            return View();
        }
    }
}
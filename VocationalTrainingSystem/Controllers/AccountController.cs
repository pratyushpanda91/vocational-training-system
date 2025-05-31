using Microsoft.AspNetCore.Mvc;
using VocationalTrainingSystem.Models;
using VocationalTrainingSystem.Data; // Assuming your DbContext is here
using System;

namespace VocationalTrainingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    var admin = new Admin
                    {
                        UserID = model.UserID,
                        Email = model.Email,
                        Password = model.Password
                    };

                    _context.Admins.Add(admin);
                    _context.SaveChanges();

                    return RedirectToAction("Login", "Home");

                }
                else if (model.Role.Equals("Approver", StringComparison.OrdinalIgnoreCase))
                {
                    var approver = new Approver
                    {
                        UserID = model.UserID,
                        Email = model.Email,
                        Password = model.Password
                    };

                    _context.Approvers.Add(approver);
                    _context.SaveChanges();

                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid role selected.");
                }
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check Admin
                var admin = _context.Admins.FirstOrDefault(a => a.UserID == model.UserID && a.Password == model.Password);
                if (admin != null)
                {
                    return RedirectToAction("AdminDashboard", "Admin");
                }

                // Check Approver
                var approver = _context.Approvers.FirstOrDefault(a => a.UserID == model.UserID && a.Password == model.Password);
                if (approver != null)
                {
                    return RedirectToAction("ApproverDashboard", "Approver");
                }

                ModelState.AddModelError("", "Invalid UserID or Password.");
            }

            return View(model);
        }


    }
}
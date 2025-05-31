using Microsoft.AspNetCore.Mvc;
using VocationalTrainingSystem.Data;
using VocationalTrainingSystem.Models;
using VocationalTrainingSystem.ViewModels;
namespace VocationalTrainingSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        //public IActionResult AdminDashboard()
        //{
        //    return View();
        //}
        public  IActionResult AdminDashboard()
        {
            var Approvers =  _context.Approvers.ToList();
            ViewBag.Approvers = Approvers;
            var Users = _context.UserDetails.ToList();
            ViewBag.Users = Users;
            return View();
        }
        public IActionResult AddUserProfile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUserProfile(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserDetail
                {
                    UserName = model.UserName,
                    FatherName = model.FatherName,
                    MotherName = model.MotherName,
                    PhoneNumber = model.PhoneNumber,
                    Gmail = model.Gmail,
                    AadharNumber = model.AadharNumber,
                    DOB = model.DOB,
                    IdentificationType = model.IdentificationType,
                    Qualification = model.Qualification,
                    College = model.College,
                    Course = model.Course,
                    RegdNo = model.RegdNo,
                    MarksOrCgpa = model.MarksOrCgpa,
                    PassingYear = model.PassingYear,
                    Gender = model.Gender
                };

                try
                {
                    // First add and save the UserDetails to get UserID
                    _context.UserDetails.Add(user);
                    await _context.SaveChangesAsync(); // Generates UserID

                    // Now use the generated UserID for FK tables
                    var presentAddress = new PresentAddress
                    {
                        UserID = user.UserID,  // Get the generated ID
                        At = model.PresentAt,
                        Post = model.PresentPost,
                        Dist = model.PresentDist,
                        TownOrCity = model.PresentTownOrCity,
                        State = model.PresentState,
                        Country = model.PresentCountry,
                        PIN = model.PresentPIN
                    };

                    var permanentAddress = new PermanentAddress
                    {
                        UserID = user.UserID,
                        At = model.PermanentAt,
                        Post = model.PermanentPost,
                        Dist = model.PermanentDist,
                        TownOrCity = model.PermanentTownOrCity,
                        State = model.PermanentState,
                        Country = model.PermanentCountry,
                        PIN = model.PermanentPIN
                    };

                    var image = new UserImage
                    {
                        UserID = user.UserID
                    };

                    if (model.Photo != null && model.Photo.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await model.Photo.CopyToAsync(ms);
                            image.Photo = ms.ToArray();
                        }
                    }

                    _context.PresentAddresses.Add(presentAddress);
                    _context.PermanentAddresses.Add(permanentAddress);
                    _context.UserImages.Add(image);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("AdminDashboard");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while saving user: " + ex.Message);
                    ModelState.AddModelError("", "An error occurred while saving the user data.");
                }
            }
            else
            {
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"Validation error in {key}: {error.ErrorMessage}");
                    }
                }
            }

            return View(model);
        }


    }
}

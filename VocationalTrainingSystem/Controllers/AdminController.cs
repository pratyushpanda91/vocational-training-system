using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private IFormFile ByteArrayToFormFile(byte[] fileBytes, string fileName, string contentType)
        {
            var stream = new MemoryStream(fileBytes);
            return new FormFile(stream, 0, fileBytes.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };
        }

        //public IActionResult AdminDashboard()
        //{
        //    return View();
        //}
        public IActionResult AdminDashboard()
        {
            var Approvers = _context.Approvers.ToList();
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
                    UserID = model.UserId, // Assuming UserId is generated or provided
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
        public IActionResult ViewInfo(int? id)
        {
            if (id == null || id == 0)
            { return NotFound(); }
            var uDetails = _context.UserDetails.Find(id);
            var preAdd = _context.PresentAddresses.Find(id);
            var parAdd = _context.PermanentAddresses.Find(id);
            var uImage = _context.UserImages.Find(id);
            var ViewModel = new UserProfileViewModel
            {
                UserId = uDetails.UserID,
                UserName = uDetails.UserName,
                FatherName = uDetails.FatherName,
                MotherName = uDetails.MotherName,
                PhoneNumber = uDetails.PhoneNumber,
                Gmail = uDetails.Gmail,
                AadharNumber = uDetails.AadharNumber,
                DOB = uDetails.DOB,
                IdentificationType = uDetails.IdentificationType,
                Qualification = uDetails.Qualification,
                College = uDetails.College,
                Course = uDetails.Course,
                RegdNo = uDetails.RegdNo,
                MarksOrCgpa = uDetails.MarksOrCgpa,
                PassingYear = uDetails.PassingYear,
                Gender = uDetails.Gender,
                PresentAt = preAdd.At,
                PresentPost = preAdd.Post,
                PresentDist = preAdd.Dist,
                PresentTownOrCity = preAdd.TownOrCity,
                PresentState = preAdd.State,
                PresentCountry = preAdd.Country,
                PresentPIN = preAdd.PIN,
                PermanentAt = parAdd.At,
                PermanentPost = parAdd.Post,
                PermanentDist = parAdd.Dist,
                PermanentTownOrCity = parAdd.TownOrCity,
                PermanentState = parAdd.State,
                PermanentCountry = parAdd.Country,
                PermanentPIN = parAdd.PIN,
                Photo = uImage.Photo != null ? ByteArrayToFormFile(uImage.Photo, "user_photo.jpg", "image/jpeg") : null
            };
            return View(ViewModel);
        }
        [HttpGet]
        public IActionResult EditInfo(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var user = _context.UserDetails.FirstOrDefault(u => u.UserID == id);
            var preAdd = _context.PresentAddresses.FirstOrDefault(p => p.UserID == id);
            var parAdd = _context.PermanentAddresses.FirstOrDefault(p => p.UserID == id);
            var image = _context.UserImages.FirstOrDefault(i => i.UserID == id);

            if (user == null || preAdd == null || parAdd == null || image == null)
                return NotFound();

            var viewModel = new UserProfileViewModel
            {
                UserId = user.UserID,
                UserName = user.UserName,
                FatherName = user.FatherName,
                MotherName = user.MotherName,
                PhoneNumber = user.PhoneNumber,
                Gmail = user.Gmail,
                AadharNumber = user.AadharNumber,
                DOB = user.DOB,
                IdentificationType = user.IdentificationType,
                Qualification = user.Qualification,
                College = user.College,
                Course = user.Course,
                RegdNo = user.RegdNo,
                MarksOrCgpa = user.MarksOrCgpa,
                PassingYear = user.PassingYear,
                Gender = user.Gender,

                PresentAt = preAdd.At,
                PresentPost = preAdd.Post,
                PresentDist = preAdd.Dist,
                PresentTownOrCity = preAdd.TownOrCity,
                PresentState = preAdd.State,
                PresentCountry = preAdd.Country,
                PresentPIN = preAdd.PIN,

                PermanentAt = parAdd.At,
                PermanentPost = parAdd.Post,
                PermanentDist = parAdd.Dist,
                PermanentTownOrCity = parAdd.TownOrCity,
                PermanentState = parAdd.State,
                PermanentCountry = parAdd.Country,
                PermanentPIN = parAdd.PIN,

                Photo = image.Photo != null ? ByteArrayToFormFile(image.Photo, "user_photo.jpg", "image/jpeg") : null
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditInfo(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.UserDetails.FirstOrDefaultAsync(u => u.UserID == model.UserId);
            var presentAddress = await _context.PresentAddresses.FirstOrDefaultAsync(p => p.UserID == model.UserId);
            var permanentAddress = await _context.PermanentAddresses.FirstOrDefaultAsync(p => p.UserID == model.UserId);
            var userImage = await _context.UserImages.FirstOrDefaultAsync(i => i.UserID == model.UserId);

            if (user == null || presentAddress == null || permanentAddress == null || userImage == null)
            {
                return NotFound();
            }

            // Update fields
            user.UserName = model.UserName;
            user.FatherName = model.FatherName;
            user.MotherName = model.MotherName;
            user.PhoneNumber = model.PhoneNumber;
            user.Gmail = model.Gmail;
            user.AadharNumber = model.AadharNumber;
            user.DOB = model.DOB;
            user.IdentificationType = model.IdentificationType;
            user.Qualification = model.Qualification;
            user.College = model.College;
            user.Course = model.Course;
            user.RegdNo = model.RegdNo;
            user.MarksOrCgpa = model.MarksOrCgpa;
            user.PassingYear = model.PassingYear;
            user.Gender = model.Gender;

            presentAddress.At = model.PresentAt;
            presentAddress.Post = model.PresentPost;
            presentAddress.Dist = model.PresentDist;
            presentAddress.TownOrCity = model.PresentTownOrCity;
            presentAddress.State = model.PresentState;
            presentAddress.Country = model.PresentCountry;
            presentAddress.PIN = model.PresentPIN;

            permanentAddress.At = model.PermanentAt;
            permanentAddress.Post = model.PermanentPost;
            permanentAddress.Dist = model.PermanentDist;
            permanentAddress.TownOrCity = model.PermanentTownOrCity;
            permanentAddress.State = model.PermanentState;
            permanentAddress.Country = model.PermanentCountry;
            permanentAddress.PIN = model.PermanentPIN;

            if (model.Photo != null && model.Photo.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await model.Photo.CopyToAsync(ms);
                    userImage.Photo = ms.ToArray();
                }
            }

            // Save changes - no Add or Update needed
            await _context.SaveChangesAsync();

            return RedirectToAction("AdminDashboard");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.UserDetails.FindAsync(id);
            var presentAddress = await _context.PresentAddresses.FirstOrDefaultAsync(p => p.UserID == id);
            var permanentAddress = await _context.PermanentAddresses.FirstOrDefaultAsync(p => p.UserID == id);
            var userImage = await _context.UserImages.FirstOrDefaultAsync(i => i.UserID == id);

            if (user == null)
            {
                return NotFound();
            }

            // Remove related records first
            if (presentAddress != null)
                _context.PresentAddresses.Remove(presentAddress);

            if (permanentAddress != null)
                _context.PermanentAddresses.Remove(permanentAddress);

            if (userImage != null)
                _context.UserImages.Remove(userImage);

            _context.UserDetails.Remove(user);

            await _context.SaveChangesAsync();

            return RedirectToAction("AdminDashboard");
        }

    }
}

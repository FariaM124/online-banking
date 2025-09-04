using OnlineBanking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Text;

namespace OnlineBanking.Controllers
{
    public class RegisterLoginController : Controller
    {
        BankingDBEntities db = new BankingDBEntities();

        // GET: RegisterLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserTable UT)
        {
            var LoginReg = db.UserTables.FirstOrDefault(model => model.Email == UT.Email && model.Password == UT.Password && model.Role == UT.Role);
            if (LoginReg != null)
            {
                Session["Role"] = UT.Role;
                string role = Session["Role"].ToString();

                if (role == "Customer")
                {
                    Session["UserID"] = LoginReg.UserID;
                    Session["Email"] = LoginReg.Email;
                    Session["FirstName"] = LoginReg.FirstName;
                    Session["LastName"] = LoginReg.LastName;

                    var getAcc = db.AccountsTables.FirstOrDefault(model => model.UserID == LoginReg.UserID);
                    Session["AccountNo"] = getAcc.AccountNumber;
                    Session["Balance"] = getAcc.Balance;
                    Session["AccType"] = getAcc.AccountType;
                    Session["AccountID"] = getAcc.AccountID;

                    // Redirect to Dashboard
                    TempData["LoginSuccessMessage"] = "<script>alert('Login Successfully!!')</script>";
                    return RedirectToAction("Index", "Dashboard");
                }
                else if (role == "Admin")
                {
                    Session["UserID"] = LoginReg.UserID;
                    Session["Email"] = LoginReg.Email;
                    Session["FirstName"] = LoginReg.FirstName;
                    Session["LastName"] = LoginReg.LastName;

                    // Redirect to Dashboard
                    TempData["LoginSuccessMessage"] = "<script>alert('Login Successfully!!')</script>";
                    return RedirectToAction("AdminIndex", "Dashboard");
                }
                else
                {
                    ViewBag.ErrorMessage = "<script>alert('Username or Password incorrect!!')</script>";
                    return View();
                }

            }
            else
            {
                ViewBag.ErrorMessage = "<script>alert('Username or Password incorrect!!')</script>";
                return View();
            }
        }

        //public ActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Register(UserTable UT)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            // Assuming UT is an instance of UserTable with the necessary data
        //            UserTable UTR = new UserTable();
        //            UTR.FirstName = UT.FirstName;
        //            UTR.LastName = UT.LastName;
        //            UTR.DOB = UT.DOB;
        //            UTR.Email = UT.Email;
        //            UTR.Phone = UT.Phone;
        //            UTR.City = UT.City;
        //            UTR.Password = UT.Password;

        //            // Add new user to UserTable
        //            db.UserTables.Add(UTR);
        //            db.SaveChanges();

        //            // Retrieve the generated UserID
        //            int newUserID = UTR.UserID;

        //            // Create new account with the retrieved UserID
        //            AccountsTable At = new AccountsTable();
        //            At.UserID = newUserID;
        //            At.AccountType = UT.AccountType;
        //            At.Balance = UT.Balance;
        //            At.DateOpened = DateTime.Now; // Assuming you want to set the current date

        //            // Add new account to AccountsTable
        //            db.AccountsTables.Add(At);
        //            db.SaveChanges();

        //            ViewBag.InsertMessage = "<script>alert('Registeration Successfull')</script>";
        //            ModelState.Clear();
        //        }
        //        catch (DbEntityValidationException ex)
        //        {
        //            // Exception occurred due to validation errors
        //            foreach (var validationErrors in ex.EntityValidationErrors)
        //            {
        //                foreach (var validationError in validationErrors.ValidationErrors)
        //                {
        //                    // Log or handle each validation error
        //                    ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
        //                }
        //            }

        //            // Handle the exception or provide feedback to the user
        //            ViewBag.InsertMessage = "<script>alert('Registeration Successfull')</script>";
        //        }
        //    }

        //    // Return to the view with appropriate feedback
        //    return View();
        //}

        public ActionResult NewCustomer()
        {
            // Check if user is logged in as admin
            if (Session["UserID"] == null)
            {
                // Redirect to login if not logged in
                return RedirectToAction("Index", "RegisterLogin");
            }
            
            string role = Session["Role"]?.ToString();
            if (role != "Admin")
            {
                // Redirect non-admin users to appropriate dashboard
                if (role == "Customer")
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return RedirectToAction("Index", "RegisterLogin");
                }
            }
            
            // No need to transfer TempData to ViewBag as we're now directly using TempData in the view
            // Keep TempData for error messages for backward compatibility
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            
            // We're now accessing TempData["SuccessMessage"] directly in the view
            // so we don't need to copy it to ViewBag
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewCustomer(UserTable UT)
        {
            if (ModelState.IsValid)
            {
                // Check if this is an update (user exists) or new registration
                var existingUser = db.UserTables.FirstOrDefault(u => u.Email == UT.Email || u.CNIC == UT.CNIC);
                
                if (existingUser != null)
                {
                    // Update existing user
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            // Update user details
                            existingUser.FirstName = UT.FirstName;
                            existingUser.LastName = UT.LastName;
                            existingUser.Gender = UT.Gender;
                            existingUser.Phone = UT.Phone;
                            existingUser.Marital_Status = UT.Marital_Status;
                            existingUser.DOB = UT.DOB;
                            existingUser.City = UT.City;
                            existingUser.CNIC = UT.CNIC;
                            existingUser.Email = UT.Email;
                            existingUser.Password = UT.Password;
                            existingUser.Role = UT.Role;
                            
                            // Update associated account
                            var account = db.AccountsTables.FirstOrDefault(a => a.UserID == existingUser.UserID);
                            if (account != null)
                            {
                                account.AccountType = UT.AccountType;
                                account.AccountStatus = UT.AccountStatus;
                                account.Balance = UT.Balance;
                            }
                            
                            // Save all changes
                            db.SaveChanges();
                            
                            // Commit the transaction
                            dbContextTransaction.Commit();
                            
                            // Set success message using TempData
                            TempData["SuccessMessage"] = "Customer updated successfully!";
                            TempData["Debug"] = "TempData set for update at " + DateTime.Now.ToString();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            // Roll back the transaction
                            dbContextTransaction.Rollback();
                            
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("Validation errors:");
                            
                            foreach (var entity in ex.EntityValidationErrors)
                            {
                                sb.AppendLine($"Entity: {entity.Entry.Entity.GetType().Name}");
                                foreach (var error in entity.ValidationErrors)
                                {
                                    sb.AppendLine($"- Property: {error.PropertyName}, Error: {error.ErrorMessage}");
                                }
                            }
                            
                            TempData["ErrorMessage"] = sb.ToString();
                            return RedirectToAction("NewCustomer");
                        }
                        catch (Exception ex)
                        {
                            // Roll back the transaction
                            dbContextTransaction.Rollback();
                            
                            TempData["ErrorMessage"] = "Error during update: " + ex.Message;
                            if (ex.InnerException != null)
                            {
                                TempData["ErrorMessage"] += " Inner: " + ex.InnerException.Message;
                            }
                            return RedirectToAction("NewCustomer");
                        }
                    }
                    
                    return RedirectToAction("NewCustomer");
                }
                else
                {
                    // Create new user - directly without transaction first to debug
                    try
                    {
                        // Create new user
                        UserTable UTR = new UserTable();
                        UTR.FirstName = UT.FirstName;
                        UTR.LastName = UT.LastName;
                        UTR.Gender = UT.Gender;
                        UTR.Phone = UT.Phone;
                        UTR.Marital_Status = UT.Marital_Status;
                        UTR.DOB = UT.DOB;
                        UTR.City = UT.City;
                        UTR.CNIC = UT.CNIC;
                        UTR.Email = UT.Email;
                        UTR.Password = UT.Password;
                        UTR.Role = UT.Role;
                        UTR.AccountStatus = UT.AccountStatus;
                        
                        // Add user to database
                        db.UserTables.Add(UTR);
                        
                        
                        TempData["Debug"] = $"User saved with ID: {UTR.UserID}";
                        


                        
                        // Create account
                        AccountsTable At = new AccountsTable();
                        At.UserID = UTR.UserID;
                        
                        At.AccountType = UT.AccountType;
                        At.AccountStatus = UT.AccountStatus;
                        At.Balance = UT.Balance;
                        At.DateOpened = DateTime.Now;
                        
                        // Add account to database
                        db.AccountsTables.Add(At);
                        db.SaveChanges();
                        
                        TempData["SuccessMessage"] = $"New customer created successfully!";
                        TempData["Debug"] += $" | Account Created";
                        
                        return RedirectToAction("NewCustomer");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Validation errors:");
                        
                        foreach (var entity in ex.EntityValidationErrors)
                        {
                            sb.AppendLine($"Entity: {entity.Entry.Entity.GetType().Name}");
                            foreach (var error in entity.ValidationErrors)
                            {
                                sb.AppendLine($"- Property: {error.PropertyName}, Error: {error.ErrorMessage}");
                            }
                        }
                        
                        TempData["ErrorMessage"] = sb.ToString();
                        return RedirectToAction("NewCustomer");
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] = "Error during account creation: " + ex.Message;
                        if (ex.InnerException != null)
                        {
                            TempData["ErrorMessage"] += " Inner: " + ex.InnerException.Message;
                        }
                        return RedirectToAction("NewCustomer");
                    }
                }
            }
            else
            {
                // Collect all model state errors to show
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                ViewBag.ErrorMessage = "Please correct the errors and try again: " + string.Join(" | ", errors);
            }

            return View(UT);
        }


        public ActionResult Search(string AccountNumber)
        {
            if (string.IsNullOrEmpty(AccountNumber))
            {
                return RedirectToAction("NewCustomer");
            }

            // Find account with the provided account number
            var account = db.AccountsTables.FirstOrDefault(a => a.AccountNumber == AccountNumber);
            if (account != null)
            {
                // Find the associated user
                var user = db.UserTables.FirstOrDefault(u => u.UserID == account.UserID);
                if (user != null)
                {
                    // Set account properties to user model for the view
                    user.AccountType = account.AccountType;
                    user.AccountStatus = account.AccountStatus;
                    user.Balance = account.Balance;
                    
                    return View("NewCustomer", user);
                }
            }

            // If no user is found, show error and return to the form
            TempData["ErrorMessage"] = "No customer found with the provided account number.";
            return RedirectToAction("NewCustomer");
        }
    }
}

using OnlineBanking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBanking.Controllers
{
    public class TransferMoneyController : Controller
    {
        BankingDBEntities db = new BankingDBEntities();
        // GET: TransferMoney
        public ActionResult Index()
        {                      
            return View();
        }        
        [HttpPost]        
        public ActionResult Search(StatementTable TA)
        {
            if (TA.ToAccount != null)
            {
                var getAcc = db.AccountsTables.FirstOrDefault(model => model.AccountNumber == TA.ToAccount);
                if (getAcc != null)
                {
                    Session["BAccountNo"] = TA.ToAccount;
                    Session["BUserID"] = getAcc.UserID.ToString();
                    Session["BAccountID"] = getAcc.AccountID.ToString();
                    Session["BBalance"] = getAcc.Balance.ToString();
                    var getUserName = db.UserTables.FirstOrDefault(model => model.UserID == getAcc.UserID);
                    Session["BeneficiaryName"] = getUserName.FirstName;
                    Session["SearchBeneficiary"] = "";

                    return RedirectToAction("Index");
                    
                }
                else
                {
                    Session["SearchBeneficiary"] = "Account not found.!!";
                    return RedirectToAction("Index", "TransferMoney");
                }
            }
            else
            {
                Session["SearchBeneficiary"] = "Enetr Account No.!!";                
                return RedirectToAction("Index", "TransferMoney");
            }                       
        }
        [HttpPost]
        public ActionResult Index(StatementTable ST)
        {
            try
            {
                if (Session["BAccountNo"] != null)
                {
                    decimal currentBal = Convert.ToDecimal(Session["Balance"]);
                    decimal BcurrentBal = Convert.ToDecimal(Session["BBalance"]);
                    if (ST.Amount <= currentBal)
                    {
                        decimal oldbal = currentBal;
                        decimal NewBal = oldbal - ST.Amount;

                        decimal Boldbal = BcurrentBal;
                        decimal BNewBal = Boldbal + ST.Amount;

                        ///////////////////////////////////////////////For Sender
                        // To Update Balance at Accounts Table                                                               
                        int ConvertUserID = Convert.ToInt32(Session["UserID"]);
                        var getAcc = db.AccountsTables.Where(AT => AT.UserID == ConvertUserID).FirstOrDefault();
                        getAcc.Balance = NewBal;
                        db.SaveChanges();
                        (Session["Balance"]) = NewBal;

                        // To record transactions
                        TransactionsTable TT = new TransactionsTable();
                        TT.AccountID = Convert.ToInt32(Session["AccountID"]);
                        TT.TransactionType = "Debit";
                        TT.Amount = ST.Amount;
                        TT.TransactionDate = DateTime.Now;
                        TT.Description = "Money Transfer";
                        db.TransactionsTables.Add(TT);
                        db.SaveChanges();

                        // To record Detailed Statement
                        ST.ThisAccount = Convert.ToInt32(Session["AccountID"]);
                        ST.TDate = DateTime.Now;
                        ST.FromAccount = Session["AccountNo"].ToString();
                        ST.ToAccount = Session["BAccountNo"].ToString();
                        ST.Type = "Money Transfer";
                        ST.OldBalance = Convert.ToInt32(oldbal);
                        ST.NewBalance = Convert.ToInt32(NewBal);
                        db.StatementTables.Add(ST);
                        int c = db.SaveChanges();

                        ///////////////////////////////////////////////For Reciever
                        // To Update Balance at Accounts Table                                                               
                        int BConvertUserID = Convert.ToInt32(Session["BUserID"]);
                        var BgetAcc = db.AccountsTables.Where(AT => AT.UserID == BConvertUserID).FirstOrDefault();
                        BgetAcc.Balance = BNewBal;
                        int BA = db.SaveChanges();

                        // To record transactions
                        TransactionsTable BTT = new TransactionsTable();
                        BTT.AccountID = Convert.ToInt32(Session["BAccountID"]);
                        BTT.TransactionType = "Credit";
                        BTT.Amount = ST.Amount;
                        BTT.TransactionDate = DateTime.Now;
                        BTT.Description = "Fund Received";
                        db.TransactionsTables.Add(BTT);
                        int BB = db.SaveChanges();

                        // To record Detailed Statement
                        ST.ThisAccount = Convert.ToInt32(Session["BAccountID"]);
                        ST.TDate = DateTime.Now;
                        ST.FromAccount = Session["AccountNo"].ToString();
                        ST.ToAccount = Session["BAccountNo"].ToString();
                        ST.Type = "Fund Received";
                        ST.OldBalance = Convert.ToInt32(Boldbal);
                        ST.NewBalance = Convert.ToInt32(BNewBal);
                        db.StatementTables.Add(ST);
                        int BC = db.SaveChanges();
                        if (BC > 0)
                        {
                            ViewBag.ErrorMessage = "<script>alert('Transaction Successfully!!')</script>";
                            ModelState.Clear();
                            return View();
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "<script>alert('Transaction Failed !!')</script>";
                            ModelState.Clear();
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "<script>alert('Insufficient Balance!!')</script>";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "<script>alert('Search Beneficiary First!!')</script>";
                    return View();
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        // Log or handle the detailed error message
                        System.Diagnostics.Debug.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw; // Re-throw the exception to see the stack trace if needed
            }
            return View();
        }

        public ActionResult Balance()
        {            
            return View();
        }
    }
}
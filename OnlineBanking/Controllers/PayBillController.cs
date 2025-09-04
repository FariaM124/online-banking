using OnlineBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBanking.Controllers
{
    public class PayBillController : Controller
    {
        BankingDBEntities db = new BankingDBEntities();
        // GET: PayBill
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(BillTable Bt)
        {
            if (ModelState.IsValid == true)
            {
                if (Bt.Amount <= Convert.ToDecimal(Session["Balance"]))
                {

                    //To Update Balance at Accounts Table                                           
                    decimal NewBalance = Convert.ToDecimal(Session["Balance"]) - Bt.Amount;
                    int ConvertUserID = Convert.ToInt32(Session["UserID"]);
                    var getAcc = db.AccountsTables.Where(AT => AT.UserID == ConvertUserID).FirstOrDefault();
                    getAcc.Balance = NewBalance;
                    //db.SaveChanges();

                    //To update Bill table
                          
                    Bt.AccountID = Convert.ToInt32(Session["AccountID"]);
                    Bt.PaymentDate = DateTime.Now;
                    db.BillTables.Add(Bt);
                    int a = db.SaveChanges();

                    //To record transactions
                    TransactionsTable TT = new TransactionsTable();
                    TT.AccountID = Convert.ToInt32(Session["AccountID"]);
                    TT.TransactionType = "Bill Payment";                   
                    TT.Amount = Bt.Amount;
                    TT.TransactionDate = DateTime.Now;
                    TT.Description = Bt.BillType;
                    db.TransactionsTables.Add(TT);
                    //db.SaveChanges();

                    StatementTable STB = new StatementTable();
                    STB.ThisAccount = Convert.ToInt32(Session["AccountID"]);
                    STB.TDate = DateTime.Now;
                    STB.FromAccount = Session["AccountNo"].ToString();
                    STB.ToAccount = "-";
                    STB.Type = "Bill Payment";
                    STB.OldBalance = Convert.ToInt32(Session["Balance"]);
                    STB.Amount = Convert.ToInt32(Session["Balance"]);
                    STB.NewBalance = Convert.ToInt32(Bt.Amount);
                    db.StatementTables.Add(STB);
                    db.SaveChanges();
                    (Session["Balance"]) = NewBalance;                
                    if (a > 0)
                    {
                        ViewBag.ErrorMessage = "<script>alert('Bill Payment Successful!!')</script>";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "<script>alert('Bill Payment Failed!!')</script>";
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "<script>alert('Insufficient Balance!!')</script>";
                    return View();
                }
                return View();

            }
            return View();

        }
    }
}
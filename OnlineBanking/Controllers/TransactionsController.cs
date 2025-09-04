using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineBanking.Models;

namespace OnlineBanking.Controllers
{
    public class TransactionsController : Controller
    {
        private BankingDBEntities db = new BankingDBEntities();

        // GET: Transactions
        public ActionResult Index(DateTime? dateFrom, DateTime? dateTo)
        {
            string CurrentUser = Session["AccountNo"].ToString();

            // It will make sure that the user can only see their Account Statement
            var transactions = db.TransactionsTables
                                .Include(t => t.AccountsTable)
                                .Where(t => t.AccountsTable.AccountNumber == CurrentUser);

            // Apply date filters if specified
            if (dateFrom.HasValue)
            {
                transactions = transactions.Where(t => t.TransactionDate >= dateFrom.Value);
            }

            if (dateTo.HasValue)
            {
                transactions = transactions.Where(t => t.TransactionDate <= dateTo.Value);
            }

            return View(transactions.ToList());
        }


        public ActionResult Detailed(DateTime? dateFrom, DateTime? dateTo)
        {
            int CurrentUser = Convert.ToInt32(Session["AccountID"]);

            // It will make sure that the user can only see their Account Statement
            var transactions = db.StatementTables
                                .Include(ST => ST.AccountsTable)
                                .Where(ST => ST.AccountsTable.AccountID == CurrentUser);

            // Apply date filters if specified
            if (dateFrom.HasValue)
            {
                transactions = transactions.Where(t => t.TDate >= dateFrom.Value);
            }

            if (dateTo.HasValue)
            {
                transactions = transactions.Where(t => t.TDate <= dateTo.Value);
            }

            return View(transactions.ToList());
        }


        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionsTable transactionsTable = db.TransactionsTables.Find(id);
            if (transactionsTable == null)
            {
                return HttpNotFound();
            }
            return View(transactionsTable);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(db.AccountsTables, "AccountID", "AccountNumber");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TID,AccountID,TransactionType,Amount,TransactionDate,Description")] TransactionsTable transactionsTable)
        {
            if (ModelState.IsValid)
            {
                db.TransactionsTables.Add(transactionsTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(db.AccountsTables, "AccountID", "AccountNumber", transactionsTable.AccountID);
            return View(transactionsTable);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionsTable transactionsTable = db.TransactionsTables.Find(id);
            if (transactionsTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.AccountsTables, "AccountID", "AccountNumber", transactionsTable.AccountID);
            return View(transactionsTable);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TID,AccountID,TransactionType,Amount,TransactionDate,Description")] TransactionsTable transactionsTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactionsTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.AccountsTables, "AccountID", "AccountNumber", transactionsTable.AccountID);
            return View(transactionsTable);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionsTable transactionsTable = db.TransactionsTables.Find(id);
            if (transactionsTable == null)
            {
                return HttpNotFound();
            }
            return View(transactionsTable);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionsTable transactionsTable = db.TransactionsTables.Find(id);
            db.TransactionsTables.Remove(transactionsTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

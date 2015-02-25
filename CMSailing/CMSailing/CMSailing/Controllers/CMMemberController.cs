using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMSailing.Models;
using CMSailing.App_GlobalResources;

namespace CMSailing.Controllers
{
    public class CMMemberController : Controller
    {
        private sailContext db = new sailContext();

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            CMLanguageController.SetLanguages(Request.Cookies);
        }

        // GET: CMMember
        public ActionResult Index()
        {
            var members = db.members.Include(m => m.province);
            return View(members.ToList());
        }

        // GET: CMMember/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            member member = db.members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: CMMember/Create
        public ActionResult Create()
        {
            ViewBag.provinceCode = new SelectList(db.provinces, "provinceCode", "name");
            return View();
        }

        // POST: CMMember/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "memberId,fullName,firstName,lastName,spouseFirstName,spouseLastName,street,city,provinceCode,postalCode,homePhone,email,yearJoined,comment,taskExempt,useCanadaPost")] member member)
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    db.members.Add(member);
                    db.SaveChanges();
                    TempData["meaage"] = Tranlations.createSucess;
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    TempData["error"] = "Error saving to database" + e.GetBaseException();

                    Create();
                    return View(member);
                }
            }

            ViewBag.provinceCode = new SelectList(db.provinces, "provinceCode", "name", member.provinceCode);
            return View(member);
        }

        // GET: CMMember/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            member member = db.members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }

            Response.Cookies.Add(new HttpCookie("currentMember", member.memberId.ToString()));
            ViewBag.provinceCode = new SelectList(db.provinces, "provinceCode", "name", member.provinceCode);
            return View(member);
        }

        // POST: CMMember/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "memberId,fullName,firstName,lastName,spouseFirstName,spouseLastName,street,city,provinceCode,postalCode,homePhone,email,yearJoined,comment,taskExempt,useCanadaPost")] member member)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    db.Entry(member).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["message"] = Tranlations.editSucess;
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["error"] = "Error saving to database: " + e.GetBaseException().Message;

                    ViewBag.provinceCode = new SelectList(db.provinces, "provinceCode", "name", member.provinceCode);
                    return View(member);
                }
            }
            ViewBag.provinceCode = new SelectList(db.provinces, "provinceCode", "name", member.provinceCode);
            return View(member);
        }

        // GET: CMMember/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            member member = db.members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: CMMember/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            member member = db.members.Find(id);
            db.members.Remove(member);
            db.SaveChanges();
            TempData["message"] = Tranlations.deleteSucess;
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

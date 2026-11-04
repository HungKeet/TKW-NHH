using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.Controllers;

public class LearnerController : Controller
{
    private readonly SchoolContext db;
    private int pageSize = 3;

    public LearnerController(SchoolContext context)
    {
        db = context;
    }

    public IActionResult Index(int? mid)
    {
        var learners = (IQueryable<Learner>)db.Learners.Include(m => m.Major);
        if (mid != null)
        {
            learners = (IQueryable<Learner>)db.Learners.Where(l => l.MajorID == mid).Include(m => m.Major);
            ViewBag.mid = mid;
        }
        int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
        ViewBag.pageNum = pageNum;
        var result = learners.Take(pageSize).ToList();
        return View(result);
    }

    public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
    {
        var learners = (IQueryable<Learner>)db.Learners;
        int page = (int)(pageIndex == null || pageIndex <= 0 ? 1 : pageIndex);

        if (mid != null)
        {
            learners = learners.Where(l => l.MajorID == mid);
            ViewBag.mid = mid;
        }

        if (!string.IsNullOrEmpty(keyword))
        {
            learners = learners.Where(l => l.FirstMidName.ToLower().Contains(keyword.ToLower()));
            ViewBag.keyword = keyword;
        }

        int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
        ViewBag.pageNum = pageNum;

        var result = learners.Skip(pageSize * (page - 1)).Take(pageSize).Include(m => m.Major).ToList();
        return PartialView("LearnerTable", result);
    }

    public IActionResult Create()
    {
        ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("FirstMidName, LastName, MajorID, EnrollmentDate")] Learner learner)
    {
        if (ModelState.IsValid)
        {
            db.Learners.Add(learner);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
        return View(learner);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || db.Learners == null)
        {
            return NotFound();
        }

        var learner = db.Learners.Find(id);
        if (learner == null)
        {
            return NotFound();
        }

        ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
        return View(learner);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("LearnerID, FirstMidName, LastName, MajorID, EnrollmentDate")] Learner learner)
    {
        if (id != learner.LearnerID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                db.Update(learner);
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LearnerExists(learner.LearnerID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
        return View(learner);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || db.Learners == null)
        {
            return NotFound();
        }

        var learner = db.Learners.Include(l => l.Major).FirstOrDefault(m => m.LearnerID == id);
        if (learner == null)
        {
            return NotFound();
        }

        return View(learner);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (db.Learners == null)
        {
            return Problem("Entity set 'SchoolContext.Learners' is null.");
        }
        var learner = db.Learners.Find(id);
        if (learner != null)
        {
            db.Learners.Remove(learner);
        }

        db.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool LearnerExists(int id)
    {
        return (db.Learners?.Any(e => e.LearnerID == id)).GetValueOrDefault();
    }
}

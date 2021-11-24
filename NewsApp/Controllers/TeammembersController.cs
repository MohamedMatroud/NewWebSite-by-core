using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsApp.Models;

namespace NewsApp.Controllers
{
    public class TeammembersController : Controller
    {
        private readonly NewsContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TeammembersController(NewsContext context,
            IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Teammembers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teammembers.ToListAsync());
        }

        // GET: Teammembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teammembers = await _context.Teammembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teammembers == null)
            {
                return NotFound();
            }

            return View(teammembers);
        }

        // GET: Teammembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teammembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,JobTitle,TeameImage")] Teammembers teammembers)
        {
            if (ModelState.IsValid)
            {


                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(teammembers.TeameImage.FileName);
                string extension = Path.GetExtension(teammembers.TeameImage.FileName);
                teammembers.Image = fileName = fileName + extension;
                string path = Path.Combine(wwwRootPath + "/image", fileName);

                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await teammembers.TeameImage.CopyToAsync(filestream);
                }

                _context.Add(teammembers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teammembers);
        }

        // GET: Teammembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teammembers = await _context.Teammembers.FindAsync(id);
            if (teammembers == null)
            {
                return NotFound();
            }
            return View(teammembers);
        }

        // POST: Teammembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TeameImage,Image,JobTitle")] Teammembers teammembers)
        {
            if (id != teammembers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    if (teammembers.TeameImage != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(teammembers.TeameImage.FileName);
                        string extension = Path.GetExtension(teammembers.TeameImage.FileName);
                        teammembers.Image = fileName = fileName + extension;
                        string path = Path.Combine(wwwRootPath + "/image", fileName);

                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await teammembers.TeameImage.CopyToAsync(filestream);
                        }
                    }
                    else
                    {
                        var imagefile = Path.Combine(_hostEnvironment.WebRootPath + "image", teammembers.Image);
                    }


                    _context.Update(teammembers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeammembersExists(teammembers.Id))
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
            return View(teammembers);
        }

        // GET: Teammembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teammembers = await _context.Teammembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teammembers == null)
            {
                return NotFound();
            }

            return View(teammembers);
        }

        // POST: Teammembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teammembers = await _context.Teammembers.FindAsync(id);
            _context.Teammembers.Remove(teammembers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeammembersExists(int id)
        {
            return _context.Teammembers.Any(e => e.Id == id);
        }
    }
}

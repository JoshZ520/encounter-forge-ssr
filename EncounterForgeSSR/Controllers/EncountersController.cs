using EncounterForgeSSR.Data;
using EncounterForgeSSR.Models;
using EncounterForgeSSR.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EncounterForgeSSR.Controllers;

public class EncountersController : Controller
{
    private readonly AppDbContext _context;

    public EncountersController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var encounters = await _context.Encounters
            .OrderByDescending(encounter => encounter.UpdatedAtUtc)
            .ToListAsync();

        return View(encounters);
    }

    public IActionResult Create()
    {
        return View(new EncounterFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EncounterFormViewModel form)
    {
        if (form.MonsterEntries.Count == 0)
        {
            ModelState.AddModelError(nameof(form.MonsterEntries), "Add at least one monster.");
        }

        if (!ModelState.IsValid)
        {
            return View(form);
        }

        var encounter = new Encounter
        {
            Name = form.Name,
            PartyLevel = form.PartyLevel,
            PartySize = form.PartySize,
            Environment = form.Environment,
            Difficulty = form.Difficulty,
            Notes = form.Notes,
            CreatedAtUtc = DateTime.UtcNow,
            UpdatedAtUtc = DateTime.UtcNow,
            MonsterEntries = form.MonsterEntries
                .Select((monster, index) => new MonsterEntry
                {
                    Name = monster.Name,
                    Quantity = monster.Quantity,
                    ChallengeRating = monster.ChallengeRating,
                    Notes = monster.Notes,
                    SortOrder = index
                })
                .ToList()
        };

        _context.Encounters.Add(encounter);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Details), new { id = encounter.Id });
    }

    public async Task<IActionResult> Details(int id)
    {
        var encounter = await _context.Encounters
            .Include(encounter => encounter.MonsterEntries)
            .FirstOrDefaultAsync(encounter => encounter.Id == id);

        if (encounter is null)
        {
            TempData["ErrorMessage"] = "Encounter not found or already deleted.";
            return RedirectToAction(nameof(Index));
        }

        return View(encounter);
    }
}
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

        if (form.MonsterEntries.Any(monster => string.IsNullOrWhiteSpace(monster.Name)))
        {
            ModelState.AddModelError(nameof(form.MonsterEntries), "Monster name is required.");
        }

        if (!ModelState.IsValid)
        {
            return View(form);
        }

        var encounter = BuildEncounterFromForm(form);

        _context.Encounters.Add(encounter);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Encounter created.";

        return RedirectToAction(nameof(Details), new { id = encounter.Id });
    }

    public async Task<IActionResult> Edit(int id)
    {
        var encounter = await _context.Encounters
            .Include(e => e.MonsterEntries)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (encounter is null)
        {
            TempData["ErrorMessage"] = "Encounter not found or already deleted.";
            return RedirectToAction(nameof(Index));
        }

        return View(BuildFormFromEncounter(encounter));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EncounterFormViewModel form)
    {
        if (id != form.Id)
        {
            TempData["ErrorMessage"] = "Encounter not found or already deleted.";
            return RedirectToAction(nameof(Index));
        }

        if (form.MonsterEntries.Count == 0)
        {
            ModelState.AddModelError(nameof(form.MonsterEntries), "Add at least one monster.");
        }

        if (form.MonsterEntries.Any(monster => string.IsNullOrWhiteSpace(monster.Name)))
        {
            ModelState.AddModelError(nameof(form.MonsterEntries), "Monster name is required.");
        }

        if (!ModelState.IsValid)
        {
            return View(form);
        }

        var encounter = await _context.Encounters
            .Include(e => e.MonsterEntries)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (encounter is null)
        {
            TempData["ErrorMessage"] = "Encounter not found or already deleted.";
            return RedirectToAction(nameof(Index));
        }

        encounter.Name = form.Name;
        encounter.PartyLevel = form.PartyLevel;
        encounter.PartySize = form.PartySize;
        encounter.Environment = form.Environment;
        encounter.Difficulty = form.Difficulty;
        encounter.Notes = form.Notes;
        encounter.UpdatedAtUtc = DateTime.UtcNow;

        _context.MonsterEntries.RemoveRange(encounter.MonsterEntries);
        encounter.MonsterEntries = form.MonsterEntries
            .Select((monster, index) => new MonsterEntry
            {
                Name = monster.Name,
                Quantity = monster.Quantity,
                ChallengeRating = monster.ChallengeRating,
                Notes = monster.Notes,
                SortOrder = index
            })
            .ToList();

        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Encounter updated.";
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var encounter = await _context.Encounters.FindAsync(id);

        if (encounter is null)
        {
            TempData["ErrorMessage"] = "Encounter not found or already deleted.";
            return RedirectToAction(nameof(Index));
        }

        _context.Encounters.Remove(encounter);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Encounter deleted.";
        return RedirectToAction(nameof(Index));
    }

    private static Encounter BuildEncounterFromForm(EncounterFormViewModel form)
    {
        return new Encounter
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
    }

    private static EncounterFormViewModel BuildFormFromEncounter(Encounter encounter)
    {
        return new EncounterFormViewModel
        {
            Id = encounter.Id,
            Name = encounter.Name,
            PartyLevel = encounter.PartyLevel,
            PartySize = encounter.PartySize,
            Environment = encounter.Environment,
            Difficulty = encounter.Difficulty,
            Notes = encounter.Notes,
            MonsterEntries = encounter.MonsterEntries
                .OrderBy(monster => monster.SortOrder)
                .Select(monster => new MonsterEntry
                {
                    Id = monster.Id,
                    EncounterId = monster.EncounterId,
                    Name = monster.Name,
                    Quantity = monster.Quantity,
                    ChallengeRating = monster.ChallengeRating,
                    Notes = monster.Notes,
                    SortOrder = monster.SortOrder
                })
                .ToList()
        };
    }
}
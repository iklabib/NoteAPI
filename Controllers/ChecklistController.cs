using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[Authorize]
public class ChecklistController(AppsContext appsContext) : Controller
{
    [HttpGet("checklist")]
    public IActionResult Checklist()
    {
        try
        {
            var checklists = appsContext.Checklists.Select(el => new { el.ChecklistId, el.ChecklistName });
            return Json(new { success = true, message = "Success", checklist = checklists });
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "Internal Server Error" });
        }
    }

    [HttpPost("checklist")]
    public IActionResult NewChecklist([FromBody] NewChecklistDTO request)
    {
        try
        {
            var checklist = new Checklist
            {
                ChecklistName = request.Name,
            };
            appsContext.Checklists.Add(checklist);
            appsContext.SaveChanges();

            return Json(new { success = true, message = "Success", checklistId = checklist.ChecklistId });
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "Internal Server Error" });
        }
    }

    [HttpDelete("checklist/{checklistId}")]
    public IActionResult DeleteChecklist(int checklistId)
    {
        try
        {
            var checklist = appsContext.Checklists.FirstOrDefault(el => el.ChecklistId == checklistId);
            if (checklist == null)
            {
                return Json(new { success = false, message = "No Checklist Found" });
            }

            appsContext.Checklists.Remove(checklist);
            appsContext.SaveChanges();

            return Json(new { success = true, message = "Success" });
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "Internal Server Error" });
        }
    }

    [HttpGet("checklist/{checklistId}/item")]
    public IActionResult ChecklistItems(int checklistId)
    {
        try
        {
            var checklist = appsContext.Checklists.FirstOrDefault(el => el.ChecklistId == checklistId);
            if (checklist == null)
            {
                return Json(new { success = false, message = "No Checklist Found" });
            }

            return Json(new { success = true, message = "Success", checklist = checklist.Items });
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "Internal Server Error" });
        }
    }

    [HttpPost("checklist/{checklistId}/item")]
    public IActionResult NewChecklistItem([FromRoute] int checklistId, [FromBody] NewChecklistItemDTO request)
    {
        try
        {
            var checklist = appsContext.Checklists.FirstOrDefault(el => el.ChecklistId == checklistId);
            if (checklist == null)
            {
                return Json(new { success = false, message = "No Checklist Found" });
            }

            var item = new ChecklistItem
            { ChecklistId = checklistId, ChecklistItemName = request.Name };
            checklist.Items.Add(item);
            appsContext.Checklists.Update(checklist);
            appsContext.SaveChanges();

            return Json(new { success = true, message = "Success", checklistItemId = item.ChecklistItemId });
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "Internal Server Error" });
        }
    }

    [HttpGet("checklist/{checklistId}/item/{checklistItemId}")]
    public IActionResult ChecklistItem([FromRoute] int checklistId, [FromRoute] int checklistItemId)
    {
        try
        {
            var checklist = appsContext.Checklists.FirstOrDefault(el => el.ChecklistId == checklistId);
            if (checklist == null)
            {
                return Json(new { success = false, message = "No Checklist Found" });
            }

            var item = checklist.Items.FirstOrDefault(el => el.ChecklistItemId == checklistItemId);
            if (item == null)
            {
                return Json(new { success = false, message = "No Checklist Item Found", });
            }

            return Json(new { success = true, message = "Success", checklistItem = item });
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "Internal Server Error" });
        }
    }

    [HttpPut("checklist/{checklistId}/item/{checklistItemId}")]
    public IActionResult UpdateChecklistItem([FromRoute] int checklistId, [FromRoute] int checklistItemId, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] UpdateChecklistItemDTO request)
    {
        try
        {
            var checklist = appsContext.Checklists.FirstOrDefault(el => el.ChecklistId == checklistId);
            if (checklist == null)
            {
                return Json(new { success = false, message = "No Checklist Found" });
            }

            var item = checklist.Items.FirstOrDefault(el => el.ChecklistItemId == checklistItemId);
            if (item == null)
            {
                return Json(new { success = false, message = "No Checklist Item Found", });
            }

            if (request == null)
            {
                item.Done = !item.Done;
            }
            else
            {
                item.ChecklistItemName = request.Name;
            }

            appsContext.Checklists.Update(checklist);
            appsContext.SaveChanges();

            return Json(new { success = true, message = "Success" });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = "Internal Server Error" });
        }
    }

    [HttpDelete("checklist/{checklistId}/item/{checklistItemId}")]
    public IActionResult DeleteChecklistItem([FromRoute] int checklistId, [FromRoute] int checklistItemId)
    {
        try
        {
            var checklist = appsContext.Checklists.FirstOrDefault(el => el.ChecklistId == checklistId);
            if (checklist == null)
            {
                return Json(new { success = false, message = "No Checklist Found" });
            }

            var item = checklist.Items.FirstOrDefault(el => el.ChecklistItemId == checklistItemId);
            if (item == null)
            {
                return Json(new { success = false, message = "No Checklist Item Found", });
            }

            checklist.Items.Remove(item);
            appsContext.Checklists.Update(checklist);
            appsContext.SaveChanges();

            return Json(new { success = true, message = "Success" });
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "Internal Server Error" });
        }
    }
}
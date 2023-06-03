using FurryFeast.Models;
using FurryFeast.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.API;

[Route("api/recipes/[action]")]
[ApiController]
public class RecipesApiController : ControllerBase
{
    private readonly db_a989fb_furryfeastContext _context;

    public RecipesApiController(db_a989fb_furryfeastContext context)
    {
        _context = context;
    }

    public object AllData()
    {
        return _context.Recipes
            .Include(x => x.MsgBoards)
            .ThenInclude(m => m.EditedMsgRecords)
            .Select(x => new
            {
                AllData = new
                {
                    name = x.RecipesName,
                    recipesId = x.RecipesId,
                    petTypesId = x.PetTypesId,
                    desc = x.RecipesDescription,
                    data = x.RecipesData,
                    method = x.RecipesMethod,
                    notes = x.RecipesNotes,
                    MsgBoards = x.MsgBoards.Select(mb => new
                    {
                        msgId = mb.MsgId,
                        recipesId = mb.MsgRecipesId,
                        userId = mb.UserId,
                        Content = mb.MsgContent,
                        DateTime = mb.MsgDateTime,
                        Active = mb.MsgActive,
                        EditedMsgRecords = mb.EditedMsgRecords.Select(emr => new
                        {
                            editedMsgId = emr.EditedMsgId,
                            msgId = emr.MsgId,
                            editedText = emr.EditedText,
                            editedTime = emr.EditedTime
                        })
                    })
                }
            });
    }

    public ApiResponseModel GetUserData()
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "Id");
        if (user == null) return new ApiResponseModel { Status = false };

        var id = int.Parse(user.Value);
        var person = _context.Members.FirstOrDefault(x => x.MemberId == id);
        if (person == null) return new ApiResponseModel { Status = false };
        return new ApiResponseModel
        {
            Status = true,
            Data = new
            {
                person.MemberId,
                person.MemberName,
                person.MemberBirthday,
                person.MemberAdress,
                person.MemberEmail,
                person.MemberPhone,
                person.MemberGender,
                person.MemberAccount,
                person.MemberPassord,
                person.ConponId
            }
        };
    }



	[HttpGet("{id}")]
	public object GetReciple(int id)
	{
		return _context.Recipes
			.Where(x => x.RecipesId == id)
			.Select(x => new
			{
                x.PetTypesId,
                x.RecipesId,
				x.RecipesName,
				x.RecipesData,
				x.RecipesMethod,
				x.RecipesNotes,
                x.RecipesDescription
			});
	}

    [HttpPost]
    public object UpdateRecipe([FromBody] RecipeViewModel model)
    {
        try
        {
            var editedRecipe = _context.Recipes.FirstOrDefault(x => x.RecipesId == model.RecipesId);
            if (editedRecipe == null) return new { success = false, message = "Recipe not found" };
            editedRecipe.RecipesId = model.RecipesId;
            editedRecipe.RecipesName = model.RecipesName;
            editedRecipe.RecipesDescription = model.RecipesDescription;
            editedRecipe.PetTypesId = model.PetTypesId;
            editedRecipe.RecipesData = model.RecipesData;
            editedRecipe.RecipesMethod = model.RecipesMethod;
            editedRecipe.RecipesNotes = model.RecipesNotes;

            _context.Recipes.Update(editedRecipe);
            _context.SaveChanges();
            return new { success = true, message = "Recipe updated successfully" };
        }
        catch (Exception e)
        {
            return new { success = false, message = e.Message };
        }

    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<string>> AddComment([FromBody] MsgBoardViewModel msgboard)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;

        var newComment = new MsgBoard
        {
            UserId = msgboard.UserId,
            MsgRecipesId = msgboard.MsgRecipesId,
            MsgContent = msgboard.MsgContent, //留言內容
            MsgDateTime = msgboard.MsgDateTime, //時間
            MsgActive = msgboard.MsgActive //狀態	
        };
        _context.Add(newComment);
        await _context.SaveChangesAsync();
        return "success";

    }

    /// <summary>
    /// Edit Update
    /// </summary>
    /// <param name="editedMsgRecord"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<string>> EditedMsg([FromBody] EditedMsgRecordViewModel editedMsgRecord)
    {
        var editedComment = new EditedMsgRecord
        {
            MsgId = editedMsgRecord.MsgId,
            EditedText = editedMsgRecord.EditedText,
            EditedTime = editedMsgRecord.EditedTime
        };
        _context.Add(editedComment);
        await _context.SaveChangesAsync();
        return "success";
    }

    [HttpPost]
    public async Task<ActionResult<string>> AddRecipe([FromBody] RecipeViewModel newRecipe)
    {
	    Recipe nRecipe = new Recipe();
	    nRecipe.PetTypesId = newRecipe.PetTypesId;
	    nRecipe.RecipesName = newRecipe.RecipesName;
	    nRecipe.RecipesDescription = newRecipe.RecipesDescription;
	    nRecipe.RecipesData = newRecipe.RecipesData;
	    nRecipe.RecipesMethod = newRecipe.RecipesMethod;
	    nRecipe.RecipesNotes = newRecipe.RecipesNotes;

	    _context.Add(nRecipe);
	    try
	    {
		    await _context.SaveChangesAsync();
	    }
		catch (DbUpdateConcurrencyException)
	    {

		    return "新增食譜失敗";

	    }

	    return "新增食譜成功";
	}

    [HttpDelete("{id}")]
    public async Task<string> DeleteRecipes(int id)
    {
	    try
	    {
		    var recipes = await _context.Recipes.FindAsync(id);
		    if (recipes != null) _context.Recipes.Remove(recipes);
		    await _context.SaveChangesAsync();
		    return "刪除食譜成功!";
	    }
	    catch (Exception)
	    {
		    return "刪除食譜失敗";
	    }
    }

}
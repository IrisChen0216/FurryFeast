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

	//[AllowAnonymous]
	public ApiResponseModel GetUserData()
	{
		var user = User.Claims.FirstOrDefault(x => x.Type == "Id");
		if (user == null)
		{
			return new ApiResponseModel() { Status = false };
		}

		var id = int.Parse(user.Value);
		var person = _context.Members.FirstOrDefault(x => x.MemberId == id);
		if (person == null)
		{
			return new ApiResponseModel() { Status = false };
		}
		return new ApiResponseModel()
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

	[HttpPost]
	[Authorize]
	public async Task<ActionResult<string>> AddComment([FromBody] MsgBoardViewModel msgboard)
	{
		var userId = User.Claims.FirstOrDefault(x => x.Type == "Id").Value;

		if (userId != null)
			msgboard.UserId = userId;
		else
			return "Error";

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

	//Edit Update
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
}
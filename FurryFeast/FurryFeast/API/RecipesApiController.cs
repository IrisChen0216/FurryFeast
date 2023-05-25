using FurryFeast.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurryFeast.API
{
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
						Name = x.RecipesName,
						recipesId = x.RecipesId,
						petTypesId = x.PetTypesId,
						Desc = x.RecipesDescription,
						Data = x.RecipesData,
						Method = x.RecipesMethod,
						Notes = x.RecipesNotes,
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
	}
}
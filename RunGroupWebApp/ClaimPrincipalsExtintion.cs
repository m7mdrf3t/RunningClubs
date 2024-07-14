using System.Security.Claims;

namespace RunGroupWebApp
{
	public static class ClaimPrincipalsExtintion
	{
		public static string GetUserID(this ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
	}
}


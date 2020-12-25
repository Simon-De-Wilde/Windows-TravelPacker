using System.IdentityModel.Tokens.Jwt;

namespace TravelPacker.Util {
	public static class Globals {

		public static string BearerToken { get; set; } = null;

		public static string LoggedInUserName {
			get {
				JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
				JwtSecurityToken jsonToken = handler.ReadJwtToken(BearerToken);
				jsonToken.Payload.TryGetValue("unique_name", out object output);
				return output.ToString();
			}

		}
	}
}

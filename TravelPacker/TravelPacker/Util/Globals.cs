using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPacker.Util {
	public static class Globals {

		public static string BearerToken { get; set; } = null;

		public static string LoggedInUserName {
			get {
				var handler = new JwtSecurityTokenHandler();
				var jsonToken = handler.ReadJwtToken(BearerToken);
				jsonToken.Payload.TryGetValue("unique_name", out object output);
				return output.ToString();
			}

		}
	}
}

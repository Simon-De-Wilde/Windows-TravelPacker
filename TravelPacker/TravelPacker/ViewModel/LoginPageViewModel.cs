using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPacker.Model;
using TravelPacker.Util;
using Windows.Web.Http;

namespace TravelPacker.ViewModel {
	public class LoginPageViewModel {

		public async Task<bool> LoginUser(String email, String password) {

			HttpClient client = new HttpClient();

			var postObj = new { email, password };

			var postJson = JsonConvert.SerializeObject(postObj);

			var result= await client.PostAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Account/login"), 
				new HttpStringContent(postJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

			if (result.IsSuccessStatusCode) {
				Globals.BearerToken = result.Content.ToString();

				return true;
			}
			else{
				return false;
			}

			

		}
	}
}

﻿using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TravelPacker.Util;
using Windows.Web.Http;

namespace TravelPacker.ViewModel {
	public class RegistrationPageViewModel {

		public async Task<bool> RegisterUser(string firstname, string lastname, string email, string password) {

			try {
				HttpClient client = new HttpClient();

				var postObj = new {
					firstname,
					lastname,
					email,
					password
				};

				string postJson = JsonConvert.SerializeObject(postObj);

				HttpResponseMessage result = await client.PostAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Account/Register"),
					new HttpStringContent(postJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

				if (result.IsSuccessStatusCode) {
					Globals.BearerToken = result.Content.ToString();

					return true;
				}
				else {
					return false;
				}
			}
			catch {
				return false;
			}



		}
	}
}

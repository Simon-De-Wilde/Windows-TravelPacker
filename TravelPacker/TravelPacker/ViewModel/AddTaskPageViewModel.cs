using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPacker.Model;
using TravelPacker.Util;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace TravelPacker.ViewModel {
	public class AddTaskPageViewModel {
		public Travel Travel { get; set; }
		public AddTaskPageViewModel() {
		}

		public async Task<TravelTask> AddTaskToTravel(string title, TimeSpan time) {
			try {
				TravelTask newTask = new TravelTask(title, time);
				var newTaskJSON = JsonConvert.SerializeObject(newTask);

				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);
				var result = await client.PostAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Task/" + Travel.Id),
					new HttpStringContent(newTaskJSON, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

				if (result.IsSuccessStatusCode) {
					return JsonConvert.DeserializeObject<TravelTask>(result.Content.ToString());
				}
				else {
					throw new Exception();
				}
			}
			catch {
				return null;
			}
		}
	}
}

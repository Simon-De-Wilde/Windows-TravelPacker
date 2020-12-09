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

namespace TravelPacker.ViewModel
{
    public class TravelsDetailPageViewModel
    {
        public Travel Travel { get; set; }
        public TravelsDetailPageViewModel()
        {
        }


		public async void GetTravel(int id)
		{
			Travel = null;
			try
			{
				HttpClient client = new HttpClient();

				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				var json = await client.GetStringAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Travels/" + id));

				var travel = JsonConvert.DeserializeObject<Travel>(json);
				Travel = travel;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}


		}

		public async Task<bool> DeleteTask(TravelTask task)
		{
			try
			{
				HttpClient client = new HttpClient();

				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				var deleteResult = await client.DeleteAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Task/{task.Id}"));

				if (deleteResult.IsSuccessStatusCode)
				{
					Travel.Tasks.Remove(task);
					return true;
				}
				else
				{
					throw new Exception();
				}
			}
			catch (Exception e)
			{
				return false;
			}


		}
	}
}

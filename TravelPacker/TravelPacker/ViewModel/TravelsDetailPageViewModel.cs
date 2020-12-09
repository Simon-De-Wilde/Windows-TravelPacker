using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPacker.Model;
using TravelPacker.Util;
using Windows.UI.Popups;
using System.Net;
using System.Net.Http.Headers;
using Windows.Web.Http.Headers;
using Windows.Web.Http;

namespace TravelPacker.ViewModel
{
    public class TravelsDetailPageViewModel
    {
        public Travel Travel { get; set; }

        public TravelsDetailPageViewModel() {  }

		private Category GetCategoryOfItem(Item item)
		{
			foreach (Category c in Travel.Categories)
			{
				foreach (Item i in c.Items)
				{
					if (i == item) { return c; }
				}
			}

			return null;
		}

		public async Task<bool> GetCategories()
		{
			Travel.Categories.Clear();

			try
			{
				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				var result = await client.GetStringAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Categories/GetCategoriesFromTravel/{Travel.Id}"));
				var list = JsonConvert.DeserializeObject<List<Category>>(result);

				foreach (Category c in list)
				{
					Travel.Categories.Add(c);
				}

				return true;
			}
			catch (Exception e) { return false; }
		}

		public async Task<bool> DeleteCategory(Category selectedCategory)
		{
			try
			{
				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);
				var result = await client.DeleteAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Categories/{selectedCategory.Id}"));

				if (result.IsSuccessStatusCode)
				{
					Travel.Categories.Remove(selectedCategory);
					return true;
				}
				else { throw new Exception(); }
			}
			catch (Exception e) { return false; }
		}

		public async Task<bool> DeleteItem(Item selectedItem)
		{
			var categoryOfItem = GetCategoryOfItem(selectedItem);

			try
			{
				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);
				var result = await client.DeleteAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Item/{selectedItem.Id}"));

				if (result.IsSuccessStatusCode)
				{
					Travel.Categories.Where(c => c.Id == categoryOfItem.Id).FirstOrDefault().Items.Remove(selectedItem);
					return true;
				}
				else { throw new Exception(); }
			}
			catch (Exception e) { return false; }
		}

		public async Task<bool> addCategory(string title)
		{

			try
			{
				Category newCategory = new Category(title);
				var json = JsonConvert.SerializeObject(newCategory);

				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				var result = await client.PostAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Categories/PostCategoryToTravel/{Travel.Id}"),
					new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

				if (result.IsSuccessStatusCode)
				{
					await GetCategories();
					return true;
				}
				else { throw new Exception(); }
			}
			catch (Exception e) { return false; }
		}

		public async Task<bool> addItem(string title, int categoryID)
		{
			try
			{
				Item newItem = new Item(title);

				var json = JsonConvert.SerializeObject(newItem);

				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				var result = await client.PostAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Item/PostItemToCategory/{categoryID}"),
					new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

				if (result.IsSuccessStatusCode)
				{
					await GetCategories();
					return true;
				}
				else { throw new Exception(); }
			}
			catch (Exception e) { return false; }
		}
	}
}

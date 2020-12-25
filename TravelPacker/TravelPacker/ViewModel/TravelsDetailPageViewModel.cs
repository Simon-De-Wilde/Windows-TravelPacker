using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelPacker.Model;
using TravelPacker.Util;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace TravelPacker.ViewModel {
	public class TravelsDetailPageViewModel {
		public Travel Travel { get; set; }

		public TravelsDetailPageViewModel() {
		}

		public Category GetCategoryOfItem(Item item) {
			foreach (Category c in Travel.Categories) {
				foreach (Item i in c.Items) {
					if (i == item) { return c; }
				}
			}

			return null;
		}

		public Item GetItem(int itemID) {
			foreach (Category c in Travel.Categories) {
				foreach (Item i in c.Items) {
					if (i.Id == itemID) { return i; }
				}
			}

			return null;
		}

		//public async Task<bool> GetCategories() {
		//	Travel.Categories.Clear();

		//	try {
		//		HttpClient client = new HttpClient();
		//		client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

		//		var result = await client.GetStringAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Categories/GetCategoriesFromTravel/{Travel.Id}"));
		//		var list = JsonConvert.DeserializeObject<List<Category>>(result);

		//		foreach (Category c in list) {
		//			Travel.Categories.Add(c);
		//		}

		//		return true;
		//	}
		//	catch (Exception e) { return false; }
		//}

		public async Task<bool> DeleteCategory(Category selectedCategory) {
			try {
				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);
				HttpResponseMessage result = await client.DeleteAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Categories/{selectedCategory.Id}"));

				if (result.IsSuccessStatusCode) {
					return true;
				}
				else { throw new Exception(); }
			}
			catch { return false; }
		}

		public async Task<bool> DeleteItem(Item selectedItem) {
			try {
				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);
				HttpResponseMessage result = await client.DeleteAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Item/{selectedItem.Id}"));

				if (result.IsSuccessStatusCode) {
					return true;
				}
				else { throw new Exception(); }
			}
			catch { return false; }
		}

		public async Task<bool> addCategory(string title) {

			try {
				Category newCategory = new Category(title);
				string json = JsonConvert.SerializeObject(newCategory);

				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				HttpResponseMessage result = await client.PostAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Categories/PostCategoryToTravel/{Travel.Id}"),
					new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

				if (result.IsSuccessStatusCode) {
					Travel.Categories.Add(JsonConvert.DeserializeObject<Category>(result.Content.ToString()));
					return true;
				}
				else { throw new Exception(); }
			}
			catch { return false; }
		}

		public async Task<bool> addItem(string title, int amount, int categoryID) {
			try {
				Item newItem = new Item(title, amount);

				string json = JsonConvert.SerializeObject(newItem);

				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				HttpResponseMessage result = await client.PostAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Item/PostItemToCategory/{categoryID}"),
					new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

				if (result.IsSuccessStatusCode) {
					Travel.Categories.FirstOrDefault(c => c.Id == categoryID).Items.Add(JsonConvert.DeserializeObject<Item>(result.Content.ToString()));
					return true;
				}
				else { throw new Exception(); }
			}
			catch { return false; }
		}

		public async Task<bool> updateItem(Item item, bool done) {
			try {
				Category category = GetCategoryOfItem(item);
				Item itemToUpdate = GetItem(item.Id);

				if (done) {
					itemToUpdate.SetDone();
					category.ItemsDone++;
				}
				else {
					itemToUpdate.SetNotDone();
					category.ItemsDone--;
				}

				string json = JsonConvert.SerializeObject(itemToUpdate);

				HttpClient client = new HttpClient();

				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				HttpResponseMessage result = await client.PutAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Item/{item.Id}"),
					new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

				if (result.IsSuccessStatusCode) {
					return true;
				}
				else { throw new Exception(); }
			}
			catch { return false; }
		}

		public async Task<bool> DeleteItineraryItem(ItineraryItem ii) {
			try {
				HttpClient client = new HttpClient();

				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);
				HttpResponseMessage deleteResult = await client.DeleteAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/ItineraryItems/{ii.Id}"));

				if (deleteResult.IsSuccessStatusCode) {
					return true;
				}
				else {
					throw new Exception();
				}
			}
			catch {
				return false;
			}
		}

		public async Task<bool> DeleteTask(TravelTask task) {
			try {
				HttpClient client = new HttpClient();

				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				HttpResponseMessage deleteResult = await client.DeleteAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Task/{task.Id}"));

				if (deleteResult.IsSuccessStatusCode) {
					Travel.Tasks.Remove(task);
					return true;
				}
				else {
					throw new Exception();
				}
			}
			catch {
				return false;
			}
		}

		public async Task<bool> UpdateTask(TravelTask task, bool done) {
			try {

				if (done) {
					task.SetDone();
				}
				else {
					task.SetNotDone();
				}

				string json = JsonConvert.SerializeObject(task);

				HttpClient client = new HttpClient();

				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				HttpResponseMessage result = await client.PutAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Task/{task.Id}"),
					new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

				if (result.IsSuccessStatusCode) {
					return true;
				}
				else { throw new Exception(); }
			}
			catch {
				return false;
			}
		}
	}
}

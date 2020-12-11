﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPacker.Model;
using TravelPacker.Util;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace TravelPacker.ViewModel {
	public class TravelsDetailPageViewModel {
		public Travel Travel { get; set; }
		public ObservableCollection<ItineraryItem> Itinerary { get; set; }

		public TravelsDetailPageViewModel() {
		}

		private Category GetCategoryOfItem(Item item) {
			foreach (Category c in Travel.Categories) {
				foreach (Item i in c.Items) {
					if (i == item) { return c; }
				}
			}

			return null;
		}

		private Item GetItem(int itemID) {
			foreach (Category c in Travel.Categories) {
				foreach (Item i in c.Items) {
					if (i.Id == itemID) { return i; }
				}
			}

			return null;
		}

		public async Task<bool> GetCategories() {
			Travel.Categories.Clear();

			try {
				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				var result = await client.GetStringAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Categories/GetCategoriesFromTravel/{Travel.Id}"));
				var list = JsonConvert.DeserializeObject<List<Category>>(result);

				foreach (Category c in list) {
					Travel.Categories.Add(c);
				}

				return true;
			}
			catch (Exception e) { return false; }
		}

		public async Task<bool> DeleteCategory(Category selectedCategory) {
			try {
				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);
				var result = await client.DeleteAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Categories/{selectedCategory.Id}"));

				if (result.IsSuccessStatusCode) {
					Travel.Categories.Remove(selectedCategory);
					return true;
				}
				else { throw new Exception(); }
			}
			catch (Exception e) { return false; }
		}

		public async Task<bool> DeleteItem(Item selectedItem) {
			var categoryOfItem = GetCategoryOfItem(selectedItem);

			try {
				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);
				var result = await client.DeleteAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Item/{selectedItem.Id}"));

				if (result.IsSuccessStatusCode) {
					Travel.Categories.Where(c => c.Id == categoryOfItem.Id).FirstOrDefault().Items.Remove(selectedItem);
					return true;
				}
				else { throw new Exception(); }
			}
			catch (Exception e) { return false; }
		}

		public async Task<bool> addCategory(string title) {

			try {
				Category newCategory = new Category(title);
				var json = JsonConvert.SerializeObject(newCategory);

				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				var result = await client.PostAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Categories/PostCategoryToTravel/{Travel.Id}"),
					new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

				if (result.IsSuccessStatusCode) {
					await GetCategories();
					return true;
				}
				else { throw new Exception(); }
			}
			catch (Exception e) { return false; }
		}

		public async Task<bool> addItem(string title, int categoryID) {
			try {
				Item newItem = new Item(title);

				var json = JsonConvert.SerializeObject(newItem);

				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				var result = await client.PostAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Item/PostItemToCategory/{categoryID}"),
					new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

				if (result.IsSuccessStatusCode) {
					await GetCategories();
					return true;
				}
				else { throw new Exception(); }
			}
			catch (Exception e) { return false; }
		}

		public async Task<bool> updateItem(Item item, bool done) {
			try {
				var itemToUpdate = GetItem(item.Id);
				if (done) { itemToUpdate.SetDone(); }
				else { itemToUpdate.SetNotDone(); }

				var json = JsonConvert.SerializeObject(itemToUpdate);

				HttpClient client = new HttpClient();

				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				var result = await client.PutAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Item/{item.Id}"),
					new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

				if (result.IsSuccessStatusCode) {
					await GetCategories();
					return true;
				}
				else { throw new Exception(); }
			}
			catch (Exception e) { return false; }
		}

		public async Task<bool> DeleteItineraryItem(ItineraryItem ii) {
			try {
				HttpClient client = new HttpClient();

				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);
				var deleteResult = await client.DeleteAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/ItineraryItems/{ii.Id}"));

				if (deleteResult.IsSuccessStatusCode) {
					return true;
				}
				else {
					throw new Exception();
				}
			}
			catch (Exception e) {
				return false;
			}
		}

		public async Task<bool> DeleteTask(TravelTask task) {
			try {
				HttpClient client = new HttpClient();

				client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Globals.BearerToken);

				var deleteResult = await client.DeleteAsync(new Uri($"{EnvironmentsProperties.BASE_URL}/Task/{task.Id}"));

				if (deleteResult.IsSuccessStatusCode) {
					Travel.Tasks.Remove(task);
					return true;
				}
				else {
					throw new Exception();
				}
			}
			catch (Exception e) {
				return false;
			}
		}
	}
}

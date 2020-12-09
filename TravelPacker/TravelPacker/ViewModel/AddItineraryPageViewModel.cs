using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPacker.Model;

namespace TravelPacker.ViewModel {
	public class AddItineraryPageViewModel {
		public int TravelId { get; internal set; }

		public async Task<bool> AddItineraryToTravel(string titel, DateTime start, int durationInMinutes) {
			// TODO posten en element terugsturen om toe te voegen aan de lokale lijst
			Console.WriteLine(titel);
			Console.WriteLine(start.ToString());
			Console.WriteLine(durationInMinutes);
			return true;
		}
	}
}

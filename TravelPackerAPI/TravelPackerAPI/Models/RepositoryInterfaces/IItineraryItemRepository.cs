using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPackerAPI.Models.RepositoryInterfaces {
	public interface IItineraryItemRepository {

		IEnumerable<ItineraryItem> GetAll();

		ItineraryItem GetById(int id);

		void Add(ItineraryItem i);

		void Delete(ItineraryItem i);

		void Update(ItineraryItem i);
	}
}

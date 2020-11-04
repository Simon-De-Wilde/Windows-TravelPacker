using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model
{
    internal class Item : IItem
    {
        [Required]
        public int Amount { get; }

        public Item(string title, int amount = 1) : base(title)
        {
            Amount = amount;
        }
    }
}
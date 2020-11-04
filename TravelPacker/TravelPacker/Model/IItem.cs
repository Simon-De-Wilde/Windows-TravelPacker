using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model
{
    internal abstract class IItem
    {
        [Required]
        public string Title { get; }

        [Required]
        private bool _done;
        public bool Done { get { return _done; } }

        public IItem(string title) {
            Title = title;
            _done = false;
        }

        public void isDone() {
            _done = true;
        }   
    }
}
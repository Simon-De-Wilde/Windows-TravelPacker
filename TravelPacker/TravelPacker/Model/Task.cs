

using System;
using System.ComponentModel.DataAnnotations;

namespace TravelPacker.Model
{
    internal class Task : IItem
    {
        [Required]
        public TimeSpan Duration { get; }

        public Task(string title, TimeSpan duration) : base(title) {
            Duration = duration;
        }
    }
}

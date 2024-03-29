﻿using System.ComponentModel.DataAnnotations;

namespace RunGroop.Models
{
	public class AppUser
	{
		// Small workaround before EF Identity is added
		[Key]
		public string Id { get; set; }
		public int? Pace { get; set; }
		public int? Mileage { get; set; }
		public Address? Address { get; set; }
		public ICollection<Club> Clubs { get; set; }
		public ICollection<Race> Races { get; set; }
	}
}

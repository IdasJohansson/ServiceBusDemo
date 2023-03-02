using System;
using System.ComponentModel.DataAnnotations;

namespace SbShared.Models
{
	public class PersonModel
	{
		[Required]
		public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}


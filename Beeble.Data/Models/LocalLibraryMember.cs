﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeble.Data.Models
{
    public class LocalLibraryMember
    {
	    [DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string  PhoneNumber { get; set; }
        public string Email { get; set; }
        //payment option
        public bool IsVerifiedLocal { get; set; }
        public bool IsGuest { get; set; }
        public DateTime? MembershipExpiryDate { get; set; }

		public OnlineUser OnlineUser { get; set; }
        public LocalLibrary LocalLibrary { get; set; }

		public ICollection<BatchOfBorrowedBooks> BatchesOfBorrowedBooks { get; set; }
		public ICollection<Reservation> Reservations { get; set; }

	}
}

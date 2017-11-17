﻿using System.ComponentModel.DataAnnotations;


namespace GettingStarted_RazorPagesContacts.Domain
{
    public class CustomerInfo
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }
    }
}

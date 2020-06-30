using System;
using System.ComponentModel.DataAnnotations;

namespace Profile.API.DTO
{
    /// <summary>
    /// Profile Data Transfer Object (DTO).
    /// </summary>
    public class ProfileDTO
    {
        /// <summary>
        /// Profile Identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// First name.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Middle name.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Birth data.
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Height [sm].
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Weight [kg].
        /// </summary>
        public int Weight { get; set; }


        /// <summary>
        /// Account Identifier.
        /// </summary>
        [Required]
        public Guid AccountId { get; set; }
    }
}
using System;

namespace Profile.API.DTO
{
    /// <summary>
    /// Profile Data Transfer Object (DTO).
    /// </summary>
    public class ProfileDTO
    {
        /// <summary>
        /// First name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Middle name.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Birth data.
        /// </summary>
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
    }
}
using Profile.API.DTO;
using System;
using System.Collections.Generic;

namespace Profile.UnitTests.Controllers
{
    /// <summary>
    /// Define base functions for ProfilesController testing.
    /// </summary>
    public class ConstrollerTestFixture
    {
        /// <summary>
        /// Generate collection of ProfileDTO.
        /// </summary>
        /// <returns>Collection of ProfileDTO.</returns>
        public ICollection<ProfileDTO> GetAllProfiles()
        {
            return new List<ProfileDTO>()
            {
                new ProfileDTO()
                {
                    Id = Guid.Parse("745e4c39-d335-440f-89ac-08d8111a9b1a"),
                    FirstName = "FirstName_One",
                    LastName = "LastName_One",
                    MiddleName = "MiddleName_One",
                    Passport = "123456789",
                    BirthDate = DateTime.Parse("01-01-1984"),
                    Gender = "Male",
                    Height = 180,
                    Weight = 80,
                },

                new ProfileDTO()
                {
                    Id = Guid.Parse("745e4c39-d335-440f-89ac-08d8111a9b1a"),
                    FirstName = "FirstName_Two",
                    LastName = "LastName_Two",
                    MiddleName = "MiddleName_Two",
                    Passport = "987654321",
                    BirthDate = DateTime.Parse("01-01-1974"),
                    Gender = "Female",
                    Height = 165,
                    Weight = 50,
                },
            };
        }

        /// <summary>
        /// Generate single ProfileDTO.
        /// </summary>
        /// <returns>ProfileDTO.</returns>
        public ProfileDTO GetProfile()
        {
            return new ProfileDTO()
            {
                Id = Guid.Parse("745e4c39-d335-440f-89ac-08d8111a9b1a"),
                FirstName = "FirstName_One",
                LastName = "LastName_One",
                MiddleName = "MiddleName_One",
                Passport = "123456789",
                BirthDate = DateTime.Parse("01-01-1984"),
                Gender = "Male",
                Height = 180,
                Weight = 80,
            };
        }

        /// <summary>
        /// Get null ProfileDTO.
        /// </summary>
        /// <returns>Null object.</returns>
        public ProfileDTO GetNullProfile() => null;
    }
}
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
                    BirthDate = DateTime.Parse("01-01-1984"),
                    Gender = "Male",
                    Height = 180,
                    Weight = 80,
                    AccountId = Guid.Parse("4c704d36-7c29-4a33-7fda-08d811bd62ef"),
                },

                new ProfileDTO()
                {
                    Id = Guid.Parse("745e4c39-d335-440f-89ac-08d8111a9b1a"),
                    FirstName = "FirstName_Two",
                    LastName = "LastName_Two",
                    MiddleName = "MiddleName_Two",
                    BirthDate = DateTime.Parse("01-01-1974"),
                    Gender = "Female",
                    Height = 165,
                    Weight = 50,
                    AccountId = Guid.Parse("892ee6da-d6af-4cf0-7fdb-08d811bd62ef"),
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
                BirthDate = DateTime.Parse("01-01-1984"),
                Gender = "Male",
                Height = 180,
                Weight = 80,
                AccountId = Guid.Parse("4c704d36-7c29-4a33-7fda-08d811bd62ef"),
            };
        }

        /// <summary>
        /// Get null ProfileDTO.
        /// </summary>
        /// <returns>Null object.</returns>
        public ProfileDTO GetNullProfile() => null;
    }
}
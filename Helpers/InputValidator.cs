using ProjectedTurefCoordinatesToGeographic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectedTurefCoordinatesToGeographic.Helpers
{
    public static class InputValidator
    {
        public static void Validate(InputEntityDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");

            if (string.IsNullOrWhiteSpace(input.ProjectedWKT))
                throw new ArgumentException("Projected WKT cannot be null or empty.", nameof(input.ProjectedWKT));

            if (input.CentralMeridian is < 27 or > 45)
                throw new ArgumentOutOfRangeException(nameof(input.CentralMeridian), "Central meridian must be one of 27, 30, 33, 36, 39, 42 or 45.");
        }
    }
}


using ProjNet.CoordinateSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectedTurefCoordinatesToGeographic.Helpers
{
    public static class CoordinateSystemFactoryHelper
    {
        public static ProjectedCoordinateSystem CreateTurefTM(int centralMeridian)
        {
            CoordinateSystemFactory factory = new CoordinateSystemFactory();

            Ellipsoid ellipsoid = factory.CreateFlattenedSphere("GRS80", 6378137.0, 298.257222100882711243, LinearUnit.Metre);

            HorizontalDatum datum = factory.CreateHorizontalDatum("TUREF", DatumType.HD_Geocentric, ellipsoid, null);

            GeographicCoordinateSystem geographicCs = factory.CreateGeographicCoordinateSystem(
                                                                                               "TUREF",
                                                                                               AngularUnit.Degrees,
                                                                                               datum,
                                                                                               PrimeMeridian.Greenwich,
                                                                                               new AxisInfo("Lon", AxisOrientationEnum.East),
                                                                                               new AxisInfo("Lat", AxisOrientationEnum.North)
                                                                                              );

            List<ProjectionParameter> parameters = new List<ProjectionParameter>
                                                                                {
                                                                                    new ProjectionParameter("latitude_of_origin", 0),
                                                                                    new ProjectionParameter("central_meridian", centralMeridian),
                                                                                    new ProjectionParameter("scale_factor", 1),
                                                                                    new ProjectionParameter("false_easting", 500000),
                                                                                    new ProjectionParameter("false_northing", 0)
                                                                                };

            IProjection projection = factory.CreateProjection("Transverse_Mercator","Transverse_Mercator",parameters);

            return factory.CreateProjectedCoordinateSystem(
                                                            $"TUREF / TM{centralMeridian}",
                                                            geographicCs,
                                                            projection,
                                                            LinearUnit.Metre,
                                                            new AxisInfo("East", AxisOrientationEnum.East),
                                                            new AxisInfo("North", AxisOrientationEnum.North)
                                                          );
        }
        public static ProjectedCoordinateSystem GetByCentraMeridian(int centralMeridian)
        {
            return centralMeridian switch
            {
                27 => CreateTurefTM(27),
                30 => CreateTurefTM(30),
                33 => CreateTurefTM(33),
                36 => CreateTurefTM(36),
                39 => CreateTurefTM(39),
                42 => CreateTurefTM(42),
                45 => CreateTurefTM(45),
                _ => throw new ArgumentException($"Out of Türkiye's border: {centralMeridian}")
            };
        }
        public static GeographicCoordinateSystem CreateTurefGeographic()
        {
            CoordinateSystemFactory factory = new CoordinateSystemFactory();
            Ellipsoid ellipsoid = factory.CreateFlattenedSphere("GRS80", 6378137.0, 298.257222100882711243, LinearUnit.Metre);
            HorizontalDatum datum = factory.CreateHorizontalDatum("TUREF", DatumType.HD_Geocentric, ellipsoid, null);

            return factory.CreateGeographicCoordinateSystem(
                "TUREF",
                AngularUnit.Degrees,
                datum,
                PrimeMeridian.Greenwich,
                new AxisInfo("Lon", AxisOrientationEnum.East),
                new AxisInfo("Lat", AxisOrientationEnum.North));
        }

    }
}

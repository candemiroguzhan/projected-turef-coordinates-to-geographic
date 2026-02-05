using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using ProjectedTurefCoordinatesToGeographic.Dtos;
using ProjectedTurefCoordinatesToGeographic.Helpers;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectedTurefCoordinatesToGeographic
{
    public static class GeometryConverter
    {
        public static string ConvertTmToGeographic(InputEntityDto e)
        {
            InputValidator.Validate(e);

            GeometryFactory geometryFactory = new GeometryFactory();
            WKTReader wktReader = new WKTReader(geometryFactory);
            WKTWriter wktWriter = new WKTWriter();

            ProjectedCoordinateSystem sourceCs = CoordinateSystemFactoryHelper.GetByCentraMeridian(e.CentralMeridian);
            GeographicCoordinateSystem targetCs = CoordinateSystemFactoryHelper.CreateTurefGeographic();

            CoordinateTransformationFactory transformFactory = new CoordinateTransformationFactory();
            ICoordinateTransformation transformation = transformFactory.CreateFromCoordinateSystems(sourceCs, targetCs);
            MathTransform mathTransform = transformation.MathTransform;

            Geometry originalGeometry = wktReader.Read(e.ProjectedWKT);
            Coordinate[] coords = originalGeometry.Coordinates;

            Coordinate[] transformedCoords = coords.Select(c =>
            {
                double[] transformed = mathTransform.Transform(new[] { c.X, c.Y });
                return new Coordinate(transformed[0], transformed[1]);
            }).ToArray();

            Geometry transformedGeometry = originalGeometry switch
            {
                Point => geometryFactory.CreatePoint(transformedCoords[0]),
                LineString => geometryFactory.CreateLineString(transformedCoords),
                Polygon => geometryFactory.CreatePolygon(transformedCoords),
                MultiPoint => geometryFactory.CreateMultiPointFromCoords(transformedCoords),
                _ => throw new NotSupportedException("Unsupported geometry type.")
            };

            return wktWriter.Write(transformedGeometry);
        }

        public static List<string> ConvertMultipleTmToGeographic(List<InputEntityDto> geometries)
        {
            return geometries.Select(g => ConvertTmToGeographic(g)).ToList();
        }
    }
}

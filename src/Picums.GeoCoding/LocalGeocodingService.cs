﻿using System.Threading.Tasks;
using Microsoft.Spatial;
using Picums.Maybe;

namespace Picums.GeoCoding
{
    public sealed class LocalGeocodingService : IGeocodingService
    {
        private readonly GeographyPoint randomGeographyPoint;

        public LocalGeocodingService()
        {
            this.randomGeographyPoint = GeographyPoint.Create(51.5033635, -0.1276248);
        }

        public Task<Maybe<GeographyPoint>> GeocodeAsync(Address address)
            => Task.FromResult<Maybe<GeographyPoint>>(this.randomGeographyPoint);
    }
}
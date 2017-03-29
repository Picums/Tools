﻿using Microsoft.Extensions.Logging;
using Picums.Tests;
using Xunit;

namespace Picums.GeoCoding.Tests.Entities
{
    public sealed class GoogleGeocodingResultsTests
    {
        private readonly string exampleFile;
        private readonly ILogger emptyLogger;

        public GoogleGeocodingResultsTests()
        {
            var embededResources = new EmbeddedContentLoader("Picums.GeoCoding.Tests");
            this.exampleFile = embededResources
                .LoadTextFileAsync("Picums.GeoCoding.Tests.Resources.GoogleServiceResluts.xml")
                .GetAwaiter()
                .GetResult();
            this.emptyLogger = new TestLoggerFactory().CreateLogger("null");
        }

        [Fact]
        public void ParseServiceResult()
        {
            var parser = new GoogleGeocodingResults(this.emptyLogger, this.exampleFile);

            Assert.True(parser.HasResults);
        }
    }
}
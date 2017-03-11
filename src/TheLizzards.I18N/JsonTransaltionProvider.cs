﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace TheLizzards.Mvc.Localisation.Services
{
    internal sealed class JsonTransaltionProvider
    {
        private readonly IConfigurationSection configuration;
        private readonly CultureInfo defaultCulture;

        public JsonTransaltionProvider(IConfigurationSection configuration, CultureInfo defaultCulture)
        {
            this.configuration = configuration;
            this.defaultCulture = defaultCulture;
        }

        public TranslationSet GetTranslationSet()
            => new TranslationSet(ConvertToTransationData(this.configuration));

        public IEnumerable<(string, string, string)> ConvertToTransationData(IConfigurationSection configuration)
            => configuration
                .GetChildren()
                .SelectMany(x => ConverToFlatDictionary(x.GetChildren()));

        private IEnumerable<(string, string, string)> ConverToFlatDictionary(
                IEnumerable<IConfigurationSection> itemsToProcess)
            => itemsToProcess
                .Aggregate(
                    Enumerable.Empty<IConfigurationSection>(),
                    (seed, item) => seed.Union(GetAlldescendantsAndSelf(item)))
                .Where(x => x.Value != null)
                .Select(x => new
                {
                    Culture = ExtractCulture(x.Path),
                    Key = ExtractKey(x.Path),
                    Value = x.Value,
                })
                .Select(x => new
                {
                    Culture = new CultureInfo(x.Culture),
                    Key = x.Key.Replace($"{x.Culture}.", string.Empty),
                    Value = x.Value,
                })
                .Select(x => (
                    x.Culture.TwoLetterISOLanguageName
                    , x.Key
                    , x.Value));

        private IEnumerable<IConfigurationSection> GetAlldescendantsAndSelf(
            IConfigurationSection section)
        {
            IEnumerable<IConfigurationSection> result = new[] { section };

            foreach (var test in section.GetChildren())
            {
                result = result.Union(GetAlldescendantsAndSelf(test));
            }

            return result;
        }

        private string ExtractCulture(string source) => source.Split(':')[1];

        private string ExtractKey(string source)
            => source
                .Substring(source.IndexOf(":"))
                .TrimStart(':')
                .Replace(':', '.');
    }
}
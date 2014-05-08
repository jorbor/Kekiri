﻿using System;
using NUnit.Framework;

namespace Kekiri
{
    // TODO: consider adding a flavor that supports enumerations rather than strings
    [AttributeUsage(AttributeTargets.Class)]
    public class FeatureAttribute : CategoryAttribute
    {
        public string[] FeatureDetails { get; private set; }

        public string FeatureSummary { get; private set; }

        public FeatureAttribute(string featureSummary, params string [] featureDetails)
            : base(featureSummary)
        {
            FeatureSummary = featureSummary;
            FeatureDetails = featureDetails ?? new string[0];
        }
    }
}
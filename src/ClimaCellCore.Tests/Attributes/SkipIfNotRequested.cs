using System;
using Xunit;

namespace ClimaCellCore.Tests.Attributes
{
    public sealed class SkipIfNotRequested : FactAttribute
    {
        public SkipIfNotRequested()
        {
            if (!IsRequested())
            {
                Skip = "Test not requested";
            }
        }

        private static bool IsRequested()
            => Boolean.TryParse(Environment.GetEnvironmentVariable("CLIMACELL_RUN_INTEGRATION_TESTS"), out var result) && result == true;
    }
}
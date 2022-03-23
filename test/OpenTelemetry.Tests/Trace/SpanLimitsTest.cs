// <copyright file="SpanLimitsTest.cs" company="OpenTelemetry Authors">
// Copyright The OpenTelemetry Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using Xunit;

namespace OpenTelemetry.Trace.Tests
{
    public class SpanLimitsTest
    {
        public SpanLimitsTest()
        {
            EmptyEnvVars();
        }

        public void Dispose()
        {
            EmptyEnvVars();
        }

        private void EmptyEnvVars() 
        {
            Environment.SetEnvironmentVariable(SpanLimits.AttributeCountLimitEnvVarName, null);
            Environment.SetEnvironmentVariable(SpanLimits.AttributeValueLenghtLimitEnvVarName, null);
            Environment.SetEnvironmentVariable(SpanLimits.SpanAttributeCountLimitEnvVarName, null);    
            Environment.SetEnvironmentVariable(SpanLimits.SpanAttributeValueLengthLimitEnvVarName, null);
            Environment.SetEnvironmentVariable(SpanLimits.SpanEventCountLimitEnvVarName, null);
            Environment.SetEnvironmentVariable(SpanLimits.SpanLinkCountLimitEnvVarName, null);
            Environment.SetEnvironmentVariable(SpanLimits.EventAttributeCountLimitEnvVarName, null);
            Environment.SetEnvironmentVariable(SpanLimits.LinkAttributeCountLimitEnvVarName, null);
        }

        [Fact]
        public void DefaultLimits()
        {
            var spanLimits = new SpanLimits();

            Assert.Null(spanLimits.AttributeValueLengthLimit);
            Assert.Null(spanLimits.SpanAttributeValueLengthLimit);

            Assert.Equal(spanLimits.AttributeCountLimit, SpanLimits.DefaultAttributeCountLimit);
            Assert.Equal(spanLimits.SpanAttributeCountLimit, SpanLimits.DefaultSpanAttributeCountLimit);
            Assert.Equal(spanLimits.EventCountLimit, SpanLimits.DefaultEventCountLimit);
            Assert.Equal(spanLimits.LinkCountLimit, SpanLimits.DefaultLinkCountLimit);
            Assert.Equal(spanLimits.AttributePerEventCountLimit, SpanLimits.DefaultAttributePerEventCountLimit);
            Assert.Equal(spanLimits.AttributePerLinkCountLimit, SpanLimits.DefaultAttributePerLinkCountLimit);
        }

        [Fact]
        public void LimitsSuppliedDirectly()
        {
            uint attributeCountLimit = 1;
            uint attributeValueLengthLimit = 2;
            uint spanAttributeCountLimit = 3;
            uint spanAttributeValueLengthLimit = 4;
            uint eventCountLimit = 5;
            uint linkCountLimit = 6;
            uint attributePerEventCountLimit = 7;
            uint attributePerLinkCountLimit = 8 ;

            var spanLimits = new SpanLimits(attributeCountLimit,
                                            attributeValueLengthLimit,
                                            spanAttributeCountLimit,
                                            spanAttributeValueLengthLimit,
                                            eventCountLimit,
                                            linkCountLimit,
                                            attributePerEventCountLimit,
                                            attributePerLinkCountLimit);

            Assert.Equal(spanLimits.AttributeCountLimit, attributeCountLimit);
            Assert.Equal(spanLimits.AttributeValueLengthLimit, attributeValueLengthLimit);
            Assert.Equal(spanLimits.SpanAttributeCountLimit, spanAttributeCountLimit);
            Assert.Equal(spanLimits.SpanAttributeValueLengthLimit, spanAttributeValueLengthLimit);
            Assert.Equal(spanLimits.EventCountLimit, eventCountLimit);
            Assert.Equal(spanLimits.LinkCountLimit, linkCountLimit);
            Assert.Equal(spanLimits.AttributePerEventCountLimit, attributePerEventCountLimit);
            Assert.Equal(spanLimits.AttributePerLinkCountLimit, attributePerLinkCountLimit);
        }



        [Fact]
        public void LimitsSuppliedByEnvVar()
        {
            uint attributeCountLimit = 1;
            uint attributeValueLengthLimit = 2;
            uint spanAttributeCountLimit = 3;
            uint spanAttributeValueLengthLimit = 4;
            uint eventCountLimit = 5;
            uint linkCountLimit = 6;
            uint attributePerEventCountLimit = 7;
            uint attributePerLinkCountLimit = 8 ;

            Environment.SetEnvironmentVariable(SpanLimits.AttributeCountLimitEnvVarName, attributeCountLimit.ToString());
            Environment.SetEnvironmentVariable(SpanLimits.AttributeValueLenghtLimitEnvVarName, attributeValueLengthLimit.ToString());
            Environment.SetEnvironmentVariable(SpanLimits.SpanAttributeCountLimitEnvVarName, spanAttributeCountLimit.ToString());    
            Environment.SetEnvironmentVariable(SpanLimits.SpanAttributeValueLengthLimitEnvVarName, spanAttributeValueLengthLimit.ToString());
            Environment.SetEnvironmentVariable(SpanLimits.SpanEventCountLimitEnvVarName, eventCountLimit.ToString());
            Environment.SetEnvironmentVariable(SpanLimits.SpanLinkCountLimitEnvVarName, linkCountLimit.ToString());
            Environment.SetEnvironmentVariable(SpanLimits.EventAttributeCountLimitEnvVarName, attributePerEventCountLimit.ToString());
            Environment.SetEnvironmentVariable(SpanLimits.LinkAttributeCountLimitEnvVarName, attributePerLinkCountLimit.ToString());

            var spanLimits = new SpanLimits();

            Assert.Equal(spanLimits.AttributeCountLimit, attributeCountLimit);
            Assert.Equal(spanLimits.AttributeValueLengthLimit, attributeValueLengthLimit);
            Assert.Equal(spanLimits.SpanAttributeCountLimit, spanAttributeCountLimit);
            Assert.Equal(spanLimits.SpanAttributeValueLengthLimit, spanAttributeValueLengthLimit);
            Assert.Equal(spanLimits.EventCountLimit, eventCountLimit);
            Assert.Equal(spanLimits.LinkCountLimit, linkCountLimit);
            Assert.Equal(spanLimits.AttributePerEventCountLimit, attributePerEventCountLimit);
            Assert.Equal(spanLimits.AttributePerLinkCountLimit, attributePerLinkCountLimit);
        }

        [Fact]
        public void DirectOverridesEnvVar()
        {
            Environment.SetEnvironmentVariable(SpanLimits.AttributeCountLimitEnvVarName, "1");
            Environment.SetEnvironmentVariable(SpanLimits.AttributeValueLenghtLimitEnvVarName, "1");
            Environment.SetEnvironmentVariable(SpanLimits.SpanAttributeCountLimitEnvVarName, "1");    
            Environment.SetEnvironmentVariable(SpanLimits.SpanAttributeValueLengthLimitEnvVarName, "1");
            Environment.SetEnvironmentVariable(SpanLimits.SpanEventCountLimitEnvVarName, "1");
            Environment.SetEnvironmentVariable(SpanLimits.SpanLinkCountLimitEnvVarName, "1");
            Environment.SetEnvironmentVariable(SpanLimits.EventAttributeCountLimitEnvVarName, "1");
            Environment.SetEnvironmentVariable(SpanLimits.LinkAttributeCountLimitEnvVarName, "1");

            uint limit = 10;
            var spanLimits = new SpanLimits(limit, limit, limit, limit, limit, limit, limit, limit);

            Assert.Equal(spanLimits.AttributeCountLimit, limit);
            Assert.Equal(spanLimits.AttributeValueLengthLimit, limit);
            Assert.Equal(spanLimits.SpanAttributeCountLimit, limit);
            Assert.Equal(spanLimits.SpanAttributeValueLengthLimit, limit);
            Assert.Equal(spanLimits.EventCountLimit, limit);
            Assert.Equal(spanLimits.LinkCountLimit, limit);
            Assert.Equal(spanLimits.AttributePerEventCountLimit, limit);
            Assert.Equal(spanLimits.AttributePerLinkCountLimit, limit);
        }
    }
}
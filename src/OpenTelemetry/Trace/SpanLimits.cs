// <copyright file="SpanLimits.cs" company="OpenTelemetry Authors">
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

using OpenTelemetry.Internal;

namespace OpenTelemetry.Trace
{
    public class SpanLimits
    {
        internal const string AttributeValueLenghtLimitEnvVarName = "OTEL_ATTRIBUTE_VALUE_LENGTH_LIMIT";
        internal const string AttributeCountLimitEnvVarName = "OTEL_ATTRIBUTE_COUNT_LIMIT";
        internal const string SpanAttributeValueLengthLimitEnvVarName = "OTEL_SPAN_ATTRIBUTE_VALUE_LENGTH_LIMIT";
        internal const string SpanAttributeCountLimitEnvVarName = "OTEL_SPAN_ATTRIBUTE_COUNT_LIMIT";
        internal const string SpanEventCountLimitEnvVarName = "OTEL_SPAN_EVENT_COUNT_LIMIT";
        internal const string SpanLinkCountLimitEnvVarName = "OTEL_SPAN_LINK_COUNT_LIMIT";
        internal const string EventAttributeCountLimitEnvVarName = "OTEL_EVENT_ATTRIBUTE_COUNT_LIMIT";
        internal const string LinkAttributeCountLimitEnvVarName = "OTEL_LINK_ATTRIBUTE_COUNT_LIMIT";

        internal const uint DefaultAttributeCountLimit = 128;
        internal const uint DefaultSpanAttributeCountLimit = 128;
        internal const uint DefaultEventCountLimit = 128;
        internal const uint DefaultLinkCountLimit = 128;
        internal const uint DefaultAttributePerEventCountLimit = 128;
        internal const uint DefaultAttributePerLinkCountLimit = 128;

        internal uint AttributeCountLimit;
        internal uint? AttributeValueLengthLimit;
        internal uint SpanAttributeCountLimit;
        internal uint? SpanAttributeValueLengthLimit;
        internal uint EventCountLimit;
        internal uint LinkCountLimit;
        internal uint AttributePerEventCountLimit;
        internal uint AttributePerLinkCountLimit;

        public SpanLimits(uint? attributeCountLimit = null,
                            uint? attributeValueLengthLimit = null,
                            uint? spanAttributeCountLimit = null,
                            uint? spanAttributeValueLengthLimit = null,
                            uint? eventCountLimit = null,
                            uint? linkCountLimit = null,
                            uint? attributePerEventCountLimit = null,
                            uint? attributePerLinkCountLimit = null)
        {
            this.AttributeCountLimit = fromEnvIfAbsent(attributeCountLimit, AttributeCountLimitEnvVarName, DefaultAttributeCountLimit).Value;
            this.AttributeValueLengthLimit = fromEnvIfAbsent(attributeValueLengthLimit, AttributeValueLenghtLimitEnvVarName, null);
            this.SpanAttributeCountLimit = fromEnvIfAbsent(spanAttributeCountLimit, SpanAttributeCountLimitEnvVarName, DefaultSpanAttributeCountLimit).Value;
            this.SpanAttributeValueLengthLimit = fromEnvIfAbsent(spanAttributeValueLengthLimit, SpanAttributeValueLengthLimitEnvVarName, null);
            this.EventCountLimit = fromEnvIfAbsent(eventCountLimit, SpanEventCountLimitEnvVarName, DefaultEventCountLimit).Value;
            this.LinkCountLimit = fromEnvIfAbsent(linkCountLimit, SpanLinkCountLimitEnvVarName, DefaultLinkCountLimit).Value;
            this.AttributePerEventCountLimit = fromEnvIfAbsent(attributePerEventCountLimit, EventAttributeCountLimitEnvVarName, DefaultAttributePerEventCountLimit).Value;
            this.AttributePerLinkCountLimit = fromEnvIfAbsent(attributePerLinkCountLimit, LinkAttributeCountLimitEnvVarName, DefaultAttributePerLinkCountLimit).Value;
        }

        private uint? fromEnvIfAbsent(uint? value, string envVarKey, uint? defaultValue) {
            if (!value.HasValue) {
                int tmp;
                if (!EnvironmentVariableHelper.LoadNumeric(envVarKey, out tmp)) {
                    return defaultValue;
                }
                return (uint)tmp;
            }
            return value;
        }
    }
}

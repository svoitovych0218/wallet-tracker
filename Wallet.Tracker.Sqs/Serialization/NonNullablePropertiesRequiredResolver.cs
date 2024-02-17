﻿namespace Wallet.Tracker.Sqs.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

internal class NonNullablePropertiesRequiredResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        JsonProperty prop = base.CreateProperty(member, memberSerialization);
        Type propType = prop.PropertyType;
        if (propType.IsValueType && !(propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>)))
        {
            prop.Required = Required.Always;
        }
        return prop;
    }
}

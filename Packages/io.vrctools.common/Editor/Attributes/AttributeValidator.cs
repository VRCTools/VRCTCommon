// Copyright 2026 .start <https://dotstart.tv>
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

using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace VRCTools.Common.Editor.Attributes
{
  public abstract class AttributeValidator
  {
    private const string AttributeSuffix = "Attribute";

    /// <summary>
    /// Indicates the number of validations which have been reported in the last invocation of <see cref="Validate"/>.
    /// </summary>
    public int ViolationCount { get; protected set; }

    /// <summary>
    /// Unity event callback which invokes all implementations of this type found within the project scope whenever
    /// scripts are reloaded (e.g. have been re-compiled).
    /// </summary>
    [DidReloadScripts]
    public static void OnScriptReload()
    {
      foreach (var validator in TypeCache.GetTypesDerivedFrom<AttributeValidator>())
      {
        if (validator.IsAbstract)
        {
          continue;
        }

        if (validator.ContainsGenericParameters)
        {
          Debug.LogWarning($"<color=#fc9f2d>[Attribute Validator]</color> Validator <color=#fc64e8>{validator.Name}</color> defines generic parameters but has not been marked abstract - It will be skipped");
          continue;
        }

        try
        {
          var constructor = validator.GetConstructor(Type.EmptyTypes);
          if (constructor == null)
          {
            Debug.LogWarning($"<color=#fc9f2d>[Attribute Validator]</color> No constructor found for validator <color=#fc64e8>{validator.Name}</color> - It will be skipped");
            continue;
          }

          var instance = (AttributeValidator)constructor.Invoke(Array.Empty<object>());
          instance.Validate();

          if (instance.ViolationCount > 0)
          {
            Debug.LogWarning($"<color=#fc9f2d>[Attribute Validator]</color> <color=#fc64e8>{validator.Name}</color> reported <color=#fc64e8>{instance.ViolationCount}</color> violations");
          }
        } catch (Exception e)
        {
          Debug.LogException(e);
        }
      }
    }

    /// <summary>
    /// Validates the current state of the project.
    /// </summary>
    protected abstract void Validate();

    /// <summary>
    /// Returns a human-readable attribute name (e.g. drops the "Attribute" suffix where necessary).
    /// </summary>
    /// <typeparam name="A">an attribute type</typeparam>
    protected static string GetAttributeName<A>()
    {
      var attributeName = typeof(A).Name;
      if (attributeName.EndsWith(AttributeSuffix))
      {
        attributeName = attributeName.Substring(0, attributeName.Length - AttributeSuffix.Length);
      }

      return attributeName;
    }

    /// <summary>
    /// Returns an asset associated with a given offending type if possible.
    /// </summary>
    /// <param name="offendingType">an offending type</param>
    /// <returns>an asset reference or null</returns>
    protected static MonoScript FindViolationAsset(Type offendingType)
    {
      return MonoImporter.GetAllRuntimeMonoScripts().FirstOrDefault(s => s.GetClass() == offendingType);
    }

    /// <summary>
    /// Reports an attribute violation within a given type.
    /// </summary>
    /// <param name="definingType">the offending type</param>
    /// <param name="message">a descriptive message</param>
    protected static void ReportAttributeViolation<A>(Type definingType, string message) where A : Attribute
    {
      var attributeName = GetAttributeName<A>();
      var typeName = definingType.FullName;
      var context = FindViolationAsset(definingType);

      Debug.LogWarning($"<color=#fc9f2d>[Attribute Validator]</color> <color=#fc64e8>{attributeName}</color> violation within <color=#fc64e8>{typeName}</color>: {message}", context);
    }

    /// <summary>
    /// Reports an attribute violation within a given method.
    /// </summary>
    /// <param name="definingType">the offending type</param>
    /// <param name="method">the offending message</param>
    /// <param name="message">a descriptive message</param>
    protected static void ReportAttributeViolation<A>(MethodInfo method, string message) where A : Attribute
    {
      var attributeName = GetAttributeName<A>();
      var methodName = method.Name;

      var declaringType = method.DeclaringType;
      var typeName = declaringType != null ? declaringType.FullName : "???";
      var context = declaringType != null ? FindViolationAsset(declaringType) : null;

      Debug.LogWarning($"<color=#fc9f2d>[Attribute Validator]</color> <color=#fc64e8>{attributeName}</color> violation within <color=#fc64e8>{typeName}</color>#<color=#fc647b>{methodName}</color>: {message}", context);
    }
  }
}

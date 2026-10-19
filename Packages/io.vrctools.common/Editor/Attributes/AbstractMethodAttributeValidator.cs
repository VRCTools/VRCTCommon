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
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;

namespace VRCTools.Common.Editor.Attributes
{
  /// <summary>
  /// Provides a basis for attribute validators which act entirely on method level.
  /// </summary>
  /// <typeparam name="A">an attribute type</typeparam>
  public abstract class AbstractMethodAttributeValidator<A> : AbstractAttributeValidator<A> where A : Attribute
  {
    protected sealed override void Validate()
    {
      foreach (var method in TypeCache.GetMethodsWithAttribute<A>())
      {
        var attribute = method.GetCustomAttribute<A>();
        this.ValidateMethod(attribute, method);
      }
    }

    /// <summary>
    /// Validates a given annotated method against the attribute's internal rules.
    /// </summary>
    /// <param name="attribute">the attribute present on the given method</param>
    /// <param name="method">the method</param>
    protected abstract void ValidateMethod(A attribute, MethodInfo method);
  }
}

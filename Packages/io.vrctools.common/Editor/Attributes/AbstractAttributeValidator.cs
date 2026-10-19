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

namespace VRCTools.Common.Editor.Attributes
{
  /// <summary>
  /// Provides a simple basis for more specific attribute validator implementations.
  /// </summary>
  /// <typeparam name="A">an attribute type</typeparam>
  public abstract class AbstractAttributeValidator<A> : AttributeValidator where A : Attribute
  {
    protected override void Validate()
    {
      this.ViolationCount = 0;
    }

    protected void ReportViolation(Type definingType, string message)
    {
      this.ViolationCount++;
      ReportAttributeViolation<A>(definingType, message);
    }

    protected void ReportViolation(MethodInfo method, string message)
    {
      this.ViolationCount++;
      ReportAttributeViolation<A>(method, message);
    }
  }
}

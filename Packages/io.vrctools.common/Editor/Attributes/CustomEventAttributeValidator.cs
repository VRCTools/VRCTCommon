// Copyright 2025-2026 .start <https://dotstart.tv>
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

using System.Reflection;
using VRCTools.Common.Attributes;

namespace VRCTools.Common.Editor.Attributes
{
  /// <summary>
  /// Validates the constraints imposed by <see cref="CustomEventAttribute"/>.
  /// </summary>
  public class CustomEventAttributeValidator : AbstractMethodAttributeValidator<CustomEventAttribute>
  {
    protected override void ValidateMethod(CustomEventAttribute attribute, MethodInfo method)
    {
      if (!method.IsPublic)
      {
        this.ReportViolation(method, "Method is not public - This event handler will never be invoked");
      }

      if (method.IsStatic)
      {
        this.ReportViolation(method, "Method is static - This event handler will never be invoked");
      }

      if (method.GetParameters().Length != 0)
      {
        this.ReportViolation(method, "Method declares one or more parameters - This event handler cannot not be invoked");
      }

      if (!attribute.permitNetworkCalls && !method.Name.StartsWith("_"))
      {
        this.ReportViolation(method, "Method should start with an underscore - Otherwise invocation via the network is permitted");
      }
    }
  }
}

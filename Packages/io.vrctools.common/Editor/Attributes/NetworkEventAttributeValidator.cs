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
using VRC.SDK3.UdonNetworkCalling;
using VRCTools.Common.Attributes;

namespace VRCTools.Common.Editor.Attributes
{
  /// <summary>
  /// Checks the constraints imposed by <see cref="NetworkEventAttribute"/>.
  /// </summary>
  public class NetworkEventAttributeValidator : AbstractMethodAttributeValidator<NetworkEventAttribute>
  {
    protected override void ValidateMethod(NetworkEventAttribute attribute, MethodInfo method)
    {
      if (!method.IsPublic)
      {
        this.ReportViolation(method, "Method is not public - Event handler will never be invoked");
      }

      if (method.IsStatic)
      {
        this.ReportViolation(method, "Method is static - Event handler will never be invoked");
      }

      if (method.Name.StartsWith("_"))
      {
        this.ReportViolation(method, "Method name is prefixed with underscore - Event handler will never be invoked");
      }

      if (method.GetParameters().Length > 8)
      {
        this.ReportViolation(method,
          "Method has too many parameters - Network event handlers are restricted to a maximum of 8 parameters");
      }

      if (((NetworkCallableAttribute[]) method.GetCustomAttributes<NetworkCallableAttribute>()).Length == 0)
      {
        this.ReportViolation(method, "Method has not been marked network callable - Event handler may not be invoked");
      }
    }
  }
}

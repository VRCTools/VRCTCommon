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

using System.Linq;
using System.Reflection;
using UnityEngine;
using VRCTools.Common.Attributes;

namespace VRCTools.Common.Editor.Attributes
{
  /// <summary>
  /// Validates the constraints specified by <see cref="EventHookAttribute"/>.
  /// </summary>
  public class EventHookAttributeValidator : AbstractMethodAttributeValidator<EventHookAttribute>
  {
    private const byte OpcodeNoop = 0x00;
    private const byte OpcodeReturn = 0x2A;

    protected override void ValidateMethod(EventHookAttribute attribute, MethodInfo method)
    {
      var declaringType = method.DeclaringType;

      // ensure the attribute is present on a top level definition - we skip all other checks if this is not the case
      // since the attribute was likely applied by mistake
      if (method.GetBaseDefinition().DeclaringType != declaringType)
      {
        this.ReportViolation(method, "Only base declarations may be marked as hooks - Annotate the root definition or omit this attribute");
        return;
      }

      // ensure the annotated method may be overridden
      if (declaringType == null || declaringType.IsSealed)
      {
        this.ReportViolation(method, "Hook method cannot be overriden due to class restrictions");
      }

      if (!method.IsVirtual)
      {
        this.ReportViolation(method, "Hook method cannot be overriden as it is not marked virtual");
      } else if (method.IsFinal)
      {
        this.ReportViolation(method, "Hook method cannot be overriden as it is final");
      }

      if (method.IsAbstract)
      {
        this.ReportViolation(method, "Hook method should not be abstract as this forces subclasses to implement it");
      }

      // check method body for instructions - we expect all hooks to be declared as empty methods
      var body = method.GetMethodBody();
      if (body != null && body.GetILAsByteArray().Any(opcode => opcode != OpcodeNoop && opcode != OpcodeReturn))
      {
        this.ReportViolation(method, "Hook method should always be empty");
      }
    }
  }
}

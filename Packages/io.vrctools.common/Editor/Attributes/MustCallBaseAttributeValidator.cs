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

using System.Reflection;
using VRCTools.Common.Attributes;

namespace VRCTools.Common.Editor.Attributes
{
  /// <summary>
  /// Evaluates the constraints defined by <see cref="MustCallBaseAttribute"/>
  /// </summary>
  public class MustCallBaseAttributeValidator : AbstractMethodAttributeValidator<MustCallBaseAttribute>
  {
    protected override void ValidateMethod(MustCallBaseAttribute attribute, MethodInfo method)
    {
      if (!method.IsVirtual || method.IsFinal)
      {
        this.ReportViolation(method,
          "Annotated method cannot be overridden as it is not marked abstract/virtual or has been marked final");
      }

      // TODO: We could parse IL to figure out whether derived classes work correctly - let's think about this later
      //       since this is rather complex
    }
  }
}

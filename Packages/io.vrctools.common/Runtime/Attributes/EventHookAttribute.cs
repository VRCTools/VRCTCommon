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

namespace VRCTools.Common.Attributes
{
  /// <summary>
  /// Marks an annotated method as an event hook.
  ///
  /// By definition, event hooks are expected to be virtual methods (hence permitting overriding) and their body should
  /// be empty within the defining type. As such, implementors are not required to invoke the base method when
  /// implementing methods annotated by this attribute unless they intend to inherit the functionality of an
  /// intermediate implementation.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public class EventHookAttribute : Attribute
  {
  }
}

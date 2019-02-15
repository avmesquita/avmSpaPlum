/*
Copyright © 2005 - 2016 Annpoint, s.r.o.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

-------------------------------------------------------------------------

NOTE: Reuse requires the following acknowledgement (see also NOTICE):
This product includes DayPilot (http://www.daypilot.org) developed by Annpoint, s.r.o.
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayPilot.Web.Mvc.Enums
{
    /// <summary>
    /// Enumeration of possible week start values.
    /// </summary>
    public enum WeekStarts
    {

        /// <summary>
        /// Week starts on Sunday.
        /// </summary>
        Sunday,

        /// <summary>
        /// Week starts on Monday.
        /// </summary>
        Monday,

        /// <summary>
        /// Week starts on Tuesday.
        /// </summary>
        Tuesday,

        /// <summary>
        /// Week starts on Wednesday.
        /// </summary>
        Wednesday,

        /// <summary>
        /// Week starts on Thursday.
        /// </summary>
        Thursday,

        /// <summary>
        /// Week starts on Friday.
        /// </summary>
        Friday,

        /// <summary>
        /// Week starts on Saturday.
        /// </summary>
        Saturday,

        /// <summary>
        /// The week start is detected using the current culture.
        /// </summary>
        Auto
    }
}

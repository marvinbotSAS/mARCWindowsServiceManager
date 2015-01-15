/*
 * Copyright (C) 2015 Marvinbot S.A.S
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Runtime.Serialization;

namespace Misc
{
    [Serializable]
    class CreateConfigFileException : Exception
    {
        public CreateConfigFileException()
            : base()
        { }

        public CreateConfigFileException(string message)
            : base(message)
        { }

        public CreateConfigFileException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public CreateConfigFileException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }

    [Serializable]
    class CreateConfigDirException : Exception
    {
        public CreateConfigDirException()
            : base()
        { }

        public CreateConfigDirException(string message)
            : base(message)
        { }

        public CreateConfigDirException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public CreateConfigDirException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }

    [Serializable]
    class FileDownloadException : Exception
    {
        public FileDownloadException()
            : base()
        { }

        public FileDownloadException(string message)
            : base(message)
        { }

        public FileDownloadException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public FileDownloadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}

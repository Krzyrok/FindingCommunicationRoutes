using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindingCommunicationRoutes
{
    public static class ExtensionForString
    {
        public static string GetLastCharacters(this string sourceString, int howManyCharactersFromTheLastPosition)
        {
            if (howManyCharactersFromTheLastPosition >= sourceString.Length)
                return sourceString;
            if (howManyCharactersFromTheLastPosition < 0)
            {
                throw new ArgumentException("Negative value of 'howManyCharactersFromTheLastPosition'");
            }
            return sourceString.Substring(sourceString.Length - howManyCharactersFromTheLastPosition);
        }
    }
}

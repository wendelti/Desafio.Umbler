
using System.Text.RegularExpressions;

namespace Desafio.Umbler.Infra.CrossCutting
{
    public static class StringHelper
    {

        public static bool IsValidDomain(string domain)
        {
            Regex regex = new Regex(@"^(?!:\/\/)([a-zA-Z0-9-]+\.){0,5}[a-zA-Z0-9-][a-zA-Z0-9-]+\.[a-zA-Z]{2,64}?$");
            Match match = regex.Match(domain);
            return match.Success;
        }

    }
}
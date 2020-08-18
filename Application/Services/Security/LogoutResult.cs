using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Security
{
    public class LogoutResult
    {
        public bool Success => !Errors.Any();

        public IEnumerable<string> Errors { get; set; }

        public LogoutResult()
        {
            this.Errors = new List<string>();
        }
    }
}

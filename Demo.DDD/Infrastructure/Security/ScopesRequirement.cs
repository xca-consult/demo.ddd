using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Demo.DDD.Security
{
    public class ScopesRequirement : IAuthorizationRequirement
    {
        public List<string> Items { get; }

        public ScopesRequirement(List<string> items)
        {
            Items = items;
        }
    }
}
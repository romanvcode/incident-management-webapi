using IncidentManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace IncidentManagement.WebAPI.Helpers
{
    public static class IncidentNameGenerator
    {
        public static async Task<string> GenerateNextKey(ApplicationDbContext context)
        {
            var lastIncident = await context.Incidents
                .OrderByDescending(i => i.IncidentName)
                .FirstOrDefaultAsync();

            if (lastIncident == null || string.IsNullOrEmpty(lastIncident.IncidentName))
            {
                return "INC001";
            }

            var lastNumber = int.Parse(lastIncident.IncidentName.Substring(3));
            var nextNumber = lastNumber + 1;
            return $"INC{nextNumber:000}";
        }
    }
}

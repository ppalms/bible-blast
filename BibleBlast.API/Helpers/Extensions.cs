using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BibleBlast.API.Helpers
{
    public static class Extensions
    {
        public static void ApplySingularTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.DisplayName() == "User" || entity.DisplayName() == "Role" || entity.DisplayName() == "UserRole")
                {
                    continue;
                }

                entity.Relational().TableName = entity.DisplayName();
            }
        }

        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(new { currentPage, itemsPerPage, totalItems, totalPages }));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}

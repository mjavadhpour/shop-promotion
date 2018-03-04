using System.Threading.Tasks;
using ShopPromotion.API.Infrastructure.Models.Parameter;

namespace ShopPromotion.API.Services.Statistics
{
    /// <inheritdoc />
    public class UsageStatisticservice : IUsageStatisticservice
    {
        /// <inheritdoc />
        public Task<object> GetUsagesChartReport(UsageStatisticsParameters reportParameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
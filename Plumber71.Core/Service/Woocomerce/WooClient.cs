using Plumber71.Core.Service.Woocomerce.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v3;
using WooCommerceNET.WooCommerce.v3.Extension;

namespace Plumber71.Core.Service.Woocomerce
{
    public class WooClient
    {
        private readonly WCObject client;
        public WooClient()
        {
            RestConfig config = RestConfig.GetDefaults();
            RestAPI rest = new RestAPI($"{config.Server}/wp-json/wc/v3/", config.UserKey, config.SecretKey);
            client = new WCObject(rest);
        }

        public async Task<List<Product>> GetProductsPage(int count = 10, int page = 1)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
            {
                { "page", $"{page}" },
                { "per_page", $"{count}" }
            };
            List<Product> products = await client.Product.GetAll(keyValuePairs);
            return products;
        }
    }
}

using Newtonsoft.Json;
using Plumber71.Core.Service.Woocomerce.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooCommerceNET;
using WooCommerceNET.Base;
using WooCommerceNET.WooCommerce.v3;
using WooCommerceNET.WooCommerce.v3.Extension;

namespace Plumber71.Core.Service.Woocomerce
{
    public class WooClient
    {
        public const int PRODUCTS_PER_PAGE = 100;
        private readonly WCObject client;
        //TODO PUT RestConfig in ctor
        public WooClient(RestConfig config)
        {
            RestAPI rest = new RestAPI($"{config.Server}/wp-json/wc/v3/", config.UserKey, config.SecretKey);
            client = new WCObject(rest);
        }

        public WooClient(string restConfigJson)
        {
            RestConfig config = JsonConvert.DeserializeObject<RestConfig>(restConfigJson);
            RestAPI rest = new RestAPI($"{config.Server}/wp-json/wc/v3/", config.UserKey, config.SecretKey);
            client = new WCObject(rest);
        }

        public static WooClient DefaultClient()
        {
            RestConfig config = RestConfig.GetDefaults();
            WooClient wooClient = new WooClient(config);
            return wooClient;
        }

        public async Task<List<Product>> GetProductsPage(int count = PRODUCTS_PER_PAGE, int page = 1)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
            {
                { "page", $"{page}" },
                { "per_page", $"{count}" }
            };
            List<Product> products = await client.Product.GetAll(keyValuePairs);
            return products;
        }

        public async Task<BatchObject<Product>> BatchProductRange(BatchObject<Product> products)
        {
            return await client.Product.UpdateRange(products);
        }

        public async Task<IEnumerable<Product>> UpdateProductRange(IEnumerable<Product> products)
        {
            BatchObject<Product> batchObject = new BatchObject<Product>
            {
                update = products.ToList()
            };
            batchObject = await client.Product.UpdateRange(batchObject);
            return batchObject.update;
        }
    }
}

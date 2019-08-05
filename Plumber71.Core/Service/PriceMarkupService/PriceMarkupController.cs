using Plumber71.Core.Model;
using Plumber71.Core.Service.JsonFileService;
using Plumber71.Core.Service.PriceMarkupService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plumber71.Core.Controller
{
    public class PriceMarkupController
    {
        private PriceMarkupConfig priceMarkupConfig;
        public PriceMarkupController()
        {
            priceMarkupConfig = JsonFileStorage.Load<PriceMarkupConfig>() ?? new PriceMarkupConfig();
        }

        public void SetGlobalRate(double rate)
        {
            priceMarkupConfig.GlobalRate = rate;
        }

        public void SetCategoryRate(string categoryName, double rate)
        {
            priceMarkupConfig.CategoryRate[categoryName] = rate;
        }

        public void SetProductRate(int productKey, double rate)
        {
            priceMarkupConfig.ProductRate[productKey] = rate;
        }

        public IEnumerable<PlumberCategory> ApplySetting(IEnumerable<PlumberCategory> plumberCatalogue)
        {
            JsonFileStorage.Save(priceMarkupConfig);
            foreach (var category in plumberCatalogue)
            {
                bool categoryRateExist = priceMarkupConfig.CategoryRate.TryGetValue(category.Name, out double categoryRate);
                foreach (var product in category.Products)
                {
                    bool producRateExist = priceMarkupConfig.ProductRate.TryGetValue(product.Id, out double productRate);
                    if (producRateExist)
                        product.TotalPrice = product.RegularPrice * productRate;
                    else if (categoryRateExist)
                        product.TotalPrice = product.RegularPrice * categoryRate;
                    else
                        product.TotalPrice = product.RegularPrice * priceMarkupConfig.GlobalRate;
                }
            }
            return plumberCatalogue;
        }
    }
}

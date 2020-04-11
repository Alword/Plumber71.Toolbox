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
        private string priceConfigName = null;
        private PriceMarkupConfig priceMarkupConfig;
        public PriceMarkupController(string priceConfigName = null)
        {
            this.priceConfigName = priceConfigName;
            priceMarkupConfig = new PriceMarkupConfig();
        }

        public void SetGlobalRate(double rate)
        {
            priceMarkupConfig.GlobalRate = rate;
        }

        public double GetGlobalRate()
        {
            return priceMarkupConfig.GlobalRate;
        }

        public void SetCategoryRate(string categoryName, double rate)
        {
            priceMarkupConfig.CategoryRate[categoryName] = rate;
        }

        public void SetProductRate(int productKey, double rate)
        {
            priceMarkupConfig.ProductRate[productKey] = rate;
        }

        public IEnumerable<CategoryDTO> ApplySetting(IEnumerable<CategoryDTO> plumberCatalogue)
        {
            JsonFileStorage.Save(priceMarkupConfig, priceConfigName);
            foreach (var category in plumberCatalogue)
            {
                bool categoryRateExist = priceMarkupConfig.CategoryRate.TryGetValue(category.Name, out double categoryRate);
                foreach (var product in category.Products)
                {
                    bool producRateExist = priceMarkupConfig.ProductRate.TryGetValue(product.Id, out double productRate);
                    if (producRateExist)
                        product.TotalPrice *= productRate;
                    else if (categoryRateExist)
                        product.TotalPrice *= categoryRate;
                    else
                        product.TotalPrice *= priceMarkupConfig.GlobalRate;
                }
            }
            return plumberCatalogue;
        }

        public PricelistDTO ApplySetting(PricelistDTO plumberCatalogue)
        {
            plumberCatalogue.Categories = ApplySetting(plumberCatalogue.Categories).ToList();
            return plumberCatalogue;
        }
    }
}

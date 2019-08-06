using Plumber71.Core.Abstractions;
using Plumber71.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Model
{
    public class PricelistDTO : PricelistAbstraction<CategoryDTO,ProductDTO>
    {
        public Currencies ProductsCurrency;
    }
}

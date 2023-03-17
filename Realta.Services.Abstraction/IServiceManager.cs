﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Services.Abstraction
{
    public interface IServiceManager
    {
        IPriceItemsPhotoService PriceItemsPhotoService { get; }
        ICategoryGroupPhotoService CategoryGroupPhotoService { get; }

        
    }
}

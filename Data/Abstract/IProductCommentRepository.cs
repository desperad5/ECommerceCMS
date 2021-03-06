﻿using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Abstract
{
    public interface IProductCommentRepository: IEntityBaseRepository<ProductComment>
    {
    }
}

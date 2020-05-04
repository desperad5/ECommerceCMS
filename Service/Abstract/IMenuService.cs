using ECommerceCMS.Data.Entity;
using ECommerceCMS.Models;
using ECommerceCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Service.Abstract
{
    public interface IMenuService
    {
        Menu GetMenuById(int menuId);
    }
}

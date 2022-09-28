using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManagement_WebApp.Repository
{
    public class DevicesRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DevicesRepository(ConnectedOfficeContext context) : base(context)
        {

        }        
    }
}

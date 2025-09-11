using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IRouteRepository
    {
        Route GetById(int routeId);
        List<Route> GetAll();
        List<Route> GetActiveRoutes();
        List<Route> SearchRoutes(string searchTerm);
        int Create(Route route);
        bool Update(Route route);
        bool Delete(int routeId);
        bool IsRouteNameExists(string routeName, int? excludeRouteId = null);
    }
}


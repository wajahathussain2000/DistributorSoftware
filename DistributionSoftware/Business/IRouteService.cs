using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IRouteService
    {
        Route GetById(int routeId);
        List<Route> GetAll();
        List<Route> GetActiveRoutes();
        List<Route> SearchRoutes(string searchTerm);
        int Create(Route route);
        bool Update(Route route);
        bool Delete(int routeId);
        bool ValidateRoute(Route route, out string errorMessage);
        bool IsRouteNameExists(string routeName, int? excludeRouteId = null);
    }
}


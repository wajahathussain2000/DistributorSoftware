using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;

        public RouteService()
        {
            _routeRepository = new RouteRepository();
        }

        public Route GetById(int routeId)
        {
            try
            {
                return _routeRepository.GetById(routeId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving route: {ex.Message}", ex);
            }
        }

        public List<Route> GetAll()
        {
            try
            {
                return _routeRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving routes: {ex.Message}", ex);
            }
        }

        public List<Route> GetActiveRoutes()
        {
            try
            {
                return _routeRepository.GetActiveRoutes();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving active routes: {ex.Message}", ex);
            }
        }

        public List<Route> SearchRoutes(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return GetAll();
                }

                return _routeRepository.SearchRoutes(searchTerm);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching routes: {ex.Message}", ex);
            }
        }

        public int Create(Route route)
        {
            try
            {
                // Validate route
                if (!ValidateRoute(route, out string errorMessage))
                {
                    throw new Exception(errorMessage);
                }

                // Set created by user
                route.CreatedBy = UserSession.CurrentUser?.UserId ?? 1;
                route.CreatedDate = DateTime.Now;

                return _routeRepository.Create(route);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating route: {ex.Message}", ex);
            }
        }

        public bool Update(Route route)
        {
            try
            {
                // Validate route
                if (!ValidateRoute(route, out string errorMessage))
                {
                    throw new Exception(errorMessage);
                }

                // Set modified by user
                route.ModifiedBy = UserSession.CurrentUser?.UserId ?? 1;
                route.ModifiedDate = DateTime.Now;

                return _routeRepository.Update(route);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating route: {ex.Message}", ex);
            }
        }

        public bool Delete(int routeId)
        {
            try
            {
                // Check if route exists
                var route = _routeRepository.GetById(routeId);
                if (route == null)
                {
                    throw new Exception("Route not found.");
                }

                // TODO: Add business logic to check if route is being used in delivery challans
                // For now, we'll allow deletion

                return _routeRepository.Delete(routeId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting route: {ex.Message}", ex);
            }
        }

        public bool ValidateRoute(Route route, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (route == null)
            {
                errorMessage = "Route cannot be null.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(route.RouteName))
            {
                errorMessage = "Route name is required.";
                return false;
            }

            if (route.RouteName.Length > 100)
            {
                errorMessage = "Route name cannot exceed 100 characters.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(route.StartLocation) && route.StartLocation.Length > 200)
            {
                errorMessage = "Start location cannot exceed 200 characters.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(route.EndLocation) && route.EndLocation.Length > 200)
            {
                errorMessage = "End location cannot exceed 200 characters.";
                return false;
            }

            if (route.Distance.HasValue && route.Distance.Value < 0)
            {
                errorMessage = "Distance cannot be negative.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(route.EstimatedTime) && route.EstimatedTime.Length > 50)
            {
                errorMessage = "Estimated time cannot exceed 50 characters.";
                return false;
            }

            // Check for duplicate route name
            if (IsRouteNameExists(route.RouteName, route.RouteId > 0 ? route.RouteId : (int?)null))
            {
                errorMessage = "Route name already exists.";
                return false;
            }

            return true;
        }

        public bool IsRouteNameExists(string routeName, int? excludeRouteId = null)
        {
            try
            {
                return _routeRepository.IsRouteNameExists(routeName, excludeRouteId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking route name existence: {ex.Message}", ex);
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class RouteRepository : IRouteRepository
    {
        private readonly string _connectionString;

        public RouteRepository()
        {
            _connectionString = ConfigurationManager.DistributionConnectionString;
        }

        public Route GetById(int routeId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT RouteID, RouteName, StartLocation, EndLocation, Distance, 
                               EstimatedTime, IsActive, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                        FROM RouteMaster 
                        WHERE RouteID = @RouteId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RouteId", routeId);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapRoute(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving route: {ex.Message}", ex);
            }

            return null;
        }

        public List<Route> GetAll()
        {
            var routes = new List<Route>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT RouteID, RouteName, StartLocation, EndLocation, Distance, 
                               EstimatedTime, IsActive, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                        FROM RouteMaster 
                        ORDER BY RouteName";

                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            routes.Add(MapRoute(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving routes: {ex.Message}", ex);
            }

            return routes;
        }

        public List<Route> GetActiveRoutes()
        {
            var routes = new List<Route>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT RouteID, RouteName, StartLocation, EndLocation, Distance, 
                               EstimatedTime, IsActive, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                        FROM RouteMaster 
                        WHERE IsActive = 1
                        ORDER BY RouteName";

                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            routes.Add(MapRoute(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving active routes: {ex.Message}", ex);
            }

            return routes;
        }

        public List<Route> SearchRoutes(string searchTerm)
        {
            var routes = new List<Route>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT RouteID, RouteName, StartLocation, EndLocation, Distance, 
                               EstimatedTime, IsActive, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                        FROM RouteMaster 
                        WHERE RouteName LIKE @SearchTerm 
                           OR StartLocation LIKE @SearchTerm 
                           OR EndLocation LIKE @SearchTerm
                        ORDER BY RouteName";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                routes.Add(MapRoute(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching routes: {ex.Message}", ex);
            }

            return routes;
        }

        public int Create(Route route)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        INSERT INTO RouteMaster (RouteName, StartLocation, EndLocation, Distance, 
                                               EstimatedTime, IsActive, CreatedDate, CreatedBy)
                        OUTPUT INSERTED.RouteID
                        VALUES (@RouteName, @StartLocation, @EndLocation, @Distance, 
                                @EstimatedTime, @IsActive, @CreatedDate, @CreatedBy)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RouteName", route.RouteName);
                        command.Parameters.AddWithValue("@StartLocation", route.StartLocation ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EndLocation", route.EndLocation ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Distance", route.Distance ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EstimatedTime", route.EstimatedTime ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsActive", route.Status);
                        command.Parameters.AddWithValue("@CreatedDate", route.CreatedDate);
                        command.Parameters.AddWithValue("@CreatedBy", route.CreatedBy ?? (object)DBNull.Value);

                        return (int)command.ExecuteScalar();
                    }
                }
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
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        UPDATE RouteMaster 
                        SET RouteName = @RouteName, 
                            StartLocation = @StartLocation, 
                            EndLocation = @EndLocation, 
                            Distance = @Distance, 
                            EstimatedTime = @EstimatedTime, 
                            IsActive = @IsActive, 
                            ModifiedDate = @ModifiedDate, 
                            ModifiedBy = @ModifiedBy
                        WHERE RouteID = @RouteId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RouteId", route.RouteId);
                        command.Parameters.AddWithValue("@RouteName", route.RouteName);
                        command.Parameters.AddWithValue("@StartLocation", route.StartLocation ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EndLocation", route.EndLocation ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Distance", route.Distance ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EstimatedTime", route.EstimatedTime ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsActive", route.Status);
                        command.Parameters.AddWithValue("@ModifiedDate", route.ModifiedDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedBy", route.ModifiedBy ?? (object)DBNull.Value);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
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
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "DELETE FROM RouteMaster WHERE RouteID = @RouteId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RouteId", routeId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting route: {ex.Message}", ex);
            }
        }

        public bool IsRouteNameExists(string routeName, int? excludeRouteId = null)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT COUNT(*) FROM RouteMaster WHERE RouteName = @RouteName";
                    
                    if (excludeRouteId.HasValue)
                    {
                        query += " AND RouteID != @ExcludeRouteId";
                    }

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RouteName", routeName);
                        if (excludeRouteId.HasValue)
                        {
                            command.Parameters.AddWithValue("@ExcludeRouteId", excludeRouteId.Value);
                        }

                        return (int)command.ExecuteScalar() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking route name existence: {ex.Message}", ex);
            }
        }

        private Route MapRoute(IDataReader reader)
        {
            return new Route
            {
                RouteId = reader.GetInt32(reader.GetOrdinal("RouteID")),
                RouteName = reader.GetString(reader.GetOrdinal("RouteName")),
                StartLocation = reader.IsDBNull(reader.GetOrdinal("StartLocation"))
                                ? null
                                : reader.GetString(reader.GetOrdinal("StartLocation")),
                EndLocation = reader.IsDBNull(reader.GetOrdinal("EndLocation"))
                              ? null
                              : reader.GetString(reader.GetOrdinal("EndLocation")),
                Distance = reader.IsDBNull(reader.GetOrdinal("Distance"))
                           ? null
                           : (decimal?)reader.GetDecimal(reader.GetOrdinal("Distance")),
                EstimatedTime = reader.IsDBNull(reader.GetOrdinal("EstimatedTime"))
                                ? null
                                : reader.GetString(reader.GetOrdinal("EstimatedTime")),
                Status = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy"))
                            ? null
                            : (int?)reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))
                               ? null
                               : (DateTime?)reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
                ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy"))
                             ? null
                             : (int?)reader.GetInt32(reader.GetOrdinal("ModifiedBy"))
            };
        }

    }
}



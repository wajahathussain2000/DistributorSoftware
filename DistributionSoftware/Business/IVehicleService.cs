using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IVehicleService
    {
        List<Vehicle> GetAllVehicles();
        List<Vehicle> GetActiveVehicles();
        Vehicle GetVehicleById(int vehicleId);
        Vehicle GetVehicleByNumber(string vehicleNo);
        List<Vehicle> SearchVehicles(string searchTerm);
        bool CreateVehicle(Vehicle vehicle);
        bool UpdateVehicle(Vehicle vehicle);
        bool DeleteVehicle(int vehicleId);
        bool IsVehicleNumberExists(string vehicleNo, int excludeVehicleId = 0);
        string ValidateVehicle(Vehicle vehicle);
    }
}

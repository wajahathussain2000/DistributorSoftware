using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;

namespace DistributionSoftware.Business
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public VehicleService()
        {
            _vehicleRepository = new VehicleRepository();
        }

        public List<Vehicle> GetAllVehicles()
        {
            try
            {
                return _vehicleRepository.GetAllVehicles();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting all vehicles: {ex.Message}");
                throw new Exception($"Error retrieving vehicles: {ex.Message}", ex);
            }
        }

        public List<Vehicle> GetActiveVehicles()
        {
            try
            {
                return _vehicleRepository.GetActiveVehicles();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting active vehicles: {ex.Message}");
                throw new Exception($"Error retrieving active vehicles: {ex.Message}", ex);
            }
        }

        public Vehicle GetVehicleById(int vehicleId)
        {
            try
            {
                if (vehicleId <= 0)
                    throw new ArgumentException("Invalid vehicle ID");

                return _vehicleRepository.GetVehicleById(vehicleId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting vehicle by ID: {ex.Message}");
                throw new Exception($"Error retrieving vehicle: {ex.Message}", ex);
            }
        }

        public Vehicle GetVehicleByNumber(string vehicleNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(vehicleNo))
                    throw new ArgumentException("Vehicle number cannot be empty");

                return _vehicleRepository.GetVehicleByNumber(vehicleNo);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting vehicle by number: {ex.Message}");
                throw new Exception($"Error retrieving vehicle: {ex.Message}", ex);
            }
        }

        public List<Vehicle> SearchVehicles(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return GetAllVehicles();

                return _vehicleRepository.SearchVehicles(searchTerm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error searching vehicles: {ex.Message}");
                throw new Exception($"Error searching vehicles: {ex.Message}", ex);
            }
        }

        public bool CreateVehicle(Vehicle vehicle)
        {
            try
            {
                // Validate vehicle data
                string validationError = ValidateVehicle(vehicle);
                if (!string.IsNullOrEmpty(validationError))
                    throw new ArgumentException(validationError);

                // Check if vehicle number already exists
                if (_vehicleRepository.IsVehicleNumberExists(vehicle.VehicleNo))
                    throw new ArgumentException("Vehicle number already exists");

                // Set default values
                vehicle.CreatedDate = DateTime.Now;
                vehicle.IsActive = true;

                return _vehicleRepository.CreateVehicle(vehicle);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating vehicle: {ex.Message}");
                throw new Exception($"Error creating vehicle: {ex.Message}", ex);
            }
        }

        public bool UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                // Validate vehicle data
                string validationError = ValidateVehicle(vehicle);
                if (!string.IsNullOrEmpty(validationError))
                    throw new ArgumentException(validationError);

                // Check if vehicle number already exists (excluding current vehicle)
                if (_vehicleRepository.IsVehicleNumberExists(vehicle.VehicleNo, vehicle.VehicleId))
                    throw new ArgumentException("Vehicle number already exists");

                // Set modification values
                vehicle.ModifiedDate = DateTime.Now;

                return _vehicleRepository.UpdateVehicle(vehicle);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating vehicle: {ex.Message}");
                throw new Exception($"Error updating vehicle: {ex.Message}", ex);
            }
        }

        public bool DeleteVehicle(int vehicleId)
        {
            try
            {
                if (vehicleId <= 0)
                    throw new ArgumentException("Invalid vehicle ID");

                // Check if vehicle exists
                var vehicle = _vehicleRepository.GetVehicleById(vehicleId);
                if (vehicle == null)
                    throw new ArgumentException("Vehicle not found");

                return _vehicleRepository.DeleteVehicle(vehicleId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting vehicle: {ex.Message}");
                throw new Exception($"Error deleting vehicle: {ex.Message}", ex);
            }
        }

        public bool IsVehicleNumberExists(string vehicleNo, int excludeVehicleId = 0)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(vehicleNo))
                    return false;

                return _vehicleRepository.IsVehicleNumberExists(vehicleNo, excludeVehicleId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking vehicle number existence: {ex.Message}");
                throw new Exception($"Error checking vehicle number: {ex.Message}", ex);
            }
        }

        public string ValidateVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                return "Vehicle data is required";

            if (string.IsNullOrWhiteSpace(vehicle.VehicleNo))
                return "Vehicle number is required";

            if (vehicle.VehicleNo.Length > 50)
                return "Vehicle number cannot exceed 50 characters";

            // Validate vehicle number format (basic validation)
            if (!IsValidVehicleNumber(vehicle.VehicleNo))
                return "Invalid vehicle number format";

            if (!string.IsNullOrWhiteSpace(vehicle.VehicleType) && vehicle.VehicleType.Length > 50)
                return "Vehicle type cannot exceed 50 characters";

            if (!string.IsNullOrWhiteSpace(vehicle.DriverName) && vehicle.DriverName.Length > 100)
                return "Driver name cannot exceed 100 characters";

            if (!string.IsNullOrWhiteSpace(vehicle.DriverContact) && vehicle.DriverContact.Length > 20)
                return "Driver contact cannot exceed 20 characters";

            if (!string.IsNullOrWhiteSpace(vehicle.DriverContact) && !IsValidPhoneNumber(vehicle.DriverContact))
                return "Invalid driver contact number format";

            if (!string.IsNullOrWhiteSpace(vehicle.TransporterName) && vehicle.TransporterName.Length > 100)
                return "Transporter name cannot exceed 100 characters";

            if (!string.IsNullOrWhiteSpace(vehicle.Remarks) && vehicle.Remarks.Length > 500)
                return "Remarks cannot exceed 500 characters";

            return string.Empty;
        }

        private bool IsValidVehicleNumber(string vehicleNo)
        {
            // Basic vehicle number validation - can be customized based on local requirements
            // This is a simple validation - you can make it more specific
            return !string.IsNullOrWhiteSpace(vehicleNo) && vehicleNo.Length >= 3;
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Basic phone number validation
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return true; // Optional field

            // Remove spaces and special characters for validation
            string cleanNumber = Regex.Replace(phoneNumber, @"[^\d]", "");
            
            // Check if it's a valid length (7-15 digits)
            return cleanNumber.Length >= 7 && cleanNumber.Length <= 15;
        }
    }
}

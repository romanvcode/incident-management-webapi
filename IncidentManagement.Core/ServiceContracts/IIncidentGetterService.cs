namespace IncidentManagement.Core.ServiceContracts
{
    /// <summary>
    /// Interface for the Incident Service
    /// </summary>
    public interface IIncidentGetterService
    {
        /// <summary>
        /// Get all incidents
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<object>> GetIncidents();
    }
}

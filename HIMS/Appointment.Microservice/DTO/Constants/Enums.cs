namespace Appointment.Microservice.Constants
{
    public class Enums
    {
        public enum AppointmentStatus
        {
            Scheduled,
            InProgress,
            Completed,
            Past
        } 
        
        public enum MedicationTime
        {
            Morning,
            Afternoon,
            Evening
        }
        public enum MedicationSchedule
        {
            BeforeMeal,
            AfterMeal
        }
    }
}

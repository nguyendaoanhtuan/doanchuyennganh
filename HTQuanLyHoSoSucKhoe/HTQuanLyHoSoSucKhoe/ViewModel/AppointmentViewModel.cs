using System;
using System.ComponentModel.DataAnnotations;

namespace HTQuanLyHoSoSucKhoe.ViewModels
{
    public class AppointmentViewModel
    {
        
     
       
        public int BenhVienId { get; set; }  // ID bệnh viện

       
        public string Name { get; set; }

      
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone_Number { get; set; }

        
        public DateTime Appointment_Date { get; set; }  // Ngày hẹn khám

    
        public TimeSpan Appointment_Time { get; set; }  // Giờ hẹn khám

        public string Status { get; set; } = "Pending";  // Trạng thái (Pending, Completed, Cancelled)

        public int? Queue_Number { get; set; }  // Số thứ tự chờ khám (có thể là null)
    }
}

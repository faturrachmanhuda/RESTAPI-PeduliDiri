using System.ComponentModel.DataAnnotations;

namespace RestfullAPI_PeduliDiri.Models
{
    public class DataRequest
    {
        [Required]
        public DateTime tanggal { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = @$"HH:mm:ss")]
        public TimeSpan jam { get; set; }
        [Required]
        public string lokasi { get; set; } = "";
        [Required]
        public double suhu_tubuh { get; set; }
        [Required]
        public int id_user { get; set; }
    }
}

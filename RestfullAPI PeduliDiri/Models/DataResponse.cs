using System.ComponentModel.DataAnnotations;

namespace RestfullAPI_PeduliDiri.Models
{
    public class DataResponse
    {
        public int id { get; set; }
        public DateTime tanggal { get; set; }
        [DisplayFormat(DataFormatString = @$"HH:mm:ss")]
        public TimeSpan jam { get; set; }
        public string lokasi { get; set; } = "";
        public double suhu_tubuh { get; set; }
        public int id_user { get; set; }
    }

    public class DataResponseWrapper
    {
        public int page { get; set; }
        public List<DataResponse> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}

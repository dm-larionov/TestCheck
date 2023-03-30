using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    public class WeatherData
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Degree { get; set; }
        public string Location { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public static List<WeatherData> weatherDatas = new()
        {
            new WeatherData() { Id = 1, Date = "21.01.2022", Degree=10, Location="��������" },
            new WeatherData() { Id = 23, Date = "10.08.2019", Degree=-20, Location="�����" },
            new WeatherData() { Id = 24, Date = "05.11.2020", Degree=15, Location="����" },
            new WeatherData() { Id = 25, Date = "07.02.2021", Degree=0, Location="�����" },
            new WeatherData() { Id = 30, Date = "30.05.2022", Degree=3, Location="�����������" },
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private Random random = new Random();
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// ��� ������ �������� ������, ������� ��� �������� ��� ������ XML-�����������
        /// </summary>
        /// <param name="�����">����� ���</param>
        /// <returns></returns>
        [HttpGet("GetSomeone")]
        public int GetNumber()
        {
            return random.Next(1, 5);
        }

        [HttpGet]
        public IActionResult GetAll(int? sortStrategy)
        {
            GetNumber();




            if (sortStrategy == null)
                return Ok(weatherDatas); // ����������� ���� ������� ������
            if (sortStrategy == -1)
                return Ok(weatherDatas.OrderByDescending(x => x.Date).ToList());
            if (sortStrategy == 1)
                return Ok(weatherDatas.OrderBy(x => x.Date).ToList());
            return BadRequest("Incorrect param");



        }

        [HttpGet("find-by-name")]
        public IActionResult GetByCityName(string location)
        {
            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Location == location)
                {
                    return Ok("������ � ��������� ������� ������� � ����� ������");
                }
            }
            return Ok("������ � ��������� ������� �� ����������");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            for (int i = 0; i < weatherDatas.Count; i++) // ����, ������� ������� ������ ������� ������� weatherDatas
            {
                if (weatherDatas[i].Id == id) // � ������, ���� �������������� ���������� - �������� ���������
                {
                    return Ok(weatherDatas[i]); // ����������� ���������� "�������" � ������� � ������
                }
            }
            return BadRequest("����� ������ �� ����������"); // ����������� ���������� "������" � ����������
        }

        [HttpPost]
        public IActionResult Add(WeatherData data)
        {
            for (int i = 0; i < weatherDatas.Count; i++) // ����, ������� ������� ������ ������� ������� weatherDatas
            {
                if (weatherDatas[i].Id == data.Id) // � ������, ���� �������������� ���������� - �������� ���������
                {
                    return BadRequest("������ � ����� Id ��� ����"); // ����������� ���������� "������" � ����������
                }
            }
            weatherDatas.Add(data); // ��������� � ������ ����� ������
            return Ok(); // ����������� ���������� "�������"
        }

        [HttpPut]
        public IActionResult Update(WeatherData data)
        {
            for (int i = 0; i < weatherDatas.Count; i++) // ����, ������� ������� ������ ������� ������� weatherDatas
            {
                if (weatherDatas[i].Id == data.Id) // � ������, ���� �������������� ���������� - �������� ���������
                {
                    weatherDatas[i] = data; // �������� �������� ��� ������ ������ �������
                    return Ok(); // ����������� ���������� "�������"
                }
            }
            return BadRequest("����� ������ �� ����������"); // ����������� ���������� "������" � ����������
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            for (int i = 0; i < weatherDatas.Count; i++) // ����, ������� ������� ������ ������� ������� weatherDatas
            {
                if (weatherDatas[i].Id == id) // � ������, ���� �������������� ���������� - �������� ���������
                {
                    weatherDatas.RemoveAt(i); // ������� ������� �� ������� �� ��� ������� (���������� i)
                    return Ok(); // ����������� ���������� "�������"
                }
            }
            return BadRequest("����� ������ �� ����������"); // ����������� ���������� "������" � ����������
        }
    }
}
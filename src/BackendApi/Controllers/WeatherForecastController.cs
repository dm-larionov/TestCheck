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
            new WeatherData() { Id = 1, Date = "21.01.2022", Degree=10, Location="Мурманск" },
            new WeatherData() { Id = 23, Date = "10.08.2019", Degree=-20, Location="Пермь" },
            new WeatherData() { Id = 24, Date = "05.11.2020", Degree=15, Location="Омск" },
            new WeatherData() { Id = 25, Date = "07.02.2021", Degree=0, Location="Томск" },
            new WeatherData() { Id = 30, Date = "30.05.2022", Degree=3, Location="Калининград" },
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private Random random = new Random();
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Это пример описания метода, который был добавлен при помощи XML-комментария
        /// </summary>
        /// <param name="Число">Чисел нет</param>
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
                return Ok(weatherDatas); // возвращение всех записей списка
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
                    return Ok("Запись с указанным городом имеется в нашем списке");
                }
            }
            return Ok("Запись с указанным городом не обнаружено");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            for (int i = 0; i < weatherDatas.Count; i++) // цикл, который обходит каждый элемент массива weatherDatas
            {
                if (weatherDatas[i].Id == id) // в случае, если идентификаторы одинаковые - выполним следующее
                {
                    return Ok(weatherDatas[i]); // возвращение результата "Успешно" с данными о записи
                }
            }
            return BadRequest("Такая запись не обнаружена"); // Возвращение результата "Ошибка" с сообщением
        }

        [HttpPost]
        public IActionResult Add(WeatherData data)
        {
            for (int i = 0; i < weatherDatas.Count; i++) // цикл, который обходит каждый элемент массива weatherDatas
            {
                if (weatherDatas[i].Id == data.Id) // в случае, если идентификаторы одинаковые - выполним следующее
                {
                    return BadRequest("Запись с таким Id уже есть"); // Возвращение результата "Ошибка" с сообщением
                }
            }
            weatherDatas.Add(data); // добавляем в список новую запись
            return Ok(); // возвращение результата "Успешно"
        }

        [HttpPut]
        public IActionResult Update(WeatherData data)
        {
            for (int i = 0; i < weatherDatas.Count; i++) // цикл, который обходит каждый элемент массива weatherDatas
            {
                if (weatherDatas[i].Id == data.Id) // в случае, если идентификаторы одинаковые - выполним следующее
                {
                    weatherDatas[i] = data; // заменяем значение для данной ячейки массива
                    return Ok(); // возвращение результата "Успешно"
                }
            }
            return BadRequest("Такая запись не обнаружена"); // Возвращение результата "Ошибка" с сообщением
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            for (int i = 0; i < weatherDatas.Count; i++) // цикл, который обходит каждый элемент массива weatherDatas
            {
                if (weatherDatas[i].Id == id) // в случае, если идентификаторы одинаковые - выполним следующее
                {
                    weatherDatas.RemoveAt(i); // удаляем элемент из массива по его индексу (переменная i)
                    return Ok(); // возвращение результата "Успешно"
                }
            }
            return BadRequest("Такая запись не обнаружена"); // Возвращение результата "Ошибка" с сообщением
        }
    }
}
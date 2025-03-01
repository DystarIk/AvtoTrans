using System.Collections.ObjectModel;
using AvtoTrans.Enums;

namespace AvtoTrans.Models
{
    public class RequestInitializer
    {
        private static readonly Random random = new Random();
        private static readonly string[] carModels = { "Toyota", "Honda", "Ford", "Chevrolet", "BMW", "Audi", "Mercedes", "Volkswagen", "Nissan", "Hyundai" };
        private static readonly string[] firstNames = { "Иван", "Петр", "Сергей", "Алексей", "Дмитрий", "Андрей", "Михаил", "Николай", "Владимир", "Александр" };
        private static readonly string[] lastNames = { "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Васильев", "Попов", "Соколов", "Михайлов", "Новиков" };
        private static readonly string[] middleNames = { "Иванович", "Петрович", "Сергеевич", "Алексеевич", "Дмитриевич", "Андреевич", "Михайлович", "Николаевич", "Владимирович", "Александрович" };
        private static readonly string[] phonePrefixes = { "+7", "+375", "+380", "+48", "+49" };

        public static ObservableCollection<Request> InitializeRequests(int size)
        {
            var requests = new ObservableCollection<Request>();

            for (int i = 0; i < size; i++)
            {
                var fio = new FIO($"{lastNames[random.Next(lastNames.Length)]} {firstNames[random.Next(firstNames.Length)]} {middleNames[random.Next(middleNames.Length)]}");
                var mechanic = random.Next(2) == 0 ? null : new Mechanic($"{lastNames[random.Next(lastNames.Length)]} {firstNames[random.Next(firstNames.Length)]} {middleNames[random.Next(middleNames.Length)]}");

                var request = new Request
                {
                    Number = (uint)i + 1,
                    CarModel = carModels[random.Next(carModels.Length)],
                    CarType = (CarType)random.Next(0, Enum.GetValues(typeof(CarType)).Length), // Случайный тип автомобиля
                    Date = DateTime.Now.AddDays(-random.Next(0, 365)), // Случайная дата в пределах года
                    FIO = fio,
                    Phone = $"{phonePrefixes[random.Next(phonePrefixes.Length)]}{random.Next(100000000, 999999999)}", // Случайный номер телефона
                    Status = (RequestStatus)random.Next(0, Enum.GetValues(typeof(RequestStatus)).Length), // Случайный статус заявки
                    Mechanic = mechanic,
                    Description = $"Описание проблемы для заявки №{i + 1}"
                };

                requests.Add(request);
            }

            return requests;
        }
    }
}